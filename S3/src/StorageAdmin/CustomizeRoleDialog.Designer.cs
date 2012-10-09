namespace StorageAdmin
{
    partial class CustomizeRoleDialog
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
            this.buttonCreateSubRole = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxCanManageSubroles = new System.Windows.Forms.CheckBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxCanCreateRootRoles = new System.Windows.Forms.CheckBox();
            this.checkBoxCanCreateUsers = new System.Windows.Forms.CheckBox();
            this.radioButtonPermissionReadonly = new System.Windows.Forms.RadioButton();
            this.radioButtonPermissionModify = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.checkBoxMakeThisRootRole = new System.Windows.Forms.CheckBox();
            this.checkBoxCanAssignUnassignRole = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDataEntities = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataEntities)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreateSubRole
            // 
            this.buttonCreateSubRole.Location = new System.Drawing.Point(12, 241);
            this.buttonCreateSubRole.Name = "buttonCreateSubRole";
            this.buttonCreateSubRole.Size = new System.Drawing.Size(86, 38);
            this.buttonCreateSubRole.TabIndex = 0;
            this.buttonCreateSubRole.Text = "Create sub-role";
            this.buttonCreateSubRole.UseVisualStyleBackColor = true;
            this.buttonCreateSubRole.Click += new System.EventHandler(this.buttonCreateSubRole_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(110, 241);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 38);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxCanManageSubroles
            // 
            this.checkBoxCanManageSubroles.AutoSize = true;
            this.checkBoxCanManageSubroles.Location = new System.Drawing.Point(15, 51);
            this.checkBoxCanManageSubroles.Name = "checkBoxCanManageSubroles";
            this.checkBoxCanManageSubroles.Size = new System.Drawing.Size(131, 17);
            this.checkBoxCanManageSubroles.TabIndex = 2;
            this.checkBoxCanManageSubroles.Text = "Can manage sub-roles";
            this.checkBoxCanManageSubroles.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(53, 25);
            this.textBoxName.MaxLength = 32;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(143, 20);
            this.textBoxName.TabIndex = 3;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name";
            // 
            // checkBoxCanCreateRootRoles
            // 
            this.checkBoxCanCreateRootRoles.AutoSize = true;
            this.checkBoxCanCreateRootRoles.Location = new System.Drawing.Point(15, 74);
            this.checkBoxCanCreateRootRoles.Name = "checkBoxCanCreateRootRoles";
            this.checkBoxCanCreateRootRoles.Size = new System.Drawing.Size(124, 17);
            this.checkBoxCanCreateRootRoles.TabIndex = 5;
            this.checkBoxCanCreateRootRoles.Text = "Can create root-roles";
            this.checkBoxCanCreateRootRoles.UseVisualStyleBackColor = true;
            // 
            // checkBoxCanCreateUsers
            // 
            this.checkBoxCanCreateUsers.AutoSize = true;
            this.checkBoxCanCreateUsers.Location = new System.Drawing.Point(15, 97);
            this.checkBoxCanCreateUsers.Name = "checkBoxCanCreateUsers";
            this.checkBoxCanCreateUsers.Size = new System.Drawing.Size(106, 17);
            this.checkBoxCanCreateUsers.TabIndex = 6;
            this.checkBoxCanCreateUsers.Text = "Can create users";
            this.checkBoxCanCreateUsers.UseVisualStyleBackColor = true;
            // 
            // radioButtonPermissionReadonly
            // 
            this.radioButtonPermissionReadonly.AutoSize = true;
            this.radioButtonPermissionReadonly.Checked = true;
            this.radioButtonPermissionReadonly.Location = new System.Drawing.Point(6, 16);
            this.radioButtonPermissionReadonly.Name = "radioButtonPermissionReadonly";
            this.radioButtonPermissionReadonly.Size = new System.Drawing.Size(73, 17);
            this.radioButtonPermissionReadonly.TabIndex = 10;
            this.radioButtonPermissionReadonly.TabStop = true;
            this.radioButtonPermissionReadonly.Text = "Read-only";
            this.radioButtonPermissionReadonly.UseVisualStyleBackColor = true;
            // 
            // radioButtonPermissionModify
            // 
            this.radioButtonPermissionModify.AutoSize = true;
            this.radioButtonPermissionModify.Location = new System.Drawing.Point(6, 39);
            this.radioButtonPermissionModify.Name = "radioButtonPermissionModify";
            this.radioButtonPermissionModify.Size = new System.Drawing.Size(56, 17);
            this.radioButtonPermissionModify.TabIndex = 11;
            this.radioButtonPermissionModify.TabStop = true;
            this.radioButtonPermissionModify.Text = "Modify";
            this.radioButtonPermissionModify.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonPermissionReadonly);
            this.groupBox1.Controls.Add(this.radioButtonPermissionModify);
            this.groupBox1.Location = new System.Drawing.Point(15, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 64);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data entity permission";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(35, 13);
            this.labelTitle.TabIndex = 13;
            this.labelTitle.Text = "label2";
            // 
            // checkBoxMakeThisRootRole
            // 
            this.checkBoxMakeThisRootRole.AutoSize = true;
            this.checkBoxMakeThisRootRole.Location = new System.Drawing.Point(15, 218);
            this.checkBoxMakeThisRootRole.Name = "checkBoxMakeThisRootRole";
            this.checkBoxMakeThisRootRole.Size = new System.Drawing.Size(142, 17);
            this.checkBoxMakeThisRootRole.TabIndex = 14;
            this.checkBoxMakeThisRootRole.Text = "Make this role a root role";
            this.checkBoxMakeThisRootRole.UseVisualStyleBackColor = true;
            // 
            // checkBoxCanAssignUnassignRole
            // 
            this.checkBoxCanAssignUnassignRole.AutoSize = true;
            this.checkBoxCanAssignUnassignRole.Location = new System.Drawing.Point(15, 120);
            this.checkBoxCanAssignUnassignRole.Name = "checkBoxCanAssignUnassignRole";
            this.checkBoxCanAssignUnassignRole.Size = new System.Drawing.Size(164, 17);
            this.checkBoxCanAssignUnassignRole.TabIndex = 15;
            this.checkBoxCanAssignUnassignRole.Text = "Can be assigned/unassigned";
            this.checkBoxCanAssignUnassignRole.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Data entities from parent role to include in child role";
            // 
            // dgvDataEntities
            // 
            this.dgvDataEntities.AllowUserToAddRows = false;
            this.dgvDataEntities.AllowUserToDeleteRows = false;
            this.dgvDataEntities.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDataEntities.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDataEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDataEntities.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDataEntities.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDataEntities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDataEntities.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDataEntities.GridColor = System.Drawing.Color.LightGray;
            this.dgvDataEntities.Location = new System.Drawing.Point(202, 41);
            this.dgvDataEntities.MultiSelect = false;
            this.dgvDataEntities.Name = "dgvDataEntities";
            this.dgvDataEntities.RowHeadersVisible = false;
            this.dgvDataEntities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDataEntities.Size = new System.Drawing.Size(288, 238);
            this.dgvDataEntities.TabIndex = 17;
            // 
            // CustomizeRoleDialog
            // 
            this.AcceptButton = this.buttonCreateSubRole;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(493, 291);
            this.Controls.Add(this.dgvDataEntities);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxCanAssignUnassignRole);
            this.Controls.Add(this.checkBoxMakeThisRootRole);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxCanCreateUsers);
            this.Controls.Add(this.checkBoxCanCreateRootRoles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.checkBoxCanManageSubroles);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonCreateSubRole);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomizeRoleDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create new sub-role";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataEntities)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateSubRole;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxCanManageSubroles;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxCanCreateRootRoles;
        private System.Windows.Forms.CheckBox checkBoxCanCreateUsers;
        private System.Windows.Forms.RadioButton radioButtonPermissionReadonly;
        private System.Windows.Forms.RadioButton radioButtonPermissionModify;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.CheckBox checkBoxMakeThisRootRole;
        private System.Windows.Forms.CheckBox checkBoxCanAssignUnassignRole;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDataEntities;
    }
}