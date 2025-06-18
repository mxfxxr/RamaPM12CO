using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Rama
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        double horas = 0.0, p_PM1 = 0.0, p_PM2 = 0.0;

        private void button2_Click_1(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            string tempPath = Path.Combine(Directory.GetCurrentDirectory(), "ReporteTemporal.csv");

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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("00 a 11 horas");
            comboBox1.Items.Add("12 a 23 horas");
            comboBox1.SelectedIndex = 0;
            dataGridView1.Columns.Add("Hora", "Hora");
            dataGridView1.Columns.Add("Porcentaje", "Porcentaje");
            dataGridView1.Columns.Add("cons", "cons");
            dataGridView1.Columns.Add("PM10", "PM10");
            dataGridView1.Columns.Add("PM25", "PM25");
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[2].Width = 40;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string cad, fecha = "", lahora = "", horafin = ((comboBox1.SelectedIndex == 0) ? "11" : "23");
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
                    html.WriteLine("<tr bgcolor=#CCCCCC><td>Fecha</td><td>Porcentaje</td><td>Cons</td>"
                        + "<td>PM10</td><td>PM2.5</td></tr>");
                    cad = arch.ReadLine();
                    string seg = cad.Substring(0, 10);
                    double C1, suma1, divi1, C2, suma2, divi2, cmaxpm1 = 0.0, cminpm1 = 0.0, cmaxpm2 = 0.0, cminpm2 = 0.0, wpm1 = 0.0, wpm2 = 0.0;
                    horas = 0.0;
                    while (true)
                    {
                        if (cad == null) break;
                        arr = cad.Split(',');
                        fecha = arr[0].Substring(0, 10);
                        lahora = arr[0].Substring(11, 8);
                        fecha = fecha.Substring(8, 2) + "/" + fecha.Substring(5, 2) + "/" + fecha.Substring(0, 4);
                        if (fecha == textBox1.Text && lahora.Substring(0,2) == comboBox1.SelectedItem.ToString().Substring(0, 2))
                        {
                            //Extraer los 12 datos
                            for (int i = 0; i < 12 && Convert.ToInt16(horafin) >= Convert.ToInt16(lahora.Substring(0,2)); i++)
                            {
                                p_PM1 = Convierte(arr[11]);
                                p_PM2 = Convierte(arr[13]);
                                if (p_PM1 > cmaxpm1) cmaxpm1 = p_PM1;
                                if (p_PM1 < cminpm1) cminpm1 = p_PM1;
                                if (p_PM2 > cmaxpm2) cmaxpm2 = p_PM2;
                                if (p_PM2 < cminpm2) cminpm2 = p_PM2;
                                dataGridView1.Rows.Add(lahora, ((horas + 1) / 12 * 100).ToString("0") + "%", (12 - i).ToString(), 
                                    p_PM1.ToString(), p_PM2.ToString());
                                int lastRow = dataGridView1.Rows.Count - 1;
                                // Colorear la celda de PM10
                                dataGridView1.Rows[lastRow].Cells[3].Style.BackColor = ObtenerColorPM10(p_PM1);

                                // Colorear la celda de PM2.5
                                dataGridView1.Rows[lastRow].Cells[4].Style.BackColor = ObtenerColorPM25(p_PM2);
                                cad = arch.ReadLine();
                                if (cad == null) break;
                                arr = cad.Split(',');
                                fecha = arr[0].Substring(0, 10);
                                lahora = arr[0].Substring(11, 8);
                                fecha = fecha.Substring(8, 2) + "/" + fecha.Substring(5, 2) + "/" + fecha.Substring(0, 4);
                                horas++;
                            }
                            wpm1 = 1 - ((cmaxpm1 - cminpm1) / cmaxpm1);
                            wpm2 = 1 - ((cmaxpm2 - cminpm2) / cmaxpm2);
                            textBox2.Text = "w: " + wpm1.ToString();
                            if (wpm1 < 0.5) wpm1 = 0.5;
                            if (wpm2 < 0.5) wpm2 = 0.5;
                            if (dataGridView1.RowCount < 9)
                            {
                                MessageBox.Show("No hay suficientes datos para el proceso.");
                                arch.Close(); html.Close();
                                return;
                            }
                            C1 = suma1 = divi1 = 0.0;
                            C2 = suma2 = divi2 = 0.0;
                            for (int i = dataGridView1.RowCount - 1; i > 0; i--)
                            {
                                suma1 += Convierte(dataGridView1.Rows[i].Cells[2].Value.ToString()) *
                                    (Math.Pow(wpm1, Convierte(dataGridView1.Rows[i].Cells[2].Value.ToString()) - 1));
                                divi1 += Math.Pow(wpm1, Convierte(dataGridView1.Rows[i].Cells[2].Value.ToString()) - 1);
                                suma2 += Convierte(dataGridView1.Rows[i].Cells[3].Value.ToString()) *
                                                        (Math.Pow(wpm2, Convierte(dataGridView1.Rows[i].Cells[2].Value.ToString()) - 1));
                                divi2 += Math.Pow(wpm2, Convierte(dataGridView1.Rows[i].Cells[2].Value.ToString()) - 1);

                            }
                            C1 = suma1 / divi1;
                            C2 = suma2 / divi2;
                            textBox2.Text = "C PM10: " + C1.ToString() + " y C PM2.5: " + C2.ToString()
                                + ", Porcentaje: " + (horas / 12 * 100).ToString("0") + "%";
                            break;
                        }
                        seg = fecha;
                        cad = arch.ReadLine();
                    }
                    html.WriteLine(Linea(seg));
                    html.WriteLine("</table></html>");
                    arch.Close(); html.Close();
                    //System.Diagnostics.Process.Start("excel", "\"" + openFileDialog1.FileName + ".html\"");

                }
                catch (Exception)
                {
                    MessageBox.Show("Error al abrir el archivo." + fecha);
                }
            }
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
                    sw.WriteLine("<html>REPORTE DE PM del día " + textBox1.Text + "<br><br><table border=1 cellspacing=0>");
                    sw.WriteLine("<tr bgcolor=#CCCCCC><td>Hora</td><td>Porcentaje</td><td>PM10</td><td>PM2.5</td></tr>");
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow && !RowIsEmpty(row))
                        {
                            List<string> cells = new List<string>();
                            //for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            //{
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

                            //}
                        }
                    }
                    sw.Write("</table><br><br>" + textBox2.Text);
                    sw.Write("</html>");
                    sw.Close();
                }
                System.Diagnostics.Process.Start("excel", "\"" + tempPath + "\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar html: " + ex.Message);
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
            //+ (p_O3_ / horas).ToString("0.000") + "</td><td align=center>"
            //+ (p_SO2 / horas).ToString("0.000") + "</td><td align=center>"
            //+ (p_NO2 / horas).ToString("0.000") + "</td><td align=center>"
            //+ (p_NO_ / horas).ToString("0.00") + "</td><td align=center>"
            //+ (p_CO_ / horas).ToString("0.00") + "</td><td align=center>"
            + (p_PM1 / horas).ToString("0") + "</td><td align=center>"
            + (p_PM2 / horas).ToString("0") + "</td></tr>");

        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(pictureBox1, "Este es un mensaje al pasar el cursor");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Y < 100) textBox3.Text = "El riesgo en salud es mínimo o nulo";
            else if(e.Y < 180) textBox3.Text = "Personas que son sensibles al ozono (O3) o material particulado (PM10 y PM2.5) pueden experimentar irritación de ojos y síntomas respiratorios como tos, irritación de vías respiratorias, expectoración o flemas, dificultad para respirar o sibilancias";
            else if (e.Y < 260) textBox3.Text = "Incremento en el riesgo de tener sintomas respiratorios y/o disminusión en la funcion pulmonar.";
            else if (e.Y < 340) textBox3.Text = "Pueden experimentar un agravamiento de asma, enfermedad pulmonar obstructiva, crónica o evento cardiovascular e incremento en la probabilidad de muesrte prematura en personas con enfermedad pulmonar obstructiva crónica y cardiaca.";
            else if (e.Y < 420) textBox3.Text = "Es mas probable que cualquier persona se vea afectada por efectos graves a la salud.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox3_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private Color ObtenerColorPM10(double valor)
        {
            if (valor < 45) return Color.FromArgb(0x00, 0xE4, 0x00);      // Verde
            else if (valor <= 60) return Color.Yellow;                    // Amarillo
            else if (valor <= 132) return Color.Orange;                   // Naranja
            else if (valor <= 213) return Color.Red;                      // Rojo
            else return Color.Purple;                                     // Morado
        }

        private Color ObtenerColorPM25(double valor)
        {
            if (valor < 15) return Color.FromArgb(0x00, 0xE4, 0x00);      // Verde
            else if (valor <= 33) return Color.Yellow;                    // Amarillo
            else if (valor <= 79) return Color.Orange;                    // Naranja
            else if (valor <= 130) return Color.Red;                      // Rojo
            else return Color.Purple;                                     // Morado
        }
    }







}

