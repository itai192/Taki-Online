﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
    /// <summary>
    /// Uses DBHelper and makes it easier to execute queries
    /// </summary>
  public static class DalHelper
    {

        //a string of the provider of the dal helper
        private static string provider;
        //a string of the source of the dal helper
        private static string source;
        //the object of DBhelper dal helper is using
        private static DBHelper h; 
        //sets the provider of the DBHelper
        public static void SetSource(string path)
        {
            source = path;
        }
        //sets the provider of the DBHelper
        public static void SetProvider(string prov)
        {
            provider = prov;
        }
        //creates an instance of the DBHelper
        public static void CreateDBHelper()
        {
            h = new DBHelper(provider, source);
        }
        //works for all insert sql that also needs to create an int key and returns that key
        public static int Insert(string sql) 
        {
            lock (h)
            {
                int id = h.InsertWithAutoNumKey(sql);
                h.CloseConnection();
                return id;
            }
        }
        public static DataRow SelectRow(string sql)  //works for all select sql that selects a single row and returns that row
        {
            lock (h)
            {
                try
                {
                    DataRow row = h.GetDataTable(sql).Rows[0];
                    h.CloseConnection();
                    return row;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    h.CloseConnection();
                }
            }
        }
        /// <summary>
        /// a method that takes a string of arrays and returns a string of them seperated by commas
        /// </summary>
        /// <param name="arr">the array</param>
        /// <returns>a string of the contents of the array seperated by commas</returns>
        public static string ArrayToStringWithCommas(string[] arr)
        {
            string ret = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i != 0)
                {
                    ret += ", ";
                }
                ret += arr[i];

            }
            return ret;
        }
        /// <summary>
        /// throws an arrays not in same length exception
        /// </summary>
        /// <param name="arr1">an array expected to be the same length as the other array</param>
        /// <param name="arr2">an array expected to be the same length as the other array</param>
        public static void ThrowArraysNotInSameLengthExeption(Array arr1,Array arr2)
        {
            Exception e = new Exception("expected arrays to be in same length");
            e.Data["arr1"] = arr1;
            e.Data["arr2"] = arr2;
        }
        /// <summary>
        /// a method that returns a simple insert query based over the parameters
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="columns">the columns you want to insert in order</param>
        /// <param name="values">the values to be inserted into the columns in order</param>
        /// <returns>a simple insert sentence based over the parameters</returns>
        public static string SimpleInsertQuery(string table, string[] columns, string[] values)
        {
            if(columns.Length!=values.Length)
            {
                ThrowArraysNotInSameLengthExeption(columns,values);
            }
            return $"INSERT INTO {table} ({ArrayToStringWithCommas(columns)}) VALUES ({ArrayToStringWithCommas(values)})";
        }
        /// <summary>
        /// a method that returns a simple update query based over the parameters
        /// </summary>
        /// <param name="table">the table to update</param>
        /// <param name="columns">the columns you want to update in order</param>
        /// <param name="values">the values to be updateded into the columns in order</param>
        /// <param name="whereCondition">the where condition (without the word WHERE)</param>
        /// <returns>a simple update sentence based over the parameters</returns>
        public static string SimpleUpdateQuery(string table, string[] columns, string[] values, string whereCondition)
        {
            if(columns.Length!=values.Length)
            {
                ThrowArraysNotInSameLengthExeption(columns, values);
            }
            string[] SetValues = new string[columns.Length];
            for (int i = 0; i < SetValues.Length; i++)
            {
                SetValues[i] = $"{columns[i]}={values[i]}";
            }
            return SimpleUpdateQuery(table, ArrayToStringWithCommas(SetValues), whereCondition);
        }
        /// <summary>
        /// a method that returns a simple update query based over the parameters
        /// </summary>
        /// <param name="table">the table to update</param>
        /// <param name="setVals">a string of the set values</param>
        /// <param name="whereCondition">the where condition (without the word WHERE)</param>
        public static string SimpleUpdateQuery(string table, string setVals, string whereCondition)
        {
            return $"UPDATE {table} SET {setVals} WHERE {whereCondition}";
        }
        /// <summary>
        /// excecutes an update query
        /// </summary>
        /// <param name="sql">an sql query</param>
        /// <returns>returns how many columns have been updated</returns>
        public static int Update(string sql)//works for all update sql and checks if the db changed, if it did than the update worked and returns true, else false
        {
            lock (h)
            {
                int num = h.WriteData(sql);
                h.CloseConnection();
                if (num == DBHelper.WRITEDATA_ERROR)
                {
                    throw new Exception("Error Updating");
                }
                return num;
            }
        }
        public static DataTable SelectTable(string sql)//works for all select sql that selects a table and returns that table
        {
            lock (h)
            {
                DataTable Table = h.GetDataTable(sql);
                h.CloseConnection();
                return Table;
            }
        }
        public static bool insertWithoutCreatingID(string sql)//works for all insert sql that doesnt need to create an auto key.
        {
            lock (h)
            {
                int num = h.WriteData(sql);
                h.CloseConnection();
                if (num == -1)
                { return false; }
                return true;
            }
        }
        public static bool IsExist(string sql)//gets a select sql for a row and checks if such row exists
        {
            lock (h)
            {
                DataTable exist = h.GetDataTable(sql);
                h.CloseConnection();
                if (exist.Rows.Count == 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
