namespace StorageAdmin
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonGetServiceStartTime = new System.Windows.Forms.Button();
            this.buttonKillPreService = new System.Windows.Forms.Button();
            this.textBoxPreAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxYourUniqueId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonGenerateMasterKeypair = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.labelAddUserGeneratedId = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonPickMasterKeys = new System.Windows.Forms.Button();
            this.buttonGenerateUserKeys = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxNewUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxTestName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelTestId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.buttonRevokeUser = new System.Windows.Forms.Button();
            this.buttonRefreshUsers = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(404, 225);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.textBoxPreAddress);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.textBoxYourUniqueId);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.textBoxServerAddress);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(396, 199);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "General Options";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonGetServiceStartTime);
            this.groupBox1.Controls.Add(this.buttonKillPreService);
            this.groupBox1.Location = new System.Drawing.Point(11, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 82);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test";
            // 
            // buttonGetServiceStartTime
            // 
            this.buttonGetServiceStartTime.Location = new System.Drawing.Point(16, 19);
            this.buttonGetServiceStartTime.Name = "buttonGetServiceStartTime";
            this.buttonGetServiceStartTime.Size = new System.Drawing.Size(114, 40);
            this.buttonGetServiceStartTime.TabIndex = 16;
            this.buttonGetServiceStartTime.Text = "Get service start time";
            this.buttonGetServiceStartTime.UseVisualStyleBackColor = true;
            this.buttonGetServiceStartTime.Click += new System.EventHandler(this.buttonGetServiceStartTime_Click);
            // 
            // buttonKillPreService
            // 
            this.buttonKillPreService.Location = new System.Drawing.Point(136, 19);
            this.buttonKillPreService.Name = "buttonKillPreService";
            this.buttonKillPreService.Size = new System.Drawing.Size(114, 40);
            this.buttonKillPreService.TabIndex = 17;
            this.buttonKillPreService.Text = "Kill Pre service";
            this.buttonKillPreService.UseVisualStyleBackColor = true;
            this.buttonKillPreService.Click += new System.EventHandler(this.buttonKillPreService_Click);
            // 
            // textBoxPreAddress
            // 
            this.textBoxPreAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPreAddress.Location = new System.Drawing.Point(95, 61);
            this.textBoxPreAddress.Name = "textBoxPreAddress";
            this.textBoxPreAddress.Size = new System.Drawing.Size(293, 20);
            this.textBoxPreAddress.TabIndex = 15;
            this.textBoxPreAddress.Text = "http://macmsc.cloudapp.net/PreService.svc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Pre address:";
            // 
            // textBoxYourUniqueId
            // 
            this.textBoxYourUniqueId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYourUniqueId.Location = new System.Drawing.Point(95, 9);
            this.textBoxYourUniqueId.Name = "textBoxYourUniqueId";
            this.textBoxYourUniqueId.ReadOnly = true;
            this.textBoxYourUniqueId.Size = new System.Drawing.Size(293, 20);
            this.textBoxYourUniqueId.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Your unique id:";
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServerAddress.Location = new System.Drawing.Point(95, 35);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(293, 20);
            this.textBoxServerAddress.TabIndex = 7;
            this.textBoxServerAddress.Text = "http://macmsc.cloudapp.net/StorageService.svc";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Server address:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonGenerateMasterKeypair);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(396, 199);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "One-time setup system";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonGenerateMasterKeypair
            // 
            this.buttonGenerateMasterKeypair.Location = new System.Drawing.Point(111, 80);
            this.buttonGenerateMasterKeypair.Name = "buttonGenerateMasterKeypair";
            this.buttonGenerateMasterKeypair.Size = new System.Drawing.Size(138, 64);
            this.buttonGenerateMasterKeypair.TabIndex = 9;
            this.buttonGenerateMasterKeypair.Text = "Reset system, become DO, Generate and save master keypair";
            this.buttonGenerateMasterKeypair.UseVisualStyleBackColor = true;
            this.buttonGenerateMasterKeypair.Click += new System.EventHandler(this.buttonGenerateAndSaveMasterKeypair_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.labelAddUserGeneratedId);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.buttonPickMasterKeys);
            this.tabPage2.Controls.Add(this.buttonGenerateUserKeys);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.textBoxNewUserId);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(396, 199);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Add user";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(62, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Id:";
            // 
            // labelAddUserGeneratedId
            // 
            this.labelAddUserGeneratedId.AutoSize = true;
            this.labelAddUserGeneratedId.Location = new System.Drawing.Point(87, 113);
            this.labelAddUserGeneratedId.Name = "labelAddUserGeneratedId";
            this.labelAddUserGeneratedId.Size = new System.Drawing.Size(0, 13);
            this.labelAddUserGeneratedId.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "1.";
            // 
            // buttonPickMasterKeys
            // 
            this.buttonPickMasterKeys.Location = new System.Drawing.Point(65, 28);
            this.buttonPickMasterKeys.Name = "buttonPickMasterKeys";
            this.buttonPickMasterKeys.Size = new System.Drawing.Size(123, 43);
            this.buttonPickMasterKeys.TabIndex = 15;
            this.buttonPickMasterKeys.Text = "Pick Master keys";
            this.buttonPickMasterKeys.UseVisualStyleBackColor = true;
            this.buttonPickMasterKeys.Click += new System.EventHandler(this.buttonPickMasterKeys_Click);
            // 
            // buttonGenerateUserKeys
            // 
            this.buttonGenerateUserKeys.Location = new System.Drawing.Point(65, 129);
            this.buttonGenerateUserKeys.Name = "buttonGenerateUserKeys";
            this.buttonGenerateUserKeys.Size = new System.Drawing.Size(123, 43);
            this.buttonGenerateUserKeys.TabIndex = 13;
            this.buttonGenerateUserKeys.Text = "Generate user keys";
            this.buttonGenerateUserKeys.UseVisualStyleBackColor = true;
            this.buttonGenerateUserKeys.Click += new System.EventHandler(this.buttonGenerateKeypairsForUser_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "3.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(62, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Enter username";
            // 
            // textBoxNewUserId
            // 
            this.textBoxNewUserId.Location = new System.Drawing.Point(65, 90);
            this.textBoxNewUserId.Name = "textBoxNewUserId";
            this.textBoxNewUserId.Size = new System.Drawing.Size(123, 20);
            this.textBoxNewUserId.TabIndex = 10;
            this.textBoxNewUserId.TextChanged += new System.EventHandler(this.textBoxNewUserId_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "2.";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.listBoxUsers);
            this.tabPage3.Controls.Add(this.buttonRevokeUser);
            this.tabPage3.Controls.Add(this.buttonRefreshUsers);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(396, 199);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Revoke user";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxTestName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.labelTestId);
            this.groupBox2.Location = new System.Drawing.Point(12, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 66);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generate test id";
            // 
            // textBoxTestName
            // 
            this.textBoxTestName.Location = new System.Drawing.Point(6, 19);
            this.textBoxTestName.Name = "textBoxTestName";
            this.textBoxTestName.Size = new System.Drawing.Size(290, 20);
            this.textBoxTestName.TabIndex = 5;
            this.textBoxTestName.TextChanged += new System.EventHandler(this.textBoxTestName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Id:";
            // 
            // labelTestId
            // 
            this.labelTestId.AutoSize = true;
            this.labelTestId.Location = new System.Drawing.Point(28, 43);
            this.labelTestId.Name = "labelTestId";
            this.labelTestId.Size = new System.Drawing.Size(0, 13);
            this.labelTestId.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Existing users";
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(11, 19);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(216, 95);
            this.listBoxUsers.TabIndex = 2;
            // 
            // buttonRevokeUser
            // 
            this.buttonRevokeUser.Location = new System.Drawing.Point(233, 48);
            this.buttonRevokeUser.Name = "buttonRevokeUser";
            this.buttonRevokeUser.Size = new System.Drawing.Size(75, 23);
            this.buttonRevokeUser.TabIndex = 1;
            this.buttonRevokeUser.Text = "Revoke";
            this.buttonRevokeUser.UseVisualStyleBackColor = true;
            this.buttonRevokeUser.Click += new System.EventHandler(this.buttonRevokeUser_Click);
            // 
            // buttonRefreshUsers
            // 
            this.buttonRefreshUsers.Location = new System.Drawing.Point(233, 19);
            this.buttonRefreshUsers.Name = "buttonRefreshUsers";
            this.buttonRefreshUsers.Size = new System.Drawing.Size(75, 23);
            this.buttonRefreshUsers.TabIndex = 0;
            this.buttonRefreshUsers.Text = "Refresh";
            this.buttonRefreshUsers.UseVisualStyleBackColor = true;
            this.buttonRefreshUsers.Click += new System.EventHandler(this.buttonRefreshUsers_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 225);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Cloud Storage Administration";
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox textBoxYourUniqueId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxNewUserId;
        private System.Windows.Forms.Button buttonGenerateUserKeys;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonGenerateMasterKeypair;
        private System.Windows.Forms.TextBox textBoxPreAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPickMasterKeys;
        private System.Windows.Forms.Button buttonGetServiceStartTime;
        private System.Windows.Forms.Button buttonKillPreService;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Button buttonRevokeUser;
        private System.Windows.Forms.Button buttonRefreshUsers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelTestId;
        private System.Windows.Forms.TextBox textBoxTestName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelAddUserGeneratedId;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}

