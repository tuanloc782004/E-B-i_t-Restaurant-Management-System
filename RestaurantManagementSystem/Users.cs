using System.Text.RegularExpressions;

namespace RestaurantManagementSystem
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            Con = new Functions();
            ShowUsers();
        }

        Functions Con;

        private void ShowUsers()
        {
            try
            {
                string Query = "select * from UsersTbl";
                UsersList.DataSource = Con.GetData(Query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || PasswordTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "" || AddTb.Text == "")
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Gender = GenCb.SelectedItem.ToString();
                    string Password = PasswordHash.HashPassword(PasswordTb.Text);
                    string Phone = PhoneTb.Text;
                    string Address = AddTb.Text;
                    string Query = "insert into UsersTbl values(N'{0}', N'{1}', N'{2}', N'{3}', N'{4}' )";
                    Query = string.Format(Query, Name, Gender, Password, Phone, Address);
                    Con.SetData(Query);
                    ShowUsers();
                    MessageBox.Show("User Added !!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        int key = 0;

        private void UsersList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NameTb.Text = UsersList.SelectedRows[0].Cells[1].Value.ToString();
            GenCb.Text = UsersList.SelectedRows[0].Cells[2].Value.ToString();
            PasswordTb.Text = UsersList.SelectedRows[0].Cells[3].Value.ToString();
            PhoneTb.Text = UsersList.SelectedRows[0].Cells[4].Value.ToString();
            AddTb.Text = UsersList.SelectedRows[0].Cells[5].Value.ToString();
            if (NameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UsersList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || PasswordTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "" || AddTb.Text == "")
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {
                try
                {
                    string Name = NameTb.Text;
                    string Gender = GenCb.SelectedItem.ToString();
                    string Password = PasswordHash.HashPassword(PasswordTb.Text);
                    string Phone = PhoneTb.Text;
                    string Address = AddTb.Text;
                    string Query = "update UsersTbl set UName = N'{0}', UGen = N'{1}', UPass = N'{2}', UPhone = N'{3}', UAddress = N'{4}' where UId = {5}";
                    Query = string.Format(Query, Name, Gender, Password, Phone, Address, key);
                    Con.SetData(Query);
                    ShowUsers();
                    MessageBox.Show("User Updated !!!");
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
                    string Gender = GenCb.SelectedItem.ToString();
                    string Password = PasswordTb.Text;
                    string Phone = PhoneTb.Text;
                    string Address = AddTb.Text;
                    string Query = "delete from UsersTbl where UId = {0}";
                    Query = string.Format(Query, key);
                    Con.SetData(Query);
                    ShowUsers();
                    MessageBox.Show("User Deleted !!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Items Obj = new Items();
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

        private void DashboardLbl_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }
    }
}
