using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseModelGenerator
{
    /// <summary>
    /// 字段信息
    /// </summary>
    public class FieldInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool IsNull { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Description { get; set; }
    }
}
