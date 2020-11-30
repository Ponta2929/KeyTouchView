
namespace StackLayout
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
            this.TabPage_Replace = new System.Windows.Forms.TabPage();
            this.Button_Downward = new System.Windows.Forms.Button();
            this.Button_Upward = new System.Windows.Forms.Button();
            this.Label_ReplaceList = new System.Windows.Forms.Label();
            this.Button_Remove = new System.Windows.Forms.Button();
            this.ListBox_ReplaceList = new System.Windows.Forms.ListBox();
            this.Button_Insert = new System.Windows.Forms.Button();
            this.TextBox_Replace = new System.Windows.Forms.TextBox();
            this.Label_To = new System.Windows.Forms.Label();
            this.ComboBox_KeyList = new System.Windows.Forms.ComboBox();
            this.TabPage_Ext = new System.Windows.Forms.TabPage();
            this.ComboBox_StackDirection = new System.Windows.Forms.ComboBox();
            this.Label_StackDirection = new System.Windows.Forms.Label();
            this.CheckBox_Antialiasing = new System.Windows.Forms.CheckBox();
            this.NumericUpDown_BorderSize = new System.Windows.Forms.NumericUpDown();
            this.Label_BorderSize = new System.Windows.Forms.Label();
            this.Button_Default = new System.Windows.Forms.Button();
            this.Button_Border = new System.Windows.Forms.Button();
            this.Panel_Border = new System.Windows.Forms.Panel();
            this.Label_Border = new System.Windows.Forms.Label();
            this.Button_Background = new System.Windows.Forms.Button();
            this.Panel_Background = new System.Windows.Forms.Panel();
            this.Label_Background = new System.Windows.Forms.Label();
            this.Button_KeyDown = new System.Windows.Forms.Button();
            this.Panel_KeyDown = new System.Windows.Forms.Panel();
            this.Label_KeyDown = new System.Windows.Forms.Label();
            this.Button_Normal = new System.Windows.Forms.Button();
            this.Panel_Normal = new System.Windows.Forms.Panel();
            this.Label_Normal = new System.Windows.Forms.Label();
            this.Button_Font = new System.Windows.Forms.Button();
            this.Label_FontView = new System.Windows.Forms.Label();
            this.Label_Font = new System.Windows.Forms.Label();
            this.TabControl_Main.SuspendLayout();
            this.TabPage_Replace.SuspendLayout();
            this.TabPage_Ext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_BorderSize)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl_Main
            // 
            this.TabControl_Main.Controls.Add(this.TabPage_Replace);
            this.TabControl_Main.Controls.Add(this.TabPage_Ext);
            this.TabControl_Main.Location = new System.Drawing.Point(12, 12);
            this.TabControl_Main.Name = "TabControl_Main";
            this.TabControl_Main.SelectedIndex = 0;
            this.TabControl_Main.Size = new System.Drawing.Size(379, 210);
            this.TabControl_Main.TabIndex = 7;
            // 
            // TabPage_Replace
            // 
            this.TabPage_Replace.Controls.Add(this.Button_Downward);
            this.TabPage_Replace.Controls.Add(this.Button_Upward);
            this.TabPage_Replace.Controls.Add(this.Label_ReplaceList);
            this.TabPage_Replace.Controls.Add(this.Button_Remove);
            this.TabPage_Replace.Controls.Add(this.ListBox_ReplaceList);
            this.TabPage_Replace.Controls.Add(this.Button_Insert);
            this.TabPage_Replace.Controls.Add(this.TextBox_Replace);
            this.TabPage_Replace.Controls.Add(this.Label_To);
            this.TabPage_Replace.Controls.Add(this.ComboBox_KeyList);
            this.TabPage_Replace.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Replace.Name = "TabPage_Replace";
            this.TabPage_Replace.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Replace.Size = new System.Drawing.Size(371, 184);
            this.TabPage_Replace.TabIndex = 1;
            this.TabPage_Replace.Text = "置換リスト";
            this.TabPage_Replace.UseVisualStyleBackColor = true;
            // 
            // Button_Downward
            // 
            this.Button_Downward.Location = new System.Drawing.Point(290, 83);
            this.Button_Downward.Name = "Button_Downward";
            this.Button_Downward.Size = new System.Drawing.Size(75, 23);
            this.Button_Downward.TabIndex = 9;
            this.Button_Downward.Text = "↓";
            this.Button_Downward.UseVisualStyleBackColor = true;
            this.Button_Downward.Click += new System.EventHandler(this.Downward_Click);
            // 
            // Button_Upward
            // 
            this.Button_Upward.Location = new System.Drawing.Point(290, 54);
            this.Button_Upward.Name = "Button_Upward";
            this.Button_Upward.Size = new System.Drawing.Size(75, 23);
            this.Button_Upward.TabIndex = 8;
            this.Button_Upward.Text = "↑";
            this.Button_Upward.UseVisualStyleBackColor = true;
            this.Button_Upward.Click += new System.EventHandler(this.Upward_Click);
            // 
            // Label_ReplaceList
            // 
            this.Label_ReplaceList.AutoSize = true;
            this.Label_ReplaceList.Location = new System.Drawing.Point(6, 39);
            this.Label_ReplaceList.Name = "Label_ReplaceList";
            this.Label_ReplaceList.Size = new System.Drawing.Size(55, 12);
            this.Label_ReplaceList.TabIndex = 7;
            this.Label_ReplaceList.Text = "置換リスト:";
            // 
            // Button_Remove
            // 
            this.Button_Remove.Location = new System.Drawing.Point(290, 155);
            this.Button_Remove.Name = "Button_Remove";
            this.Button_Remove.Size = new System.Drawing.Size(75, 23);
            this.Button_Remove.TabIndex = 6;
            this.Button_Remove.Text = "削除(&D)";
            this.Button_Remove.UseVisualStyleBackColor = true;
            this.Button_Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // ListBox_ReplaceList
            // 
            this.ListBox_ReplaceList.FormattingEnabled = true;
            this.ListBox_ReplaceList.ItemHeight = 12;
            this.ListBox_ReplaceList.Location = new System.Drawing.Point(6, 54);
            this.ListBox_ReplaceList.Name = "ListBox_ReplaceList";
            this.ListBox_ReplaceList.Size = new System.Drawing.Size(278, 124);
            this.ListBox_ReplaceList.TabIndex = 5;
            this.ListBox_ReplaceList.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.ReplaceList_Format);
            // 
            // Button_Insert
            // 
            this.Button_Insert.Location = new System.Drawing.Point(290, 6);
            this.Button_Insert.Name = "Button_Insert";
            this.Button_Insert.Size = new System.Drawing.Size(75, 23);
            this.Button_Insert.TabIndex = 4;
            this.Button_Insert.Text = "追加(&A)";
            this.Button_Insert.UseVisualStyleBackColor = true;
            this.Button_Insert.Click += new System.EventHandler(this.Insert_Click);
            // 
            // TextBox_Replace
            // 
            this.TextBox_Replace.Location = new System.Drawing.Point(164, 8);
            this.TextBox_Replace.Name = "TextBox_Replace";
            this.TextBox_Replace.Size = new System.Drawing.Size(120, 19);
            this.TextBox_Replace.TabIndex = 2;
            // 
            // Label_To
            // 
            this.Label_To.AutoSize = true;
            this.Label_To.Location = new System.Drawing.Point(147, 11);
            this.Label_To.Name = "Label_To";
            this.Label_To.Size = new System.Drawing.Size(11, 12);
            this.Label_To.TabIndex = 1;
            this.Label_To.Text = ">";
            // 
            // ComboBox_KeyList
            // 
            this.ComboBox_KeyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_KeyList.FormattingEnabled = true;
            this.ComboBox_KeyList.Location = new System.Drawing.Point(6, 8);
            this.ComboBox_KeyList.Name = "ComboBox_KeyList";
            this.ComboBox_KeyList.Size = new System.Drawing.Size(135, 20);
            this.ComboBox_KeyList.TabIndex = 0;
            // 
            // TabPage_Ext
            // 
            this.TabPage_Ext.Controls.Add(this.ComboBox_StackDirection);
            this.TabPage_Ext.Controls.Add(this.Label_StackDirection);
            this.TabPage_Ext.Controls.Add(this.CheckBox_Antialiasing);
            this.TabPage_Ext.Controls.Add(this.NumericUpDown_BorderSize);
            this.TabPage_Ext.Controls.Add(this.Label_BorderSize);
            this.TabPage_Ext.Controls.Add(this.Button_Default);
            this.TabPage_Ext.Controls.Add(this.Button_Border);
            this.TabPage_Ext.Controls.Add(this.Panel_Border);
            this.TabPage_Ext.Controls.Add(this.Label_Border);
            this.TabPage_Ext.Controls.Add(this.Button_Background);
            this.TabPage_Ext.Controls.Add(this.Panel_Background);
            this.TabPage_Ext.Controls.Add(this.Label_Background);
            this.TabPage_Ext.Controls.Add(this.Button_KeyDown);
            this.TabPage_Ext.Controls.Add(this.Panel_KeyDown);
            this.TabPage_Ext.Controls.Add(this.Label_KeyDown);
            this.TabPage_Ext.Controls.Add(this.Button_Normal);
            this.TabPage_Ext.Controls.Add(this.Panel_Normal);
            this.TabPage_Ext.Controls.Add(this.Label_Normal);
            this.TabPage_Ext.Controls.Add(this.Button_Font);
            this.TabPage_Ext.Controls.Add(this.Label_FontView);
            this.TabPage_Ext.Controls.Add(this.Label_Font);
            this.TabPage_Ext.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Ext.Name = "TabPage_Ext";
            this.TabPage_Ext.Size = new System.Drawing.Size(371, 184);
            this.TabPage_Ext.TabIndex = 2;
            this.TabPage_Ext.Text = "その他";
            this.TabPage_Ext.UseVisualStyleBackColor = true;
            // 
            // ComboBox_StackDirection
            // 
            this.ComboBox_StackDirection.FormattingEnabled = true;
            this.ComboBox_StackDirection.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.ComboBox_StackDirection.Location = new System.Drawing.Point(190, 111);
            this.ComboBox_StackDirection.Name = "ComboBox_StackDirection";
            this.ComboBox_StackDirection.Size = new System.Drawing.Size(125, 20);
            this.ComboBox_StackDirection.TabIndex = 22;
            this.ComboBox_StackDirection.SelectedIndexChanged += new System.EventHandler(this.ComboBox_StackDirection_SelectedIndexChanged);
            // 
            // Label_StackDirection
            // 
            this.Label_StackDirection.AutoSize = true;
            this.Label_StackDirection.Location = new System.Drawing.Point(188, 96);
            this.Label_StackDirection.Name = "Label_StackDirection";
            this.Label_StackDirection.Size = new System.Drawing.Size(29, 12);
            this.Label_StackDirection.TabIndex = 21;
            this.Label_StackDirection.Text = "方向";
            // 
            // CheckBox_Antialiasing
            // 
            this.CheckBox_Antialiasing.AutoSize = true;
            this.CheckBox_Antialiasing.Location = new System.Drawing.Point(190, 157);
            this.CheckBox_Antialiasing.Name = "CheckBox_Antialiasing";
            this.CheckBox_Antialiasing.Size = new System.Drawing.Size(94, 16);
            this.CheckBox_Antialiasing.TabIndex = 20;
            this.CheckBox_Antialiasing.Text = "アンチエイリアス";
            this.CheckBox_Antialiasing.UseVisualStyleBackColor = true;
            this.CheckBox_Antialiasing.CheckedChanged += new System.EventHandler(this.Antialiasing_CheckedChanged);
            // 
            // NumericUpDown_BorderSize
            // 
            this.NumericUpDown_BorderSize.Location = new System.Drawing.Point(190, 70);
            this.NumericUpDown_BorderSize.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.NumericUpDown_BorderSize.Name = "NumericUpDown_BorderSize";
            this.NumericUpDown_BorderSize.Size = new System.Drawing.Size(125, 19);
            this.NumericUpDown_BorderSize.TabIndex = 17;
            this.NumericUpDown_BorderSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericUpDown_BorderSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown_BorderSize.ValueChanged += new System.EventHandler(this.BorderSize_ValueChanged);
            // 
            // Label_BorderSize
            // 
            this.Label_BorderSize.AutoSize = true;
            this.Label_BorderSize.Location = new System.Drawing.Point(188, 53);
            this.Label_BorderSize.Name = "Label_BorderSize";
            this.Label_BorderSize.Size = new System.Drawing.Size(29, 12);
            this.Label_BorderSize.TabIndex = 16;
            this.Label_BorderSize.Text = "枠幅";
            // 
            // Button_Default
            // 
            this.Button_Default.Location = new System.Drawing.Point(288, 153);
            this.Button_Default.Name = "Button_Default";
            this.Button_Default.Size = new System.Drawing.Size(75, 23);
            this.Button_Default.TabIndex = 15;
            this.Button_Default.Text = "デフォルト(&F)";
            this.Button_Default.UseVisualStyleBackColor = true;
            this.Button_Default.Click += new System.EventHandler(this.Default_Click);
            // 
            // Button_Border
            // 
            this.Button_Border.Location = new System.Drawing.Point(321, 26);
            this.Button_Border.Name = "Button_Border";
            this.Button_Border.Size = new System.Drawing.Size(28, 20);
            this.Button_Border.TabIndex = 14;
            this.Button_Border.Text = "...";
            this.Button_Border.UseVisualStyleBackColor = true;
            this.Button_Border.Click += new System.EventHandler(this.Border_Click);
            // 
            // Panel_Border
            // 
            this.Panel_Border.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Border.Location = new System.Drawing.Point(190, 27);
            this.Panel_Border.Name = "Panel_Border";
            this.Panel_Border.Size = new System.Drawing.Size(125, 18);
            this.Panel_Border.TabIndex = 13;
            this.Panel_Border.Click += new System.EventHandler(this.Border_Click);
            // 
            // Label_Border
            // 
            this.Label_Border.AutoSize = true;
            this.Label_Border.Location = new System.Drawing.Point(188, 10);
            this.Label_Border.Name = "Label_Border";
            this.Label_Border.Size = new System.Drawing.Size(29, 12);
            this.Label_Border.TabIndex = 12;
            this.Label_Border.Text = "枠色";
            // 
            // Button_Background
            // 
            this.Button_Background.Location = new System.Drawing.Point(141, 155);
            this.Button_Background.Name = "Button_Background";
            this.Button_Background.Size = new System.Drawing.Size(28, 20);
            this.Button_Background.TabIndex = 11;
            this.Button_Background.Text = "...";
            this.Button_Background.UseVisualStyleBackColor = true;
            this.Button_Background.Click += new System.EventHandler(this.Background_Click);
            // 
            // Panel_Background
            // 
            this.Panel_Background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Background.Location = new System.Drawing.Point(10, 156);
            this.Panel_Background.Name = "Panel_Background";
            this.Panel_Background.Size = new System.Drawing.Size(125, 18);
            this.Panel_Background.TabIndex = 10;
            this.Panel_Background.Click += new System.EventHandler(this.Background_Click);
            // 
            // Label_Background
            // 
            this.Label_Background.AutoSize = true;
            this.Label_Background.Location = new System.Drawing.Point(8, 139);
            this.Label_Background.Name = "Label_Background";
            this.Label_Background.Size = new System.Drawing.Size(41, 12);
            this.Label_Background.TabIndex = 9;
            this.Label_Background.Text = "背景色";
            // 
            // Button_KeyDown
            // 
            this.Button_KeyDown.Location = new System.Drawing.Point(141, 112);
            this.Button_KeyDown.Name = "Button_KeyDown";
            this.Button_KeyDown.Size = new System.Drawing.Size(28, 20);
            this.Button_KeyDown.TabIndex = 8;
            this.Button_KeyDown.Text = "...";
            this.Button_KeyDown.UseVisualStyleBackColor = true;
            this.Button_KeyDown.Click += new System.EventHandler(this.KeyDown_Click);
            // 
            // Panel_KeyDown
            // 
            this.Panel_KeyDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_KeyDown.Location = new System.Drawing.Point(10, 113);
            this.Panel_KeyDown.Name = "Panel_KeyDown";
            this.Panel_KeyDown.Size = new System.Drawing.Size(125, 18);
            this.Panel_KeyDown.TabIndex = 7;
            this.Panel_KeyDown.Click += new System.EventHandler(this.KeyDown_Click);
            // 
            // Label_KeyDown
            // 
            this.Label_KeyDown.AutoSize = true;
            this.Label_KeyDown.Location = new System.Drawing.Point(8, 96);
            this.Label_KeyDown.Name = "Label_KeyDown";
            this.Label_KeyDown.Size = new System.Drawing.Size(61, 12);
            this.Label_KeyDown.TabIndex = 6;
            this.Label_KeyDown.Text = "キー押下色";
            // 
            // Button_Normal
            // 
            this.Button_Normal.Location = new System.Drawing.Point(141, 69);
            this.Button_Normal.Name = "Button_Normal";
            this.Button_Normal.Size = new System.Drawing.Size(28, 20);
            this.Button_Normal.TabIndex = 5;
            this.Button_Normal.Text = "...";
            this.Button_Normal.UseVisualStyleBackColor = true;
            this.Button_Normal.Click += new System.EventHandler(this.Normal_Click);
            // 
            // Panel_Normal
            // 
            this.Panel_Normal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Normal.Location = new System.Drawing.Point(10, 70);
            this.Panel_Normal.Name = "Panel_Normal";
            this.Panel_Normal.Size = new System.Drawing.Size(125, 18);
            this.Panel_Normal.TabIndex = 4;
            this.Panel_Normal.Click += new System.EventHandler(this.Normal_Click);
            // 
            // Label_Normal
            // 
            this.Label_Normal.AutoSize = true;
            this.Label_Normal.Location = new System.Drawing.Point(8, 53);
            this.Label_Normal.Name = "Label_Normal";
            this.Label_Normal.Size = new System.Drawing.Size(41, 12);
            this.Label_Normal.TabIndex = 3;
            this.Label_Normal.Text = "標準色";
            // 
            // Button_Font
            // 
            this.Button_Font.Location = new System.Drawing.Point(141, 26);
            this.Button_Font.Name = "Button_Font";
            this.Button_Font.Size = new System.Drawing.Size(28, 20);
            this.Button_Font.TabIndex = 2;
            this.Button_Font.Text = "...";
            this.Button_Font.UseVisualStyleBackColor = true;
            this.Button_Font.Click += new System.EventHandler(this.Font_Click);
            // 
            // Label_FontView
            // 
            this.Label_FontView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_FontView.Location = new System.Drawing.Point(10, 27);
            this.Label_FontView.Name = "Label_FontView";
            this.Label_FontView.Size = new System.Drawing.Size(125, 18);
            this.Label_FontView.TabIndex = 1;
            this.Label_FontView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_FontView.Click += new System.EventHandler(this.Font_Click);
            // 
            // Label_Font
            // 
            this.Label_Font.AutoSize = true;
            this.Label_Font.Location = new System.Drawing.Point(8, 10);
            this.Label_Font.Name = "Label_Font";
            this.Label_Font.Size = new System.Drawing.Size(38, 12);
            this.Label_Font.TabIndex = 0;
            this.Label_Font.Text = "フォント";
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
            this.Text = "設定 [StackLayout]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingForm_FormClosing);
            this.TabControl_Main.ResumeLayout(false);
            this.TabPage_Replace.ResumeLayout(false);
            this.TabPage_Replace.PerformLayout();
            this.TabPage_Ext.ResumeLayout(false);
            this.TabPage_Ext.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_BorderSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl_Main;
        private System.Windows.Forms.TabPage TabPage_Replace;
        private System.Windows.Forms.Label Label_ReplaceList;
        private System.Windows.Forms.Button Button_Remove;
        private System.Windows.Forms.ListBox ListBox_ReplaceList;
        private System.Windows.Forms.Button Button_Insert;
        private System.Windows.Forms.TextBox TextBox_Replace;
        private System.Windows.Forms.Label Label_To;
        private System.Windows.Forms.ComboBox ComboBox_KeyList;
        private System.Windows.Forms.TabPage TabPage_Ext;
        private System.Windows.Forms.NumericUpDown NumericUpDown_BorderSize;
        private System.Windows.Forms.Label Label_BorderSize;
        private System.Windows.Forms.Button Button_Default;
        private System.Windows.Forms.Button Button_Border;
        private System.Windows.Forms.Panel Panel_Border;
        private System.Windows.Forms.Label Label_Border;
        private System.Windows.Forms.Button Button_Background;
        private System.Windows.Forms.Panel Panel_Background;
        private System.Windows.Forms.Label Label_Background;
        private System.Windows.Forms.Button Button_KeyDown;
        private System.Windows.Forms.Panel Panel_KeyDown;
        private System.Windows.Forms.Label Label_KeyDown;
        private System.Windows.Forms.Button Button_Normal;
        private System.Windows.Forms.Panel Panel_Normal;
        private System.Windows.Forms.Label Label_Normal;
        private System.Windows.Forms.Button Button_Font;
        private System.Windows.Forms.Label Label_FontView;
        private System.Windows.Forms.Label Label_Font;
        private System.Windows.Forms.CheckBox CheckBox_Antialiasing;
        private System.Windows.Forms.Button Button_Downward;
        private System.Windows.Forms.Button Button_Upward;
        private System.Windows.Forms.ComboBox ComboBox_StackDirection;
        private System.Windows.Forms.Label Label_StackDirection;
    }
}