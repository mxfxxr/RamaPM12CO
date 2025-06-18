using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rama
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        double horas = 0, p_CO_ = 0.0;

        private void Form9_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("00 a 7 horas");
            comboBox1.Items.Add("8 a 15 horas");
            comboBox1.Items.Add("16 a 23 horas");
            comboBox1.SelectedIndex = 0;
            dataGridView1.Columns.Add("Hora", "Hora");
            dataGridView1.Columns.Add("cons", "cons");
            dataGridView1.Columns.Add("CO", "CO");
            dataGridView1.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Rows.Clear();
            string cad, fecha = "", lahora = "", horafin = ((comboBox1.SelectedIndex == 0) ? "07" : (comboBox1.SelectedIndex == 8) ? "15" : "23");
            string[] arr = new string[15];
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader arch = new StreamReader(openFileDialog1.FileName);
                    StreamWriter html = new StreamWriter(openFileDialog1.FileName + ".html");
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    html.WriteLine("<html>REPORTE DE PROMEDIO MOVIL PONDERADOS");
                    html.WriteLine("<br><br><table border=1 cellspacing=0>");
                    html.WriteLine("<tr bgcolor=#CCCCCC><td>Fecha</td><td>Porcentaje</td><td>CO</td></tr>");
                    cad = arch.ReadLine();
                    string seg = cad.Substring(0, 10);
                    double C1, suma1, divi1,
                        cmaxCO = 0.0, cminCO = 0.0, wCO = 0.0;
                    while (true)
                    {
                        if (cad == null) break;
                        arr = cad.Split(',');
                        fecha = arr[0].Substring(0, 10);
                        lahora = arr[0].Substring(11, 8);
                        fecha = fecha.Substring(8, 2) + "/" + fecha.Substring(5, 2) + "/" + fecha.Substring(0, 4);

                        //if (fecha == textBox1.Text && lahora.Substring(0, 2) == comboBox1.SelectedItem.ToString().Substring(0, 2))
                        if (fecha == textBox1.Text && lahora.Substring(0, 2) == int.Parse(comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf(" "))).ToString("D2"))
                        {
                            //Extraer los 8 datos
                            for (int i = 0; i < 8 && Convert.ToInt16(horafin) >= Convert.ToInt16(lahora.Substring(0, 2)); i++)
                            {
                                p_CO_ = Convierte(arr[9]);
                                if (p_CO_ > cmaxCO) cmaxCO = p_CO_;
                                if (p_CO_ < cminCO) cminCO = p_CO_;
                                dataGridView1.Rows.Add(lahora, (8 - i).ToString(), p_CO_.ToString());
                                cad = arch.ReadLine();

                                if (cad == null) break;
                                arr = cad.Split(',');
                                fecha = arr[0].Substring(0, 10);
                                lahora = arr[0].Substring(11, 8);
                                fecha = fecha.Substring(8, 2) + "/" + fecha.Substring(5, 2) + "/" + fecha.Substring(0, 4);
                            }
                            wCO = 1 - ((cmaxCO - cminCO) / cmaxCO);
                            textBox2.Text = "w: " + wCO.ToString();
                            if (wCO < 0.5) wCO = 0.5;
                            if (dataGridView1.RowCount < 6)
                            {
                                MessageBox.Show("No hay suficientes datos para el proceso.");
                                arch.Close(); html.Close();
                                return;
                            }
                            C1 = suma1 = divi1 = 0.0;
                            for (int i = dataGridView1.RowCount - 1; i > 0; i--)
                            {
                                suma1 += Convierte(dataGridView1.Rows[i].Cells[2].Value.ToString()) *
                                    (Math.Pow(wCO, Convierte(dataGridView1.Rows[i].Cells[1].Value.ToString()) - 1));
                                divi1 += Math.Pow(wCO, Convierte(dataGridView1.Rows[i].Cells[1].Value.ToString()) - 1);

                            }
                            C1 = suma1 / divi1;
                            textBox2.Text = "C CO: " + C1.ToString();
                            break;
                        }
                        horas++;
                        seg = fecha;
                        cad = arch.ReadLine();
                    }
                    html.WriteLine(Linea(seg));
                    html.WriteLine("</table></html>");
                    arch.Close(); html.Close();
                    //System.Diagnostics.Process.Start("excel", "\"" + openFileDialog1.FileName + ".html\"");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message + "\n" + ex.StackTrace);
                }
            }

        }

        private double Convierte(string arr)
        {
            return ((arr == "null") ? 0 : Convert.ToDouble(arr));
        }

        private string Linea(string seg)
        {
            return (
            "<tr><td>" + seg + "</td><td align=center bgcolor=#FFFF"
            + ((horas < 24) ? "00" : "FF") + ">"
            + ((horas / 24) * 100) + "%" + "</td><td align=center bgcolor=#FFFFFF>"
            + (p_CO_ / horas).ToString("0.000") + "</td></tr>");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            string tempPath = Path.Combine(Path.GetTempPath(), "ReporteTemporal.csv");

            try
            {
                using (StreamWriter sw = new StreamWriter(tempPath, false, Encoding.UTF8))
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        sw.Write(dataGridView1.Columns[i].HeaderText);
                        if (i < dataGridView1.Columns.Count - 1) sw.Write(",");
                    }
                    sw.WriteLine();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow && !RowIsEmpty(row))
                        {
                            List<string> cells = new List<string>();
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                string val = row.Cells[i].Value?.ToString() ?? "";
                                if (val.Contains(",") || val.Contains("\""))
                                {
                                    val = "\"" + val.Replace("\"", "\"\"") + "\"";
                                }
                                cells.Add(val);
                            }
                            sw.WriteLine(string.Join(",", cells));
                        }
                    }
                }

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = tempPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar CSV: " + ex.Message);
            }
        }

        private bool RowIsEmpty(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value != null && cell.Value.ToString().Trim() != "")
                {
                    return false;
                }
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Excel
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            string tempPath = Directory.GetCurrentDirectory() + "\\Descargas\\ReporteTemporal.html";
            double dato = 0.0;

            try
            {
                using (StreamWriter sw = new StreamWriter(tempPath, false, Encoding.UTF8))
                {
                    sw.WriteLine("<html>REPORTE DE PM<br><br><table border=1 cellspacing=0>");
                    sw.WriteLine("<tr bgcolor=#CCCCCC><td>Fecha</td><td>Porcentaje</td><td>CO</td></tr>");
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow && !RowIsEmpty(row))
                        {
                            List<string> cells = new List<string>();
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                dato = Convert.ToDouble(row.Cells[2].Value?.ToString());
                                sw.WriteLine("<tr><td>" + row.Cells[0].Value?.ToString()
                                + "</td><td>"
                                + row.Cells[1].Value?.ToString()
                                + "</td><td bgcolor=" + ((dato < 5.00) ? "\"#00E400\"" :
                                    (dato <= 9.00) ? "\"#FFFF00\"" :
                                    (dato <= 12.00) ? "\"#FF7E00\"" :
                                    (dato <= 16.00) ? "\"#FF0000\"" :
                                    "\"#8F3F97\"") + 
                                    ">" + dato.ToString()
                                + "</td></tr>");

                            }
                        }
                    }
                    sw.Write("</table></html>");
                    sw.Close();
                }
                System.Diagnostics.Process.Start("excel", "\"" + tempPath + "\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar html: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y < 100) textBox3.Text = "El riesgo en salud es mínimo o nulo";
            else if (e.Y < 180) textBox3.Text = "Personas que son sensibles al ozono (O3) o material particulado (PM10 y PM2.5) pueden experimentar irritación de ojos y síntomas respiratorios como tos, irritación de vías respiratorias, expectoración o flemas, dificultad para respirar o sibilancias";
            else if (e.Y < 260) textBox3.Text = "Incremento en el riesgo de tener sintomas respiratorios y/o disminusión en la funcion pulmonar.";
            else if (e.Y < 340) textBox3.Text = "Pueden experimentar un agravamiento de asma, enfermedad pulmonar obstructiva, crónica o evento cardiovascular e incremento en la probabilidad de muesrte prematura en personas con enfermedad pulmonar obstructiva crónica y cardiaca.";
            else if (e.Y < 420) textBox3.Text = "Es mas probable que cualquier persona se vea afectada por efectos graves a la salud.";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
