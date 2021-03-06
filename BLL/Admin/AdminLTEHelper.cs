﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class AdminLTEHelper
    {

        /// <summary>
        /// 根据DataTable生成AdminLTE的多级菜单目录
        /// GetTreeJsonByTable(datatable, "id", "title", "pid", "0","menulevel");
        /// </summary>
        /// <param name="tabel">数据源</param>
        /// <param name="idCol">ID列</param>
        /// <param name="txtCol">Text列</param>
        /// <param name="rela">关系字段(字典表中的树结构字段)</param>
        /// <param name="pId">父ID值(0)</param>
        /// <param name="colmenulevel">菜单显示层级列名</param>
        public StringBuilder result = new StringBuilder();
        public StringBuilder sb = new StringBuilder();
        public void GetTreeJsonByTable(DataTable tabel, string idCol, string txtCol, string rela, object pId, string colmenulevel)
        {
            result.Append(sb.ToString());
            sb.Clear();
            if (tabel.Rows.Count > 0)
            {
                string filer = string.Format("{0}='{1}'", rela, pId);
                DataRow[] rows = tabel.Select(filer, "MeanOrder asc");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                        {
                            //第一层级,名称在<span>多级菜单</span>中 class为treeview
                            //colmenulevel为menulevel，为菜单的显示层级，可以在后台进行配置
                            //和树的层级可能不同
                            if (row["MeanHeader"].ToString() != "")
                                sb.Append("<li class=\"header\">" + row["MeanHeader"] + "</li>");
                            //html自定义属性 url isleaf等，中间不用,进行分割，而用空格进行分割，否则jquery $().attr("isleaf")获取不到值
                            if (row[colmenulevel].ToString() == "1")
                            {
                                sb.Append("<li class=\"treeview\"><a href=\"javascript:void(0);\"><i class=\"fa fa-" + row["MeanClass"] + "\"></i><span>" + row[txtCol] + "</span><span class=\"pull-right-container\"> <span class=\"label label-primary pull-right\"></span></span></a>");

                            }
                            else
                            {

                                sb.Append("<li class=\"treeview\"><a href=\"javascript:void(0);\"><i class=\"fa fa-" + row["MeanClass"] + "\"></i>" + row[txtCol] + "<span class=\"pull-right-container\"> <span class=\"label label-primary pull-right\"></span></span></a>");

                            }
                            sb.Append("<ul class=\"treeview-menu\">");
                            GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol], colmenulevel);
                            sb.Append("</ul>");
                            sb.Append("</li>");
                            result.Append(sb.ToString());
                            sb.Clear();
                        }
                        else
                        {
                            if (row["MeanHeader"].ToString() != "")
                                sb.Append("<li class=\"header\">" + row["MeanHeader"] + "</li>");
                            //isleaf=true
                            if (row[colmenulevel].ToString() == "1")
                            {
                                //顶级菜单，标题显示在span中，否则显示图标时，标题不能隐藏
                                sb.Append("<li class=\"treeview \"><a href=\"javascript:void(0);\" moid=\"" + row[idCol] + "\" text=\"" + row[txtCol] + "\" isleaf=\"true\"" + " menu-controller=\"" + row["meanurl"] + "\"><i class=\"fa fa-" + row["MeanClass"] + "\"></i><span>" + row[txtCol] + "</span></a></li>");

                            }
                            else
                            {
                                sb.Append("<li><a href=\"javascript:void(0);\" moid=\"" + row[idCol] + "\" text=\"" + row[txtCol] + "\" isleaf=\"true\"" + " menu-controller=\"" + row["meanurl"] + "\"><i class=\"fa fa-" + row["MeanClass"] + "\"></i>" + row[txtCol] + "</a></li>");
                            }
                            result.Append(sb.ToString());
                            sb.Clear();
                        }
                        result.Append(sb.ToString());
                        sb.Clear();

                    }

                }
                result.Append(sb.ToString());
                sb.Clear();
            }
        }
        public void GetMeanJsonData(string idCol, string txtCol, string rela, object pId, string colmenulevel)
        {
            DataTable tabel = GetMeanDataTable().Tables[0];
            result.Append(sb.ToString());
            sb.Clear();
            if (tabel.Rows.Count > 0)
            {
                string filer = string.Format("{0}='{1}'", rela, pId);
                DataRow[] rows = tabel.Select(filer, "MeanOrder asc");
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                        {
                            //第一层级,名称在<span>多级菜单</span>中 class为treeview
                            //colmenulevel为menulevel，为菜单的显示层级，可以在后台进行配置
                            //和树的层级可能不同
                            if (row["MeanHeader"].ToString() != "")
                                sb.Append("<li class=\"header\">" + row["MeanHeader"] + "</li>");
                            //html自定义属性 url isleaf等，中间不用,进行分割，而用空格进行分割，否则jquery $().attr("isleaf")获取不到值
                            if (row[colmenulevel].ToString() == "1")
                            {
                                sb.Append("<li class=\"treeview\"><a href=\"javascript:void(0);\"><i class=\"fa fa-" + row["MeanClass"] + "\"></i><span>" + row[txtCol] + "</span><span class=\"pull-right-container\"> <span class=\"label label-primary pull-right\"></span></span></a>");
                            }
                            else
                            {
                                sb.Append("<li class=\"treeview\"><a href=\"javascript:void(0);\"><i class=\"fa fa-" + row["MeanClass"] + "\"></i>" + row[txtCol] + "<span class=\"pull-right-container\"> <span class=\"label label-primary pull-right\"></span></span></a>");
                            }
                            sb.Append("<ul class=\"treeview-menu\">");
                            GetMeanJsonData(idCol, txtCol, rela, row[idCol], colmenulevel);
                            sb.Append("</ul>");
                            sb.Append("</li>");
                            result.Append(sb.ToString());
                            sb.Clear();
                        }
                        else
                        {
                            if (row["MeanHeader"].ToString() != "")
                                sb.Append("<li class=\"header\">" + row["MeanHeader"] + "</li>");
                            //isleaf=true
                            if (row[colmenulevel].ToString() == "1")
                            {
                                //顶级菜单，标题显示在span中，否则显示图标时，标题不能隐藏
                                sb.Append("<li class=\"treeview \"><a href=\"javascript:void(0);\" menu-moid=\"" + row[idCol] + "\" menu-text=\"" + row[txtCol] + "\" isleaf=\"true\"" + " menu-controller=\"" + row["meanurl"] + "\"><i class=\"fa fa-" + row["MeanClass"] + "\"></i><span>" + row[txtCol] + "</span></a></li>");
                            }
                            else
                            {
                                sb.Append("<li><a href=\"javascript:void(0);\" menu-moid=\"" + row[idCol] + "\" menu-text=\"" + row[txtCol] + "\" isleaf=\"true\"" + " menu-controller=\"" + row["meanurl"] + "\"><i class=\"fa fa-" + row["MeanClass"] + "\"></i>" + row[txtCol] + "</a></li>");
                            }
                            result.Append(sb.ToString());
                            sb.Clear();
                        }
                        result.Append(sb.ToString());
                        sb.Clear();

                    }

                }
                result.Append(sb.ToString());
                sb.Clear();
            }
        }

        public DataSet GetMeanDataTable()
        {
            string sql = string.Format("SELECT * FROM  [t_Mean]");
            return DAL.SQLDBHelpercs.ExecuteReader(sql, null);
        }       
    }
}
