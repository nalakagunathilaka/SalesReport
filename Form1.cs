using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
using Microsoft.Office.Interop.Excel;

namespace SalesReport
{
    public partial class Form1 : Form
    {

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        System.Data.DataTable dt = new System.Data. DataTable();
        
        public Form1()
        {
            InitializeComponent();
            fillData();
        }

        public void fillData()
        {
            string selectQuery = "SELECT * FROM sampleinventory.invoice ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void enterbttn_Click(object sender, EventArgs e)
        {
            string fulldate = "";
            string year = yeartxt.Text;
            string month = monthtxt.Text;
            string date = datetxt.Text;

            fulldate = month + "/" + date + "/" + year;
            
            DataView dv = new DataView(dt);
            dv.RowFilter = string.Format("date LIKE '{0}'", fulldate);
            dataGridView1.DataSource = dv;
            dataGridView2.DataSource = dv;
            splitData();
            addExcel();
        }

        public void splitData()
        {
            string detailsstring = "";
            for (int i = 0; i < dataGridView2.RowCount - 1; i++ )
            {
                detailsstring += dataGridView2.Rows[i].Cells[1].Value;
            }
            firststringtxt.Text = detailsstring;
            string[] details = firststringtxt.Text.Split('/');
           
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach(string item in details){
                string[] datas = item.Split(' ');
                dataGridView3.Rows.Add(datas);
            }
            
        }

        public void addExcel()
        {
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)excel.ActiveSheet;
            excel.Visible = true;

            ws.Cells[1, 1] = "Product Code";
            ws.Cells[1, 2] = "Product";
            ws.Cells[1, 3] = "Quantity";
            ws.Cells[1, 4] = "Price";
            ws.Cells[1, 5] = "Total";

            for (int j = 2; j < dataGridView3.Rows.Count; j++)
            {
                for (int i = 1; i <= 5; i++)
                {
                    ws.Cells[j,i] = dataGridView3.Rows[j-2].Cells[i-1].Value;
                }
            }
        }
 
    }
}
