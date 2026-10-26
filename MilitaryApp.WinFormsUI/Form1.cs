using System;
using System.Drawing;
using System.Windows.Forms;
using MilitaryApp.Core;

namespace MilitaryApp.WinFormsUI
{
    public partial class Form1 : Form
    {
        private Logic _logic = new Logic();
        private DataGridView dataGridView;
        private TextBox txtName, txtRank, txtPosition, txtSearchRank;
        private Button btnAdd, btnUpdate, btnDelete, btnSearch, btnGroup;

        public Form1()
        {
            InitializeComponent();
            SetupUI();
            RefreshGrid();
        }

        private void SetupUI()
        {
            this.Text = "Воинская часть (WinForms)";
            this.Size = new Size(800, 600);

            dataGridView = new DataGridView();
            dataGridView.Location = new Point(10, 10);
            dataGridView.Size = new Size(760, 300);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.Click += DataGridView_Click;
            this.Controls.Add(dataGridView);

            Label lblName = new Label { Text = "ФИО:", Location = new Point(10, 330), Width = 50 };
            txtName = new TextBox { Location = new Point(60, 327), Width = 150 };
            Label lblRank = new Label { Text = "Звание:", Location = new Point(230, 330), Width = 60 };
            txtRank = new TextBox { Location = new Point(290, 327), Width = 120 };
            Label lblPos = new Label { Text = "Должность:", Location = new Point(430, 330), Width = 80 };
            txtPosition = new TextBox { Location = new Point(510, 327), Width = 150 };

            this.Controls.Add(lblName); this.Controls.Add(txtName);
            this.Controls.Add(lblRank); this.Controls.Add(txtRank);
            this.Controls.Add(lblPos); this.Controls.Add(txtPosition);

            btnAdd = new Button { Text = "Добавить", Location = new Point(10, 370), Width = 100 };
            btnAdd.Click += (s, e) => {
                if (!string.IsNullOrWhiteSpace(txtName.Text))
                {
                    _logic.CreateSoldier(new Soldier { FullName = txtName.Text, Rank = txtRank.Text, Position = txtPosition.Text });
                    ClearInputs(); RefreshGrid();
                }
            };

            btnUpdate = new Button { Text = "Обновить", Location = new Point(120, 370), Width = 100 };
            btnUpdate.Click += (s, e) => {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var soldier = (Soldier)dataGridView.SelectedRows[0].DataBoundItem;
                    soldier.FullName = txtName.Text;
                    soldier.Rank = txtRank.Text;
                    soldier.Position = txtPosition.Text;
                    _logic.UpdateSoldier(soldier);
                    ClearInputs(); RefreshGrid();
                }
            };

            btnDelete = new Button { Text = "Удалить", Location = new Point(230, 370), Width = 100 };
            btnDelete.Click += (s, e) => {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var soldier = (Soldier)dataGridView.SelectedRows[0].DataBoundItem;
                    _logic.DeleteSoldier(soldier.Id);
                    ClearInputs(); RefreshGrid();
                }
            };

            this.Controls.Add(btnAdd); this.Controls.Add(btnUpdate); this.Controls.Add(btnDelete);

            Label lblSearch = new Label { Text = "Поиск по званию:", Location = new Point(10, 420), Width = 110 };
            txtSearchRank = new TextBox { Location = new Point(120, 417), Width = 150 };
            btnSearch = new Button { Text = "Найти", Location = new Point(280, 415), Width = 100 };
            btnSearch.Click += (s, e) => {
                dataGridView.DataSource = null;
                dataGridView.DataSource = _logic.GetSoldiersByRank(txtSearchRank.Text);
            };

            btnGroup = new Button { Text = "Группировать по должностям", Location = new Point(390, 415), Width = 200 };
            btnGroup.Click += (s, e) => {
                var groups = _logic.GroupSoldiersByPosition();
                string result = "Группировка по должностям:\n\n";
                foreach (var g in groups)
                {
                    result += $"Должность: {g.Key}\n";
                    foreach (var sol in g.Value) result += $"  - {sol.FullName} ({sol.Rank})\n";
                    result += "\n";
                }
                MessageBox.Show(result, "Результат");
            };

            this.Controls.Add(lblSearch); this.Controls.Add(txtSearchRank);
            this.Controls.Add(btnSearch); this.Controls.Add(btnGroup);
        }

        private void RefreshGrid()
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = _logic.ReadAllSoldiers();
        }

        private void DataGridView_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var soldier = (Soldier)dataGridView.SelectedRows[0].DataBoundItem;
                txtName.Text = soldier.FullName;
                txtRank.Text = soldier.Rank;
                txtPosition.Text = soldier.Position;
            }
        }

        private void ClearInputs()
        {
            txtName.Clear(); txtRank.Clear(); txtPosition.Clear();
        }
    }
}