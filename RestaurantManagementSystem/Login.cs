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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            Con = new Functions();
        }

        Functions Con;

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Data !!!");
            } else
            {
                string pwd = PasswordHash.HashPassword(PasswordTb.Text);
                string Query = "select * from UsersTbl where UName = N'{0}' and UPass = '{1}'";
                Query = string.Format(Query, UnameTb.Text, pwd);
                DataTable dt = Con.GetData(Query);
                if (dt.Rows.Count == 0) 
                {
                    MessageBox.Show("Missing Data !!!");
                } else
                {
                    if (UnameTb.Text == "admin")
                    {
                        Dashboard Obj = new Dashboard();
                        Obj.Show();
                        this.Hide();
                    }
                    else
                    {
                        Billing Obj = new Billing(UnameTb.Text);
                        Obj.Show();
                        this.Hide();
                    }
                }
            }
        }
    }
}
