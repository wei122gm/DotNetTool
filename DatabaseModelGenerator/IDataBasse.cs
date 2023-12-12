using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseModelGenerator
{
    /// <summary>
    /// 数据库
    /// </summary>
    public interface IDataBasse
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string DataBaseStr { get; set; }

        /// <summary>
        /// 获取所有表名称
        /// </summary>
        /// <returns></returns>
        List<string> GetAllTableNames();

        /// <summary>
        /// 获取表所有字段信息
        /// </summary>
        /// <param name="tebleName">表名称</param>
        /// <returns>字段信息列表</returns>
        List<FieldInfo> GetTableAllFieldInfos(string tebleName);

        /// <summary>
        /// 转换类型名称-将数据库类型转换为C#类型
        /// </summary>
        /// <param name="dbTypeName">数据库类型名称</param>
        /// <returns>C#类型名称</returns>
        string ConvertTypeName(string dbTypeName);
    }
}
