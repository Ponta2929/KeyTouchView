
namespace ImageLayout
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl_Main = new System.Windows.Forms.TabControl();
            this.TabPage_Image = new System.Windows.Forms.TabPage();
            this.Button_Apply = new System.Windows.Forms.Button();
            this.ComboBox_SkinList = new System.Windows.Forms.ComboBox();
            this.TabControl_Main.SuspendLayout();
            this.TabPage_Image.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl_Main
            // 
            this.TabControl_Main.Controls.Add(this.TabPage_Image);
            this.TabControl_Main.Location = new System.Drawing.Point(12, 12);
            this.TabControl_Main.Name = "TabControl_Main";
            this.TabControl_Main.SelectedIndex = 0;
            this.TabControl_Main.Size = new System.Drawing.Size(379, 210);
            this.TabControl_Main.TabIndex = 7;
            // 
            // TabPage_Image
            // 
            this.TabPage_Image.Controls.Add(this.Button_Apply);
            this.TabPage_Image.Controls.Add(this.ComboBox_SkinList);
            this.TabPage_Image.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Image.Name = "TabPage_Image";
            this.TabPage_Image.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Image.Size = new System.Drawing.Size(371, 184);
            this.TabPage_Image.TabIndex = 1;
            this.TabPage_Image.Text = "スキンリスト";
            this.TabPage_Image.UseVisualStyleBackColor = true;
            // 
            // Button_Apply
            // 
            this.Button_Apply.Location = new System.Drawing.Point(290, 4);
            this.Button_Apply.Name = "Button_Apply";
            this.Button_Apply.Size = new System.Drawing.Size(75, 23);
            this.Button_Apply.TabIndex = 4;
            this.Button_Apply.Text = "適用(&A)";
            this.Button_Apply.UseVisualStyleBackColor = true;
            this.Button_Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // ComboBox_SkinList
            // 
            this.ComboBox_SkinList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_SkinList.FormattingEnabled = true;
            this.ComboBox_SkinList.Location = new System.Drawing.Point(6, 6);
            this.ComboBox_SkinList.Name = "ComboBox_SkinList";
            this.ComboBox_SkinList.Size = new System.Drawing.Size(278, 20);
            this.ComboBox_SkinList.TabIndex = 0;
            this.ComboBox_SkinList.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.ComboBox_SkinList_Format);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 234);
            this.Controls.Add(this.TabControl_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingForm";
            this.Text = "設定 [ImageLayout]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingForm_FormClosing);
            this.TabControl_Main.ResumeLayout(false);
            this.TabPage_Image.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl_Main;
        private System.Windows.Forms.TabPage TabPage_Image;
        private System.Windows.Forms.Button Button_Apply;
        private System.Windows.Forms.ComboBox ComboBox_SkinList;
    }
}