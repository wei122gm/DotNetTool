using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DatabaseModelGenerator
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class DataBase : IDataBasse
    {
        public string DataBaseStr { get; set; }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        private IDbConnection GetConnection()
        {
            DbConnection connection = new SqlConnection(DataBaseStr);
            connection.Open();
            return connection;
        }

        public List<string> GetAllTableNames()
        {
            List<string> names = new List<string>();
            using (IDbConnection connection = GetConnection())
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select name from sys.tables";
                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    names.Add(dataReader[0].ToString());
                }
            }
            return names;
        }

        public List<FieldInfo> GetTableAllFieldInfos(string tebleName)
        {
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            using (IDbConnection connection = GetConnection())
            {
                string sqlText = "select COLUMN_NAME as [Name],DATA_TYPE as [Type],IS_NULLABLE as [IsNull],d.value as [Description] " +
                    "from information_schema.columns i " +
                    "left join " +
                    "(select c.name,p.value from sys.tables t " +
                    "join sys.columns c on c.object_id = t.object_id " +
                    "join sys.extended_properties p on p.major_id=c.object_id and p.minor_id=c.column_id " +
                    "where t.name = @TebleName) d on d.name=i.COLUMN_NAME " +
                    "where table_name = @TebleName ";
                sqlText = sqlText.Replace("@TebleName", $"'{tebleName}'");
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandText = sqlText;
                IDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    FieldInfo fieldInfo = new FieldInfo();
                    fieldInfo.Name = dataReader[0].ToString();
                    fieldInfo.Type = dataReader[1].ToString();
                    fieldInfo.IsNull = bool.Parse(dataReader[2].ToString().ToLower() != "yes" ? "false" : "true");
                    fieldInfo.Description = dataReader[3]?.ToString();
                    fieldInfos.Add(fieldInfo);
                }
            }
            return fieldInfos;
        }

        public string ConvertTypeName(string dbTypeName)
        {
            string name = dbTypeName.ToLower();
            string outName = name;
            //参考:https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings
            switch (name)
            {
                case "bigint": outName = "long"; break;
                case "binary": outName = "byte[]"; break;
                case "bit": outName = "bool"; break;
                case "char": outName = "string"; break;
                case "date": outName = "DateTime"; break;
                case "datetime": outName = "DateTime"; break;
                case "datetime2": outName = "DateTime"; break;
                case "datetimeoffset": outName = "DateTimeOffset"; break;
                case "decimal": outName = "decimal"; break;
                case "float": outName = "double"; break;
                case "image": outName = "byte[]"; break;
                case "int": outName = "int"; break;
                case "money": outName = "decimal"; break;
                case "nchar": outName = "string"; break;
                case "ntext": outName = "string"; break;
                case "numeric": outName = "decimal"; break;
                case "nvarchar": outName = "string"; break;
                case "real": outName = "float"; break;
                case "rowversion": outName = "byte[]"; break;
                case "smalldatetime": outName = "DateTime"; break;
                case "smallint": outName = "short"; break;
                case "smallmoney": outName = "decimal"; break;
                case "sql_variant": outName = "object"; break;
                case "text": outName = "string"; break;
                case "time": outName = "TimeSpan"; break;
                case "timestamp": outName = "byte[]"; break;
                case "tinyint": outName = "byte"; break;
                case "uniqueidentifier": outName = "Guid"; break;
                case "varbinary": outName = "byte[]"; break;
                case "varchar": outName = "string"; break;
                case "xml": outName = "string"; break;
            }
            return outName;
        }
    }
}
