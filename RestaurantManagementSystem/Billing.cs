using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RestaurantManagementSystem
{
    public partial class Billing : Form
    {
        public Billing(string userName)
        {
            InitializeComponent();
            Con = new Functions();
            UNameLbl.Text = userName;
            ShowItems();
            ItemsList.SelectionChanged -= ItemsList_SelectionChanged;
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

        int n = 0;
        int GrdTotal = 0;

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Missing Data !!!");
            }
            else
            {
                int Total = Convert.ToInt32(PriceTb.Text) * Convert.ToInt32(QtyTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ItemTb.Text;
                newRow.Cells[2].Value = PriceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = Total;
                BillDGV.Rows.Add(newRow);
                n++;
                GrdTotal = GrdTotal + Total;
                GrdTotalLbl.Text = GrdTotal + "000 VND";
            }
        }

        int key = 0;

        private void ItemsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItemsList.SelectionChanged += ItemsList_SelectionChanged;
            ItemTb.Text = ItemsList.SelectedRows[0].Cells[1].Value.ToString();
            PriceTb.Text = ItemsList.SelectedRows[0].Cells[2].Value.ToString();
            if (ItemTb.Text == "")
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
                ItemTb.Text = Convert.ToString(selectedRow.Cells[1].Value ?? "");
                PriceTb.Text = Convert.ToString(selectedRow.Cells[2].Value ?? "");
                key = Convert.ToInt32(selectedRow.Cells[0].Value);
            }
            else
            {
                ItemTb.Text = "";
                PriceTb.Text = "";
                key = 0;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo một hóa đơn mới
                string insertBillQuery = "INSERT INTO BillTbl (UName, BDate, BAmount) VALUES (N'{0}', GETDATE(), {1});";
                insertBillQuery = string.Format(insertBillQuery, UNameLbl.Text, GrdTotal);
                Con.SetData(insertBillQuery);

                // Lấy BillID của hóa đơn vừa được tạo
                string getBillIDQuery = "SELECT MAX(BNum) FROM BillTbl;";
                int billID = (int)Con.GetData(getBillIDQuery).Rows[0][0];

                // Lặp qua các mục trong DataGridView và thêm chúng vào bảng BillDetailTbl
                foreach (DataGridViewRow row in BillDGV.Rows)
                {
                    if (!row.IsNewRow && row.Cells[1].Value != null && row.Cells[2].Value != null && row.Cells[3].Value != null)
                    {
                        string itemName = row.Cells[1].Value.ToString();
                        int quantity = Convert.ToInt32(row.Cells[3].Value);
                        int price = Convert.ToInt32(row.Cells[2].Value);
                        string insertDetailQuery = "INSERT INTO BillDetailTbl (BillId, ItName, Quantity, Price) VALUES ({0}, N'{1}', {2}, {3});";
                        insertDetailQuery = string.Format(insertDetailQuery, billID, itemName, quantity, price);
                        Con.SetData(insertDetailQuery);
                    }
                }
                MessageBox.Show("Bill saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            BillDGV.Rows.Clear();
            GrdTotal = 0;
            GrdTotalLbl.Text = "0 VND";
            n = 0;
        }
    }
}
