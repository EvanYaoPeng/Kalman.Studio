using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Kalman.Class
{
    /// <summary>
    /// 根据Sql预句生成类
    /// </summary>
    public class ClassGenerating
    {
        /// <summary>
        /// 根据SQL语句获取实体类的字符串
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public string SqlToClass(string ConnectionString, string sql, string className)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = sql;
                DataTable dt = new DataTable();
                SqlDataAdapter sad = new SqlDataAdapter(command);
                sad.Fill(dt);
                var reval = DataTableToClass(dt, className);
                return reval;
            }
        }

        /// <summary>
        /// 根据DataTable获取实体类的字符串
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public string DataTableToClass(DataTable dt, string className, string nameSpace = null)
        {
            StringBuilder reval = new StringBuilder();
            StringBuilder propertiesValue = new StringBuilder();
            string replaceGuid = Guid.NewGuid().ToString();
            foreach (DataColumn r in dt.Columns)
            {
                propertiesValue.AppendLine();
                string typeName = ChangeType(r.DataType);
                propertiesValue.AppendFormat("    public {0} {1} {2}",  typeName, r.ColumnName, "{get;set;}");
                propertiesValue.AppendLine();
            }

            reval.AppendFormat(@"   public class {0}{{
                        {1}
   }}
            ", className, propertiesValue);

            if (nameSpace != null)
            {
                return string.Format(@"using System;
namespace {1}
{{
 {0}
}}", reval.ToString(), nameSpace);
            }
            else
            {
                return reval.ToString();
            }
        }
     
        /// <summary>
        /// 匹配类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string ChangeType(Type type)
        {
            string typeName = type.Name;
            switch (typeName)
            {
                case "Int32": typeName = "int"; break;
                case "String": typeName = "string"; break;
            }
            return typeName;
        }
       
        public string DbTypeToFieldType(string dbtype)
        {
            if (string.IsNullOrEmpty(dbtype)) return dbtype;
            dbtype = dbtype.ToLower();
            string csharpType = "object";
            switch (dbtype)
            {
                case "bigint": csharpType = "long"; break;
                case "binary": csharpType = "byte[]"; break;
                case "bit": csharpType = "bool"; break;
                case "char": csharpType = "string"; break;
                case "date": csharpType = "DateTime"; break;
                case "datetime": csharpType = "DateTime"; break;
                case "datetime2": csharpType = "DateTime"; break;
                case "datetimeoffset": csharpType = "DateTimeOffset"; break;
                case "decimal": csharpType = "decimal"; break;
                case "float": csharpType = "double"; break;
                case "image": csharpType = "byte[]"; break;
                case "int": csharpType = "int"; break;
                case "money": csharpType = "decimal"; break;
                case "nchar": csharpType = "string"; break;
                case "ntext": csharpType = "string"; break;
                case "numeric": csharpType = "decimal"; break;
                case "nvarchar": csharpType = "string"; break;
                case "real": csharpType = "Single"; break;
                case "smalldatetime": csharpType = "DateTime"; break;
                case "smallint": csharpType = "short"; break;
                case "smallmoney": csharpType = "decimal"; break;
                case "sql_variant": csharpType = "object"; break;
                case "sysname": csharpType = "object"; break;
                case "text": csharpType = "string"; break;
                case "time": csharpType = "TimeSpan"; break;
                case "timestamp": csharpType = "byte[]"; break;
                case "tinyint": csharpType = "byte"; break;
                case "uniqueidentifier": csharpType = "Guid"; break;
                case "varbinary": csharpType = "byte[]"; break;
                case "varchar": csharpType = "string"; break;
                case "xml": csharpType = "string"; break;
                default: csharpType = "object"; break;
            }
            return csharpType;
        }
    
    }

}
