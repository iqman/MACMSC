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

namespace StorageAdmin
{
    public partial class MainForm : Form
    {
        private SignKeys signKeyPair;
        private KeyPair userKeypair;
        private DelegationToken delegationToken;

        private KeyPair masterKeypair;

        private Guid myId;
        private Guid newUserId;

        public MainForm()
        {
            InitializeComponent();

            this.myId = UserIdentity.GetIdOfCurrentUser();

            this.userKeypair = new KeyPair();
            this.textBoxYourUniqueId.Text = this.myId.ToString();

            this.delegationToken = new DelegationToken();
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
                IGatewayService gwProxy = GetServiceProxy();
                gwProxy.InitializeSystem(this.myId);

                IPreService proxy = GetPreProxy();
                this.masterKeypair = proxy.GenerateKeyPair();

                string filename = FileDialogs.AskUserForFileNameToSaveIn();
                if (!string.IsNullOrEmpty(filename))
                {
                    if (!Path.HasExtension(filename))
                    {
                        filename = filename + ".xml";
                    }

                    MasterKeys mk = new MasterKeys();
                    mk.MasterKeyPublicKey = Convert.ToBase64String(this.masterKeypair.Public);
                    mk.MasterKeyPrivateKey = Convert.ToBase64String(this.masterKeypair.Private);

                    XmlFile.WriteFile(mk, filename);

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
                MasterKeys masterKeys = XmlFile.ReadFile<MasterKeys>(filename);

                this.masterKeypair = new KeyPair();
                this.masterKeypair.Public = Convert.FromBase64String(masterKeys.MasterKeyPublicKey);
                this.masterKeypair.Private = Convert.FromBase64String(masterKeys.MasterKeyPrivateKey);
            }
        }

        private void buttonGenerateKeypairsForUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBoxNewUserId.Text))
                {
                    MessageBox.Show("You must enter a username");
                    return;
                }
                this.newUserId = GuidCreator.CreateGuidFromString(this.textBoxNewUserId.Text);

                if (this.masterKeypair == null)
                {
                    MessageBox.Show("You must load master key pair first");
                    return;
                }

                string filename = FileDialogs.AskUserForFileNameToSaveIn();
                if (!string.IsNullOrEmpty(filename))
                {
                    if (!Path.HasExtension(filename))
                    {
                        filename = filename + ".xml";
                    }


                    this.signKeyPair = DataSigner.GenerateSignKeyPair();

                    IPreService proxy = GetPreProxy();
                    this.userKeypair = proxy.GenerateKeyPair();

                    proxy = GetPreProxy();
                    this.delegationToken.ToUser = proxy.GenerateDelegationKey(this.masterKeypair.Private, this.userKeypair.Public);

                    IGatewayService gateWayproxy = GetServiceProxy();
                    gateWayproxy.RegisterUser(this.myId, this.newUserId, this.delegationToken, this.signKeyPair.PublicOnly);


                    UserKeys uk = new UserKeys();
                    uk.MasterKeyPublicKey = Convert.ToBase64String(this.masterKeypair.Public);
                    uk.UserPrivateKey = Convert.ToBase64String(this.userKeypair.Private);
                    uk.UserSignKeys = Convert.ToBase64String(this.signKeyPair.PublicAndPrivate);

                    XmlFile.WriteFile(uk, filename);

                    MessageBox.Show("Done");
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

        private void buttonRefreshUsers_Click(object sender, EventArgs e)
        {
            try
            {
                IGatewayService proxy = GetServiceProxy();
                IList<Guid> userIds = proxy.GetAllUsersWithAccess(this.myId);

                this.listBoxUsers.Items.Clear();
                this.listBoxUsers.Items.AddRange(userIds.Select(id => id.ToString()).ToArray());

                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error refreshing list of users", ex);
            }
        }

        private void buttonRevokeUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listBoxUsers.SelectedIndex != -1)
                {
                    string idToRevoke = (string)this.listBoxUsers.SelectedItem;
                    DialogResult result = MessageBox.Show("Are you sure you want to revoke: " + idToRevoke, "Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

                    if(result == DialogResult.Yes)
                    {
                        IGatewayService proxy = GetServiceProxy();
                        proxy.RevokeUser(this.myId, new Guid(idToRevoke));

                        MessageBox.Show("Done");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error revoking user", ex);
            }
        }

        private void textBoxNewUserId_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBoxNewUserId.Text))
            {
                this.labelAddUserGeneratedId.Text = GuidCreator.CreateGuidFromString(this.textBoxNewUserId.Text).ToString();
            }
            else
            {
                this.labelAddUserGeneratedId.Text = string.Empty;
            }
        }

        private void textBoxTestName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBoxTestName.Text))
            {
                this.labelTestId.Text = GuidCreator.CreateGuidFromString(this.textBoxTestName.Text).ToString();
            }
            else
            {
                this.labelTestId.Text = string.Empty;
            }
        }
    }
}
