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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.textBoxYourName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.textBoxDORoleName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxDOUsername = new System.Windows.Forms.TextBox();
            this.buttonGenerateMasterKeypair = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonRemoveUserFromRole = new System.Windows.Forms.Button();
            this.buttonUpdateSubRole = new System.Windows.Forms.Button();
            this.buttonDeleteRole = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelDetailsCanBeAssigned = new System.Windows.Forms.Label();
            this.labelDetailsCanBeAssignedTitle = new System.Windows.Forms.Label();
            this.labelDetailIsRootRole = new System.Windows.Forms.Label();
            this.labelDetailIsRootRoleTitle = new System.Windows.Forms.Label();
            this.labelDetailPermission = new System.Windows.Forms.Label();
            this.labelDetailCreateUsers = new System.Windows.Forms.Label();
            this.labelDetailId = new System.Windows.Forms.Label();
            this.labelDetailName = new System.Windows.Forms.Label();
            this.labelDetailRootRoles = new System.Windows.Forms.Label();
            this.labelDetailSubRoles = new System.Windows.Forms.Label();
            this.labelDetailRootRolesTitle = new System.Windows.Forms.Label();
            this.labelDetailPermissionTitle = new System.Windows.Forms.Label();
            this.labelDetailCreateUsersTitle = new System.Windows.Forms.Label();
            this.labelDetailSubRolesTitle = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.treeViewRoles = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonCreateSubRole = new System.Windows.Forms.Button();
            this.labelKeyStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonPickKeys = new System.Windows.Forms.Button();
            this.buttonRefreshUserTree = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonAddUserToRole = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxNewUserName = new System.Windows.Forms.TextBox();
            this.labelAddUserGeneratedId = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonGenerateUserKeys = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(606, 347);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.textBoxYourName);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.textBoxPreAddress);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.textBoxYourUniqueId);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.textBoxServerAddress);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(598, 321);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "General Options";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // textBoxYourName
            // 
            this.textBoxYourName.Location = new System.Drawing.Point(95, 19);
            this.textBoxYourName.Name = "textBoxYourName";
            this.textBoxYourName.Size = new System.Drawing.Size(293, 20);
            this.textBoxYourName.TabIndex = 20;
            this.textBoxYourName.TextChanged += new System.EventHandler(this.textBoxYourName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Your name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonGetServiceStartTime);
            this.groupBox1.Controls.Add(this.buttonKillPreService);
            this.groupBox1.Location = new System.Drawing.Point(11, 143);
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
            this.textBoxPreAddress.Location = new System.Drawing.Point(95, 97);
            this.textBoxPreAddress.Name = "textBoxPreAddress";
            this.textBoxPreAddress.Size = new System.Drawing.Size(293, 20);
            this.textBoxPreAddress.TabIndex = 15;
            this.textBoxPreAddress.Text = "http://macmsc.cloudapp.net/PreService.svc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Pre address:";
            // 
            // textBoxYourUniqueId
            // 
            this.textBoxYourUniqueId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYourUniqueId.Location = new System.Drawing.Point(95, 45);
            this.textBoxYourUniqueId.Name = "textBoxYourUniqueId";
            this.textBoxYourUniqueId.ReadOnly = true;
            this.textBoxYourUniqueId.Size = new System.Drawing.Size(293, 20);
            this.textBoxYourUniqueId.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Your unique id:";
            // 
            // textBoxServerAddress
            // 
            this.textBoxServerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServerAddress.Location = new System.Drawing.Point(95, 71);
            this.textBoxServerAddress.Name = "textBoxServerAddress";
            this.textBoxServerAddress.Size = new System.Drawing.Size(293, 20);
            this.textBoxServerAddress.TabIndex = 7;
            this.textBoxServerAddress.Text = "http://macmsc.cloudapp.net/StorageService.svc";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Server address:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxDORoleName);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.textBoxDOUsername);
            this.tabPage1.Controls.Add(this.buttonGenerateMasterKeypair);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(598, 321);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "System initialization";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxDORoleName
            // 
            this.textBoxDORoleName.Location = new System.Drawing.Point(100, 66);
            this.textBoxDORoleName.MaxLength = 32;
            this.textBoxDORoleName.Name = "textBoxDORoleName";
            this.textBoxDORoleName.Size = new System.Drawing.Size(100, 20);
            this.textBoxDORoleName.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "DO Role name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "DO Username";
            // 
            // textBoxDOUsername
            // 
            this.textBoxDOUsername.Location = new System.Drawing.Point(100, 36);
            this.textBoxDOUsername.MaxLength = 32;
            this.textBoxDOUsername.Name = "textBoxDOUsername";
            this.textBoxDOUsername.Size = new System.Drawing.Size(100, 20);
            this.textBoxDOUsername.TabIndex = 0;
            // 
            // buttonGenerateMasterKeypair
            // 
            this.buttonGenerateMasterKeypair.Location = new System.Drawing.Point(62, 92);
            this.buttonGenerateMasterKeypair.Name = "buttonGenerateMasterKeypair";
            this.buttonGenerateMasterKeypair.Size = new System.Drawing.Size(138, 64);
            this.buttonGenerateMasterKeypair.TabIndex = 2;
            this.buttonGenerateMasterKeypair.Text = "Reset system, become DO, Generate and save DO/master keys";
            this.buttonGenerateMasterKeypair.UseVisualStyleBackColor = true;
            this.buttonGenerateMasterKeypair.Click += new System.EventHandler(this.buttonGenerateAndSaveMasterKeypair_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonRemoveUserFromRole);
            this.tabPage2.Controls.Add(this.buttonUpdateSubRole);
            this.tabPage2.Controls.Add(this.buttonDeleteRole);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.buttonCreateSubRole);
            this.tabPage2.Controls.Add(this.labelKeyStatus);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.buttonPickKeys);
            this.tabPage2.Controls.Add(this.buttonRefreshUserTree);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(598, 321);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Role and user management";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveUserFromRole
            // 
            this.buttonRemoveUserFromRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveUserFromRole.Location = new System.Drawing.Point(254, 273);
            this.buttonRemoveUserFromRole.Name = "buttonRemoveUserFromRole";
            this.buttonRemoveUserFromRole.Size = new System.Drawing.Size(90, 42);
            this.buttonRemoveUserFromRole.TabIndex = 28;
            this.buttonRemoveUserFromRole.Text = "Remove user from role";
            this.buttonRemoveUserFromRole.UseVisualStyleBackColor = true;
            this.buttonRemoveUserFromRole.Click += new System.EventHandler(this.buttonRemoveUserFromRole_Click);
            // 
            // buttonUpdateSubRole
            // 
            this.buttonUpdateSubRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUpdateSubRole.Location = new System.Drawing.Point(92, 273);
            this.buttonUpdateSubRole.Name = "buttonUpdateSubRole";
            this.buttonUpdateSubRole.Size = new System.Drawing.Size(75, 42);
            this.buttonUpdateSubRole.TabIndex = 27;
            this.buttonUpdateSubRole.Text = "Update sub-role";
            this.buttonUpdateSubRole.UseVisualStyleBackColor = true;
            this.buttonUpdateSubRole.Click += new System.EventHandler(this.buttonUpdateSubRole_Click);
            // 
            // buttonDeleteRole
            // 
            this.buttonDeleteRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteRole.Location = new System.Drawing.Point(173, 273);
            this.buttonDeleteRole.Name = "buttonDeleteRole";
            this.buttonDeleteRole.Size = new System.Drawing.Size(75, 42);
            this.buttonDeleteRole.TabIndex = 23;
            this.buttonDeleteRole.Text = "Delete role";
            this.buttonDeleteRole.UseVisualStyleBackColor = true;
            this.buttonDeleteRole.Click += new System.EventHandler(this.buttonDeleteRole_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.labelDetailsCanBeAssigned);
            this.groupBox4.Controls.Add(this.labelDetailsCanBeAssignedTitle);
            this.groupBox4.Controls.Add(this.labelDetailIsRootRole);
            this.groupBox4.Controls.Add(this.labelDetailIsRootRoleTitle);
            this.groupBox4.Controls.Add(this.labelDetailPermission);
            this.groupBox4.Controls.Add(this.labelDetailCreateUsers);
            this.groupBox4.Controls.Add(this.labelDetailId);
            this.groupBox4.Controls.Add(this.labelDetailName);
            this.groupBox4.Controls.Add(this.labelDetailRootRoles);
            this.groupBox4.Controls.Add(this.labelDetailSubRoles);
            this.groupBox4.Controls.Add(this.labelDetailRootRolesTitle);
            this.groupBox4.Controls.Add(this.labelDetailPermissionTitle);
            this.groupBox4.Controls.Add(this.labelDetailCreateUsersTitle);
            this.groupBox4.Controls.Add(this.labelDetailSubRolesTitle);
            this.groupBox4.Controls.Add(this.label51);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(353, 37);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(237, 190);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Details";
            // 
            // labelDetailsCanBeAssigned
            // 
            this.labelDetailsCanBeAssigned.AutoSize = true;
            this.labelDetailsCanBeAssigned.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDetailsCanBeAssigned.Location = new System.Drawing.Point(127, 161);
            this.labelDetailsCanBeAssigned.Name = "labelDetailsCanBeAssigned";
            this.labelDetailsCanBeAssigned.Size = new System.Drawing.Size(48, 13);
            this.labelDetailsCanBeAssigned.TabIndex = 16;
            this.labelDetailsCanBeAssigned.Text = "label21";
            this.labelDetailsCanBeAssigned.Visible = false;
            // 
            // labelDetailsCanBeAssignedTitle
            // 
            this.labelDetailsCanBeAssignedTitle.AutoSize = true;
            this.labelDetailsCanBeAssignedTitle.Location = new System.Drawing.Point(6, 161);
            this.labelDetailsCanBeAssignedTitle.Name = "labelDetailsCanBeAssignedTitle";
            this.labelDetailsCanBeAssignedTitle.Size = new System.Drawing.Size(86, 13);
            this.labelDetailsCanBeAssignedTitle.TabIndex = 15;
            this.labelDetailsCanBeAssignedTitle.Text = "Can be assigned";
            // 
            // labelDetailIsRootRole
            // 
            this.labelDetailIsRootRole.AutoSize = true;
            this.labelDetailIsRootRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDetailIsRootRole.Location = new System.Drawing.Point(127, 142);
            this.labelDetailIsRootRole.Name = "labelDetailIsRootRole";
            this.labelDetailIsRootRole.Size = new System.Drawing.Size(48, 13);
            this.labelDetailIsRootRole.TabIndex = 14;
            this.labelDetailIsRootRole.Text = "label21";
            this.labelDetailIsRootRole.Visible = false;
            // 
            // labelDetailIsRootRoleTitle
            // 
            this.labelDetailIsRootRoleTitle.AutoSize = true;
            this.labelDetailIsRootRoleTitle.Location = new System.Drawing.Point(6, 142);
            this.labelDetailIsRootRoleTitle.Name = "labelDetailIsRootRoleTitle";
            this.labelDetailIsRootRoleTitle.Size = new System.Drawing.Size(59, 13);
            this.labelDetailIsRootRoleTitle.TabIndex = 13;
            this.labelDetailIsRootRoleTitle.Text = "Is root role:";
            this.labelDetailIsRootRoleTitle.Visible = false;
            // 
            // labelDetailPermission
            // 
            this.labelDetailPermission.AutoSize = true;
            this.labelDetailPermission.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDetailPermission.Location = new System.Drawing.Point(127, 121);
            this.labelDetailPermission.Name = "labelDetailPermission";
            this.labelDetailPermission.Size = new System.Drawing.Size(48, 13);
            this.labelDetailPermission.TabIndex = 12;
            this.labelDetailPermission.Text = "label21";
            this.labelDetailPermission.Visible = false;
            // 
            // labelDetailCreateUsers
            // 
            this.labelDetailCreateUsers.AutoSize = true;
            this.labelDetailCreateUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDetailCreateUsers.Location = new System.Drawing.Point(127, 100);
            this.labelDetailCreateUsers.Name = "labelDetailCreateUsers";
            this.labelDetailCreateUsers.Size = new System.Drawing.Size(48, 13);
            this.labelDetailCreateUsers.TabIndex = 11;
            this.labelDetailCreateUsers.Text = "label20";
            this.labelDetailCreateUsers.Visible = false;
            // 
            // labelDetailId
            // 
            this.labelDetailId.AutoSize = true;
            this.labelDetailId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDetailId.Location = new System.Drawing.Point(127, 16);
            this.labelDetailId.Name = "labelDetailId";
            this.labelDetailId.Size = new System.Drawing.Size(0, 13);
            this.labelDetailId.TabIndex = 10;
            // 
            // labelDetailName
            // 
            this.labelDetailName.AutoSize = true;
            this.labelDetailName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDetailName.Location = new System.Drawing.Point(127, 37);
            this.labelDetailName.Name = "labelDetailName";
            this.labelDetailName.Size = new System.Drawing.Size(0, 13);
            this.labelDetailName.TabIndex = 9;
            // 
            // labelDetailRootRoles
            // 
            this.labelDetailRootRoles.AutoSize = true;
            this.labelDetailRootRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDetailRootRoles.Location = new System.Drawing.Point(127, 79);
            this.labelDetailRootRoles.Name = "labelDetailRootRoles";
            this.labelDetailRootRoles.Size = new System.Drawing.Size(48, 13);
            this.labelDetailRootRoles.TabIndex = 8;
            this.labelDetailRootRoles.Text = "label17";
            this.labelDetailRootRoles.Visible = false;
            // 
            // labelDetailSubRoles
            // 
            this.labelDetailSubRoles.AutoSize = true;
            this.labelDetailSubRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDetailSubRoles.Location = new System.Drawing.Point(127, 58);
            this.labelDetailSubRoles.Name = "labelDetailSubRoles";
            this.labelDetailSubRoles.Size = new System.Drawing.Size(48, 13);
            this.labelDetailSubRoles.TabIndex = 7;
            this.labelDetailSubRoles.Text = "label14";
            this.labelDetailSubRoles.Visible = false;
            // 
            // labelDetailRootRolesTitle
            // 
            this.labelDetailRootRolesTitle.AutoSize = true;
            this.labelDetailRootRolesTitle.Location = new System.Drawing.Point(6, 79);
            this.labelDetailRootRolesTitle.Name = "labelDetailRootRolesTitle";
            this.labelDetailRootRolesTitle.Size = new System.Drawing.Size(108, 13);
            this.labelDetailRootRolesTitle.TabIndex = 6;
            this.labelDetailRootRolesTitle.Text = "Can create root-roles:";
            this.labelDetailRootRolesTitle.Visible = false;
            // 
            // labelDetailPermissionTitle
            // 
            this.labelDetailPermissionTitle.AutoSize = true;
            this.labelDetailPermissionTitle.Location = new System.Drawing.Point(6, 121);
            this.labelDetailPermissionTitle.Name = "labelDetailPermissionTitle";
            this.labelDetailPermissionTitle.Size = new System.Drawing.Size(85, 13);
            this.labelDetailPermissionTitle.TabIndex = 5;
            this.labelDetailPermissionTitle.Text = "Data permission:";
            this.labelDetailPermissionTitle.Visible = false;
            // 
            // labelDetailCreateUsersTitle
            // 
            this.labelDetailCreateUsersTitle.AutoSize = true;
            this.labelDetailCreateUsersTitle.Location = new System.Drawing.Point(6, 100);
            this.labelDetailCreateUsersTitle.Name = "labelDetailCreateUsersTitle";
            this.labelDetailCreateUsersTitle.Size = new System.Drawing.Size(90, 13);
            this.labelDetailCreateUsersTitle.TabIndex = 3;
            this.labelDetailCreateUsersTitle.Text = "Can create users:";
            this.labelDetailCreateUsersTitle.Visible = false;
            // 
            // labelDetailSubRolesTitle
            // 
            this.labelDetailSubRolesTitle.AutoSize = true;
            this.labelDetailSubRolesTitle.Location = new System.Drawing.Point(6, 58);
            this.labelDetailSubRolesTitle.Name = "labelDetailSubRolesTitle";
            this.labelDetailSubRolesTitle.Size = new System.Drawing.Size(115, 13);
            this.labelDetailSubRolesTitle.TabIndex = 2;
            this.labelDetailSubRolesTitle.Text = "Can manage sub-roles:";
            this.labelDetailSubRolesTitle.Visible = false;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(6, 37);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(38, 13);
            this.label51.TabIndex = 1;
            this.label51.Text = "Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Id:";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.treeViewRoles);
            this.groupBox5.Location = new System.Drawing.Point(11, 37);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(336, 230);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Roles and users";
            // 
            // treeViewRoles
            // 
            this.treeViewRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewRoles.ImageIndex = 0;
            this.treeViewRoles.ImageList = this.imageList1;
            this.treeViewRoles.Location = new System.Drawing.Point(3, 16);
            this.treeViewRoles.Name = "treeViewRoles";
            this.treeViewRoles.SelectedImageIndex = 0;
            this.treeViewRoles.Size = new System.Drawing.Size(330, 211);
            this.treeViewRoles.TabIndex = 19;
            this.treeViewRoles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewRoles_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Cap.png");
            this.imageList1.Images.SetKeyName(1, "User.png");
            // 
            // buttonCreateSubRole
            // 
            this.buttonCreateSubRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreateSubRole.Location = new System.Drawing.Point(11, 273);
            this.buttonCreateSubRole.Name = "buttonCreateSubRole";
            this.buttonCreateSubRole.Size = new System.Drawing.Size(75, 42);
            this.buttonCreateSubRole.TabIndex = 21;
            this.buttonCreateSubRole.Text = "Create sub-role";
            this.buttonCreateSubRole.UseVisualStyleBackColor = true;
            this.buttonCreateSubRole.Click += new System.EventHandler(this.buttonCreateSubRole_Click);
            // 
            // labelKeyStatus
            // 
            this.labelKeyStatus.AutoSize = true;
            this.labelKeyStatus.Location = new System.Drawing.Point(322, 12);
            this.labelKeyStatus.Name = "labelKeyStatus";
            this.labelKeyStatus.Size = new System.Drawing.Size(0, 13);
            this.labelKeyStatus.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Key status:";
            // 
            // buttonPickKeys
            // 
            this.buttonPickKeys.Location = new System.Drawing.Point(11, 6);
            this.buttonPickKeys.Name = "buttonPickKeys";
            this.buttonPickKeys.Size = new System.Drawing.Size(123, 25);
            this.buttonPickKeys.TabIndex = 15;
            this.buttonPickKeys.Text = "Pick your keys";
            this.buttonPickKeys.UseVisualStyleBackColor = true;
            this.buttonPickKeys.Click += new System.EventHandler(this.buttonPickMasterKeys_Click);
            // 
            // buttonRefreshUserTree
            // 
            this.buttonRefreshUserTree.Location = new System.Drawing.Point(140, 6);
            this.buttonRefreshUserTree.Name = "buttonRefreshUserTree";
            this.buttonRefreshUserTree.Size = new System.Drawing.Size(111, 25);
            this.buttonRefreshUserTree.TabIndex = 24;
            this.buttonRefreshUserTree.Text = "Refresh roles and users";
            this.buttonRefreshUserTree.UseVisualStyleBackColor = true;
            this.buttonRefreshUserTree.Click += new System.EventHandler(this.buttonRefreshRolesAndUsers_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonAddUserToRole);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxNewUserName);
            this.groupBox2.Controls.Add(this.labelAddUserGeneratedId);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.buttonGenerateUserKeys);
            this.groupBox2.Location = new System.Drawing.Point(353, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 80);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add user to role or create new user";
            // 
            // buttonAddUserToRole
            // 
            this.buttonAddUserToRole.Location = new System.Drawing.Point(141, 49);
            this.buttonAddUserToRole.Name = "buttonAddUserToRole";
            this.buttonAddUserToRole.Size = new System.Drawing.Size(90, 25);
            this.buttonAddUserToRole.TabIndex = 19;
            this.buttonAddUserToRole.Text = "Add user to role";
            this.buttonAddUserToRole.UseVisualStyleBackColor = true;
            this.buttonAddUserToRole.Click += new System.EventHandler(this.buttonAddUserToRole_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Id:";
            // 
            // textBoxNewUserName
            // 
            this.textBoxNewUserName.Location = new System.Drawing.Point(9, 32);
            this.textBoxNewUserName.MaxLength = 32;
            this.textBoxNewUserName.Name = "textBoxNewUserName";
            this.textBoxNewUserName.Size = new System.Drawing.Size(123, 20);
            this.textBoxNewUserName.TabIndex = 10;
            this.textBoxNewUserName.TextChanged += new System.EventHandler(this.textBoxNewUserId_TextChanged);
            // 
            // labelAddUserGeneratedId
            // 
            this.labelAddUserGeneratedId.AutoSize = true;
            this.labelAddUserGeneratedId.Location = new System.Drawing.Point(31, 55);
            this.labelAddUserGeneratedId.Name = "labelAddUserGeneratedId";
            this.labelAddUserGeneratedId.Size = new System.Drawing.Size(0, 13);
            this.labelAddUserGeneratedId.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Enter user name";
            // 
            // buttonGenerateUserKeys
            // 
            this.buttonGenerateUserKeys.Location = new System.Drawing.Point(141, 18);
            this.buttonGenerateUserKeys.Name = "buttonGenerateUserKeys";
            this.buttonGenerateUserKeys.Size = new System.Drawing.Size(90, 25);
            this.buttonGenerateUserKeys.TabIndex = 13;
            this.buttonGenerateUserKeys.Text = "Create user";
            this.buttonGenerateUserKeys.UseVisualStyleBackColor = true;
            this.buttonGenerateUserKeys.Click += new System.EventHandler(this.buttonCreateUser_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 347);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Cloud Storage Administration";
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
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
        private System.Windows.Forms.Button buttonGenerateMasterKeypair;
        private System.Windows.Forms.TextBox textBoxPreAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPickKeys;
        private System.Windows.Forms.Button buttonGetServiceStartTime;
        private System.Windows.Forms.Button buttonKillPreService;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxDORoleName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxDOUsername;
        private System.Windows.Forms.Button buttonRefreshUserTree;
        private System.Windows.Forms.Label labelKeyStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxYourName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TreeView treeViewRoles;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonDeleteRole;
        private System.Windows.Forms.Button buttonCreateSubRole;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxNewUserName;
        private System.Windows.Forms.Label labelAddUserGeneratedId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonGenerateUserKeys;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelDetailRootRolesTitle;
        private System.Windows.Forms.Label labelDetailPermissionTitle;
        private System.Windows.Forms.Label labelDetailCreateUsersTitle;
        private System.Windows.Forms.Label labelDetailSubRolesTitle;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label labelDetailPermission;
        private System.Windows.Forms.Label labelDetailCreateUsers;
        private System.Windows.Forms.Label labelDetailId;
        private System.Windows.Forms.Label labelDetailName;
        private System.Windows.Forms.Label labelDetailRootRoles;
        private System.Windows.Forms.Label labelDetailSubRoles;
        private System.Windows.Forms.Button buttonUpdateSubRole;
        private System.Windows.Forms.Label labelDetailIsRootRole;
        private System.Windows.Forms.Label labelDetailIsRootRoleTitle;
        private System.Windows.Forms.Button buttonRemoveUserFromRole;
        private System.Windows.Forms.Button buttonAddUserToRole;
        private System.Windows.Forms.Label labelDetailsCanBeAssigned;
        private System.Windows.Forms.Label labelDetailsCanBeAssignedTitle;

    }
}

