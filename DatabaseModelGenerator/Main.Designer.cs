
namespace DatabaseModelGenerator
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.DatabaseStr_TextBox = new System.Windows.Forms.TextBox();
            this.OutPath_TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Generator_Button = new System.Windows.Forms.Button();
            this.Namespace_TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TableName_TextBox = new System.Windows.Forms.TextBox();
            this.TableName_Label = new System.Windows.Forms.Label();
            this.OnlyOneTable_CheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库连接字符串";
            // 
            // DatabaseStr_TextBox
            // 
            this.DatabaseStr_TextBox.Location = new System.Drawing.Point(15, 29);
            this.DatabaseStr_TextBox.Name = "DatabaseStr_TextBox";
            this.DatabaseStr_TextBox.Size = new System.Drawing.Size(677, 21);
            this.DatabaseStr_TextBox.TabIndex = 1;
            // 
            // OutPath_TextBox
            // 
            this.OutPath_TextBox.Location = new System.Drawing.Point(15, 69);
            this.OutPath_TextBox.Name = "OutPath_TextBox";
            this.OutPath_TextBox.Size = new System.Drawing.Size(677, 21);
            this.OutPath_TextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "输出路径";
            // 
            // Generator_Button
            // 
            this.Generator_Button.Location = new System.Drawing.Point(294, 193);
            this.Generator_Button.Name = "Generator_Button";
            this.Generator_Button.Size = new System.Drawing.Size(75, 23);
            this.Generator_Button.TabIndex = 4;
            this.Generator_Button.Text = "生成";
            this.Generator_Button.UseVisualStyleBackColor = true;
            this.Generator_Button.Click += new System.EventHandler(this.Generator_Button_Click);
            // 
            // Namespace_TextBox
            // 
            this.Namespace_TextBox.Location = new System.Drawing.Point(15, 108);
            this.Namespace_TextBox.Name = "Namespace_TextBox";
            this.Namespace_TextBox.Size = new System.Drawing.Size(677, 21);
            this.Namespace_TextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "命名空间";
            // 
            // TableName_TextBox
            // 
            this.TableName_TextBox.Enabled = false;
            this.TableName_TextBox.Location = new System.Drawing.Point(208, 148);
            this.TableName_TextBox.Name = "TableName_TextBox";
            this.TableName_TextBox.Size = new System.Drawing.Size(484, 21);
            this.TableName_TextBox.TabIndex = 8;
            // 
            // TableName_Label
            // 
            this.TableName_Label.AutoSize = true;
            this.TableName_Label.Enabled = false;
            this.TableName_Label.Location = new System.Drawing.Point(206, 133);
            this.TableName_Label.Name = "TableName_Label";
            this.TableName_Label.Size = new System.Drawing.Size(41, 12);
            this.TableName_Label.TabIndex = 7;
            this.TableName_Label.Text = "表名称";
            // 
            // OnlyOneTable_CheckBox
            // 
            this.OnlyOneTable_CheckBox.AutoSize = true;
            this.OnlyOneTable_CheckBox.Location = new System.Drawing.Point(15, 148);
            this.OnlyOneTable_CheckBox.Name = "OnlyOneTable_CheckBox";
            this.OnlyOneTable_CheckBox.Size = new System.Drawing.Size(48, 16);
            this.OnlyOneTable_CheckBox.TabIndex = 9;
            this.OnlyOneTable_CheckBox.Text = "单表";
            this.OnlyOneTable_CheckBox.UseVisualStyleBackColor = true;
            this.OnlyOneTable_CheckBox.CheckedChanged += new System.EventHandler(this.OnlyOneTable_CheckBox_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 229);
            this.Controls.Add(this.OnlyOneTable_CheckBox);
            this.Controls.Add(this.TableName_TextBox);
            this.Controls.Add(this.TableName_Label);
            this.Controls.Add(this.Namespace_TextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Generator_Button);
            this.Controls.Add(this.OutPath_TextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DatabaseStr_TextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DatabaseStr_TextBox;
        private System.Windows.Forms.TextBox OutPath_TextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Generator_Button;
        private System.Windows.Forms.TextBox Namespace_TextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TableName_TextBox;
        private System.Windows.Forms.Label TableName_Label;
        private System.Windows.Forms.CheckBox OnlyOneTable_CheckBox;
    }
}

