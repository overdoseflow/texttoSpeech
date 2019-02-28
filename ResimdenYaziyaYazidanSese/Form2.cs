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
using System.Speech.Synthesis;

namespace ResimdenYaziyaYazidanSese
{
    public partial class Form2 : Form
    {
        SpeechSynthesizer reader = new SpeechSynthesizer();
        public Form2()
        {
            InitializeComponent();
        }

       
        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataDataSet4.resimbil' table. You can move, or remove it, as needed.
            this.resimbilTableAdapter1.Fill(this.dataDataSet4.resimbil);


            label1.Visible = false;
            reader = new SpeechSynthesizer(); //yeni obje 
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        
            // TODO: This line of code loads data into the 'dataDataSet2.resimbil' table. You can move, or remove it, as needed.
            this.resimbilTableAdapter.Fill(this.dataDataSet2.resimbil);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            reader.Dispose();
            if (comboBox1.Text != "")    //eğer metin kutusu boş değilse
            {

                reader = new SpeechSynthesizer();
                reader.SpeakAsync(comboBox1.Text);
                label2.Visible = true;
                label2.Text = "KONUŞUYOR...";
                button2.Enabled = true;
                button4.Enabled = true;
                reader.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(reader_SpeakCompleted);
            }
            else
            {
                MessageBox.Show("Lütfen metin kutusuna birşeyler yazın.", "Message", MessageBoxButtons.OK);
            }

        }

        private void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Speaking)
                {
                    label2.Visible = true;
                    reader.Pause();
                    label2.Text = "DURDURULDU";
                    button3.Enabled = true;

                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Paused)
                {
                    reader.Resume();
                    label2.Visible = true;
                    label2.Text = "KONUŞUYOR...";
                }
                button3.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                reader.Dispose();
                label2.Visible = true;
                label2.Text = "BEKLİYOR...";
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=LAPTOP-4KJEGQSU\\SQLEXPRESS;Initial Catalog=data;Integrated Security=True";
            string resimsil = "DELETE resimbil" + "WHERE resim";
            SqlCommand komut = new SqlCommand(resimsil,baglanti);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 formbir = new Form1();
            this.Hide();
            formbir.Show();
        }
    }
    }

