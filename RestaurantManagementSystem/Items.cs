using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
            Con = new Functions();
            ShowItems();
            GetCategories();
            ItemsList.SelectionChanged -= ItemsList_SelectionChanged;
            ItemsList.CellContentClick += ItemsList_CellContentClick;
        }

        Functions Con;

        private void ShowItems()
        {
            try
            {
                string Query = "select * from ItemTbl";
                ItemsList.DataSource = Con.GetData(Query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetCategories()
        {
            try
            {
                string Query = "SELECT DISTINCT CatCode, TRIM(CatName) AS CatName FROM CategoryTbl";
                DataTable categoriesTable = Con.GetData(Query);

                CatCb.DataSource = categoriesTable;

                CatCb.ValueMember = "CatCode";
                CatCb.DisplayMember = "CatName";

                if (categoriesTable.Rows.Count > 0)
                {
                    CatCb.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || PriceTb.Text == "" || CatCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Category = CatCb.SelectedValue.ToString();
                    int Price = Convert.ToInt32(PriceTb.Text);
                    string Query = "insert into ItemTbl values(N'{0}', N'{1}', N'{2}')";
                    Query = string.Format(Query, Name, Price, Category);
                    Con.SetData(Query);
                    ShowItems();
                    MessageBox.Show("Item Added !!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        int key = 0;

        private void ItemsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NameTb.Text = ItemsList.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.Text = ItemsList.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = ItemsList.SelectedRows[0].Cells[2].Value.ToString();
            if (NameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ItemsList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void ItemsList_SelectionChanged(object? sender, EventArgs e)
        {
            if (ItemsList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = ItemsList.SelectedRows[0];
                NameTb.Text = Convert.ToString(selectedRow.Cells[1].Value ?? "");
                CatCb.Text = Convert.ToString(selectedRow.Cells[3].Value ?? "");
                PriceTb.Text = Convert.ToString(selectedRow.Cells[2].Value ?? "");
                key = Convert.ToInt32(selectedRow.Cells[0].Value);
            }
            else
            {
                NameTb.Text = "";
                CatCb.Text = "";
                PriceTb.Text = "";
                key = 0;
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || PriceTb.Text == "" || CatCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Category = CatCb.SelectedValue.ToString();
                    int Price = Convert.ToInt32(PriceTb.Text);
                    string Query = "update ItemTbl set ItName = N'{0}', ItPrice = N'{1}', ItCategory = N'{2}' where ItNum = N'{3}'";
                    Query = string.Format(Query, Name, Price, Category, key);
                    Con.SetData(Query);
                    ShowItems();
                    MessageBox.Show("Item Edited !!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Category = CatCb.SelectedValue.ToString();
                    int Price = Convert.ToInt32(PriceTb.Text);
                    string Query = "delete from ItemTbl where ItNum = {0}";
                    Query = string.Format(Query, key);
                    Con.SetData(Query);
                    ShowItems();
                    MessageBox.Show("Item Deleted !!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }
    }
}
