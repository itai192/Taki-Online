using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace DAL
{
    public class DBHelper
    {
        //Constants 
        public const int WRITEDATA_ERROR = -1;
        //class member variables 
        private OleDbConnection conn;
        private string provider;
        private string source;
        private bool connOpen;

        //Construct the object with a provided provider and source 
        public DBHelper(string provider, string source)
        {
            this.provider = provider;
            this.source = source;
            this.conn = new OleDbConnection(BuildConnString());
            this.connOpen = false;
        }

        //This function builds the connection string to be used to open connection.
        private string BuildConnString()
        {
            return String.Format(@"Provider={0};Data Source={1};", this.provider, this.source);
        }
        //This function is closing the connection unless it is already closed! 
        public void CloseConnection()
        {
            if (!this.connOpen)
            {
                this.OpenConnection();
            }


            if (this.connOpen)
            {                 
                    conn.Close();
                    connOpen = false;
            }
        }
        //Open connection to the database.
        public void OpenConnection()
        {
            if(connOpen==false)
            {
                this.conn.Open();
                connOpen = true;
            }
        }
        //Execute UPDATE or INSERT sql commands and return number of rows affected.         
        //return WRITEDATA_ERROR on failure
        public int WriteData(string sql)
        {
            int ret = WRITEDATA_ERROR;
            if (!this.connOpen)
            {
                this.OpenConnection();
            }


            if (this.connOpen)
            {
                OleDbDataReader rd = null;
                try
                {
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    rd = cmd.ExecuteReader();
                    ret = rd.RecordsAffected;
                }
                catch
                {
                    return WRITEDATA_ERROR;
                }
            }
            return ret;
        }
        //Execute SELECT sql commands and return a reference to an OleDbDataReader         
        //if execution fails return null
        public OleDbDataReader ReadData(string sql)
        {
            OleDbDataReader rd = null;
            if (!this.connOpen)
            {
                this.OpenConnection();
            }


            if (this.connOpen)
            {
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                rd = cmd.ExecuteReader();

            }
            return rd;
        }
        //This function should be used for inserting a single record into a table in the database with an autonmuber key. the format of the sql must be          
        //INSERT INTO <TableName> (Fields...) VALUES (values...)         
        //the function return the autonumber key generated for the new record or WRITEDATA_ERROR if fail
        public int InsertWithAutoNumKey(string sql)
        {
            int newID=WRITEDATA_ERROR;
            if(!this.connOpen)
            {
                this.OpenConnection();
            }
            if(this.connOpen)
            {
                if (this.WriteData(sql) != WRITEDATA_ERROR)
                {
                    OleDbDataReader rd = ReadData(@"SELECT @@Identity");
                    if (rd != null)
                    {
                        
                        while (rd.Read())
                        {
                            newID = (int)rd[0];
                        }
                    }
                }
            }
            return newID;
        }
        //This function reads from the database a data table fully cached in memory using a standard SQL SELECT statement.         
        //The function returns the data table or null on failure. 
        public DataTable GetDataTable(string sql)
        {
                DataTable tbl = new DataTable();
                OleDbDataReader rd = ReadData(sql);
                tbl.Load(rd);
                return tbl;
        }

        //This function reads from the database a data set fully cached in memory using an array of standard SQL SELECT statements.         
        //The function returns the data set or null on failure. The table names inside the dataset are sql1, sql2,... 
        public DataSet GetDataSet(string[] sql)
        {
            try
            {
                DataSet ds = new DataSet();

                for (int i = 0; i < sql.Length; i++)
                {
                    OleDbDataAdapter da = new OleDbDataAdapter(sql[i], this.conn);
                    da.Fill(ds, "sql" + (i + 1));
                }
                return ds;
            }
            catch
            {
                return null;
            }
        }
    }
}
