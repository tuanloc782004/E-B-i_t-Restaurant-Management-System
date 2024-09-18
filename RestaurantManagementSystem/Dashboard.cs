using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            Con = new Functions();
            Count();
            ShowBill();
        }

        Functions Con;

        private void Count()
        {
            try
            {
                string countItem = "SELECT COUNT(*) FROM ItemTbl";
                int numI = (int)Con.GetData(countItem).Rows[0][0];
                NumILbl.Text = numI.ToString();

                string countCategory = "SELECT COUNT(*) FROM CategoryTbl";
                int numC = (int)Con.GetData(countCategory).Rows[0][0];
                NumCLbl.Text = numC.ToString();

                string countUser = "SELECT COUNT(*) FROM UsersTbl";
                int numU = (int)Con.GetData(countUser).Rows[0][0];
                NumULbl.Text = numU.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowBill()
        {
            try
            {
                // Lấy ngày được chọn từ DateTimePicker
                DateTime selectedDate = BDateTn.Value.Date;

                // Format lại ngày để sử dụng trong truy vấn SQL
                string formattedDate = selectedDate.ToString("yyyy-MM-dd");

                // Truy vấn SQL để lấy danh sách hóa đơn cho ngày được chọn
                string query = $"SELECT * FROM BillTbl WHERE CONVERT(date, BDate) = '{formattedDate}'";
                BillsList.DataSource = Con.GetData(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowBillDetails(int billId)
        {
            try
            {
                string query = $"SELECT ItName, Quantity, Price FROM BillDetailTbl WHERE BillId = {billId}";
                DataTable billDetails = Con.GetData(query);

                // Xóa các cột hiện tại trong BillDetailsList (nếu có)
                BillDetailsList.Columns.Clear();

                // Tạo và thêm các cột mới vào BillDetailsList
                BillDetailsList.Columns.Add("ItName", "Item Name");
                BillDetailsList.Columns.Add("Quantity", "Quantity");
                BillDetailsList.Columns.Add("Price", "Price");

                // Tính tổng giá trị hóa đơn
                int totalBill = 0;

                // Thêm dữ liệu vào BillDetailsList từ DataTable và tính tổng giá trị hóa đơn
                foreach (DataRow row in billDetails.Rows)
                {
                    BillDetailsList.Rows.Add(row["ItName"], row["Quantity"], row["Price"]);
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    int price = Convert.ToInt32(row["Price"]);
                    totalBill += quantity * price;
                }

                // Hiển thị tổng giá trị hóa đơn trong TotalBillLbl
                TotalBillLbl.Text = totalBill.ToString() + "000 VND";

                // Hiển thị tổng BAmount ngày đó trong TotalDayLbl
                DateTime selectedDate = BDateTn.Value.Date;
                string formattedDate = selectedDate.ToString("yyyy-MM-dd");
                string totalDayQuery = $"SELECT SUM(BAmount) FROM BillTbl WHERE CONVERT(date, BDate) = '{formattedDate}'";
                int totalDayAmount = Convert.ToInt32(Con.GetData(totalDayQuery).Rows[0][0]);
                TotalDayLbl.Text = totalDayAmount.ToString() + "000 VND";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void label12_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Items Obj = new Items();
            Obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void BDateTn_ValueChanged(object sender, EventArgs e)
        {
            ShowBill();
        }

        private void BillsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = BillsList.Rows[e.RowIndex];
                    int billId = Convert.ToInt32(row.Cells["BNum"].Value); // Lấy giá trị BNum từ hàng đã chọn
                    ShowBillDetails(billId); // Hiển thị chi tiết hóa đơn dựa trên BillId
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
