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
using Excel = Microsoft.Office.Interop.Excel;

namespace Rama
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        double horas = 0, p_O3_ = 0.0, p_SO2 = 0.0, p_NO2 = 0.0;

        private void Form10_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("00 horas");
            comboBox1.Items.Add("01 horas");
            comboBox1.Items.Add("02 horas");
            comboBox1.Items.Add("03 horas");
            comboBox1.Items.Add("04 horas");
            comboBox1.Items.Add("05 horas");
            comboBox1.Items.Add("06 horas");
            comboBox1.Items.Add("07 horas");
            comboBox1.Items.Add("08 horas");
            comboBox1.Items.Add("09 horas");
            comboBox1.Items.Add("10 horas");
            comboBox1.Items.Add("11 horas");
            comboBox1.Items.Add("12 horas");
            comboBox1.Items.Add("13 horas");
            comboBox1.Items.Add("14 horas");
            comboBox1.Items.Add("15 horas");
            comboBox1.Items.Add("16 horas");
            comboBox1.Items.Add("17 horas");
            comboBox1.Items.Add("18 horas");
            comboBox1.Items.Add("19 horas");
            comboBox1.Items.Add("20 horas");
            comboBox1.Items.Add("21 horas");
            comboBox1.Items.Add("22 horas");
            comboBox1.Items.Add("23 horas");
            comboBox1.SelectedIndex = 0;
            dataGridView1.Columns.Add("Hora", "Hora");
            dataGridView1.Columns.Add("cons", "cons");
            dataGridView1.Columns.Add("O3", "O3");
            dataGridView1.Columns.Add("SO2", "SO2");
            dataGridView1.Columns.Add("NO2", "NO2");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            string tempPath = Path.Combine(Directory.GetCurrentDirectory(), "ReporteTemporalUnaHora.csv");

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

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string cad, fecha = "", lahora = "";
            string[] arr = new string[15];
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader arch = new StreamReader(openFileDialog1.FileName);
                    StreamWriter html = new StreamWriter(openFileDialog1.FileName + ".html");
                    StreamWriter csv = new StreamWriter(openFileDialog1.FileName + ".csv");
                    csv.WriteLine("Hora,Cons,O3,SO2,NO2");
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    html.WriteLine("<html>REPORTE DE PROMEDIO DE UNA HORA");
                    html.WriteLine("<br><br><table border=1 cellspacing=0>");
                    html.WriteLine("<tr bgcolor=#CCCCCC><td>Fecha</td><td>Porcentaje</td><td>O3</td>"
                        + "<td>SO2</td><td>NO2</td></tr>");
                    cad = arch.ReadLine();
                    string seg = cad.Substring(0, 10);
                    double C1, suma1, divi1, C2, suma2, divi2, C3, suma3, divi3,
                        cmaxpo3 = 0.0, cminpo3 = 0.0, cmaxpso2 = 0.0, cminpso2 = 0.0, cmaxpno2 = 0.0, cminpno2 = 0.0, wpo3 = 0.0, wpso2 = 0.0, wpno2 = 0.0;
                    horas = 0;
                    while (true)
                    {
                        if (cad == null) break;
                        arr = cad.Split(',');
                        fecha = arr[0].Substring(0, 10);
                        lahora = arr[0].Substring(11, 8);
                        fecha = fecha.Substring(8, 2) + "/" + fecha.Substring(5, 2) + "/" + fecha.Substring(0, 4);
                        if (fecha == textBox1.Text && lahora.Substring(0, 2) == comboBox1.SelectedItem.ToString().Substring(0, 2))
                        {
                            //Extraer los datos de 1 hora
                            for (int i = 0; i < horas; i++)
                            {
                                p_O3_ = Convierte(arr[1]);
                                p_SO2 = Convierte(arr[3]);
                                p_NO2 = Convierte(arr[5]);
                                if (p_O3_ > cmaxpo3) cmaxpo3 = p_O3_;
                                if (p_O3_ < cminpo3) cminpo3 = p_O3_;
                                if (p_SO2 > cmaxpso2) cmaxpso2 = p_SO2;
                                if (p_SO2 < cminpso2) cminpso2 = p_SO2;
                                if (p_NO2 > cmaxpno2) cmaxpno2 = p_NO2;
                                if (p_NO2 < cminpno2) cminpno2 = p_NO2;
                                dataGridView1.Rows.Add(lahora, (horas - i).ToString(), p_O3_.ToString(), p_SO2.ToString(), p_NO2.ToString());
                                csv.WriteLine($"{lahora},{horas - i},{ p_O3_},{p_SO2},{p_NO2}");
                                int lastRow = dataGridView1.Rows.Count - 1;
                                // Colorear la celda de o3
                                dataGridView1.Rows[lastRow].Cells[2].Style.BackColor = ObtenerColorP_O3_(p_O3_);

                                // Colorear la celda de so2
                                dataGridView1.Rows[lastRow].Cells[3].Style.BackColor = ObtenerColorP_SO2(p_SO2);

                                // Colorear la celda de no2
                                dataGridView1.Rows[lastRow].Cells[4].Style.BackColor = ObtenerColorP_NO2(p_NO2);
                                cad = arch.ReadLine();
                                if (cad == null) break;
                                arr = cad.Split(',');
                                fecha = arr[0].Substring(0, 10);
                                lahora = arr[0].Substring(11, 8);
                                fecha = fecha.Substring(8, 2) + "/" + fecha.Substring(5, 2) + "/" + fecha.Substring(0, 4);
                                if (lahora.Substring(0, 2) != comboBox1.SelectedItem.ToString().Substring(0, 2))
                                    break;
                            }
                            wpo3 = 1 - ((cmaxpo3 - cminpo3) / cmaxpo3);
                            wpso2 = 1 - ((cmaxpso2 - cminpso2) / cmaxpso2);
                            textBox2.Text = "w: " + wpo3.ToString();
                            if (wpo3 < 0.5) wpo3 = 0.5;
                            if (wpso2 < 0.5) wpso2 = 0.5;
                            C1 = suma1 = divi1 = 0.0;
                            C2 = suma2 = divi2 = 0.0;
                            C3 = suma3 = divi3 = 0.0;
                            for (int i = dataGridView1.RowCount - 1; i > 0; i--)
                            {
                                suma1 += Convierte(dataGridView1.Rows[i].Cells[2].Value.ToString()) *
                                         (Math.Pow(wpo3, Convierte(dataGridView1.Rows[i].Cells[1].Value.ToString()) - 1));
                                divi1 += Math.Pow(wpo3, Convierte(dataGridView1.Rows[i].Cells[1].Value.ToString()) - 1);
                                suma2 += Convierte(dataGridView1.Rows[i].Cells[3].Value.ToString()) *
                                        (Math.Pow(wpso2, Convierte(dataGridView1.Rows[i].Cells[1].Value.ToString()) - 1));
                                divi2 += Math.Pow(wpso2, Convierte(dataGridView1.Rows[i].Cells[1].Value.ToString()) - 1);
                                suma3 += Convierte(dataGridView1.Rows[i].Cells[4].Value.ToString()) *
                                        (Math.Pow(wpno2, Convierte(dataGridView1.Rows[i].Cells[1].Value.ToString()) - 1));
                                divi3 += Math.Pow(wpno2, Convierte(dataGridView1.Rows[i].Cells[1].Value.ToString()) - 1);

                            }
                            C1 = suma1 / divi1;
                            C2 = suma2 / divi2;
                            C3 = suma3 / divi3;
                            textBox2.Text = "C O3: " + C1.ToString() + " y C SO2: " + C2.ToString() + " y C NO2: " + C3.ToString();
                            //ColorearFilas();
                            //ExportarAExcelConColores();
                            break;
                        }
                        horas++;
                        seg = fecha;
                        cad = arch.ReadLine();
                    }
                    html.WriteLine(Linea(seg));
                    html.WriteLine("</table></html>");
                    arch.Close(); html.Close(); csv.Close();
                    //System.Diagnostics.Process.Start("excel", "\"" + openFileDialog1.FileName + ".html\"");

                }
                catch (Exception)
                {
                    MessageBox.Show("Error al abrir el archivo." + fecha);
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
            + (p_O3_ / horas).ToString("0.000") + "</td><td align=center>"
            + (p_SO2 / horas).ToString("0.000") + "</td><td align=center>"
            + (p_NO2 / horas).ToString("0.000") + "</td></tr>");
        }
       



        private void button3_Click(object sender, EventArgs e)
        {//Excel
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
                    sw.WriteLine("<tr bgcolor=#CCCCCC><td>Fecha</td><td>Porcentaje</td><td>O3</td><td>SO2</td><td>NO2</td></tr>");
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow && !RowIsEmpty(row))
                        {
                            List<string> cells = new List<string>();
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                dato = Convert.ToDouble(row.Cells[3].Value?.ToString());
                                sw.WriteLine("<tr><td>" + row.Cells[0].Value?.ToString()
                                + "</td><td>"
                                + row.Cells[1].Value?.ToString()
                                + "</td><td bgcolor=" + ((dato < 45) ? "\"#00E400\"" :
                                    (dato <= 60) ? "\"#FFFF00\"" :
                                    (dato <= 132) ? "\"#FF7E00\"" :
                                    (dato <= 213) ? "\"#FF0000\"" :
                                    "\"#8F3F97\"") + ">"
                                + row.Cells[2].Value?.ToString()
                                + "</td><td bgcolor=" + ((dato < 15) ? "\"#00E400\"" :
                                    (dato <= 33) ? "\"#FFFF00\"" :
                                    (dato <= 79) ? "\"#FF7E00\"" :
                                    (dato <= 130) ? "\"#FF0000\"" :
                                    "\"#8F3F97\"") + ">" + dato.ToString()
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

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private Color ObtenerColorP_O3_(double valor)
        {
            if (valor < 0.058) return Color.FromArgb(0x00, 0xE4, 0x00);      // Verde
            else if (valor <= 0.090) return Color.Yellow;                    // Amarillo
            else if (valor <= 0.135) return Color.Orange;                   // Naranja
            else if (valor <= 0.175) return Color.Red;                      // Rojo
            else return Color.Purple;                                     // Morado
        }

        private Color ObtenerColorP_SO2(double valor)
        {
            if (valor < 0.035) return Color.FromArgb(0x00, 0xE4, 0x00);      // Verde
            else if (valor <= 0.075) return Color.Yellow;                    // Amarillo
            else if (valor <= 0.185) return Color.Orange;                    // Naranja
            else if (valor <= 0.304) return Color.Red;                      // Rojo
            else return Color.Purple;                                     // Morado
        }

        private Color ObtenerColorP_NO2(double valor)
        {
            if (valor < 0.053) return Color.FromArgb(0x00, 0xE4, 0x00);      // Verde
            else if (valor <= 0.106) return Color.Yellow;                    // Amarillo
            else if (valor <= 0.160) return Color.Orange;                    // Naranja
            else if (valor <= 0.213) return Color.Red;                      // Rojo
            else return Color.Purple;                                     // Morado
        }
    }
}
