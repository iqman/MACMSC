using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Shared;
using Shared.Dto;
using Shared.ServiceContracts;
using Shared.ServiceProxy;
using StorageClient.Properties;
using Attribute = Shared.Dto.Attribute;

namespace StorageClient
{
    public partial class Form1 : Form
    {
        private KeyPair keyPair;
        private byte[] signingKeys;
        private Guid myId;

        private int searchResultGridIdColumnIndex;
        private int searchResultGridNameColumnIndex;
        private int searchResultGridSizeColumnIndex;
        private int searchResultGridSaveColumnIndex;
        private int searchResultGridDeleteColumnIndex;
        private int searchResultGridVerifyIntegrityColumnIndex;

        private IList<DataEntity> searchResults;

        public Form1()
        {
            InitializeComponent();

            this.textBoxYourName.Text = Environment.UserName;
            this.myId = GuidCreator.CreateGuidFromString(this.textBoxYourName.Text);
            this.textBoxYourUniqueId.Text = this.myId.ToString();

            InitializeSearchResultGrid();
        }

        private void InitializeSearchResultGrid()
        {
            this.searchResultGridIdColumnIndex = this.dgvSearchResult.Columns.Add("Id", "Id");
            this.searchResultGridNameColumnIndex = this.dgvSearchResult.Columns.Add("Name", "Name");

            this.searchResultGridSizeColumnIndex = this.dgvSearchResult.Columns.Add("Size", "Size");

            DataGridViewButtonColumn saveCol = new DataGridViewButtonColumn();
            saveCol.Text = "Save";
            saveCol.Name = "Save";
            saveCol.UseColumnTextForButtonValue = true;
            this.searchResultGridSaveColumnIndex = this.dgvSearchResult.Columns.Add(saveCol);

            DataGridViewButtonColumn deleteCol = new DataGridViewButtonColumn();
            deleteCol.Text = "Delete";
            deleteCol.Name = "Delete";
            deleteCol.UseColumnTextForButtonValue = true;
            this.searchResultGridDeleteColumnIndex = this.dgvSearchResult.Columns.Add(deleteCol);

            DataGridViewButtonColumn verifyIntegrityCol = new DataGridViewButtonColumn();
            verifyIntegrityCol.Text = "Verify Integrity";
            verifyIntegrityCol.Name = "Verify Integrity";
            verifyIntegrityCol.UseColumnTextForButtonValue = true;
            this.searchResultGridVerifyIntegrityColumnIndex = this.dgvSearchResult.Columns.Add(verifyIntegrityCol);

            this.dgvSearchResult.Columns[this.searchResultGridIdColumnIndex].MinimumWidth = 40;
            this.dgvSearchResult.Columns[this.searchResultGridIdColumnIndex].Width = 70;

            this.dgvSearchResult.Columns[this.searchResultGridNameColumnIndex].MinimumWidth = 135;
            this.dgvSearchResult.Columns[this.searchResultGridNameColumnIndex].Width = 135;

            this.dgvSearchResult.Columns[this.searchResultGridSizeColumnIndex].MinimumWidth = 50;
            this.dgvSearchResult.Columns[this.searchResultGridSizeColumnIndex].Width = 80;

            this.dgvSearchResult.Columns[this.searchResultGridSaveColumnIndex].MinimumWidth = 40;
            this.dgvSearchResult.Columns[this.searchResultGridSaveColumnIndex].Width = 40;

            this.dgvSearchResult.Columns[this.searchResultGridDeleteColumnIndex].MinimumWidth = 50;
            this.dgvSearchResult.Columns[this.searchResultGridDeleteColumnIndex].Width = 50;

            this.dgvSearchResult.Columns[this.searchResultGridVerifyIntegrityColumnIndex].MinimumWidth = 70;
            this.dgvSearchResult.Columns[this.searchResultGridVerifyIntegrityColumnIndex].Width = 70;

            this.dgvSearchResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSearchResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            try
            {
                if (!string.IsNullOrEmpty(this.textBoxServerAddress.Text))
                {
                    Settings.Default.ServerAddress = this.textBoxServerAddress.Text;
                    Settings.Default.Save();
                }
            }
            catch(Exception ex)
            {
                Logger.LogError("Error saving server address value", ex);
            }

            try
            {
                if (!string.IsNullOrEmpty(this.textBoxPreAddress.Text))
                {
                    Settings.Default.PreAddress = this.textBoxPreAddress.Text;
                    Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error saving server address value", ex);
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                if (!string.IsNullOrEmpty(Settings.Default.UserKeysFile))
                {
                    ReadUserKeys(Settings.Default.UserKeysFile);
                }
            }
            catch (Exception ex)
            {
                Settings.Default.UserKeysFile = "";
                Settings.Default.Save();
                Logger.LogError("Error loading public key from previously picked file", ex);
            }
        }

        private void ReadUserKeys(string filename)
        {
            KeyCollection keys = XmlFile.ReadFile<KeyCollection>(filename);

            this.keyPair = new KeyPair();
            this.keyPair.Public = Convert.FromBase64String(keys.PublicKey);
            this.keyPair.Private = Convert.FromBase64String(keys.PrivateKey);

            this.signingKeys = Convert.FromBase64String(keys.SignKeys);

            this.labelKeyStatus.Text = "Keys loaded";
        }

        private IGatewayService CreateServiceProxy()
        {
            ProxyFactory.RegisterProxy(typeof(IGatewayService), typeof(GatewayServiceProxy), this.textBoxServerAddress.Text);
            return ProxyFactory.CreateProxy<IGatewayService>();
        }

        private IPreService CreatePreProxy()
        {
            ProxyFactory.RegisterProxy(typeof(IPreService), typeof(PreServiceProxy), this.textBoxPreAddress.Text);
            return ProxyFactory.CreateProxy<IPreService>();
        }

        private void ButtonPickUserKeysClick(object sender, EventArgs e)
        {
            try
            {
                string filename = FileDialogs.AskUserForFileNameToOpen();
                if (!string.IsNullOrEmpty(filename))
                {
                    ReadUserKeys(filename);

                    Settings.Default.UserKeysFile = filename;
                    Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error loading keys", ex);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBoxSearchKeyword.Text))
                {
                    MessageBox.Show("You must enter a search keyword");
                    return;
                }
                if (this.keyPair == null)
                {
                    MessageBox.Show("You must load user keys first");
                    return;
                }

                Guid attributeId = GuidCreator.CreateGuidFromString(this.textBoxSearchKeyword.Text);

                IGatewayService proxy = CreateServiceProxy();
                this.searchResults = proxy.SearchForDataEntities(this.myId, attributeId);

                if (RefreshRoles(this.rolesUserControlDownload))
                {
                    DecryptSearchResultsMetadata();

                    ShowSearchResults();

                    MessageBox.Show("Search complete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error searching for entities", ex);
            }
        }

        private void DecryptSearchResultsMetadata()
        {
            IPreService preProxy;
            foreach (DataEntity entity in this.searchResults)
            {
                preProxy = CreatePreProxy();
                entity.AesInfo.Key = preProxy.Decrypt(this.keyPair.Private, entity.AesInfo.Key);
                if (entity.AesInfo.Key.Length > 32)  // hack to fix a weird error in preLib where the length is sometimes 1 byte to large
                {
                    entity.AesInfo.Key = entity.AesInfo.Key.Take(32).ToArray();
                }
                

                preProxy = CreatePreProxy();
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

        private void ShowSearchResults()
        {
            this.dgvSearchResult.Rows.Clear();

            string[] values = new string[this.dgvSearchResult.Columns.Count];

            foreach (DataEntity entity in this.searchResults)
            {
                values[this.searchResultGridIdColumnIndex] = entity.Id.ToString();
                values[this.searchResultGridNameColumnIndex] = entity.Payload.Name.GetString();

                values[this.searchResultGridSizeColumnIndex] = entity.Payload.Size.ToString();
                //values[this.searchResultGridSaveColumnIndex] // button with no explicit value set on purpose

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dgvSearchResult, values);
                row.Tag = entity;

                this.dgvSearchResult.Rows.Add(row);
            }
        }

        private void dgvSearchResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataEntity entity = (DataEntity)this.dgvSearchResult.Rows[e.RowIndex].Tag;
                if (e.ColumnIndex == this.searchResultGridSaveColumnIndex)
                {
                    SaveEntityToFile(entity);
                }

                if (e.ColumnIndex == this.searchResultGridDeleteColumnIndex)
                {
                    DeleteEntity(entity);
                }

                if (e.ColumnIndex == this.searchResultGridVerifyIntegrityColumnIndex)
                {
                    VerifyIntegrity(entity);
                }
            }
        }

        private void VerifyIntegrity(DataEntity entity)
        {
            try
            {
                IGatewayService proxy = ProxyFactory.CreateProxy<IGatewayService>();
                bool verified = proxy.VerifyIntegrity(this.myId, entity.Id);

                if (!verified)
                {
                    MessageBox.Show("The integrity is NOT valid");
                }
                else
                {
                    MessageBox.Show("The integrity has been verified");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error verifying integrity of entity", ex);
            }
        }

        private void DeleteEntity(DataEntity entity)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete: " + entity.Payload.Name.GetString(), "Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);

                if (result == DialogResult.Yes)
                {
                    IGatewayService proxy = ProxyFactory.CreateProxy<IGatewayService>();
                    proxy.DeleteDataEntities(this.myId, rolesUserControlDownload.SelectedRoles, new List<Guid>(new[] { entity.Id }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error delting entity from server", ex);
            }
        }

        private void SaveEntityToFile(DataEntity entity)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = entity.Payload.Name.GetString();
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    IGatewayService proxy = ProxyFactory.CreateProxy<IGatewayService>();
                    byte[] payloadContent = proxy.GetPayload(this.myId, entity.Id);

                    byte[] plainText = SymmetricEncryptor.Decrypt(payloadContent, entity.AesInfo);

                    File.WriteAllBytes(dialog.FileName, plainText);

                    MessageBox.Show("Done saving file");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error saving entity to file", ex);
            }
        }

        private void buttonPickDataForUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.labelUploadData.Text = dialog.FileName;
            }
        }

        private void buttonUploadNow_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listBoxUploadKeywords.Items.Count == 0)
                {
                    MessageBox.Show("At least one keyword must be associated with the data before it is uploaded");
                    return;
                }
                if (this.keyPair == null)
                {
                    MessageBox.Show("You must load user keys first");
                    return;
                }

                if (this.rolesUserControlUploadData.SelectedRoles.Count == 0)
                {
                    MessageBox.Show("You must select at least one role which should have access to the uploaded data");
                    return;
                }

                byte[] fileContent = File.ReadAllBytes(this.labelUploadData.Text);

                AesEncryptionInfo encryptionInfo = SymmetricEncryptor.GenerateSymmetricKeyInfo();

                byte[] fileCiphertext = SymmetricEncryptor.Encrypt(fileContent, encryptionInfo);

                IPreService preProxy = CreatePreProxy();
                byte[] encSymIv = preProxy.Encrypt(this.keyPair.Public, encryptionInfo.IV);

                preProxy = CreatePreProxy();
                byte[] encSymKey = preProxy.Encrypt(this.keyPair.Public, encryptionInfo.Key);

                byte[] name = SymmetricEncryptor.Encrypt(Path.GetFileName(this.labelUploadData.Text).GetBytes(), encryptionInfo);

                DataEntity entity = new DataEntity();
                entity.Attributes = CollectAndEncryptAttributes(encryptionInfo);
                entity.Payload = new FilePayload(name, fileCiphertext);
                entity.AesInfo = new AesEncryptionInfo(encSymKey, encSymIv);
                entity.Id = Guid.NewGuid();

                entity.Signature = DataSigner.Sign(entity, this.signingKeys);

                IGatewayService proxy = CreateServiceProxy();

                proxy.CreateDataEntities(this.myId, this.rolesUserControlUploadData.SelectedRoles, new[] { entity });

                MessageBox.Show("Done uploading");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error preparing and uploading data to server", ex);
            }
        }

        private IList<Attribute> CollectAndEncryptAttributes(AesEncryptionInfo encryptionInfo)
        {
            IList<Attribute> attributes = new List<Attribute>();
            foreach (string s in this.listBoxUploadKeywords.Items)
            {
                byte[] att = SymmetricEncryptor.Encrypt(s.GetBytes(), encryptionInfo);
                attributes.Add(new Attribute(GuidCreator.CreateGuidFromString(s), att));
            }

            return attributes;
        }

        private void buttonUploadAddKeyword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBoxUploadAddKeyword.Text))
            {
                MessageBox.Show("You must type in a keyword");
                return;
            }

            this.listBoxUploadKeywords.Items.Add(this.textBoxUploadAddKeyword.Text);
        }

        private void buttonUploadKeywordsRemove_Click(object sender, EventArgs e)
        {
            if (this.listBoxUploadKeywords.SelectedIndex != -1)
            {
                this.listBoxUploadKeywords.Items.RemoveAt(this.listBoxUploadKeywords.SelectedIndex);
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

        private void buttonRefreshRoles_Click(object sender, EventArgs e)
        {
            RefreshRoles(this.rolesUserControlUploadData);
        }

        private bool RefreshRoles(RolesUserControl uc)
        {
            try
            {
                IGatewayService proxy = CreateServiceProxy();
                IList<RoleClientInfo> roles = proxy.GetMyImmediateRoles(this.myId);

                foreach (RoleClientInfo role in roles)
                {
                    IPreService preService = CreatePreProxy();
                    role.Name = preService.Decrypt(this.keyPair.Private, role.Name);
                }

                uc.InsertRoles(roles);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Error refreshing roles", ex);
            }
            return false;
        }

        private void dgvSearchResult_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvSearchResult.SelectedRows.Count == 1)
            {
                DataEntity entity = (DataEntity)this.dgvSearchResult.SelectedRows[0].Tag;

                this.rolesUserControlDownload.SelectRolesWithEntity(entity.Id);
            }
        }
    }
}
