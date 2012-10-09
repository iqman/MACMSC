using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Shared;
using Shared.Dto;

namespace StorageClient
{
    public partial class RolesUserControl : UserControl
    {
        private int roleSelectColumnIndex;
        private int roleNameColumnIndex;
        private int roleIsRootColumnIndex;
        private int roleIsReadonlyColumnIndex;

        public RolesUserControl()
        {
            InitializeComponent();

            InitializeSearchResultGrid();
        }

        public IList<Guid> SelectedRoles
        {
            get
            {
                IList<Guid> selectedRoles = new List<Guid>();
                foreach (DataGridViewRow row in this.dgvRoles.Rows)
                {
                    object value = row.Cells[this.roleSelectColumnIndex].Value;
                    if (value != null &&
                        (value is string && ((string)value) == "True" ||
                        ((bool)row.Cells[this.roleSelectColumnIndex].Value)))
                    {
                        selectedRoles.Add(((RoleClientInfo)row.Tag).Id);
                    }
                }
                return selectedRoles;
            }
        }

        public void SelectRolesWithEntity(Guid entityId)
        {
            foreach (DataGridViewRow row in this.dgvRoles.Rows)
            {
                row.Cells[roleSelectColumnIndex].Value = ((RoleClientInfo) row.Tag).DataEntities.Contains(entityId);
            }
        }

        public void InsertRoles(IEnumerable<RoleClientInfo> roles)
        {
            this.dgvRoles.Rows.Clear();
            string[] values = new string[this.dgvRoles.Columns.Count];

            foreach (RoleClientInfo role in roles)
            {
                //values[roleSelectColumnIndex] // not assigned on purpose
                values[roleNameColumnIndex] = role.Name.GetString();
                values[roleIsRootColumnIndex] = role.IsRoot.ToString();
                values[roleIsReadonlyColumnIndex] = (role.DataEntityPermission == DataEntityPermission.ReadContent).ToString();

                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dgvRoles, values);
                row.Tag = role;

                this.dgvRoles.Rows.Add(row);
            }
        }

        private void InitializeSearchResultGrid()
        {
            DataGridViewCheckBoxColumn selectedColumn = new DataGridViewCheckBoxColumn();
            selectedColumn.Name = "Selected";
            selectedColumn.ReadOnly = false;
            this.roleSelectColumnIndex = this.dgvRoles.Columns.Add(selectedColumn);

            this.roleNameColumnIndex = this.dgvRoles.Columns.Add("Name", "Name");

            DataGridViewCheckBoxColumn rootColumn = new DataGridViewCheckBoxColumn();
            rootColumn.Name = "Root";
            rootColumn.ReadOnly = true;
            this.roleIsRootColumnIndex = this.dgvRoles.Columns.Add(rootColumn);

            DataGridViewCheckBoxColumn readonlyColumn = new DataGridViewCheckBoxColumn();
            readonlyColumn.Name = "Readonly";
            readonlyColumn.ReadOnly = true;
            this.roleIsReadonlyColumnIndex = this.dgvRoles.Columns.Add(readonlyColumn);

            this.dgvRoles.Columns[this.roleSelectColumnIndex].MinimumWidth = 65;
            this.dgvRoles.Columns[this.roleSelectColumnIndex].Width = 65;

            this.dgvRoles.Columns[this.roleNameColumnIndex].MinimumWidth = 110;
            this.dgvRoles.Columns[this.roleNameColumnIndex].Width = 110;

            this.dgvRoles.Columns[this.roleIsRootColumnIndex].MinimumWidth = 45;
            this.dgvRoles.Columns[this.roleIsRootColumnIndex].Width = 45;

            this.dgvRoles.Columns[this.roleIsReadonlyColumnIndex].MinimumWidth = 60;
            this.dgvRoles.Columns[this.roleIsReadonlyColumnIndex].Width = 60;

            this.dgvRoles.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
