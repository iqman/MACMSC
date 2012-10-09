using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Shared;
using Shared.Dto;
using Shared.ServiceContracts;
using Shared.ServiceProxy;
using StorageAdmin.Properties;
using Attribute = Shared.Dto.Attribute;

namespace StorageAdmin
{
    public partial class MainForm : Form
    {
        private KeyPair masterKeypair;
        public KeyPair keyPair;

        private Guid myId;

        public MainForm()
        {
            InitializeComponent();

            this.textBoxYourName.Text = Environment.UserName;
            this.myId = GuidCreator.CreateGuidFromString(this.textBoxYourName.Text);
            this.textBoxYourUniqueId.Text = this.myId.ToString();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            try
            {
                if (!string.IsNullOrEmpty(this.textBoxServerAddress.Text))
                {
                    Settings.Default.ServerAddress = this.textBoxServerAddress.Text;
                    Settings.Default.Save();
                }

                if (!string.IsNullOrEmpty(this.textBoxPreAddress.Text))
                {
                    Settings.Default.PreAddress = this.textBoxPreAddress.Text;
                    Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error saving server and pre urls", ex);
            }
        }

        private void buttonGenerateAndSaveMasterKeypair_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBoxDOUsername.Text))
                {
                    MessageBox.Show("You must enter a DO user name");
                    return;
                }

                if (string.IsNullOrEmpty(this.textBoxDORoleName.Text))
                {
                    MessageBox.Show("You must enter a DO role name");
                    return;
                }

                IPreService proxy = GetPreProxy();
                this.masterKeypair = proxy.GenerateKeyPair();

                SignKeys doSignKeyPair = DataSigner.GenerateSignKeyPair();

                proxy = GetPreProxy();
                byte[] doUserName = proxy.Encrypt(this.masterKeypair.Public, this.textBoxDOUsername.Text.GetBytes());

                proxy = GetPreProxy();
                byte[] doRoleName = proxy.Encrypt(this.masterKeypair.Public, this.textBoxDORoleName.Text.GetBytes());


                IGatewayService gwProxy = GetServiceProxy();
                gwProxy.InitializeSystem(this.myId, doUserName, doRoleName, doSignKeyPair.PublicOnly);

                string filename = FileDialogs.AskUserForFileNameToSaveIn();
                if (!string.IsNullOrEmpty(filename))
                {
                    if (!Path.HasExtension(filename))
                    {
                        filename = filename + ".xml";
                    }

                    KeyCollection keys = new KeyCollection();
                    keys.MasterPublicKey = Convert.ToBase64String(this.masterKeypair.Public);
                    keys.MasterPrivateKey = Convert.ToBase64String(this.masterKeypair.Private);
                    keys.PrivateKey = keys.MasterPrivateKey;
                    keys.PublicKey = keys.MasterPublicKey;
                    keys.SignKeys = Convert.ToBase64String(doSignKeyPair.PublicAndPrivate);

                    XmlFile.WriteFile(keys, filename);

                    this.labelKeyStatus.Text = "Keys including MASTER KEYS loaded";

                    MessageBox.Show("Done");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error generating master keypair", ex);
            }
        }

        private IPreService GetPreProxy()
        {
            ProxyFactory.RegisterProxy(typeof(IPreService), typeof(PreServiceProxy), this.textBoxPreAddress.Text);
            return ProxyFactory.CreateProxy<IPreService>();
        }

        private IGatewayService GetServiceProxy()
        {
            ProxyFactory.RegisterProxy(typeof(IGatewayService), typeof(GatewayServiceProxy), this.textBoxServerAddress.Text);
            return ProxyFactory.CreateProxy<IGatewayService>();
        }

        private void buttonPickMasterKeys_Click(object sender, EventArgs e)
        {
            string filename = FileDialogs.AskUserForFileNameToOpen();
            if (filename != null)
            {
                KeyCollection keys = XmlFile.ReadFile<KeyCollection>(filename);

                this.keyPair = new KeyPair();
                this.keyPair.Public = Convert.FromBase64String(keys.PublicKey);
                this.keyPair.Private = Convert.FromBase64String(keys.PrivateKey);

                if (keys.MasterPrivateKey != null && keys.MasterPublicKey != null)
                {
                    this.masterKeypair = new KeyPair();
                    this.masterKeypair.Public = Convert.FromBase64String(keys.MasterPublicKey);
                    this.masterKeypair.Private = Convert.FromBase64String(keys.MasterPrivateKey);
                    this.labelKeyStatus.Text = "Keys including MASTER KEYS loaded";
                }
                else
                {
                    this.labelKeyStatus.Text = "Keys but NOT master keys loaded";
                }
            }
        }

        private void buttonCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeViewRoles.SelectedNode == null ||
                    !(this.treeViewRoles.SelectedNode.Tag is RoleDescription))
                {
                    return;
                }

                if (string.IsNullOrEmpty(this.textBoxNewUserName.Text))
                {
                    MessageBox.Show("You must enter a username");
                    return;
                }
                Guid newUserId = GuidCreator.CreateGuidFromString(this.textBoxNewUserName.Text);

                if (this.masterKeypair == null && this.keyPair == null)
                {
                    MessageBox.Show("You must load your key pair first");
                    return;
                }

                string filename = FileDialogs.AskUserForFileNameToSaveIn();
                if (!string.IsNullOrEmpty(filename))
                {
                    if (!Path.HasExtension(filename))
                    {
                        filename = filename + ".xml";
                    }

                    SignKeys userSignKeyPair = DataSigner.GenerateSignKeyPair();
                    IPreService proxy;
                    KeyPair userKeypair;
                    DelegationToken userDelegationToken;

                    if (this.masterKeypair != null)
                    {
                        proxy = GetPreProxy();
                        userKeypair = proxy.GenerateKeyPair();

                        userDelegationToken = new DelegationToken();
                        proxy = GetPreProxy();
                        userDelegationToken.ToUser = proxy.GenerateDelegationKey(this.masterKeypair.Private, userKeypair.Public);
                    }
                    else
                    {
                        userKeypair = this.keyPair; // I am not a DO, so when creating a new user then reuse my key
                        userDelegationToken = null; // I do not know my own delegation key. The server will put it in for me.
                    }

                    proxy = GetPreProxy();
                    byte[] username = proxy.Encrypt(this.keyPair.Public, this.textBoxNewUserName.Text.GetBytes());

                    User user = new User();
                    user.DelegationToken = userDelegationToken;
                    user.Id = newUserId;
                    user.Name = username;
                    user.SignPublicKey = userSignKeyPair.PublicOnly;


                    RoleDescription role = (RoleDescription) this.treeViewRoles.SelectedNode.Tag;
                    IGatewayService gateWayproxy = GetServiceProxy();
                    gateWayproxy.CreateUser(this.myId, role.Id, user);


                    KeyCollection uk = new KeyCollection();
                    uk.PublicKey = Convert.ToBase64String(this.keyPair.Public); // use original DO public key
                    uk.PrivateKey = Convert.ToBase64String(userKeypair.Private);
                    uk.SignKeys= Convert.ToBase64String(userSignKeyPair.PublicAndPrivate);

                    XmlFile.WriteFile(uk, filename);

                    buttonRefreshRolesAndUsers_Click(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error generating user keypair", ex);
            }
        }

        private void buttonGetServiceStartTime_Click(object sender, EventArgs e)
        {
            try
            {
                IPreService preService = GetPreProxy();
                DateTime startTime = preService.GetServiceStartTime();
                MessageBox.Show("Start time: " + startTime);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error getting pre service start time", ex);
            }
        }

        private void buttonKillPreService_Click(object sender, EventArgs e)
        {
            try
            {
                IPreService preService = GetPreProxy();
                preService.ResetLibPre();
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error killing pre service", ex);
            }
        }

        private void buttonRefreshRolesAndUsers_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.keyPair == null)
                {
                    MessageBox.Show("You must select your key pair first");
                    return;
                }

                IGatewayService proxy = GetServiceProxy();
                IList<RoleDescription> roles = proxy.GetMyRoles(this.myId);

                TreeNode rootNode = new TreeNode("dummy");

                BuildUserTree(rootNode, roles);

                this.treeViewRoles.Nodes.Clear();

                foreach (TreeNode node in rootNode.Nodes)
                {
                    this.treeViewRoles.Nodes.Add(node);
                }

                this.treeViewRoles.ExpandAll();

                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error refreshing list of users", ex);
            }
        }

        private void BuildUserTree(TreeNode rootNode, IEnumerable<RoleDescription> roles)
        {
            foreach (RoleDescription role in roles)
            {
                IPreService preProxy = GetPreProxy();
                role.Name = preProxy.Decrypt(this.keyPair.Private, role.Name);
                TreeNode node = new TreeNode(role.Name.GetString(), 0, 0);

                node.Tag = role;
                rootNode.Nodes.Add(node);

                BuildUserTree(node, role.ChildRoles);

                foreach (UserDescription user in role.Users)
                {
                    preProxy = GetPreProxy();
                    user.Name = preProxy.Decrypt(this.keyPair.Private, user.Name);
                    TreeNode userNode = new TreeNode(user.Name.GetString(), 1, 1);
                    userNode.Tag = user;
                    node.Nodes.Add(userNode);
                }
            }
        }

        private void buttonRevokeUser_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.treeViewRoles.SelectedNode != null &&
            //        this.treeViewRoles.SelectedNode.Tag != null)
            //    {
            //        UserDescription userToRevoke = (UserDescription)this.treeViewRoles.SelectedNode.Tag;

            //        DialogResult result = MessageBox.Show("Are you sure you want to revoke: " + userToRevoke.Name.GetString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //        if (result == DialogResult.Yes)
            //        {
            //            IGatewayService proxy = GetServiceProxy();
            //            proxy.DeleteUser(this.myId, userToRevoke.Id);

            //            MessageBox.Show("Done");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //    Logger.LogError("Error revoking user", ex);
            //}
        }

        private void textBoxNewUserId_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBoxNewUserName.Text))
            {
                this.labelAddUserGeneratedId.Text = GuidCreator.CreateGuidFromString(this.textBoxNewUserName.Text).ToString();
            }
            else
            {
                this.labelAddUserGeneratedId.Text = string.Empty;
            }
        }

        private void textBoxYourName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBoxYourName.Text))
            {
                this.myId = GuidCreator.CreateGuidFromString(this.textBoxYourName.Text);
                this.textBoxYourUniqueId.Text = this.myId.ToString();
            }
            else
            {
                this.myId = Guid.Empty;
                this.textBoxYourUniqueId.Text = "";
            }
        }

        private void buttonDeleteRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeViewRoles.SelectedNode != null &&
                    this.treeViewRoles.SelectedNode.Tag is RoleDescription)
                {
                    RoleDescription selectedRole = (RoleDescription)this.treeViewRoles.SelectedNode.Tag;

                    if (selectedRole.IsRoot ||
                        this.treeViewRoles.SelectedNode.Parent != null)
                    {
                        string message = "Are you sure you want to delete" + selectedRole.Name.GetString() + " ?";
                        if (selectedRole.IsRoot)
                        {
                            message = "Are you sure you want to delete " + selectedRole.Name.GetString() +
                                      "? It is a root role, so all its child roles will also be deleted!";
                        }

                        DialogResult result = MessageBox.Show(message, "Deletion confirmation", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            IGatewayService proxy = GetServiceProxy();

                            Guid parentRoleId = Guid.Empty;
                            if (!selectedRole.IsRoot)
                            {
                                parentRoleId = ((RoleDescription)this.treeViewRoles.SelectedNode.Parent.Tag).Id;
                            }

                            proxy.DeleteSubRole(this.myId, parentRoleId, selectedRole.Id);

                            buttonRefreshRolesAndUsers_Click(this, EventArgs.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error deleting role user", ex);
            }
        }

        private void treeViewRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.Tag is RoleDescription)
                {
                    ShowRoleDetail(e.Node.Tag as RoleDescription);
                }
                else if (e.Node.Tag is UserDescription)
                {
                    ShowUserDetail(e.Node.Tag as UserDescription);
                }
            }
        }

        private void ShowRoleDetail(RoleDescription role)
        {
            SetDetailMode(true);

            this.labelDetailId.Text = role.Id.ToString();
            this.labelDetailName.Text = role.Name.GetString();
            this.labelDetailCreateUsers.Text = role.CanCreateUsers.ToString();
            this.labelDetailPermission.Text = Enum.GetName(typeof (DataEntityPermission), role.DataEntityPermission);
            this.labelDetailRootRoles.Text = role.CanCreateRoot.ToString();
            this.labelDetailSubRoles.Text = role.CanManageSubRoles.ToString();
            this.labelDetailIsRootRole.Text = role.IsRoot.ToString();
            this.labelDetailsCanBeAssigned.Text = role.AssignUnassignRole.ToString();
        }

        private void ShowUserDetail(UserDescription user)
        {
            SetDetailMode(false);

            this.labelDetailId.Text = user.Id.ToString();
            this.labelDetailName.Text = user.Name.GetString();
        }

        private void SetDetailMode(bool roleMode)
        {
            this.labelDetailCreateUsersTitle.Visible = roleMode;
            this.labelDetailCreateUsers.Visible = roleMode;
            this.labelDetailPermission.Visible = roleMode;
            this.labelDetailPermissionTitle.Visible = roleMode;
            this.labelDetailRootRoles.Visible = roleMode;
            this.labelDetailRootRolesTitle.Visible = roleMode;
            this.labelDetailSubRoles.Visible = roleMode;
            this.labelDetailSubRolesTitle.Visible = roleMode;
            this.labelDetailIsRootRole.Visible = roleMode;
            this.labelDetailIsRootRoleTitle.Visible = roleMode;

            this.labelDetailsCanBeAssigned.Visible = roleMode;
            this.labelDetailsCanBeAssignedTitle.Visible = roleMode;
        }

        private void buttonCreateSubRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeViewRoles.SelectedNode != null &&
                    this.treeViewRoles.SelectedNode.Tag is RoleDescription)
                {
                    RoleDescription selectedRole = (RoleDescription)this.treeViewRoles.SelectedNode.Tag;

                    IGatewayService proxy = GetServiceProxy();
                    IList<DataEntity> dataEntities = proxy.GetDataEntitiesForRole(this.myId, selectedRole.Id);

                    DecryptDataEntities(dataEntities);

                    CustomizeRoleDialog dialog = new CustomizeRoleDialog("Create new subrole from " + selectedRole.Name.GetString());
                    dialog.SetDataEntities(dataEntities);

                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        Role newRole = dialog.Role;
                        newRole.Id = Guid.NewGuid();

                        IPreService preProxy = GetPreProxy();
                        newRole.Name = preProxy.Encrypt(this.keyPair.Public, newRole.Name);

                        if (newRole.IsRoot)
                        {
                            newRole.Users.Add(this.myId);
                        }

                        proxy = GetServiceProxy();
                        proxy.CreateSubRole(this.myId, selectedRole.Id, newRole);

                        buttonRefreshRolesAndUsers_Click(this, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error creating sub-role user", ex);
            }
        }

        private void DecryptDataEntities(IEnumerable<DataEntity> dataEntities) 
        {
            IPreService preProxy;
            foreach (DataEntity entity in dataEntities)
            {
                preProxy = GetPreProxy();
                entity.AesInfo.Key = preProxy.Decrypt(this.keyPair.Private, entity.AesInfo.Key);
                if (entity.AesInfo.Key.Length > 32)  // hack to fix a weird error in preLib where the length is sometimes 1 byte to large
                {
                    entity.AesInfo.Key = entity.AesInfo.Key.Take(32).ToArray();
                }


                preProxy = GetPreProxy();
                entity.AesInfo.IV = preProxy.Decrypt(this.keyPair.Private, entity.AesInfo.IV);
                if (entity.AesInfo.IV.Length >= 16)  // hack to fix a weird error in preLib where the length is sometimes 1 byte to large
                {
                    entity.AesInfo.IV = entity.AesInfo.IV.Take(16).ToArray();
                }

                foreach (Attribute attribute in entity.Attributes)
                {
                    attribute.Keyword = SymmetricEncryptor.Decrypt(attribute.Keyword, entity.AesInfo);
                }

                entity.Payload.Name = SymmetricEncryptor.Decrypt(entity.Payload.Name, entity.AesInfo);
            }
        }

        private void buttonUpdateSubRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeViewRoles.SelectedNode != null &&
                    this.treeViewRoles.SelectedNode.Tag is RoleDescription &&
                    this.treeViewRoles.SelectedNode.Parent != null)
                {
                    RoleDescription selectedRole = (RoleDescription)this.treeViewRoles.SelectedNode.Tag;

                    RoleDescription parentRole = (RoleDescription) this.treeViewRoles.SelectedNode.Parent.Tag;
                    
                    CustomizeRoleDialog dialog = new CustomizeRoleDialog(selectedRole, "Update the role " + selectedRole.Name.GetString());
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        IPreService preProxy = GetPreProxy();
                        dialog.Role.Name = preProxy.Encrypt(this.keyPair.Public, dialog.Role.Name);
                        

                        IGatewayService proxy = GetServiceProxy();
                        proxy.UpdateSubRole(this.myId, parentRole.Id, dialog.Role);

                        buttonRefreshRolesAndUsers_Click(this, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error updating sub-role user", ex);
            }
        }

        private void buttonAddUserToRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBoxNewUserName.Text))
                {
                    MessageBox.Show("You must enter a username");
                    return;
                }

                Guid userId = GuidCreator.CreateGuidFromString(this.textBoxNewUserName.Text);

                if (this.treeViewRoles.SelectedNode != null &&
                    this.treeViewRoles.SelectedNode.Tag is RoleDescription)
                {
                    RoleDescription selectedRole = (RoleDescription)this.treeViewRoles.SelectedNode.Tag;

                    DialogResult result = MessageBox.Show("Are you sure you wan't to grant the role " + selectedRole.Name.GetString() + " to the user " + this.textBoxNewUserName.Text + "?", "Role assignment confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        IGatewayService proxy = GetServiceProxy();
                        proxy.AssignRoleToUser(this.myId, selectedRole.Id, userId);

                        buttonRefreshRolesAndUsers_Click(this, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error updating sub-role user", ex);
            }
        }

        private void buttonRemoveUserFromRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.treeViewRoles.SelectedNode != null &&
                    this.treeViewRoles.SelectedNode.Tag is UserDescription &&
                    this.treeViewRoles.SelectedNode.Parent != null)
                {
                    UserDescription selectedUser = (UserDescription)this.treeViewRoles.SelectedNode.Tag;
                    RoleDescription parentRole = (RoleDescription) this.treeViewRoles.SelectedNode.Parent.Tag;

                    DialogResult result = MessageBox.Show("Are you sure you wan't remove the user " + selectedUser.Name.GetString() + " from the role " + parentRole.Name.GetString() + "?", "Role un-assignment confirmation", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        IGatewayService proxy = GetServiceProxy();
                        proxy.RemoveRoleFromUser(this.myId, parentRole.Id, selectedUser.Id);

                        buttonRefreshRolesAndUsers_Click(this, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error updating sub-role user", ex);
            }
        }
    }
}
