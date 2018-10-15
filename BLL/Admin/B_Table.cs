using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class B_Table
    {
        public int bItemID { get; set; }
        public B_Table(int ItemID)
        {
            bItemID = ItemID;
        }
        /// <summary>
        /// 获取表格实体
        /// </summary>
        /// <returns></returns>
        public Model.M_Table GetTableModel()
        {
            return BaseClass.XmlSelectTableModel(bItemID);
        }
        /// <summary>
        /// 查询字段信息
        /// </summary>
        public List<Model.M_TableField> GetTableFieldModel()
        {
            return BaseClass.XmlSelectAllTableFieldInfo(bItemID);
        }
        /// <summary>
        /// 修改表格实体
        /// </summary>
        public bool UpdateTableModel(System.Data.DataTable dataTable)
        {
            return BaseClass.XmlUpdateTableModel(bItemID, dataTable);
        }
        /// <summary>
        /// 修改字段信息
        /// </summary>
        public bool UpdateTableFieldModel(System.Data.DataTable dataTable)
        {
            return BaseClass.XmlUpdateTableFielModel(bItemID, dataTable);
        }
        /// <summary>
        /// 跟新字段信息
        /// </summary>
        public bool ToUpdateTableFieldModel(System.Data.DataTable dataTable)
        {
            return BaseClass.XmlToUpdateTableFielModel(bItemID);
        }
    }
}

