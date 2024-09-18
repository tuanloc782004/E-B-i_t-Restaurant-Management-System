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
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
            Con = new Functions();
            ShowCategories();
            CategoriesList.SelectionChanged -= CategoriesList_SelectionChanged;

            CategoriesList.CellContentClick += CategoriesList_CellContentClick;
        }

        Functions Con;

        private void ShowCategories()
        {
            try
            {
                string Query = "select * from CategoryTbl";
                CategoriesList.DataSource = Con.GetData(Query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (CatNameTb.Text == "" || DescTb.Text == "")
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {
                try
                {
                    string Category = CatNameTb.Text;
                    string Desc = DescTb.Text;
                    string Query = "insert into CategoryTbl values(N'{0}',N'{1}')";
                    Query = string.Format(Query, Category, Desc);
                    Con.SetData(Query);
                    ShowCategories();
                    MessageBox.Show("Category Added !!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        int key = 0;

        private void CategoriesList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatNameTb.Text = CategoriesList.SelectedRows[0].Cells[1].Value.ToString();
            DescTb.Text = CategoriesList.SelectedRows[0].Cells[2].Value.ToString();
            if (CatNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CategoriesList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void CategoriesList_SelectionChanged(object? sender, EventArgs e)
        {
            if (CategoriesList.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = CategoriesList.SelectedRows[0];
                CatNameTb.Text = Convert.ToString(selectedRow.Cells[1].Value ?? "");
                DescTb.Text = Convert.ToString(selectedRow.Cells[2].Value ?? "");
                key = Convert.ToInt32(selectedRow.Cells[0].Value);
            }
            else
            {
                CatNameTb.Text = "";
                DescTb.Text = "";
                key = 0;
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (CatNameTb.Text == "" || DescTb.Text == "")
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {
                try
                {
                    string Category = CatNameTb.Text;
                    string Desc = DescTb.Text;
                    string Query = "update CategoryTbl set CatName = N'{0}',CatDesc = N'{1}' where CatCode = {2}";
                    Query = string.Format(Query, Category, Desc, key);
                    Con.SetData(Query);
                    ShowCategories();
                    MessageBox.Show("Category Updated !!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {
                try
                {
                    string Category = CatNameTb.Text;
                    string Desc = DescTb.Text;
                    string Query = "delete from CategoryTbl where CatCode = {0}";
                    Query = string.Format(Query, key);
                    Con.SetData(Query);
                    ShowCategories();
                    MessageBox.Show("Category Deleted !!!");
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

        private void label2_Click(object sender, EventArgs e)
        {
            Items Obj = new Items();
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
