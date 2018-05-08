namespace PharmacyApplication.UserInterfaces
{
    partial class DisplaySalesRecord
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_stock = new System.Windows.Forms.Label();
            this.lbl_id = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_stock_output = new System.Windows.Forms.Label();
            this.lbl_id_output = new System.Windows.Forms.Label();
            this.lbl_name_output = new System.Windows.Forms.Label();
            this.btn_increase = new System.Windows.Forms.Button();
            this.btn_decrease = new System.Windows.Forms.Button();
            this.lbl_index = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_stock
            // 
            this.lbl_stock.AutoSize = true;
            this.lbl_stock.Location = new System.Drawing.Point(49, 42);
            this.lbl_stock.Name = "lbl_stock";
            this.lbl_stock.Size = new System.Drawing.Size(49, 13);
            this.lbl_stock.TabIndex = 0;
            this.lbl_stock.Text = "Quantity:";
            // 
            // lbl_id
            // 
            this.lbl_id.AutoSize = true;
            this.lbl_id.Location = new System.Drawing.Point(49, 80);
            this.lbl_id.Name = "lbl_id";
            this.lbl_id.Size = new System.Drawing.Size(21, 13);
            this.lbl_id.TabIndex = 1;
            this.lbl_id.Text = "ID:";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(49, 120);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(38, 13);
            this.lbl_name.TabIndex = 2;
            this.lbl_name.Text = "Name:";
            // 
            // lbl_stock_output
            // 
            this.lbl_stock_output.AutoSize = true;
            this.lbl_stock_output.Location = new System.Drawing.Point(136, 42);
            this.lbl_stock_output.Name = "lbl_stock_output";
            this.lbl_stock_output.Size = new System.Drawing.Size(25, 13);
            this.lbl_stock_output.TabIndex = 3;
            this.lbl_stock_output.Text = "Null";
            // 
            // lbl_id_output
            // 
            this.lbl_id_output.AutoSize = true;
            this.lbl_id_output.Location = new System.Drawing.Point(136, 80);
            this.lbl_id_output.Name = "lbl_id_output";
            this.lbl_id_output.Size = new System.Drawing.Size(25, 13);
            this.lbl_id_output.TabIndex = 4;
            this.lbl_id_output.Text = "Null";
            // 
            // lbl_name_output
            // 
            this.lbl_name_output.AutoSize = true;
            this.lbl_name_output.Location = new System.Drawing.Point(136, 120);
            this.lbl_name_output.Name = "lbl_name_output";
            this.lbl_name_output.Size = new System.Drawing.Size(25, 13);
            this.lbl_name_output.TabIndex = 5;
            this.lbl_name_output.Text = "Null";
            // 
            // btn_increase
            // 
            this.btn_increase.Location = new System.Drawing.Point(162, 188);
            this.btn_increase.Name = "btn_increase";
            this.btn_increase.Size = new System.Drawing.Size(28, 23);
            this.btn_increase.TabIndex = 6;
            this.btn_increase.Text = ">";
            this.btn_increase.UseVisualStyleBackColor = true;
            this.btn_increase.Click += new System.EventHandler(this.btn_increase_Click);
            // 
            // btn_decrease
            // 
            this.btn_decrease.Location = new System.Drawing.Point(42, 188);
            this.btn_decrease.Name = "btn_decrease";
            this.btn_decrease.Size = new System.Drawing.Size(28, 23);
            this.btn_decrease.TabIndex = 7;
            this.btn_decrease.Text = "<";
            this.btn_decrease.UseVisualStyleBackColor = true;
            this.btn_decrease.Click += new System.EventHandler(this.btn_decrease_Click);
            // 
            // lbl_index
            // 
            this.lbl_index.AutoSize = true;
            this.lbl_index.Location = new System.Drawing.Point(99, 193);
            this.lbl_index.Name = "lbl_index";
            this.lbl_index.Size = new System.Drawing.Size(25, 13);
            this.lbl_index.TabIndex = 8;
            this.lbl_index.Text = "Null";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Null";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Date:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // DisplaySalesRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 243);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_index);
            this.Controls.Add(this.btn_decrease);
            this.Controls.Add(this.btn_increase);
            this.Controls.Add(this.lbl_name_output);
            this.Controls.Add(this.lbl_id_output);
            this.Controls.Add(this.lbl_stock_output);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.lbl_id);
            this.Controls.Add(this.lbl_stock);
            this.Name = "DisplaySalesRecord";
            this.Text = "DisplaySalesRecord";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_stock;
        private System.Windows.Forms.Label lbl_id;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_stock_output;
        private System.Windows.Forms.Label lbl_id_output;
        private System.Windows.Forms.Label lbl_name_output;
        private System.Windows.Forms.Button btn_increase;
        private System.Windows.Forms.Button btn_decrease;
        private System.Windows.Forms.Label lbl_index;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}