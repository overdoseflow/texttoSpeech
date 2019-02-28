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
using System.IO;

namespace ResimdenYaziyaYazidanSese
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string resimPath;
        public string constring = "Data Source=LAPTOP-4KJEGQSU\\SQLEXPRESS;Initial Catalog=data;Integrated Security=True";




        private void button1_Click_1(object sender, EventArgs e)
        {
            {

                openFileDialog1.Title = "Resim Aç";

                openFileDialog1.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Gif Dosyası (*.gif)|*.gif|Png Dosyası (*.png)|*.png|Tif Dosyası (*.tif)|*.tif";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)

                {

                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);

                    resimPath = openFileDialog1.FileName.ToString();

                }

            }

          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Resimimizi FileStream metoduyla okuma modunda açıyoruz.

            FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read);

            //BinaryReader ile byte dizisi ile FileStream arasında veri akışı sağlanıyor.

            BinaryReader br = new BinaryReader(fs);

            /*ReadBytes ile FileStreamde belirtilen resim dosyasındaki byte lar

            byte dizisine aktarılıyor.

            */

            byte[] resim = br.ReadBytes((int)fs.Length);
            int sayac=0;
            sayac = sayac + 1;
            int id = sayac;
            string yazi;
            yazi = richTextBox1.Text;


            br.Close();

            fs.Close();

            //Sql Veritabanı ve Kayıt işlemleri

            SqlConnection bag = new SqlConnection("Data Source=LAPTOP-4KJEGQSU\\SQLEXPRESS;Initial Catalog=data;Integrated Security=True");

            SqlCommand kmt = new SqlCommand("insert into resimbil(resim) Values (@image) ", bag);
            


            kmt.Parameters.Add("@image", SqlDbType.Image, resim.Length).Value = resim;

            try

            {

                bag.Open();

                kmt.ExecuteNonQuery();

                MessageBox.Show(" Veritabanına kayıt yapıldı.");

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message.ToString());

            }

            finally

            {

                bag.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataDataSet1.resimbil' table. You can move, or remove it, as needed.
            this.resimbilTableAdapter1.Fill(this.dataDataSet1.resimbil);
            // TODO: This line of code loads data into the 'dataDataSet.resimbil' table. You can move, or remove it, as needed.
            this.resimbilTableAdapter.Fill(this.dataDataSet.resimbil);

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string yazi = richTextBox1.Text;
            SqlConnection bag = new SqlConnection("Data Source=LAPTOP-4KJEGQSU\\SQLEXPRESS;Initial Catalog=data;Integrated Security=True");
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string q = "insert into resimbil(yazi)values('" +yazi.ToString()+"')";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Veritabanına kaydettiniz :)");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 frm = new Form2();
            frm.Show();
        }
    }
}

    

