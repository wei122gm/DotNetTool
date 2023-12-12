using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseModelGenerator
{
    /// <summary>
    /// 生成器
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// 生成C#代码
        /// </summary>
        /// <param name="namespaceName">命名空间</param>
        /// <param name="tebleName">表名称</param>
        /// <param name="fieldInfos">字段信息</param>
        /// <returns>C#代码</returns>
        public string GeneratorCSharpCode(string namespaceName, string tebleName, List<FieldInfo> fieldInfos)
        {
            StringBuilder sb = new StringBuilder(400);
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine();
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// ");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    public class {StartUpper(tebleName)}");
            sb.AppendLine("    {");
            foreach (var item in fieldInfos)
            {
                sb.AppendLine("        /// <summary>");
                sb.AppendLine($"        /// {item.Description}");
                sb.AppendLine("        /// </summary>");
                string isNull = item.IsNull ? (IsNullType(item.Type) ? string.Empty : "?") : string.Empty;
                sb.AppendLine($"        public {item.Type}{isNull} {StartUpper(item.Name)} {{ get; set; }}");
            }
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        /// <summary>
        /// 文本开头改大写
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>开头的文本</returns>
        public string StartUpper(string text)
        {
            if (string.IsNullOrEmpty(text)) { return text; }
            return text[0].ToString().ToUpper() + text.Substring(1);
        }

        /// <summary>
        /// 判断是否为可空类型
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <returns>true=可以为空,false=不可以为空</returns>
        public bool IsNullType(string typeName)
        {
            bool isNullType;
            switch (typeName)
            {
                case "long": isNullType = false; break;
                case "bool": isNullType = false; break;
                case "DateTime": isNullType = false; break;
                case "DateTimeOffset": isNullType = false; break;
                case "decimal": isNullType = false; break;
                case "double": isNullType = false; break;
                case "int": isNullType = false; break;
                case "float": isNullType = false; break;
                case "short": isNullType = false; break;
                case "TimeSpan": isNullType = false; break;
                case "byte": isNullType = false; break;
                case "Guid": isNullType = false; break;
                default: isNullType = true; break;
            }
            return isNullType;
        }
    }
}
