
namespace WF_Calc {
    partial class CategoryForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txtNum = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.lv_cate = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lv_cate)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNum
            // 
            this.txtNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum.Location = new System.Drawing.Point(81, 20);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(276, 21);
            this.txtNum.TabIndex = 0;
            this.txtNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNum_KeyPress);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_save.Location = new System.Drawing.Point(282, 101);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "保存数据";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Controls.Add(this.txtNum);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 133);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分类名称";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnReset.Location = new System.Drawing.Point(201, 101);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "重置数据";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "  分类备注";
            // 
            // txtRemark
            // 
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemark.Location = new System.Drawing.Point(81, 74);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(276, 21);
            this.txtRemark.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "  分类名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "  分类序号";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(81, 47);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(276, 21);
            this.txtName.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lv_cate);
            this.groupBox2.Controls.Add(this.btn_edit);
            this.groupBox2.Controls.Add(this.btn_delete);
            this.groupBox2.Location = new System.Drawing.Point(6, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(363, 307);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "全部分类";
            // 
            // btn_edit
            // 
            this.btn_edit.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_edit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_edit.Location = new System.Drawing.Point(201, 16);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(75, 23);
            this.btn_edit.TabIndex = 2;
            this.btn_edit.Text = "编辑选中";
            this.btn_edit.UseVisualStyleBackColor = false;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.Color.Crimson;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.ForeColor = System.Drawing.Color.Transparent;
            this.btn_delete.Location = new System.Drawing.Point(282, 16);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 1;
            this.btn_delete.Text = "删除选中";
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // lv_cate
            // 
            this.lv_cate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lv_cate.Location = new System.Drawing.Point(12, 45);
            this.lv_cate.Name = "lv_cate";
            this.lv_cate.RowTemplate.Height = 23;
            this.lv_cate.Size = new System.Drawing.Size(345, 262);
            this.lv_cate.TabIndex = 3;
            // 
            // CategoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(381, 476);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CategoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "项目分类";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lv_cate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridView lv_cate;
    }
}