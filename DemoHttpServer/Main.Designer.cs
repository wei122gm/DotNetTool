
namespace DemoHttpServer
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
            this.Open_Button = new System.Windows.Forms.Button();
            this.Port_TextBox = new System.Windows.Forms.TextBox();
            this.FolderPath_TextBox = new System.Windows.Forms.TextBox();
            this.Start_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Folder_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Open_Button
            // 
            this.Open_Button.Enabled = false;
            this.Open_Button.Location = new System.Drawing.Point(273, 72);
            this.Open_Button.Name = "Open_Button";
            this.Open_Button.Size = new System.Drawing.Size(75, 23);
            this.Open_Button.TabIndex = 7;
            this.Open_Button.Text = "浏览";
            this.Open_Button.UseVisualStyleBackColor = true;
            this.Open_Button.Click += new System.EventHandler(this.Open_Button_Click);
            // 
            // Port_TextBox
            // 
            this.Port_TextBox.Location = new System.Drawing.Point(14, 72);
            this.Port_TextBox.Name = "Port_TextBox";
            this.Port_TextBox.Size = new System.Drawing.Size(81, 21);
            this.Port_TextBox.TabIndex = 6;
            this.Port_TextBox.Text = "8080";
            // 
            // FolderPath_TextBox
            // 
            this.FolderPath_TextBox.Location = new System.Drawing.Point(12, 28);
            this.FolderPath_TextBox.Name = "FolderPath_TextBox";
            this.FolderPath_TextBox.Size = new System.Drawing.Size(498, 21);
            this.FolderPath_TextBox.TabIndex = 5;
            // 
            // Start_Button
            // 
            this.Start_Button.Location = new System.Drawing.Point(166, 72);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(75, 23);
            this.Start_Button.TabIndex = 4;
            this.Start_Button.Text = "启动";
            this.Start_Button.UseVisualStyleBackColor = true;
            this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "文件夹地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "端口";
            // 
            // Folder_Button
            // 
            this.Folder_Button.Location = new System.Drawing.Point(525, 28);
            this.Folder_Button.Name = "Folder_Button";
            this.Folder_Button.Size = new System.Drawing.Size(75, 23);
            this.Folder_Button.TabIndex = 10;
            this.Folder_Button.Text = "地址";
            this.Folder_Button.UseVisualStyleBackColor = true;
            this.Folder_Button.Click += new System.EventHandler(this.Folder_Button_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 105);
            this.Controls.Add(this.Folder_Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Open_Button);
            this.Controls.Add(this.Port_TextBox);
            this.Controls.Add(this.FolderPath_TextBox);
            this.Controls.Add(this.Start_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Open_Button;
        private System.Windows.Forms.TextBox Port_TextBox;
        private System.Windows.Forms.TextBox FolderPath_TextBox;
        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Folder_Button;
    }
}

