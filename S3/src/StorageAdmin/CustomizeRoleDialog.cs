using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Shared;
using Shared.Dto;

namespace StorageAdmin
{
    public partial class CustomizeRoleDialog : Form
    {
        private int deSelectedColumnIndex;
        private int deIdColumnIndex;
        private int deNameColumnIndex;
        private int deSizeColumnIndex;

        public Role Role { get; set; }

        public CustomizeRoleDialog(RoleDescription existingRole, string text)
            : this(text)
        {
            SetRole(existingRole);
            this.buttonCreateSubRole.Text = "Update sub-role";
            this.Text = "Update existing sub-role";
            this.checkBoxMakeThisRootRole.Enabled = false;
        }

        public CustomizeRoleDialog(string text)
        {
            InitializeComponent();

            InitializeSearchResultGrid();

            this.labelTitle.Text = text;
            textBoxName_TextChanged(this, EventArgs.Empty);

            this.Role = new Role();
        }

        public void SetDataEntities(IEnumerable<DataEntity> dataEntities)
        {
            this.dgvDataEntities.Rows.Clear();

            string[] values = new string[this.dgvDataEntities.Columns.Count];

            foreach (DataEntity entity in dataEntities)
            {
                //values[this.deSelectedColumnIndex] // not set on purpose
                values[this.deIdColumnIndex] = entity.Id.ToString();
                values[this.deNameColumnIndex] = entity.Payload.Name.GetString();

                values[this.deSizeColumnIndex] = entity.Payload.Size.ToString();

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dgvDataEntities, values);
                row.Tag = entity;

                this.dgvDataEntities.Rows.Add(row);
            }
        }

        private void SetRole(RoleDescription existingRole)
        {
            //this.Role.AssignUnassignRole
            //this.Role.CanCreateRoot
            //this.Role.CanCreateUsers
            //this.Role.CanManageSubRoles
            this.Role.ChildRoles = existingRole.ChildRoles.Select(r => r.Id).ToList();
            //this.Role.DataEntities
            //this.Role.DataEntityPermission
            this.Role.Id = existingRole.Id;
            this.Role.IsRoot = existingRole.IsRoot;
            this.Role.Name = existingRole.Name;
            this.Role.Users = existingRole.Users.Select(u => u.Id).ToList();

            this.textBoxName.Text = existingRole.Name.GetString();
            this.checkBoxCanAssignUnassignRole.Checked = existingRole.AssignUnassignRole;
            this.checkBoxCanManageSubroles.Checked = existingRole.CanManageSubRoles;
            this.checkBoxCanCreateRootRoles.Checked = existingRole.CanCreateRoot;
            this.checkBoxCanCreateUsers.Checked = existingRole.CanCreateUsers;

            switch (existingRole.DataEntityPermission)
            {
                case DataEntityPermission.ReadContent:
                    this.radioButtonPermissionReadonly.Checked = true;
                    break;
                case DataEntityPermission.ModifyContent:
                    this.radioButtonPermissionModify.Checked = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void buttonCreateSubRole_Click(object sender, EventArgs e)
        {
            GetRole();
            DialogResult = DialogResult.OK;
        }

        private void GetRole()
        {
            this.Role.Name = this.textBoxName.Text.GetBytes();
            this.Role.AssignUnassignRole = this.checkBoxCanAssignUnassignRole.Checked;
            this.Role.CanManageSubRoles = this.checkBoxCanManageSubroles.Checked;
            this.Role.CanCreateRoot = this.checkBoxCanCreateRootRoles.Checked;
            this.Role.CanCreateUsers = this.checkBoxCanCreateUsers.Checked;

            this.Role.DataEntityPermission = radioButtonPermissionReadonly.Checked
                                                 ? DataEntityPermission.ReadContent
                                                 : DataEntityPermission.ModifyContent;

            this.Role.IsRoot = this.checkBoxMakeThisRootRole.Checked;

            foreach (DataGridViewRow row in this.dgvDataEntities.Rows)
            {
                object value = row.Cells[this.deSelectedColumnIndex].Value;
                if (value != null &&
                    (value is string && ((string)value) == "True" ||
                    ((bool)value)))
                {
                    this.Role.DataEntities.Add(((DataEntity)row.Tag).Id);
                }
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            this.buttonCreateSubRole.Enabled = !string.IsNullOrEmpty(this.textBoxName.Text);
        }

        private void InitializeSearchResultGrid()
        {
            DataGridViewCheckBoxColumn selectedCol = new DataGridViewCheckBoxColumn();
            selectedCol.Name = "Select";
            this.deSelectedColumnIndex = this.dgvDataEntities.Columns.Add(selectedCol);

            this.deIdColumnIndex = this.dgvDataEntities.Columns.Add("Id", "Id");
            this.deNameColumnIndex = this.dgvDataEntities.Columns.Add("Name", "Name");

            this.deSizeColumnIndex = this.dgvDataEntities.Columns.Add("Size", "Size");

            this.dgvDataEntities.Columns[this.deSelectedColumnIndex].MinimumWidth = 55;
            this.dgvDataEntities.Columns[this.deSelectedColumnIndex].Width = 70;

            this.dgvDataEntities.Columns[this.deIdColumnIndex].MinimumWidth = 40;
            this.dgvDataEntities.Columns[this.deIdColumnIndex].Width = 70;

            this.dgvDataEntities.Columns[this.deNameColumnIndex].MinimumWidth = 135;
            this.dgvDataEntities.Columns[this.deNameColumnIndex].Width = 135;

            this.dgvDataEntities.Columns[this.deSizeColumnIndex].MinimumWidth = 50;
            this.dgvDataEntities.Columns[this.deSizeColumnIndex].Width = 80;

            this.dgvDataEntities.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDataEntities.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
