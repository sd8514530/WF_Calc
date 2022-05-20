using System;
using System.Collections.Generic;
using System.Data;

namespace WF_Calc.Modules {

    public class CategoryEntity {

        #region 构造函数

        public CategoryEntity(DataRow dr) {
            this.Id = Convert.ToInt32(dr["Id"]);
            this.CategoryNum = Convert.ToInt32(dr["CategoryNum"]);
            this.CategoryName = dr["CategoryName"].ToString();
            this.Remark = dr["Remark"].ToString();
        }

        #endregion 构造函数

        #region 对象属性

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 分类编码
        /// </summary>
        public int CategoryNum { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public List<RecordEntity> records { get; set; }

        #endregion 对象属性
    }
}