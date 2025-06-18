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
using System.IO;

namespace Rama
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string connectionString = "datasource=localhost;port=3307;username=root;password=;database=rama;";
            string query = "Select * from ciudades";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;

            comboBox1.Items.Clear();
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(0) + " - " + reader.GetString(1));
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron datos.");
                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int TOPE = 0;
        PictureBox[] globos = new PictureBox[30];
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id_ciudad = comboBox1.SelectedItem.ToString().Substring(0,
                comboBox1.SelectedItem.ToString().IndexOf(' '));
            if(!File.Exists(id_ciudad + ".png"))
            {
                MessageBox.Show("No se encontró el mapa de la ciudad:\n" 
                    + comboBox1.SelectedItem.ToString());
                return;
            }
            Bitmap bmp = new Bitmap(id_ciudad + ".png");
            Bitmap v = new Bitmap("globo_verde.png");
            Bitmap a = new Bitmap("globo_amarillo.png");
            Bitmap n = new Bitmap("globo_naranja.png");
            Bitmap r = new Bitmap("globo_rojo.png");
            Bitmap m = new Bitmap("globo_morado.png");
            v.MakeTransparent(Color.White);
            a.MakeTransparent(Color.White);
            n.MakeTransparent(Color.White);
            r.MakeTransparent(Color.White);
            m.MakeTransparent(Color.White);
            for (int i = 0; i < 30; i++)
            {
                globos[i] = new System.Windows.Forms.PictureBox();
                globos[i].Location = new System.Drawing.Point(190, 55);
                globos[i].Name = "pictureBox_globo" + i.ToString();
                globos[i].Size = new System.Drawing.Size(29, 50);
                globos[i].SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
                globos[i].MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
                globos[i].MouseLeave += new System.EventHandler(this.pictureBox2_MouseLeave);
                globos[i].Click += new System.EventHandler(this.pictureBox2_Click);
                globos[i].Cursor = System.Windows.Forms.Cursors.Hand;
                globos[i].TabIndex = 4 + i;
                globos[i].TabStop = false;
                globos[i].Visible = false;
                this.Controls.Add(globos[i]);
            }
            Graphics g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            string connectionString = "datasource=localhost;port=3307;username=root;password=;database=rama;";
            string query = "Select * from estaciones where id_ciudad=" + id_ciudad;
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    TOPE = 0;
                    int posX, posY;
                    comboBox2.Items.Clear();
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(1));
                        posX = Convert.ToInt16(reader.GetString(3));
                        posY = Convert.ToInt16(reader.GetString(4));
                        globos[TOPE].Image = r; //Seleccionar el globo
                        globos[TOPE].Left = panel1.Left + posX;
                        globos[TOPE].Top = panel1.Top + posY;
                        globos[TOPE].Tag = reader.GetString(1);
                        globos[TOPE].Visible = true;
                        TOPE++;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron datos.");
                }
                panel1.SendToBack();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = 0;
            listBox1.Items.Clear();
            listBox1.Items.Add("Procesando datos.");
            listBox1.Refresh();
            string cad, res, archivo = "descargas\\materias.csv";
            string[] arr = new string[9];
            StreamReader arch = new StreamReader(archivo);
            cad = arch.ReadLine(); //encabezado
            while (cad != "") //cada linea
            {
                cad = arch.ReadLine();
                if (cad == null) break;
                arr = cad.Split(',');
                //MessageBox.Show("Contenido:\n" + arr[0] + "\n" + arr[1] + "\n" + arr[2] + "\n"
                //    + arr[3] + "\n" + arr[4] + "\n" + arr[5] + "\n" + arr[6] + "\n"
                //    + arr[7] + "\n" + arr[8] + "\n");
                try {
                    n = Convert.ToInt16(arr[6]);
                    res = arr[6];
                } catch (Exception)
                {
                    res = "nulo";
                }
                if (n > 9)
                {
                    listBox1.Items.Add("Dato: " + arr[0] + " n_unidades: " + res);
                    //listBox1.Refresh();
                    Bitmap m = new Bitmap("globo_morado.png");
                    globos[0].Image = m;
                }
            }
            arch.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;
            label3.Text = "Estación: " + clickedPictureBox.Tag.ToString();
            comboBox2.Text = clickedPictureBox.Tag.ToString();
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;
            label3.Text = "Estación: " + clickedPictureBox.Tag.ToString();
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            label3.Text = "";
        }
    }
}
