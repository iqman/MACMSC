namespace StorageClient
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearchKeyword = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.labelPickKeysStatus = new System.Windows.Forms.Label();
            this.textBoxPreAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxYourName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxServerAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonPickUserKeys = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvSearchResult = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelUploadData = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonUploadNow = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonUploadKeywordsRemove = new System.Windows.Forms.Button();
            this.listBoxUploadKeywords = new System.Windows.Forms.ListBox();
            this.buttonUploadAddKeyword = new System.Windows.Forms.Button();
            this.textBoxUploadAddKeyword = new System.Windows.Forms.TextBox();
            this.buttonPickDataForUpload = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResult)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search keyword";
            // 
            // textBoxSearchKeyword
            // 
            this.textBoxSearchKeyword.Location = new System.Drawing.Point(98, 25);
            this.textBoxSearchKeyword.Name = "textBoxSearchKeyword";
            this.textBoxSearchKeyword.Size = new System.Drawing.Size(100, 20);
            this.textBoxSearchKeyword.TabIndex = 1;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(204, 23);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(558, 330);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.labelPickKeysStatus);
            this.tabPage4.Controls.Add(this.textBoxPreAddress);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.textBoxYourName);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.textBoxServerAddress);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.buttonPickUserKeys);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(550, 304);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Options";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // labelPickKeysStatus
            // 
            this.labelPickKeysStatus.AutoSize = true;
            this.labelPickKeysStatus.Location = new System.Drawing.Point(224, 155);
            this.labelPickKeysStatus.Name = "labelPickKeysStatus";
            this.labelPickKeysStatus.Size = new System.Drawing.Size(51, 13);
            this.labelPickKeysStatus.TabIndex = 29;
            this.labelPickKeysStatus.Text = "Not done";
            // 
            // textBoxPreAddress
            // 
            this.textBoxPreAddress.Location = new System.Drawing.Point(95, 78);
            this.textBoxPreAddress.Name = "textBoxPreAddress";
            this.textBoxPreAddress.Size = new System.Drawing.Size(262, 20);
            this.textBoxPreAddress.TabIndex = 28;
            this.textBoxPreAddress.Text = "http://macmsc.cloudapp.net/PreService.svc";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Pre address:";
            // 
            // textBoxYourName
            // 
            this.textBoxYourName.Location = new System.Drawing.Point(95, 35);
            this.textBoxYourName.Name = "textBoxYourName";
            this.textBoxYourName.Size = new System.Drawing.Size(262, 20);
            this.textBoxYourName.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Your name:";
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Location = new System.Drawing.Point(95, 6);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(262, 20);
            this.textBoxServerAddress.TabIndex = 7;
            this.textBoxServerAddress.Text = "http://macmsc.cloudapp.net/StorageService.svc";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Server address:";
            // 
            // buttonPickUserKeys
            // 
            this.buttonPickUserKeys.Location = new System.Drawing.Point(34, 134);
            this.buttonPickUserKeys.Name = "buttonPickUserKeys";
            this.buttonPickUserKeys.Size = new System.Drawing.Size(155, 55);
            this.buttonPickUserKeys.TabIndex = 4;
            this.buttonPickUserKeys.Text = "Pick user keys";
            this.buttonPickUserKeys.UseVisualStyleBackColor = true;
            this.buttonPickUserKeys.Click += new System.EventHandler(this.ButtonPickUserKeysClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.dgvSearchResult);
            this.tabPage1.Controls.Add(this.buttonSearch);
            this.tabPage1.Controls.Add(this.textBoxSearchKeyword);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(550, 304);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Download or delete data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Files";
            // 
            // dgvSearchResult
            // 
            this.dgvSearchResult.AllowUserToAddRows = false;
            this.dgvSearchResult.AllowUserToDeleteRows = false;
            this.dgvSearchResult.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvSearchResult.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSearchResult.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSearchResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSearchResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSearchResult.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSearchResult.GridColor = System.Drawing.Color.LightGray;
            this.dgvSearchResult.Location = new System.Drawing.Point(11, 74);
            this.dgvSearchResult.Name = "dgvSearchResult";
            this.dgvSearchResult.ReadOnly = true;
            this.dgvSearchResult.RowHeadersVisible = false;
            this.dgvSearchResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchResult.Size = new System.Drawing.Size(533, 222);
            this.dgvSearchResult.TabIndex = 3;
            this.dgvSearchResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchResult_CellContentClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelUploadData);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.buttonUploadNow);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.buttonPickDataForUpload);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(550, 304);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Upload data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelUploadData
            // 
            this.labelUploadData.AutoSize = true;
            this.labelUploadData.Location = new System.Drawing.Point(50, 42);
            this.labelUploadData.Name = "labelUploadData";
            this.labelUploadData.Size = new System.Drawing.Size(35, 13);
            this.labelUploadData.TabIndex = 8;
            this.labelUploadData.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Data:";
            // 
            // buttonUploadNow
            // 
            this.buttonUploadNow.Location = new System.Drawing.Point(291, 61);
            this.buttonUploadNow.Name = "buttonUploadNow";
            this.buttonUploadNow.Size = new System.Drawing.Size(110, 51);
            this.buttonUploadNow.TabIndex = 6;
            this.buttonUploadNow.Text = "Upload now";
            this.buttonUploadNow.UseVisualStyleBackColor = true;
            this.buttonUploadNow.Click += new System.EventHandler(this.buttonUploadNow_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonUploadKeywordsRemove);
            this.groupBox1.Controls.Add(this.listBoxUploadKeywords);
            this.groupBox1.Controls.Add(this.buttonUploadAddKeyword);
            this.groupBox1.Controls.Add(this.textBoxUploadAddKeyword);
            this.groupBox1.Location = new System.Drawing.Point(8, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 150);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keywords";
            // 
            // buttonUploadKeywordsRemove
            // 
            this.buttonUploadKeywordsRemove.Location = new System.Drawing.Point(196, 60);
            this.buttonUploadKeywordsRemove.Name = "buttonUploadKeywordsRemove";
            this.buttonUploadKeywordsRemove.Size = new System.Drawing.Size(75, 37);
            this.buttonUploadKeywordsRemove.TabIndex = 7;
            this.buttonUploadKeywordsRemove.Text = "Remove keyword";
            this.buttonUploadKeywordsRemove.UseVisualStyleBackColor = true;
            this.buttonUploadKeywordsRemove.Click += new System.EventHandler(this.buttonUploadKeywordsRemove_Click);
            // 
            // listBoxUploadKeywords
            // 
            this.listBoxUploadKeywords.FormattingEnabled = true;
            this.listBoxUploadKeywords.Location = new System.Drawing.Point(6, 45);
            this.listBoxUploadKeywords.Name = "listBoxUploadKeywords";
            this.listBoxUploadKeywords.Size = new System.Drawing.Size(181, 95);
            this.listBoxUploadKeywords.TabIndex = 6;
            // 
            // buttonUploadAddKeyword
            // 
            this.buttonUploadAddKeyword.Location = new System.Drawing.Point(193, 17);
            this.buttonUploadAddKeyword.Name = "buttonUploadAddKeyword";
            this.buttonUploadAddKeyword.Size = new System.Drawing.Size(75, 37);
            this.buttonUploadAddKeyword.TabIndex = 5;
            this.buttonUploadAddKeyword.Text = "Add keyword";
            this.buttonUploadAddKeyword.UseVisualStyleBackColor = true;
            this.buttonUploadAddKeyword.Click += new System.EventHandler(this.buttonUploadAddKeyword_Click);
            // 
            // textBoxUploadAddKeyword
            // 
            this.textBoxUploadAddKeyword.Location = new System.Drawing.Point(6, 19);
            this.textBoxUploadAddKeyword.Name = "textBoxUploadAddKeyword";
            this.textBoxUploadAddKeyword.Size = new System.Drawing.Size(181, 20);
            this.textBoxUploadAddKeyword.TabIndex = 4;
            // 
            // buttonPickDataForUpload
            // 
            this.buttonPickDataForUpload.Location = new System.Drawing.Point(8, 16);
            this.buttonPickDataForUpload.Name = "buttonPickDataForUpload";
            this.buttonPickDataForUpload.Size = new System.Drawing.Size(111, 23);
            this.buttonPickDataForUpload.TabIndex = 3;
            this.buttonPickDataForUpload.Text = "Pick data for upload";
            this.buttonPickDataForUpload.UseVisualStyleBackColor = true;
            this.buttonPickDataForUpload.Click += new System.EventHandler(this.buttonPickDataForUpload_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 330);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Cloud storage prototype client";
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResult)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxSearchKeyword;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button buttonPickDataForUpload;
        private System.Windows.Forms.Button buttonUploadNow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxUploadKeywords;
        private System.Windows.Forms.Button buttonUploadAddKeyword;
        private System.Windows.Forms.TextBox textBoxUploadAddKeyword;
        private System.Windows.Forms.Label labelUploadData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonPickUserKeys;
        private System.Windows.Forms.TextBox textBoxServerAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxYourName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvSearchResult;
        private System.Windows.Forms.Button buttonUploadKeywordsRemove;
        private System.Windows.Forms.TextBox textBoxPreAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelPickKeysStatus;
    }
}

