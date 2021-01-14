using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
namespace BLL
{
    public class BLL_Helper
    {
        public static void SetSourceAndProvider(string source, string provider)
        {
            DalHelper.SetProvider(provider);
            DalHelper.SetSource(source);
        }
        public static void CreateDBHelperInDalHelper(string source, string provider)
        {
            SetSourceAndProvider(source, provider);
            DalHelper.CreateDBHelper();
        }
        public static List<T> DataTableToList<T>(DataTable dt, string field)
        {
            List<T> l = new List<T>();
            foreach(DataRow dr in dt.Rows)
            {
                l.Add(dr.Field<T>(field));
            }
            return l;
        }
        public static List<T> UniteLists<T>(List<T> l1, List<T> l2)
        {
            List<T> l = new List<T>();
            l.AddRange(l1);
            l.AddRange(l2);
            return l;
        }
        
    }
}
