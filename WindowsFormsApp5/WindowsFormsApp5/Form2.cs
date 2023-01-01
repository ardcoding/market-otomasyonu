using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp5
{
    public partial class Form2 : Form
    {
        static string constring = "Data Source=LAPTOP-2K4RN5DR\\MSSQLSERVER01;Initial Catalog=marketapp;Integrated Security=True";
        SqlConnection connect = new SqlConnection(constring);
        SqlCommand com = new SqlCommand();
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            çalışandata.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
        }
        public void managerData(int id)
        {
            connect.Open();
            string getir = "Select Manager_ID, Name, Surname, Store_ID from Manager_Table where Manager_ID='" + id + "'";
            SqlCommand komut = new SqlCommand(getir, connect);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            MessageBox.Show("Yönetici Paneline Giriş Yapıldı");
            dataGridView2.DataSource = dt;
            connect.Close();
        }
        void çalışanBilgi()
        {
            connect.Open();
            string getir = "Select Cashier_ID, Name, Surname, PhoneNumber, Address, SellerScore, Store_ID From Cashier_Table";
            SqlCommand komut = new SqlCommand(getir, connect);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            çalışandata.DataSource = dt;
            çalışandata.Visible=true;
            connect.Close();
        }
        void mağazaBilgi()
        {
            connect.Open();
            string getir = "Select * From Store_Table";
            SqlCommand komut = new SqlCommand(getir, connect);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            çalışandata.DataSource = dt;
            çalışandata.Visible = true;
            connect.Close();
        }
        public void ürünBilgi(System.Windows.Forms.DataGridView data)
        {
            connect.Open();
            string getir = "Select BarkodNo, Name, Quantity, Price, ExpiryDate From Product_Table";
            SqlCommand komut = new SqlCommand(getir, connect);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            data.DataSource = dt;
            data.Visible = true;
            connect.Close();
        }
        void tedarikçiBilgi()
        {
            connect.Open();
            string getir = "Select * From Supplier_Table";
            SqlCommand komut = new SqlCommand(getir, connect);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            çalışandata.DataSource = dt;
            çalışandata.Visible = true;
            connect.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            çalışanBilgi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            tedarikçiBilgi();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible=false;   
            mağazaBilgi();
        }
        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void çalışandata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            connect.Open();
            string kayit = "insert into Cashier_Table (Cashier_ID, Name, Surname, Address, PhoneNumber, SellerScore, Password) values(@Cashier_ID,@Name, @Surname, @Address, @PhoneNumber, @SellerScore, @Password)";
            SqlCommand komut = new SqlCommand(kayit, connect);
            komut.Parameters.AddWithValue("@Cashier_ID", textBox6.Text);
            komut.Parameters.AddWithValue("@Name", textBox1.Text);
            komut.Parameters.AddWithValue("@Surname", textBox2.Text);
            komut.Parameters.AddWithValue("@Address", textBox3.Text);
            komut.Parameters.AddWithValue("@PhoneNumber", textBox4.Text);
            komut.Parameters.AddWithValue("@SellerScore", 0);
            komut.Parameters.AddWithValue("@Password", "null");
            komut.ExecuteNonQuery();
            connect.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            MessageBox.Show("Kayıt Başarıyla Eklendi");
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            connect.Open();
            cmd.Connection = connect;
            cmd.CommandText = "delete from Cashier_Table where Cashier_ID='" + textBox7.Text + "'And Name ='" + textBox8.Text + "'";
            cmd.ExecuteNonQuery();
            connect.Close();
            textBox7.Text = "";
            textBox8.Text = "";
            MessageBox.Show("Kayıt Başarıyla Silindi");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
            groupBox4.Visible = false; 
            textBox7.Visible = true;
            textBox8.Visible = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            ürünBilgi(çalışandata);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int salary;
            int sscore;
            connect.Open();
            string con = "Select * from Cashier_Table where Cashier_ID='" + textBox9.Text + "'";
            SqlCommand k = new SqlCommand(con, connect);
            SqlDataAdapter ad2 = new SqlDataAdapter(k);
            DataTable dt2 = new DataTable();
            ad2.Fill(dt2);
            connect.Close();
            sscore = Convert.ToInt32(dt2.Rows[0]["SellerScore"]);
            salary = Convert.ToInt32(dt2.Rows[0]["Salary"]);
            SqlCommand cmd2 = new SqlCommand();
            connect.Open();
            cmd2.Connection = connect;
            salary = salary+sscore;
            cmd2.CommandText = "Update Cashier_Table SET Salary='" + salary + "'where Cashier_ID='" + textBox9.Text + "'";
            cmd2.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Maaş Güncellendi");

            connect.Open();
            string puan = "Select SellerScore from Cashier_Table where Cashier_ID='" + textBox9.Text + "'";
            SqlCommand k1 = new SqlCommand(puan, connect);
            SqlDataAdapter ad3 = new SqlDataAdapter(k1);
            DataTable dt3 = new DataTable();
            ad3.Fill(dt3);
            connect.Close();
            sscore = Convert.ToInt32(dt3.Rows[0]["SellerScore"]);
            SqlCommand cmd3 = new SqlCommand();
            connect.Open();
            cmd3.Connection = connect;
            sscore=0;
            cmd3.CommandText = "Update Cashier_Table SET SellerScore='" + sscore + "'where Cashier_ID='" + textBox9.Text + "'";
            cmd3.ExecuteNonQuery();
            connect.Close();
        }
        void çalışanBilgi2()
        {
            connect.Open();
            string getir = "Select Cashier_ID, Name, Surname, PhoneNumber, Address, SellerScore, Store_ID, Salary From Cashier_Table";
            SqlCommand komut = new SqlCommand(getir, connect);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Visible = true;
            connect.Close();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            çalışanBilgi2();
        }
    }
}
