using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WF_Calc.Modules;
using WF_Calc.Service;

namespace WF_Calc {

    public partial class CategoryForm : Form {
        private int? id = null;
        private CategoryService categoryService = new CategoryService();

        public CategoryForm() {
            InitializeComponent();
            DataGridViewTextBoxColumn[] headers = new DataGridViewTextBoxColumn[4];
            this.lv_cate.ColumnHeadersVisible = true;
            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            this.lv_cate.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            this.lv_cate.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//设置为整行被选中
            headers[0] = new DataGridViewTextBoxColumn() { DataPropertyName = "CategoryNum", Name = "CategoryNum", HeaderText = "序号" };
            headers[1] = new DataGridViewTextBoxColumn() { DataPropertyName = "CategoryName", Name = "CategoryName", HeaderText = "分类名称" };
            headers[2] = new DataGridViewTextBoxColumn() { DataPropertyName = "Remark", Name = "Remark", HeaderText = "备注" };
            headers[3] = new DataGridViewTextBoxColumn() { DataPropertyName = "Id", Name = "Id", HeaderText = "ID", Visible = false };
            this.lv_cate.Columns.AddRange(headers);
            this.lv_cate.AllowUserToAddRows = false;
            this.lv_cate.AutoGenerateColumns = false;
            this.lv_cate.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            InitCategoryData();
        }

        private void btn_save_Click(object sender, EventArgs e) {
            CategoryEntity entity = GetEntity();
            if (this.id == null) {
                if (categoryService.AddCategory(entity)) {
                    MessageBox.Show("添加分类成功！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitCategoryData();
                } else {
                    MessageBox.Show("添加分类失败！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                if (categoryService.EditCategory(entity)) {
                    MessageBox.Show("编辑分类成功！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitCategoryData();
                } else {
                    MessageBox.Show("编辑分类失败！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e) {
            CategoryEntity entity = GetSelected();
            this.id = entity.Id;
            this.txtNum.Text = entity.CategoryNum.ToString();
            this.txtName.Text = entity.CategoryName;
            this.txtRemark.Text = entity.Remark;
        }

        private void btn_delete_Click(object sender, EventArgs e) {
            int a = this.lv_cate.CurrentRow.Index;
            var row = lv_cate.Rows[a];
            this.id = Convert.ToInt32(row.Cells["Id"].Value);
            if (this.id != null) {
                if (MessageBox.Show("是否确认删除数据？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                    if (categoryService.DelCategory(Convert.ToInt32(this.id))) {
                        MessageBox.Show("删除分类成功！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InitCategoryData();
                    } else {
                        MessageBox.Show("删除分类失败！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } else {
                MessageBox.Show("请选择需要删除的数据！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click(object sender, EventArgs e) {
            resetControls();
        }

        private void resetControls() {
            this.id = null;
            this.txtNum.Text = "";
            this.txtRemark.Text = "";
            this.txtName.Text = "";
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e) {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
                e.Handled = true;
        }

        private CategoryEntity GetEntity() {
            CategoryEntity entity = new CategoryEntity();
            if (this.id != null) {
                entity.Id = Convert.ToInt32(this.id);
            }
            if (string.IsNullOrEmpty(this.txtNum.Text)) {
                MessageBox.Show("序号必填！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            entity.CategoryNum = Convert.ToInt32(this.txtNum.Text);
            entity.CategoryName = this.txtName.Text;
            entity.Remark = this.txtRemark.Text;
            return entity;
        }

        private void InitCategoryData() {
            List<CategoryEntity> entities = categoryService.ListCategory();
            this.lv_cate.Rows.Clear();
            foreach (CategoryEntity entity in entities) {
                DataGridViewRow dr = new DataGridViewRow();
                string[] row = new string[] { entity.CategoryNum.ToString(), entity.CategoryName, entity.Remark, entity.Id.ToString() };
                this.lv_cate.Rows.Add(row);
            }
            resetControls();
        }

        private CategoryEntity GetSelected() {
            int a = this.lv_cate.CurrentRow.Index;
            CategoryEntity entity = new CategoryEntity();
            var row = lv_cate.Rows[a];
            entity.Id = Convert.ToInt32(row.Cells["Id"].Value);
            entity.CategoryNum = Convert.ToInt32(row.Cells["CategoryNum"].Value);
            entity.CategoryName = (string)row.Cells["CategoryName"].Value;
            entity.Remark = (string)row.Cells["Remark"].Value;
            return entity;
        }
    }
}