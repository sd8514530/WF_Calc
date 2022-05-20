using System;
using System.Data;

namespace WF_Calc.Modules {

    public class RecordDetailsEntity {

        public RecordDetailsEntity(DataRow dr) {
            this.Id = Convert.ToInt32(dr["Id"]);
            this.RecordId = Convert.ToInt32(dr["RecordId"]);
            this.ItemNum = Convert.ToInt32(dr["ItemNum"]);
            this.ItemName = dr["ItemName"].ToString();
            this.ItemContent = dr["ItemContent"].ToString();
            this.Unit = dr["Unit"].ToString();
            this.Quantities = decimal.Parse(dr["Quantities"].ToString());
            this.Artificial_UP = decimal.Parse(dr["Artificial_UP"].ToString());
            this.Advocate_UP = decimal.Parse(dr["Advocate_UP"].ToString());
            this.Complementary_UP = decimal.Parse(dr["Complementary_UP"].ToString());
            this.TotalPrice = (this.Artificial_UP + this.Advocate_UP + this.Complementary_UP) * this.Quantities;//计算总量
        }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 记录ID
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public int ItemNum { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 项目内容
        /// </summary>
        public string ItemContent { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 工程量
        /// </summary>
        public decimal Quantities { get; set; }

        /// <summary>
        /// 人工单价
        /// </summary>
        public decimal Artificial_UP { get; set; }

        /// <summary>
        /// 主材单价
        /// </summary>
        public decimal Advocate_UP { get; set; }

        /// <summary>
        /// 辅材单价
        /// </summary>
        public decimal Complementary_UP { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }

        #region 扩展操作

        public void Create() {//设置默认值
            this.Quantities = decimal.Parse("0");
            this.Artificial_UP = decimal.Parse("0");
            this.Advocate_UP = decimal.Parse("0");
            this.Complementary_UP = decimal.Parse("0");
        }

        #endregion 扩展操作
    }
}