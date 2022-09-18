
namespace Nazam_pos
{
    partial class frmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePassword));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConfirm = new MetroFramework.Controls.MetroTextBox();
            this.txtNew = new MetroFramework.Controls.MetroTextBox();
            this.txtOld = new MetroFramework.Controls.MetroTextBox();
            this.btnlogin = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 40);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(347, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(606, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 28);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Change Password";
            // 
            // txtConfirm
            // 
            // 
            // 
            // 
            this.txtConfirm.CustomButton.Image = null;
            this.txtConfirm.CustomButton.Location = new System.Drawing.Point(307, 2);
            this.txtConfirm.CustomButton.Name = "";
            this.txtConfirm.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtConfirm.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtConfirm.CustomButton.TabIndex = 1;
            this.txtConfirm.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtConfirm.CustomButton.UseSelectable = true;
            this.txtConfirm.CustomButton.Visible = false;
            this.txtConfirm.DisplayIcon = true;
            this.txtConfirm.Icon = ((System.Drawing.Image)(resources.GetObject("txtConfirm.Icon")));
            this.txtConfirm.Lines = new string[0];
            this.txtConfirm.Location = new System.Drawing.Point(25, 152);
            this.txtConfirm.MaxLength = 32767;
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '●';
            this.txtConfirm.PromptText = "Confirm New Password";
            this.txtConfirm.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtConfirm.SelectedText = "";
            this.txtConfirm.SelectionLength = 0;
            this.txtConfirm.SelectionStart = 0;
            this.txtConfirm.ShortcutsEnabled = true;
            this.txtConfirm.Size = new System.Drawing.Size(331, 26);
            this.txtConfirm.TabIndex = 4;
            this.txtConfirm.UseSelectable = true;
            this.txtConfirm.UseSystemPasswordChar = true;
            this.txtConfirm.WaterMark = "Confirm New Password";
            this.txtConfirm.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtConfirm.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtNew
            // 
            // 
            // 
            // 
            this.txtNew.CustomButton.Image = null;
            this.txtNew.CustomButton.Location = new System.Drawing.Point(307, 2);
            this.txtNew.CustomButton.Name = "";
            this.txtNew.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtNew.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNew.CustomButton.TabIndex = 1;
            this.txtNew.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNew.CustomButton.UseSelectable = true;
            this.txtNew.CustomButton.Visible = false;
            this.txtNew.DisplayIcon = true;
            this.txtNew.Icon = ((System.Drawing.Image)(resources.GetObject("txtNew.Icon")));
            this.txtNew.Lines = new string[0];
            this.txtNew.Location = new System.Drawing.Point(25, 111);
            this.txtNew.MaxLength = 32767;
            this.txtNew.Name = "txtNew";
            this.txtNew.PasswordChar = '●';
            this.txtNew.PromptText = "New Password";
            this.txtNew.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNew.SelectedText = "";
            this.txtNew.SelectionLength = 0;
            this.txtNew.SelectionStart = 0;
            this.txtNew.ShortcutsEnabled = true;
            this.txtNew.Size = new System.Drawing.Size(331, 26);
            this.txtNew.TabIndex = 5;
            this.txtNew.UseSelectable = true;
            this.txtNew.UseSystemPasswordChar = true;
            this.txtNew.WaterMark = "New Password";
            this.txtNew.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNew.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtOld
            // 
            // 
            // 
            // 
            this.txtOld.CustomButton.Image = null;
            this.txtOld.CustomButton.Location = new System.Drawing.Point(307, 2);
            this.txtOld.CustomButton.Name = "";
            this.txtOld.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtOld.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtOld.CustomButton.TabIndex = 1;
            this.txtOld.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtOld.CustomButton.UseSelectable = true;
            this.txtOld.CustomButton.Visible = false;
            this.txtOld.DisplayIcon = true;
            this.txtOld.Icon = ((System.Drawing.Image)(resources.GetObject("txtOld.Icon")));
            this.txtOld.Lines = new string[0];
            this.txtOld.Location = new System.Drawing.Point(25, 69);
            this.txtOld.MaxLength = 32767;
            this.txtOld.Name = "txtOld";
            this.txtOld.PasswordChar = '●';
            this.txtOld.PromptText = "Old Password";
            this.txtOld.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtOld.SelectedText = "";
            this.txtOld.SelectionLength = 0;
            this.txtOld.SelectionStart = 0;
            this.txtOld.ShortcutsEnabled = true;
            this.txtOld.Size = new System.Drawing.Size(331, 26);
            this.txtOld.TabIndex = 6;
            this.txtOld.UseSelectable = true;
            this.txtOld.UseSystemPasswordChar = true;
            this.txtOld.WaterMark = "Old Password";
            this.txtOld.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtOld.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnlogin
            // 
            this.btnlogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(18)))));
            this.btnlogin.FlatAppearance.BorderSize = 0;
            this.btnlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogin.ForeColor = System.Drawing.Color.White;
            this.btnlogin.Location = new System.Drawing.Point(25, 191);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(331, 33);
            this.btnlogin.TabIndex = 7;
            this.btnlogin.Text = "Save ";
            this.btnlogin.UseVisualStyleBackColor = false;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 244);
            this.ControlBox = false;
            this.Controls.Add(this.btnlogin);
            this.Controls.Add(this.txtOld);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox txtConfirm;
        private MetroFramework.Controls.MetroTextBox txtNew;
        private MetroFramework.Controls.MetroTextBox txtOld;
        public System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}