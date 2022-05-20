using System;
using System.Data;

namespace WF_Calc.Modules {

    public class RecordEntity {

        #region 构造函数

        public RecordEntity(DataRow dr) {
            this.Id = Convert.ToInt32(dr["Id"]);
            this.SortCode = Convert.ToInt32(dr["SortCode"]);
            this.RecordName = dr["RecordName"].ToString();
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
        public int SortCode { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string RecordName { get; set; }

        #endregion 对象属性
    }
}