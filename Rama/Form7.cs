using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Rama
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        double horas = 0, p_O3_ = 0.0, p_SO2 = 0.0, p_NO2 = 0.0, p_CO_ = 0.0, p_PM1 = 0.0, p_PM2 = 0.0;

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("excel", "\"" + openFileDialog1.FileName + ".html\"");
        }

        OpenFileDialog openFileDialog1;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y < 100) textBox2.Text = "El riesgo en salud es mínimo o nulo";
            else if (e.Y < 120) textBox2.Text = "Personas que son sensibles al ozono (O3) o material particulado (PM10 y PM2.5) pueden experimentar irritación de ojos y síntomas respiratorios como tos, irritación de vías respiratorias, expectoración o flemas, dificultad para respirar o sibilancias";
            else if (e.Y < 260) textBox2.Text = "Incremento en el riesgo de tener sintomas respiratorios y/o disminusión en la funcion pulmonar.";
            else if (e.Y < 340) textBox2.Text = "Pueden experimentar un agravamiento de asma, enfermedad pulmonar obstructiva, crónica o evento cardiovascular e incremento en la probabilidad de muesrte prematura en personas con enfermedad pulmonar obstructiva crónica y cardiaca.";
            else if (e.Y < 420) textBox2.Text = "Es mas probable que cualquier persona se vea afectada por efectos graves a la salud.";
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y < 100) textBox2.Text = "El riesgo en salud es mínimo o nulo";
            else if (e.Y < 160) textBox2.Text = "Personas que son sensibles al ozono (O3) o material particulado (PM10 y PM2.5) pueden experimentar irritación de ojos y síntomas respiratorios como tos, irritación de vías respiratorias, expectoración o flemas, dificultad para respirar o sibilancias";
            else if (e.Y < 200) textBox2.Text = "Incremento en el riesgo de tener sintomas respiratorios y/o disminusión en la funcion pulmonar.";
            else if (e.Y < 280) textBox2.Text = "Pueden experimentar un agravamiento de asma, enfermedad pulmonar obstructiva, crónica o evento cardiovascular e incremento en la probabilidad de muesrte prematura en personas con enfermedad pulmonar obstructiva crónica y cardiaca.";
            else if (e.Y < 350) textBox2.Text = "Es mas probable que cualquier persona se vea afectada por efectos graves a la salud.";
            else if (e.Y < 365) textBox2.Text = "No se cuenta con datos válidos. Este rango aparece cuando los valores de los contaminantes son nulos (null) o igual a cero, indicando que el monitoreo no está activo o no se ha registrado información.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (webBrowser1.Document == null)
            {
                MessageBox.Show("No hay ningún documento cargado en el navegador.");
                return;
            }

            HtmlElementCollection tablas = webBrowser1.Document.GetElementsByTagName("table");
            if (tablas == null || tablas.Count == 0)
            {
                MessageBox.Show("No se encontró ninguna tabla en el documento HTML.");
                return;
            }

            HtmlElement tabla = tablas[0]; // Primera tabla encontrada

            StringBuilder csv = new StringBuilder();

            foreach (HtmlElement fila in tabla.GetElementsByTagName("tr"))
            {
                List<string> celdas = new List<string>();

                HtmlElementCollection ths = fila.GetElementsByTagName("th");
                HtmlElementCollection tds = fila.GetElementsByTagName("td");

                foreach (HtmlElement th in ths)
                {
                    celdas.Add(EscapeCSV(th.InnerText));
                }

                foreach (HtmlElement td in tds)
                {
                    celdas.Add(EscapeCSV(td.InnerText));
                }

                csv.AppendLine(string.Join(",", celdas));
            }

            try
            {
                string rutaArchivo = Path.Combine(Path.GetDirectoryName(openFileDialog1.FileName), "ReporteWebBrowser.csv");
                File.WriteAllText(rutaArchivo, csv.ToString(), Encoding.UTF8);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = rutaArchivo,
                    UseShellExecute = true
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el archivo CSV: " + ex.Message);
            }
        }

        private string EscapeCSV(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return "";

            valor = valor.Replace("\"", "\"\"");
            if (valor.Contains(",") || valor.Contains("\"") || valor.Contains("\n"))
            {
                valor = "\"" + valor + "\"";
            }
            return valor;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string cad, fecha = "";
            string[] arr = new string[15];
            openFileDialog1 = new OpenFileDialog();
            string connectionString = "datasource=localhost;port=3307;username=root;password=;database=rama;";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader arch = new StreamReader(openFileDialog1.FileName);
                    StreamWriter html = new StreamWriter(openFileDialog1.FileName + ".html");
                    StreamWriter csv = new StreamWriter(openFileDialog1.FileName + ".csv");
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    cad = arch.ReadLine();
                    html.WriteLine("<html>REPORTE MENSUAL DE VALORES PROMEDIOS");
                    html.WriteLine("<br><br><table border=1 cellspacing=0>");
                    html.WriteLine("<tr bgcolor=#CCCCCC><td>Fecha</td><td>Porcentaje</td><td>O3</td>"
                        + "<td>SO2</td><td>NO2</td></tr>");
                    cad = arch.ReadLine();
                    string seg = cad.Substring(0, 10);
                    while (true)
                    {
                        if (cad == null) break;
                        arr = cad.Split(',');
                        fecha = arr[0].Substring(0, 10);
                        if (seg != fecha)
                        {
                            html.WriteLine(Linea(seg));
                            //insertar mensuales

                            horas = p_O3_ = p_SO2 = p_NO2 /*= p_NO_*/ /*= p_CO_ = p_PM1 = p_PM2*/ = 0.0;
                        }
                        p_O3_ += Convierte(arr[1]);
                        p_SO2 += Convierte(arr[3]);
                        p_NO2 += Convierte(arr[5]);
                        //p_NO_ += Convierte(arr[7]);
                        p_CO_ += Convierte(arr[9]);
                        p_PM1 += Convierte(arr[11]);
                        p_PM2 += Convierte(arr[13]);
                        horas++;
                        seg = fecha;
                        cad = arch.ReadLine();
                    }
                    html.WriteLine(Linea(seg));
                    html.WriteLine("</table></html>");
                    arch.Close(); html.Close(); csv.Close();
                    Uri dir = new Uri(openFileDialog1.FileName + ".html");
                    webBrowser1.Url = dir;

                    string query = "insert into reportes values(null, 'Mensual " + seg.Substring(0, 7) + "', 1, '', '', '');";
                    MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                    MySqlDataReader reader;
                    databaseConnection.Open();
                    reader = commandDatabase.ExecuteReader();
                    databaseConnection.Close();
                }
                catch (Exception cc)
                {
                    MessageBox.Show("Error al abrir el archivo." + fecha + "\n" + cc.Message);
                }
            }
        }

        private double Convierte(string arr)
        {
            return ((arr == "null") ? 0 : Convert.ToDouble(arr));
        }

        private string Linea(string seg)
        {


            string no2 = (p_NO2 / horas == 0) ? "<td align=center bgcolor=#B9D1EA>FS</td>"
                                      : "<td align=center bgcolor=#" + ColorNO2() + ">" + (p_NO2 / horas).ToString("0.000") + "</td>";

            //string no = (p_NO_ / horas == 0) ? "<td align=center bgcolor=#FFFF00>En Mantenimiento</td>"
            //                                 : "<td align=center bgcolor=#" + ColorNO() + ">" + (p_NO_ / horas).ToString("0.00") + "</td>";

            return (
                "<tr><td>" + seg + "</td><td align=center bgcolor=#FFFF"
                + ((horas < 24) ? "00" : "FF") + ">"
                + ((horas / 24) * 100).ToString("0") + "%" + "</td><td align=center bgcolor=#" + ColorO3() + ">"
                + (p_O3_ / horas).ToString("0.000") + "</td><td align=center bgcolor=#" + ColorSO2() + ">"
                + (p_SO2 / horas).ToString("0.000") + "</td>"
                + no2 /*+ no */
                + "</td></tr>");
        }

        private string ColorO3()
        {
            if ((p_O3_ / horas) <= 058) return "00E400";
            else if ((p_O3_ / horas) <= 090) return "FFFF00";
            else if ((p_O3_ / horas) <= 135) return "FF7E00";
            else if ((p_O3_ / horas) <= 175) return "FF0000";
            return "8F3F97";
        }

        private string ColorSO2()
        {
            if ((p_SO2 / horas) <= 035) return "00E400";
            else if ((p_SO2 / horas) <= 075) return "FFFF00";
            else if ((p_SO2 / horas) <= 185) return "FF7E00";
            else if ((p_SO2 / horas) <= 304) return "FF0000";
            return "8F3F97";
        }
        private string ColorNO2()
        {
            if ((p_NO2 / horas) <= 053) return "00E400";
            else if ((p_NO2 / horas) <= 106) return "FFFF00";
            else if ((p_NO2 / horas) <= 160) return "FF7E00";
            else if ((p_NO2 / horas) <= 213) return "FF0000";
            return "8F3F97";
        }

        //private string ColorNO()
        //{
        //    if ((p_NO_ / horas) <= 053) return "00E400";
        //    else if ((p_NO_ / horas) <= 106) return "FFFF00";
        //    else if ((p_NO_ / horas) <= 160) return "FF7E00";
        //    else if ((p_NO_ / horas) <= 213) return "FF0000";
        //    return "8F3F97";
        //}


    }
}
