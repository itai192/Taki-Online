using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
  public class DalHelper
    {  
        //note: you dont really need to read the following comment its mostly a joke(still true though)
        //Have you ever been exhausted by those annoying sql senteses? and when you are done with them
        //you would usually need to go to that depressing DBHelper in order to connect the two,
    // WELL NO MORE - Dal Helper is here to make your life better, easier and filled with joy
    // just provide us with the right sql and return! Yes exactly! DAL commands will only take 2 rows
    // from now on and it is all because DALHelper is just Sooooooooo amazing 
    // NO MORE Closeconnectiosn! the dal helper does it for you! 
    //~Alon Metuky's speech.
    
        public static string provider;
        public static string source;
        static DBHelper h; //only the dal helper is using the dbhelper
        public static void SetSource(string path)
        {
            source = path;
        }
        public static void SetProvider(string prov)
        {
            provider = prov;
        }
        public static void CreateDBHelper()
        {
            h = new DBHelper(provider, source);
        }
        public static int Insert(string sql) //works for all insert sql that also needs to create an int key and returns that key
        {
            int id = h.InsertWithAutoNumKey(sql);
            h.CloseConnection();
            return id;
        }
        public static DataRow SelectRow(string sql)  //works for all select sql that selects a single row and returns that row
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
        public static bool Update(string sql)//works for all update sql and checks if the db changed, if it did than the update worked and returns true, else false
        {
           int num = h.WriteData(sql);
            h.CloseConnection();
            if(num ==-1)
           {
              return false;
           }
              return true;
        }
        public static DataTable SelectTable(string sql)//works for all select sql that selects a table and returns that table
        {
            DataTable Table = h.GetDataTable(sql);
            h.CloseConnection();
            return Table;
        }
        public static bool insertWithoutCreatingID(string sql)//works for all insert sql that doesnt need to create an auto key.
        {
            int num = h.WriteData(sql); 
            h.CloseConnection();
             if (num == -1)
            { return false; }
            return true;
        }
        public static bool IsExist(string sql)//gets a select sql for a row and checks if such row exists
        { 
            DataTable exist = h.GetDataTable(sql);
            h.CloseConnection();
            if(exist.Rows.Count==0)
                {
                    return false;
                }
                return true;
        }
    }
}
