
namespace WF_Calc {
    partial class MainFrame {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbxBtn = new System.Windows.Forms.GroupBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.cbxTheme = new System.Windows.Forms.ComboBox();
            this.lblTheme = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_delete_item = new System.Windows.Forms.Button();
            this.btn_edit_item = new System.Windows.Forms.Button();
            this.btn_add_item = new System.Windows.Forms.Button();
            this.gbxBtn.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(374, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(146, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "系统名称";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxBtn
            // 
            this.gbxBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxBtn.Controls.Add(this.btnDel);
            this.gbxBtn.Controls.Add(this.btnEdit);
            this.gbxBtn.Controls.Add(this.btnAdd);
            this.gbxBtn.Location = new System.Drawing.Point(667, 54);
            this.gbxBtn.Name = "gbxBtn";
            this.gbxBtn.Size = new System.Drawing.Size(280, 56);
            this.gbxBtn.TabIndex = 1;
            this.gbxBtn.TabStop = false;
            this.gbxBtn.Text = "功能操作";
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(182, 20);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 2;
            this.btnDel.Text = "删除分类";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(101, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "编辑分类";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(20, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "新增分类";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Location = new System.Drawing.Point(12, 116);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(938, 452);
            this.MainPanel.TabIndex = 2;
            // 
            // cbxTheme
            // 
            this.cbxTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTheme.FormattingEnabled = true;
            this.cbxTheme.Location = new System.Drawing.Point(826, 30);
            this.cbxTheme.Name = "cbxTheme";
            this.cbxTheme.Size = new System.Drawing.Size(121, 20);
            this.cbxTheme.TabIndex = 3;
            // 
            // lblTheme
            // 
            this.lblTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTheme.AutoSize = true;
            this.lblTheme.Location = new System.Drawing.Point(755, 33);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(65, 12);
            this.lblTheme.TabIndex = 4;
            this.lblTheme.Text = "主题切换：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_delete_item);
            this.groupBox1.Controls.Add(this.btn_edit_item);
            this.groupBox1.Controls.Add(this.btn_add_item);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 56);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据操作";
            // 
            // btn_delete_item
            // 
            this.btn_delete_item.Location = new System.Drawing.Point(182, 20);
            this.btn_delete_item.Name = "btn_delete_item";
            this.btn_delete_item.Size = new System.Drawing.Size(75, 23);
            this.btn_delete_item.TabIndex = 2;
            this.btn_delete_item.Text = "删除项目";
            this.btn_delete_item.UseVisualStyleBackColor = true;
            // 
            // btn_edit_item
            // 
            this.btn_edit_item.Location = new System.Drawing.Point(101, 20);
            this.btn_edit_item.Name = "btn_edit_item";
            this.btn_edit_item.Size = new System.Drawing.Size(75, 23);
            this.btn_edit_item.TabIndex = 1;
            this.btn_edit_item.Text = "编辑项目";
            this.btn_edit_item.UseVisualStyleBackColor = true;
            // 
            // btn_add_item
            // 
            this.btn_add_item.Location = new System.Drawing.Point(20, 20);
            this.btn_add_item.Name = "btn_add_item";
            this.btn_add_item.Size = new System.Drawing.Size(75, 23);
            this.btn_add_item.TabIndex = 0;
            this.btn_add_item.Text = "新增项目";
            this.btn_add_item.UseVisualStyleBackColor = true;
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(962, 580);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTheme);
            this.Controls.Add(this.cbxTheme);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.gbxBtn);
            this.Controls.Add(this.lblTitle);
            this.Name = "MainFrame";
            this.Text = "主程序";
            this.Shown += new System.EventHandler(this.MainFrame_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFrame_KeyDown);
            this.gbxBtn.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox gbxBtn;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.ComboBox cbxTheme;
        private System.Windows.Forms.Label lblTheme;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_delete_item;
        private System.Windows.Forms.Button btn_edit_item;
        private System.Windows.Forms.Button btn_add_item;
    }
}

