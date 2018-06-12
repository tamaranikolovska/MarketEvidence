using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {  
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'marketEvidenceDataSet3.Warehouse' table. You can move, or remove it, as needed.
            this.warehouseTableAdapter.Fill(this.marketEvidenceDataSet3.Warehouse);
            // TODO: This line of code loads data into the 'marketEvidenceDataSet3.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter2.Fill(this.marketEvidenceDataSet3.Product);
            // TODO: This line of code loads data into the 'marketEvidenceDataSet1.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter1.Fill(this.marketEvidenceDataSet1.Product);
            // TODO: This line of code loads data into the 'marketEvidenceDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.marketEvidenceDataSet.Product);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=MarketEvidence;Integrated Security=True");
            con.Open();
            SqlCommand sqlc = new SqlCommand("select Code, Name, Descriptionn, Price, quantity" +
                " from Product where Name = @productname", con);
            sqlc.Parameters.AddWithValue("@productname", comboBox1.Text);
            SqlDataReader reader;
            reader = sqlc.ExecuteReader();
            if (reader.Read())
            {
                richTextBox1.Text = reader.GetValue(2).ToString() + " - " + reader.GetValue(3).ToString() + "den." + " - " + reader.GetValue(4).ToString();
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=MarketEvidence;Integrated Security=True");
            con.Open();
            SqlCommand sqlc = new SqlCommand("update Product set quantity = @productquantity where Name = @productname", con);
            sqlc.Parameters.AddWithValue("@productname", comboBox1.Text);
            sqlc.Parameters.AddWithValue("@productquantity", Convert.ToInt32(textBox1.Text));
            sqlc.ExecuteNonQuery();
            con.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=MarketEvidence;Integrated Security=True");
            con.Open();
            SqlCommand sqlc = new SqlCommand("insert into Product(Name, Descriptionn, Price, quantity) values(@productname,@productdescr,@productprice,@productquantity)", con);
            sqlc.Parameters.AddWithValue("@productname", textBox2.Text);
            sqlc.Parameters.AddWithValue("@productdescr", richTextBox2.Text);
            sqlc.Parameters.AddWithValue("@productprice", Convert.ToInt32(textBox3.Text));
            sqlc.Parameters.AddWithValue("@productquantity", Convert.ToInt32(textBox4.Text));
            sqlc.ExecuteNonQuery();
            textBox2.Text = "";
            richTextBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            Invalidate();
            con.Close();
        }
    }
}
