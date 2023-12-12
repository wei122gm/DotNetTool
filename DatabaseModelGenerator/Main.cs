using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseModelGenerator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Generator_Button_Click(object sender, EventArgs e)
        {
            IDataBasse dataBase = new DataBase();
            dataBase.DataBaseStr = DatabaseStr_TextBox.Text;
            if (OnlyOneTable_CheckBox.Checked == false)
            {
                List<string> names = dataBase.GetAllTableNames();
                foreach (var item in names)
                {
                    List<FieldInfo> fieldInfos = dataBase.GetTableAllFieldInfos(item);
                    fieldInfos.ForEach(v => v.Type = dataBase.ConvertTypeName(v.Type));
                    string text = new Generator().GeneratorCSharpCode(Namespace_TextBox.Text, item, fieldInfos);
                    File.WriteAllText($"{OutPath_TextBox.Text}\\{new Generator().StartUpper(item)}.cs", text);
                }
            }
            else
            {
                string item = TableName_TextBox.Text;
                List<FieldInfo> fieldInfos = dataBase.GetTableAllFieldInfos(item);
                fieldInfos.ForEach(v => v.Type = dataBase.ConvertTypeName(v.Type));
                string text = new Generator().GeneratorCSharpCode(Namespace_TextBox.Text, item, fieldInfos);
                File.WriteAllText($"{OutPath_TextBox.Text}\\{new Generator().StartUpper(item)}.cs", text);
            }

            MessageBox.Show("完成");
        }

        private void OnlyOneTable_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool isOnlyOneTable = OnlyOneTable_CheckBox.Checked;
            TableName_Label.Enabled = TableName_TextBox.Enabled = isOnlyOneTable;
        }
    }
}
