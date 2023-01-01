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
    public partial class Form1 : Form
    {
        SqlDataReader dr;

        int a;
        bool isLogin;
        int ürün;
        int toplam = 0;
        string alınanürünler;
        string idcashier;
        string mid;
        bool mlogin;
        System.Windows.Forms.TextBox active;

        public Form1()
        {
            InitializeComponent();
        }
        static string constring = "Data Source=LAPTOP-2K4RN5DR\\MSSQLSERVER01;Initial Catalog=marketapp;Integrated Security=True";
        SqlConnection connect = new SqlConnection(constring);
        SqlCommand com = new SqlCommand();

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                login(int.Parse(textBox2.Text), textBox1.Text);
            }
        }
        public void login(int id, string password)
        {
            int c_id = id;
            a = c_id;
            connect.Close();
            connect.Open();
            com.Connection = connect;
            com.CommandText = "SELECT Name, Password FROM Cashier_Table where Cashier_ID='" + id + "'And password ='" + password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                panel1.Visible = true;
                panel2.Visible = false;
                groupBox4.Visible = false;
                groupBox5.Visible = false;
                groupBox6.Visible = false;
                groupBox7.Visible = false;
                groupBox8.Visible = false;
                connect.Close();
                cashierData(id);
                textBox2.Text = "";
                textBox1.Text = "";
                isLogin = true;
                idcashier = id.ToString();
            }
            else
            {
                connect.Close();
                connect.Open();
                com.CommandText = "SELECT Name, Password FROM Cashier_Table where Cashier_ID='" + id + "'And password ='" + "null" + "'";
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("İlk Giriş");
                    groupBox4.Visible = true;
                }
                else
                {
                    connect.Close();
                    connect.Open();
                    com.CommandText = "SELECT Name, Password FROM Manager_Table where Manager_ID='" + id + "'And password ='" + password + "'";
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        Form2 form2 = new Form2();
                        form2.Show();
                        form2.managerData(id);
                        textBox2.Text = "";
                        textBox1.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı ID veya Şifre Hatalı");
                    }
                }
                connect.Close();
            }
        }

        void cashierData(int id)
        {
            connect.Open();
            string getir = "Select Cashier_ID, Name, Surname, SellerScore from Cashier_Table where Cashier_ID='" + id + "'";
            SqlCommand komut = new SqlCommand(getir, connect);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            MessageBox.Show("Giriş Yapıldı");
            dataGridView1.DataSource = dt;
            connect.Close();
            panel1.Visible = true;
            panel2.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            active.Text = active.Text + "4";
        }

        private void button25_Click(object sender, EventArgs e)
        {
            receipt("nakit");
            payment();
            textBox5.Text = "";
            textBox6.Text = "";
            textBox19.Text = "";
            textBox18.Text = "";
            textBox17.Text = "";
            textBox7.Text = "";
            toplam = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (isLogin)
            {
                panel2.Visible = false;
                panel1.Visible = false;
                groupBox4.Visible = false;
                groupBox5.Visible = false;
                groupBox6.Visible = false;
                groupBox7.Visible = false;
                groupBox8.Visible = true;
                dataGridView2.Visible = true;
                Form2 form2 = new Form2();
                form2.ürünBilgi(dataGridView2);
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;
            groupBox7.Visible = false;
            groupBox8.Visible = false;
            isLogin = false;
            label20.Visible = false;
            label22.Visible = false;
            label21.Visible = false;
            button39.Visible = false;
            textBox19.Visible = false;
            textBox18.Visible = false;
            textBox17.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == false)
            {
                panel2.Visible = true;
                panel1.Visible = false;
                dataGridView1.DataSource = null;
                groupBox5.Visible = false;
                groupBox4.Visible = false;
                groupBox6.Visible = false;
                groupBox7.Visible = false;
                groupBox8.Visible = false;
            }
            else
            {
                this.Close();
            }
            isLogin = false;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == textBox4.Text)
            {
                SqlCommand cmd = new SqlCommand();
                connect.Open();
                cmd.Connection = connect;
                cmd.CommandText = "Update Cashier_Table SET password='" + textBox3.Text + "'where Cashier_ID='" + a + "'";
                cmd.ExecuteNonQuery();
                connect.Close();
                textBox3.Text = "";
                textBox4.Text = "";
                MessageBox.Show("Parola Belirlendi");
                groupBox4.Visible = false;
            }
            else
            {
                MessageBox.Show("Parolalar Eşleşmiyor", a.ToString());
            }
        }

        public int seçilen()
        {
            connect.Open();
            string soğan = "Select Price from Product_Table where BarkodNo='" + ürün + "'";
            SqlCommand komut = new SqlCommand(soğan, connect);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            connect.Close();
            return Convert.ToInt32(dt.Rows[0]["Price"].ToString());

        }
        public int idseçilen(string p_id)
        {
            connect.Open();
            string prod = "Select Price from Product_Table where BarkodNo='" + p_id + "'";
            SqlCommand komut = new SqlCommand(prod, connect);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            connect.Close();
            return Convert.ToInt32(dt.Rows[0]["Price"].ToString());

        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (isLogin == true)
            {
                if (groupBox6.Visible == true)
                {
                    textBox8.Text = "Sogan";
                }
                else
                {
                    if (groupBox7.Visible == true)
                    {
                        active.Text = "Sogan";
                    }

                    else
                    {
                        if (groupBox8.Visible == true)
                        {
                            textBox20.Text = "sogan";
                        }
                        else
                        {
                            alınanürünler = alınanürünler + "soğan \n";
                            groupBox5.Visible = true;
                            panel1.Visible = true;
                            ürün = 101;
                            textBox7.Text = seçilen().ToString();
                            seçilen();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "" && groupBox7.Visible == false)
            {
                toplam = toplam + Convert.ToInt32(textBox7.Text) * Convert.ToInt32(textBox5.Text);
                textBox6.Text = Convert.ToString(toplam);
            }
            if (groupBox7.Visible == true)
            {
                int ücret;
                connect.Open();
                string fiyat = "Select Quantity, Price from Product_Table where Name='" + textBox10.Text + "'";
                SqlCommand komut = new SqlCommand(fiyat, connect);
                dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    connect.Close();
                    connect.Open();
                    SqlDataAdapter ad = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ücret = Convert.ToInt32(dt.Rows[0]["Price"]);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connect;
                    textBox13.Text = (Convert.ToInt32(textBox12.Text) * ücret).ToString();
                    connect.Close();
                }
                else
                {
                    string f1 = "Select Quantity, Price from Product_Table where BarkodNo='" + textBox10.Text + "'";
                    SqlCommand k1 = new SqlCommand(f1, connect);
                    connect.Close();
                    connect.Open();
                    SqlDataAdapter ad = new SqlDataAdapter(k1);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ücret = Convert.ToInt32(dt.Rows[0]["Price"]);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connect;
                    textBox13.Text = (Convert.ToInt32(textBox12.Text) * ücret).ToString();
                    connect.Close();
                }
                connect.Open();
                string f = "Select Quantity, Price from Product_Table where Name='" + textBox11.Text + "'";
                SqlCommand k = new SqlCommand(f, connect);
                dr = k.ExecuteReader();
                if (dr.Read())
                {
                    connect.Close();
                    connect.Open();
                    SqlDataAdapter ad = new SqlDataAdapter(k);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ücret = Convert.ToInt32(dt.Rows[0]["Price"]);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connect;
                    textBox14.Text = (Convert.ToInt32(textBox15.Text) * ücret).ToString();
                    connect.Close();
                }
                else
                {
                    connect.Close();
                    connect.Open();
                    string f2 = "Select Quantity, Price from Product_Table where BarkodNo='" + textBox11.Text + "'";
                    SqlCommand k2 = new SqlCommand(f2, connect);
                    connect.Close();
                    connect.Open();
                    SqlDataAdapter ad = new SqlDataAdapter(k2);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    ücret = Convert.ToInt32(dt.Rows[0]["Price"]);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connect;
                    textBox14.Text = (Convert.ToInt32(textBox15.Text) * ücret).ToString();
                    connect.Close();
                }


                if (Convert.ToInt32(textBox13.Text) < Convert.ToInt32(textBox14.Text))
                {
                    label19.Text = "Ödenecek Tutar:";
                    textBox16.Text = (Convert.ToInt32(textBox14.Text) - Convert.ToInt32(textBox13.Text)).ToString();
                }
                else
                {
                    label19.Text = "Para Üstü:";
                    textBox16.Text = (Convert.ToInt32(textBox13.Text) - Convert.ToInt32(textBox14.Text)).ToString();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (isLogin)
            {
                panel2.Visible = false;
                panel1.Visible = true;
                groupBox4.Visible = false;
                groupBox5.Visible = true;
                groupBox6.Visible = false;
                groupBox7.Visible = false;
                groupBox8.Visible = false;
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            active.Text = active.Text + "1";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            active.Text = active.Text + "5";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            active.Text = active.Text + "6";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            active.Text = active.Text + "7";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            active.Text = active.Text + "8";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            active.Text = active.Text + "9";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            active.Text = active.Text + "3";
        }
        private void textBox5_Click(object sender, EventArgs e)
        {
            active = textBox5;
        }
        private void button17_Click(object sender, EventArgs e)
        {
            active.Text = active.Text + "2";
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            if (active.Text != "")
            {
                string content = active.Text;
                active.Text = content.Substring(0, content.Length - 1);
            }
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            active.Text = active.Text + "0";
        }
        void receipt(string type)
        {
            MessageBox.Show("Ödeme Yapıldı.\n\nÜrünler\n" + alınanürünler + "\nTutar: " + textBox6.Text + "\nÖdeme Şekli: " + type + "\n\nİyi Günler Dileriz");
        }
        void payment()
        {
            int adet;
            int sellscore;
            int shopscore;
            connect.Open();
            string fiyat = "Select Quantity from Product_Table where BarkodNo='" + ürün + "'";
            SqlCommand komut = new SqlCommand(fiyat, connect);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            connect.Close();
            adet = Convert.ToInt32(dt.Rows[0]["Quantity"]);
            SqlCommand cmd = new SqlCommand();
            connect.Open();
            cmd.Connection = connect;
            adet = adet - Convert.ToInt32(textBox5.Text);
            cmd.CommandText = "Update Product_Table SET Quantity='" + adet + "'where BarkodNo='" + ürün + "'";
            cmd.ExecuteNonQuery();
            connect.Close();

            connect.Open();
            string puan = "Select SellerScore from Cashier_Table where Cashier_ID='" + idcashier + "'";
            SqlCommand k = new SqlCommand(puan, connect);
            SqlDataAdapter ad2 = new SqlDataAdapter(k);
            DataTable dt2 = new DataTable();
            ad2.Fill(dt2);
            connect.Close();
            sellscore = Convert.ToInt32(dt2.Rows[0]["SellerScore"]);
            SqlCommand cmd2 = new SqlCommand();
            connect.Open();
            cmd2.Connection = connect;
            sellscore++;
            cmd2.CommandText = "Update Cashier_Table SET SellerScore='" + sellscore + "'where Cashier_ID='" + idcashier + "'";
            cmd2.ExecuteNonQuery();
            connect.Close();

            if (mlogin == true)
            {
                connect.Open();
                string spuan = "Select ShoppingScore from Customer_Table where Customer_ID='" + mid + "'";
                SqlCommand k3 = new SqlCommand(spuan, connect);
                SqlDataAdapter ad3 = new SqlDataAdapter(k3);
                DataTable dt3 = new DataTable();
                ad3.Fill(dt3);
                connect.Close();
                shopscore = Convert.ToInt32(dt3.Rows[0]["ShoppingScore"]);
                SqlCommand cmd3 = new SqlCommand();
                connect.Open();
                cmd3.Connection = connect;
                shopscore++;
                cmd3.CommandText = "Update Customer_Table SET ShoppingScore='" + shopscore + "'where Customer_ID='" + mid + "'";
                cmd3.ExecuteNonQuery();
                connect.Close();

                mid = "";
                idcashier = "";
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            toplam = Convert.ToInt32(textBox6.Text);

            payment();
            textBox5.Text = " ";
        }

        private void button29_Click(object sender, EventArgs e)
        {
            active = textBox5;
            if (isLogin == true)
            {
                if (groupBox6.Visible == true)
                {
                    textBox8.Text = "uzum";
                }
                else
                {
                    if (groupBox7.Visible == true)
                    {
                        active.Text = "uzum";
                    }

                    else
                    {
                        if (groupBox8.Visible == true)
                        {
                            textBox20.Text = "uzum";
                        }
                        else
                        {
                            alınanürünler = alınanürünler + "üzüm \n";
                            groupBox5.Visible = true;
                            panel1.Visible = true;
                            ürün = 101;
                            textBox7.Text = seçilen().ToString();
                            seçilen();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            active = textBox2;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            active = textBox1;
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isLogin)
            {
                panel2.Visible = false;
                panel1.Visible = false;
                groupBox4.Visible = false;
                groupBox5.Visible = false;
                groupBox7.Visible = false;
                groupBox8.Visible = false;
                groupBox6.Visible = true;
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void textBox8_Click(object sender, EventArgs e)
        {
            active = textBox8;
        }

        private void textBox9_Click(object sender, EventArgs e)
        {
            active = textBox9;
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void button37_Click(object sender, EventArgs e)
        {
            int adet;
            connect.Open();
            string fiyat = "Select Quantity from Product_Table where Name='" + textBox8.Text + "'";
            SqlCommand komut = new SqlCommand(fiyat, connect);
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                SqlDataAdapter ad = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                connect.Close();
                adet = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                SqlCommand cmd = new SqlCommand();
                connect.Open();
                cmd.Connection = connect;
                adet = adet + Convert.ToInt32(textBox9.Text);
                cmd.CommandText = "Update Product_Table SET Quantity='" + adet + "'where Name='" + textBox8.Text + "'";
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            else
            {
                string f = "Select Quantity from Product_Table where BarkodNo='" + textBox8.Text + "'";
                SqlCommand k = new SqlCommand(f, connect);
                connect.Close();
                connect.Open();
                SqlDataAdapter ad = new SqlDataAdapter(k);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                connect.Close();
                adet = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                SqlCommand cmd = new SqlCommand();
                connect.Open();
                cmd.Connection = connect;
                adet = adet + Convert.ToInt32(textBox9.Text);
                cmd.CommandText = "Update Product_Table SET Quantity='" + adet + "'where BarkodNo='" + textBox8.Text + "'";
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            textBox8.Text = "";
            textBox9.Text = "";
            MessageBox.Show("İade Başarıyla Gerçekleştirildi");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isLogin)
            {
                panel1.Visible = false;
                groupBox4.Visible = false;
                groupBox5.Visible = false;
                groupBox6.Visible = false;
                groupBox8.Visible = false;
                groupBox7.Visible = true;
                panel2.Visible = false;
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            int adet;
            connect.Open();
            string fiyat = "Select Quantity from Product_Table where Name='" + textBox10.Text + "'";
            SqlCommand komut = new SqlCommand(fiyat, connect);
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                connect.Close();
                connect.Open();
                SqlDataAdapter ad = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                connect.Close();
                adet = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                SqlCommand cmd = new SqlCommand();
                connect.Open();
                cmd.Connection = connect;
                adet = adet + Convert.ToInt32(textBox12.Text);
                cmd.CommandText = "Update Product_Table SET Quantity='" + adet + "'where Name='" + textBox10.Text + "'";
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            else
            {
                connect.Close();
                connect.Open();
                string f = "Select Quantity from Product_Table where BarkodNo='" + textBox10.Text + "'";
                SqlCommand k = new SqlCommand(f, connect);
                connect.Close();
                connect.Open();
                SqlDataAdapter ad = new SqlDataAdapter(k);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                connect.Close();
                adet = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                SqlCommand cmd = new SqlCommand();
                connect.Open();
                cmd.Connection = connect;
                adet = adet + Convert.ToInt32(textBox12.Text);
                cmd.CommandText = "Update Product_Table SET Quantity='" + adet + "'where BarkodNo='" + textBox10.Text + "'";
                cmd.ExecuteNonQuery();
                connect.Close();
            }

            connect.Open();
            string f1 = "Select Quantity from Product_Table where Name='" + textBox11.Text + "'";
            SqlCommand k1 = new SqlCommand(f1, connect);
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                connect.Close();
                connect.Open();
                SqlDataAdapter ad = new SqlDataAdapter(k1);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                connect.Close();
                adet = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                SqlCommand cmd = new SqlCommand();
                connect.Open();
                cmd.Connection = connect;
                adet = adet - Convert.ToInt32(textBox15.Text);
                cmd.CommandText = "Update Product_Table SET Quantity='" + adet + "'where Name='" + textBox11.Text + "'";
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            else
            {
                connect.Close();
                connect.Open();
                string f2 = "Select Quantity from Product_Table where BarkodNo='" + textBox11.Text + "'";
                SqlCommand k2 = new SqlCommand(f2, connect);
                connect.Close();
                connect.Open();
                SqlDataAdapter ad = new SqlDataAdapter(k2);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                connect.Close();
                adet = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                SqlCommand cmd = new SqlCommand();
                connect.Open();
                cmd.Connection = connect;
                adet = adet - Convert.ToInt32(textBox15.Text);
                cmd.CommandText = "Update Product_Table SET Quantity='" + adet + "'where BarkodNo='" + textBox11.Text + "'";
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            textBox11.Text = "";
            textBox12.Text = "";
            textBox15.Text = "";
            textBox10.Text = "";
            textBox16.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            MessageBox.Show("Değişim Başarıyla Gerçekleştirildi");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
        }

        private void textBox11_Click(object sender, EventArgs e)
        {
            active = textBox11;
        }

        private void textBox10_Click(object sender, EventArgs e)
        {
            active = textBox10;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isLogin)
            {
                label20.Visible = true;
                textBox17.Visible = true;
                button39.Visible = true;
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            connect.Close();
            connect.Open();
            com.Connection = connect;
            com.CommandText = "SELECT ShoppingScore FROM Customer_Table where Customer_ID='" + textBox17.Text + "'";
            SqlDataAdapter ad = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                connect.Close();
                connect.Open();
                int puan;
                label22.Visible = true;
                label21.Visible = true;
                textBox18.Visible = true;
                textBox19.Visible = true;
                ad.Fill(dt);
                connect.Close();
                puan = Convert.ToInt32(dt.Rows[0]["ShoppingScore"]);
                textBox18.Text = puan.ToString();
                textBox19.Text = (Convert.ToInt32(textBox6.Text) - puan).ToString();
                mlogin = true;
                mid = textBox17.Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox6.Text = textBox19.Text;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (isLogin)
            {
                receipt("kredi kartı");
                payment();
                textBox5.Text = "";
                textBox6.Text = "";
                textBox19.Text = "";
                textBox18.Text = "";
                textBox17.Text = "";
                textBox7.Text = "";
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            receipt("banka kartı");
            payment();
            textBox5.Text = "";
            textBox6.Text = "";
            textBox19.Text = "";
            textBox18.Text = "";
            textBox17.Text = "";
            textBox7.Text = "";
        }

        private void button24_Click(object sender, EventArgs e)
        {
            receipt("NFC");
            payment();
            textBox5.Text = "";
            textBox6.Text = "";
            textBox19.Text = "";
            textBox18.Text = "";
            textBox17.Text = "";
            textBox7.Text = "";
        }

        private void button40_Click(object sender, EventArgs e)
        {
            connect.Close();
            connect.Open();
            com.Connection = connect;
            com.CommandText = "SELECT BarkodNo, Name, Quantity, Price, ExpiryDate FROM Product_Table where Name='" + textBox20.Text + "'";
            SqlDataAdapter ad = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                connect.Close();
                connect.Open();
                ad.Fill(dt);
                dataGridView2.DataSource = dt;
                connect.Close();
            }/*
            else
            {
                connect.Close();
                connect.Open();
                com.Connection = connect;
                com.CommandText = "SELECT BarkodNo, Name, Quantity, ExpiryDate, Price FROM Product_Table where BarkodNo='" + textBox20.Text + "'";
                SqlDataAdapter ad2 = new SqlDataAdapter(com);
                DataTable dt2 = new DataTable();
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    connect.Close();
                    connect.Open();
                    ad2.Fill(dt2);
                    dataGridView2.DataSource = dt2;
                    connect.Close();
                }*/
            else
            {
                MessageBox.Show("Hatalı Ürün Girildi");
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            active = textBox5;
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            active = textBox17;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            idseçilen(textBox21.Text);
            textBox7.Text = idseçilen(textBox21.Text).ToString();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (isLogin == true)
            {
                if (groupBox6.Visible == true)
                {
                    textBox8.Text = "karpuz";
                }
                else
                {
                    if (groupBox7.Visible == true)
                    {
                        active.Text = "karpuz";
                    }

                    else
                    {
                        if (groupBox8.Visible == true)
                        {
                            textBox20.Text = "karpuz";
                        }
                        else
                        {
                            alınanürünler = alınanürünler + "karpuz \n";
                            groupBox5.Visible = true;
                            panel1.Visible = true;
                            ürün = 105;
                            textBox7.Text = seçilen().ToString();
                            seçilen();
                        }
                    }
                }
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (isLogin == true)
            {
                if (groupBox6.Visible == true)
                {
                    textBox8.Text = "armut";
                }
                else
                {
                    if (groupBox7.Visible == true)
                    {
                        active.Text = "armut";
                    }

                    else
                    {
                        if (groupBox8.Visible == true)
                        {
                            textBox20.Text = "armut";
                        }
                        else
                        {
                            alınanürünler = alınanürünler + "armut \n";
                            groupBox5.Visible = true;
                            panel1.Visible = true;
                            ürün = 104;
                            textBox7.Text = seçilen().ToString();
                            seçilen();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (isLogin == true)
            {
                if (groupBox6.Visible == true)
                {
                    textBox8.Text = "amasya elması";
                }
                else
                {
                    if (groupBox7.Visible == true)
                    {
                        active.Text = "amasya elması";
                    }

                    else
                    {
                        if (groupBox8.Visible == true)
                        {
                            textBox20.Text = "amasya elması";
                        }
                        else
                        {
                            alınanürünler = alınanürünler + "amasya elması \n";
                            groupBox5.Visible = true;
                            panel1.Visible = true;
                            ürün = 103;
                            textBox7.Text = seçilen().ToString();
                            seçilen();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (isLogin == true)
            {
                if (groupBox6.Visible == true)
                {
                    textBox8.Text = "kivi";
                }
                else
                {
                    if (groupBox7.Visible == true)
                    {
                        active.Text = "kivi";
                    }

                    else
                    {
                        if (groupBox8.Visible == true)
                        {
                            textBox20.Text = "kivi";
                        }
                        else
                        {
                            alınanürünler = alınanürünler + "kivi \n";
                            groupBox5.Visible = true;
                            panel1.Visible = true;
                            ürün = 106;
                            textBox7.Text = seçilen().ToString();
                            seçilen();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (isLogin == true)
            {
                if (groupBox6.Visible == true)
                {
                    textBox8.Text = "kavun";
                }
                else
                {
                    if (groupBox7.Visible == true)
                    {
                        active.Text = "kavun";
                    }

                    else
                    {
                        if (groupBox8.Visible == true)
                        {
                            textBox20.Text = "kavun";
                        }
                        else
                        {
                            alınanürünler = alınanürünler + "kavun \n";
                            groupBox5.Visible = true;
                            panel1.Visible = true;
                            ürün = 109;
                            textBox7.Text = seçilen().ToString();
                            seçilen();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (isLogin == true)
            {
                if (groupBox6.Visible == true)
                {
                    textBox8.Text = "muz";
                }
                else
                {
                    if (groupBox7.Visible == true)
                    {
                        active.Text = "muz";
                    }

                    else
                    {
                        if (groupBox8.Visible == true)
                        {
                            textBox20.Text = "muz";
                        }
                        else
                        {
                            alınanürünler = alınanürünler + "muz \n";
                            groupBox5.Visible = true;
                            panel1.Visible = true;
                            ürün = 107;
                            textBox7.Text = seçilen().ToString();
                            seçilen();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (isLogin == true)
            {
                if (groupBox6.Visible == true)
                {
                    textBox8.Text = "cilek";
                }
                else
                {
                    if (groupBox7.Visible == true)
                    {
                        active.Text = "cilek";
                    }

                    else
                    {
                        if (groupBox8.Visible == true)
                        {
                            textBox20.Text = "cilek";
                        }
                        else
                        {
                            alınanürünler = alınanürünler + "çilek \n";
                            groupBox5.Visible = true;
                            panel1.Visible = true;
                            ürün = 108;
                            textBox7.Text = seçilen().ToString();
                            seçilen();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Giriş Yapın");
            }
        }
    }
}
