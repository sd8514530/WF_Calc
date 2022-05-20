using System;
using System.Collections.Generic;
using System.Data;
using WF_Calc.DBUtility;
using WF_Calc.Modules;

namespace WF_Calc.Service {

    public class CategoryService {

        public bool AddCategory(CategoryEntity entity) {
            try {
                Db db = new Db();
                string sql = String.Format(@"INSERT INTO wf_category (CategoryNum,CategoryName,Remark) VALUES ({0},'{1}','{2}');",
                    entity.CategoryNum, entity.CategoryName, entity.Remark);
                if (db.Command(sql.Trim()) > 0) {
                    return true;
                }
            } catch (Exception ex) {
                LogHelper.SaveLog(ex.Message, LogLevel.Error, LogType.SqlForDB);
                return false;
            }
            return false;
        }

        public bool EditCategory(CategoryEntity entity) {
            try {
                Db db = new Db();
                string sql = String.Format(@"UPDATE wf_category SET CategoryNum = {0},CategoryName = '{1}',Remark = '{2}' WHERE ID = {3} ",
                    entity.CategoryNum, entity.CategoryName, entity.Remark, entity.Id);
                if (db.Command(sql.Trim()) > 0) {
                    return true;
                }
            } catch (Exception ex) {
                LogHelper.SaveLog(ex.Message, LogLevel.Error, LogType.SqlForDB);
                return false;
            }
            return false;
        }

        public bool DelCategory(int id) {
            try {
                Db db = new Db();
                string sql = String.Format(@"DELETE FROM wf_category WHERE ID = {0} ", id);
                if (db.Command(sql.Trim()) > 0) {
                    return true;
                }
            } catch (Exception ex) {
                LogHelper.SaveLog(ex.Message, LogLevel.Error, LogType.SqlForDB);
                return false;
            }
            return false;
        }

        public List<CategoryEntity> ListCategory() {
            DataTable dt = null;
            List<CategoryEntity> list = null;
            try {
                Db db = new Db();
                string sql = String.Format(@" SELECT * FROM wf_category ORDER BY CategoryNum");
                dt = db.DataTable(sql);
                list = new List<CategoryEntity>();
                foreach (DataRow dr in dt.Rows) {
                    list.Add(new CategoryEntity(dr));
                }
                return list;
            } catch (Exception ex) {
                LogHelper.SaveLog(ex.Message, LogLevel.Error, LogType.SqlForDB);
                return null;
            }
        }
    }
}