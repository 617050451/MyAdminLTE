﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{
    public class SQLDBHelpercs
    {
        private static readonly string strCon = ConfigurationManager.ConnectionStrings["strCon"].ToString();
        private static SqlConnection conn;
        public static SqlConnection Conn
        {
            get
            {
                if (conn == null)
                {
                    conn = new SqlConnection(strCon);
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Broken)
                {
                    conn.Close();
                    conn.Open();
                }
                return conn;
            }
        }
        public static DataSet ExecuteReader(string sql, SqlParameter[] paras)
        {
            try
            {

                SqlCommand comm = new SqlCommand(sql, Conn);

                if (paras != null)
                {
                    comm.Parameters.AddRange(paras);
                }

                SqlDataAdapter da = new SqlDataAdapter(comm);

                DataSet ds = new DataSet();

                da.Fill(ds);

                return ds;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataSet ExecuteReader(string sql, SqlParameter[] paras,string type)
        {
            try
            {

                SqlCommand comm = new SqlCommand(sql, Conn);

                if (paras != null)
                {
                    if (type != "sql") comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddRange(paras);
                }

                SqlDataAdapter da = new SqlDataAdapter(comm);

                DataSet ds = new DataSet();

                da.Fill(ds);

                return ds;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable ExecuteReaderTable(string sql, SqlParameter[] paras)
        {
            try
            {

                SqlCommand comm = new SqlCommand(sql, Conn);

                if (paras != null)
                {
                    comm.Parameters.AddRange(paras);
                }

                SqlDataAdapter da = new SqlDataAdapter(comm);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable ExecuteReaderTable(string sql, SqlParameter[] paras, string type)
        {
            try
            {

                SqlCommand comm = new SqlCommand(sql, Conn);

                if (paras != null)
                {
                    if (type != "sql") comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddRange(paras);
                }

                SqlDataAdapter da = new SqlDataAdapter(comm);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string ExecuteReaderView(string sql, SqlParameter[] paras)
        {
            try
            {

                SqlCommand comm = new SqlCommand(sql, Conn);

                if (paras != null)
                {
                    comm.Parameters.AddRange(paras);
                }

                SqlDataAdapter da = new SqlDataAdapter(comm);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                else
                    return "";
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string ExecuteReaderView(string sql, SqlParameter[] paras, string type)
        {
            try
            {

                SqlCommand comm = new SqlCommand(sql, Conn);

                if (paras != null)
                {
                    comm.Parameters.AddRange(paras);
                    if (type != "sql") comm.CommandType = CommandType.StoredProcedure;
                }

                SqlDataAdapter da = new SqlDataAdapter(comm);

                DataSet ds = new DataSet();

                da.Fill(ds);

                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0].Rows[0][0].ToString();
                else
                    return "";
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool ExecuteNonQuery(string sql)
        {
            try
            {
                SqlCommand comm = new SqlCommand(sql, Conn);
                return comm.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool ExecuteNonQuery(string sql, SqlParameter[] paras)
        {
            try
            {
                SqlCommand comm = new SqlCommand(sql, Conn);
                if (paras != null)
                    comm.Parameters.AddRange(paras);
                return comm.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool ExecuteNonQuery(string sql, SqlParameter[] paras, string type)
        {
            try
            {
                SqlCommand comm = new SqlCommand(sql, Conn);
                if (paras != null)
                {
                    comm.Parameters.AddRange(paras);
                    if (type != "sql") comm.CommandType = CommandType.StoredProcedure;
                }
                return comm.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static int ExecuteNonQueryInt(string sql)
        {
            try
            {
                SqlCommand comm = new SqlCommand(sql, Conn);
                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int ExecuteNonQueryInt(string sql, SqlParameter[] paras)
        {
            try
            {
                SqlCommand comm = new SqlCommand(sql, Conn);
                if (paras != null)
                {
                    comm.Parameters.AddRange(paras);
                }
                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int ExecuteNonQueryInt(string sql, SqlParameter[] paras, string type)
        {
            try
            {
                SqlCommand comm = new SqlCommand(sql, Conn);
                if (paras != null)
                {
                    comm.Parameters.AddRange(paras);
                    if (type != "sql") comm.CommandType = CommandType.StoredProcedure;
                }
                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
