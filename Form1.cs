using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Product_Inventory_System
{
    public partial class Form1 : Form
    {
        private readonly string _dbPath;
        private readonly string _connectionString;

        public Form1()
        {
            InitializeComponent();

            // SQLite provider is initialized in Program.Main (Batteries.Init()).
            _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Product.db");
            _connectionString = new SqliteConnectionStringBuilder { DataSource = _dbPath }.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeDatabase();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initialization error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Database

        private void InitializeDatabase()
        {
            try
            {
                var dir = Path.GetDirectoryName(_dbPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                using var conn = new SqliteConnection(_connectionString);
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Products (
                        ProductID   TEXT PRIMARY KEY,
                        ProductName TEXT NOT NULL,
                        Category    TEXT,
                        UnitPrice   REAL,
                        Quantity    INTEGER,
                        Supplier    TEXT,
                        Status      TEXT
                    );";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        private SqliteConnection GetConnection() => new SqliteConnection(_connectionString);

        public List<Product> LoadAllProducts()
        {
            var list = new List<Product>();

            try
            {
                using var conn = GetConnection();
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID, ProductName, Category, UnitPrice, Quantity, Supplier, Status FROM Products ORDER BY ProductName;";
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Product
                    {
                        ProductID = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        ProductName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Category = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        UnitPrice = reader.IsDBNull(3) ? 0m : Convert.ToDecimal(reader.GetDouble(3)),
                        Quantity = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                        Supplier = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                        Status = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                    };
                    list.Add(p);
                }
            }
            catch
            {
                throw;
            }

            return list;
        }

        public void InsertProduct(Product p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));

            try
            {
                using var conn = GetConnection();
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO Products (ProductID, ProductName, Category, UnitPrice, Quantity, Supplier, Status)
                    VALUES ($id, $name, $category, $unitPrice, $quantity, $supplier, $status);";
                cmd.Parameters.AddWithValue("$id", p.ProductID);
                cmd.Parameters.AddWithValue("$name", p.ProductName);
                cmd.Parameters.AddWithValue("$category", string.IsNullOrEmpty(p.Category) ? DBNull.Value : (object)p.Category);
                cmd.Parameters.AddWithValue("$unitPrice", (double)p.UnitPrice);
                cmd.Parameters.AddWithValue("$quantity", p.Quantity);
                cmd.Parameters.AddWithValue("$supplier", string.IsNullOrEmpty(p.Supplier) ? DBNull.Value : (object)p.Supplier);
                cmd.Parameters.AddWithValue("$status", string.IsNullOrEmpty(p.Status) ? DBNull.Value : (object)p.Status);

                cmd.ExecuteNonQuery();
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 19) // constraint violation
            {
                throw new InvalidOperationException("ProductID already exists.", ex);
            }
        }

        public void UpdateProduct(Product p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));

            try
            {
                using var conn = GetConnection();
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    UPDATE Products
                    SET ProductName = $name,
                        Category = $category,
                        UnitPrice = $unitPrice,
                        Quantity = $quantity,
                        Supplier = $supplier,
                        Status = $status
                    WHERE ProductID = $id;";
                cmd.Parameters.AddWithValue("$id", p.ProductID);
                cmd.Parameters.AddWithValue("$name", p.ProductName);
                cmd.Parameters.AddWithValue("$category", string.IsNullOrEmpty(p.Category) ? DBNull.Value : (object)p.Category);
                cmd.Parameters.AddWithValue("$unitPrice", (double)p.UnitPrice);
                cmd.Parameters.AddWithValue("$quantity", p.Quantity);
                cmd.Parameters.AddWithValue("$supplier", string.IsNullOrEmpty(p.Supplier) ? DBNull.Value : (object)p.Supplier);
                cmd.Parameters.AddWithValue("$status", string.IsNullOrEmpty(p.Status) ? DBNull.Value : (object)p.Status);

                var affected = cmd.ExecuteNonQuery();
                if (affected == 0)
                    throw new InvalidOperationException("No product found with the specified ProductID.");
            }
            catch
            {
                throw;
            }
        }

        public void DeleteProduct(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId)) throw new ArgumentException("ProductID is required.", nameof(productId));

            try
            {
                using var conn = GetConnection();
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM Products WHERE ProductID = $id;";
                cmd.Parameters.AddWithValue("$id", productId);

                var affected = cmd.ExecuteNonQuery();
                if (affected == 0)
                    throw new InvalidOperationException("No product found with the specified ProductID.");
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region LINQ helpers (in-memory)

        public List<Product> QueryProductsByCategory(string category)
        {
            var products = LoadAllProducts();
            if (string.IsNullOrWhiteSpace(category)) return products;
            return products.Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase))
                           .OrderBy(p => p.ProductName)
                           .ToList();
        }

        public List<Product> GetLowStockProducts(int threshold)
        {
            return LoadAllProducts().Where(p => p.Quantity <= threshold).OrderBy(p => p.Quantity).ToList();
        }

        public decimal GetInventoryValue()
        {
            return LoadAllProducts().Sum(p => p.UnitPrice * p.Quantity);
        }

        #endregion

        #region Validation & UI helpers

        private bool ValidateInput(out Product product)
        {
            product = null;

            var id = textBox1.Text.Trim();
            var name = textBox2.Text.Trim();
            var category = comboBox1.Text.Trim();
            var unitPriceText = textBox3.Text.Trim();
            var quantityText = textBox4.Text.Trim();
            var supplier = textBox5.Text.Trim();
            var status = comboBox2.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("ProductID is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("ProductName is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }

            if (!decimal.TryParse(unitPriceText, out var unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("UnitPrice must be a non-negative number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return false;
            }

            if (!int.TryParse(quantityText, out var quantity) || quantity < 0)
            {
                MessageBox.Show("Quantity must be a non-negative integer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return false;
            }

            product = new Product
            {
                ProductID = id,
                ProductName = name,
                Category = category,
                UnitPrice = unitPrice,
                Quantity = quantity,
                Supplier = supplier,
                Status = status
            };

            return true;
        }

        private void RefreshGrid()
        {
            try
            {
                var products = LoadAllProducts();
                var bs = new BindingSource { DataSource = new BindingList<Product>(products) };
                dataGridViewProducts.DataSource = bs;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            // Basic formatting: columns match properties
            if (dataGridViewProducts.Columns["UnitPrice"] != null)
            {
                dataGridViewProducts.Columns["UnitPrice"].DefaultCellStyle.Format = "C2";
                dataGridViewProducts.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dataGridViewProducts.Columns["Quantity"] != null)
            {
                dataGridViewProducts.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void ClearInputFields()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            comboBox1.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            comboBox2.Text = string.Empty;
        }

        #endregion

        #region Event handlers

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput(out var p)) return;
                InsertProduct(p);
                MessageBox.Show("Product saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save failed: {ex.Message}\n\nDB path: {_dbPath}\n\nDetails:\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput(out var p)) return;
                UpdateProduct(p);
                MessageBox.Show("Product updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var id = textBox1.Text.Trim();
                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Select a product (ProductID) to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show($"Delete product {id}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                DeleteProduct(id);
                MessageBox.Show("Product deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Delete failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void dataGridViewProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows == null || dataGridViewProducts.SelectedRows.Count == 0) return;

            try
            {
                var row = dataGridViewProducts.SelectedRows[0];
                if (row == null) return;

                // The grid is bound to Product properties; column names match property names
                textBox1.Text = row.Cells["ProductID"].Value?.ToString() ?? string.Empty;
                textBox2.Text = row.Cells["ProductName"].Value?.ToString() ?? string.Empty;
                comboBox1.Text = row.Cells["Category"].Value?.ToString() ?? string.Empty;
                textBox3.Text = row.Cells["UnitPrice"].Value?.ToString() ?? string.Empty;
                textBox4.Text = row.Cells["Quantity"].Value?.ToString() ?? string.Empty;
                textBox5.Text = row.Cells["Supplier"].Value?.ToString() ?? string.Empty;
                comboBox2.Text = row.Cells["Status"].Value?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading selection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }

    public class Product
    {
        public string ProductID { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
