using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace Common
{
    /// <summary>  
    /// 根据DataTable生成下拉列表树  
    /// </summary>  
    public class DropDownListHelp
    {
        private string gridline;
        private DataTable dt;
        public DropDownListHelp()
        {
            //  
            //TODO: 在此处添加构造函数逻辑  
            //  
        }
        /// <summary>  
        /// 根据Datatable生成树形下拉菜单  
        /// </summary>  
        /// <param name="datatable"></param>  
        /// <param name="parentKeyField">上级节点关键字段</param>  
        /// <param name="parentKey">上级节点值</param>  
        /// <param name="keyField">本节点关键字段</param>  
        /// <param name="sortString">排序字符串</param>  
        /// <param name="ddl">DownList</param>  
        public void createDropDownTree(DataTable datatable, string parentKeyField, string parentKey, string keyField, string textField, string sortString, DropDownList ddl)
        {
            dt = datatable;
            ddl.Items.Add(new ListItem("请选择父类", "0"));
            addChildItems(parentKeyField, parentKey, keyField, textField, sortString, ddl);
        }
        /// <summary>  
        /// 递归生成树节点  
        /// </summary>  
        /// <param name="parentKeyField">上级节点关键字段</param>  
        /// <param name="parentKey">上级节点值</param>  
        /// <param name="keyField">本节点关键字段</param>  
        /// <param name="sortString">排序字符串</param>  
        /// <param name="ddl">DownList控件</param>  
        /// <returns></returns>  
        private void addChildItems(string parentKeyField, string parentKey, string keyField, string textField, string sortString, DropDownList ddl)
        {
            DataView dv = new DataView(dt, parentKeyField + "='" + parentKey + "'", sortString, DataViewRowState.CurrentRows);
            int a = dv.Count;
            if (dv.Count == 0)
            {
                return;
            }
            for (int i = 0; i < a; i++)
            {
                gridline = "";
                dv.RowFilter = parentKeyField + "='" + parentKey + "'";
                dv.Sort = sortString;
                getTreeLine(parentKeyField, dv[i][parentKeyField].ToString(), keyField, dv[i][keyField].ToString(), sortString);
                dv.RowFilter = parentKeyField + "='" + parentKey + "'";
                dv.Sort = sortString;
                ddl.Items.Add(new ListItem(gridline + (i == a - 1 ? "┗" : "┣") + dv[i][textField].ToString(), dv[i][keyField].ToString()));
                addChildItems(parentKeyField, dv[i][keyField].ToString(), keyField, textField, sortString, ddl);
            }
            dv.Dispose();
        }

        /// <summary>  
        /// 回溯生成树的连接线  
        /// </summary>  
        /// <param name="parentKeyField">上级节点关键字段</param>  
        /// <param name="parentKey">上级节点值</param>  
        /// <param name="keyField">本节点关键字段</param>  
        /// <param name="nodeKey">本节点值</param>  
        /// <param name="sortString">排序字符串</param>  
        /// <returns></returns>  
        private void getTreeLine(string parentKeyField, string parentKey, string keyField, string nodeKey, string sortString)
        {
            //选择父层节点  
            DataView dv = new DataView(dt, keyField + "='" + parentKey + "'", sortString, DataViewRowState.CurrentRows);
            if (dv.Count > 0)
            {
                //选择父节点同级节点  
                dv.RowFilter = parentKeyField + "='" + dv[0][parentKeyField].ToString() + "'";
                dv.Sort = sortString;
                for (int j = 0; j < dv.Count; j++)
                {
                    if (dv[j][keyField].ToString() == parentKey)
                    {
                        if (j == dv.Count - 1)
                        {
                            gridline = "　" + gridline;
                        }
                        else
                        {
                            gridline = "┃" + gridline;
                        }
                    }
                }
                getTreeLine(parentKeyField, dv[0][parentKeyField].ToString(), keyField, dv[0][keyField].ToString(), sortString);
            }
            dv.Dispose();
        }
    }
}