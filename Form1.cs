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

namespace SalesReport
{
    public partial class Form1 : Form
    {

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        DataTable dt = new DataTable();
        
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
        }

        

        
    }
}
