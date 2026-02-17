namespace Product_Inventory_System
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBox1 = new TextBox();
            comboBox1 = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            label4 = new Label();
            textBox3 = new TextBox();
            label5 = new Label();
            textBox4 = new TextBox();
            label6 = new Label();
            textBox5 = new TextBox();
            label7 = new Label();
            comboBox2 = new ComboBox();
            label8 = new Label();
            dataGridViewProducts = new DataGridView();
            buttonSave = new Button();
            buttonUpdate = new Button();
            buttonDelete = new Button();
            buttonClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(135, 18);
            label1.Name = "label1";
            label1.Size = new Size(291, 37);
            label1.TabIndex = 0;
            label1.Text = "โปรแกรมบันทึกข้อมูลสินค้า";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(56, 100);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(115, 23);
            textBox1.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "food", "car", "bicycle", "furniture" });
            comboBox1.Location = new Point(56, 252);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(178, 23);
            comboBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(56, 73);
            label2.Name = "label2";
            label2.Size = new Size(71, 19);
            label2.TabIndex = 3;
            label2.Text = "ProductID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(56, 150);
            label3.Name = "label3";
            label3.Size = new Size(93, 19);
            label3.TabIndex = 4;
            label3.Text = "ProductName";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(56, 177);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(154, 23);
            textBox2.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(56, 224);
            label4.Name = "label4";
            label4.Size = new Size(65, 19);
            label4.TabIndex = 6;
            label4.Text = "Category";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(480, 100);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(192, 23);
            textBox3.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F);
            label5.Location = new Point(480, 73);
            label5.Name = "label5";
            label5.Size = new Size(64, 19);
            label5.TabIndex = 7;
            label5.Text = "UnitPrice";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(285, 100);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(114, 23);
            textBox4.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(285, 73);
            label6.Name = "label6";
            label6.Size = new Size(63, 19);
            label6.TabIndex = 9;
            label6.Text = "Quantity";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(285, 177);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(154, 23);
            textBox5.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10F);
            label7.Location = new Point(285, 150);
            label7.Name = "label7";
            label7.Size = new Size(58, 19);
            label7.TabIndex = 11;
            label7.Text = "Supplier";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "damaged", "Out of stock", "Leftover products" });
            comboBox2.Location = new Point(480, 177);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(178, 23);
            comboBox2.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F);
            label8.Location = new Point(480, 150);
            label8.Name = "label8";
            label8.Size = new Size(47, 19);
            label8.TabIndex = 14;
            label8.Text = "Status";
            // 
            // dataGridViewProducts
            // 
            dataGridViewProducts.AllowUserToAddRows = false;
            dataGridViewProducts.AllowUserToDeleteRows = false;
            dataGridViewProducts.Location = new Point(26, 360);
            dataGridViewProducts.MultiSelect = false;
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.ReadOnly = true;
            dataGridViewProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProducts.Size = new Size(734, 180);
            dataGridViewProducts.TabIndex = 20;
            dataGridViewProducts.SelectionChanged += dataGridViewProducts_SelectionChanged;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(304, 302);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 30);
            buttonSave.TabIndex = 21;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new Point(508, 302);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(75, 30);
            buttonUpdate.TabIndex = 22;
            buttonUpdate.Text = "Update";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Click += buttonUpdate_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(621, 300);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(75, 30);
            buttonDelete.TabIndex = 23;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(406, 302);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(73, 30);
            buttonClear.TabIndex = 24;
            buttonClear.Text = "Clear";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(buttonClear);
            Controls.Add(buttonDelete);
            Controls.Add(buttonUpdate);
            Controls.Add(buttonSave);
            Controls.Add(dataGridViewProducts);
            Controls.Add(label8);
            Controls.Add(comboBox2);
            Controls.Add(textBox5);
            Controls.Add(label7);
            Controls.Add(textBox4);
            Controls.Add(label6);
            Controls.Add(textBox3);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Product Inventory";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private ComboBox comboBox1;
        private Label label2;
        private Label label3;
        private TextBox textBox2;
        private Label label4;
        private TextBox textBox3;
        private Label label5;
        private TextBox textBox4;
        private Label label6;
        private TextBox textBox5;
        private Label label7;
        private ComboBox comboBox2;
        private Label label8;
        private DataGridView dataGridViewProducts;
        private Button buttonSave;
        private Button buttonUpdate;
        private Button buttonDelete;
        private Button buttonClear;
    }
}
