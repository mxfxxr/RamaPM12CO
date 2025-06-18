namespace Rama
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.procesamientoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ciudadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.procesosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promediosMensualesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pMDe12HorasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.o3De8HorasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promediosDe1HoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferenciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graficandoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promediosDe24HorasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.procesosToolStripMenuItem,
            this.preferenciasToolStripMenuItem,
            this.reportesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(879, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.procesamientoToolStripMenuItem,
            this.monitorToolStripMenuItem,
            this.ciudadesToolStripMenuItem,
            this.estacionesToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // procesamientoToolStripMenuItem
            // 
            this.procesamientoToolStripMenuItem.Name = "procesamientoToolStripMenuItem";
            this.procesamientoToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.procesamientoToolStripMenuItem.Text = "Procesamiento";
            this.procesamientoToolStripMenuItem.Click += new System.EventHandler(this.procesamientoToolStripMenuItem_Click);
            // 
            // monitorToolStripMenuItem
            // 
            this.monitorToolStripMenuItem.Name = "monitorToolStripMenuItem";
            this.monitorToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.monitorToolStripMenuItem.Text = "Monitor";
            this.monitorToolStripMenuItem.Click += new System.EventHandler(this.monitorToolStripMenuItem_Click);
            // 
            // ciudadesToolStripMenuItem
            // 
            this.ciudadesToolStripMenuItem.Name = "ciudadesToolStripMenuItem";
            this.ciudadesToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.ciudadesToolStripMenuItem.Text = "Ciudades";
            this.ciudadesToolStripMenuItem.Click += new System.EventHandler(this.ciudadesToolStripMenuItem_Click);
            // 
            // estacionesToolStripMenuItem
            // 
            this.estacionesToolStripMenuItem.Name = "estacionesToolStripMenuItem";
            this.estacionesToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.estacionesToolStripMenuItem.Text = "Estaciones";
            this.estacionesToolStripMenuItem.Click += new System.EventHandler(this.estacionesToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // procesosToolStripMenuItem
            // 
            this.procesosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.promediosMensualesToolStripMenuItem,
            this.pMDe12HorasToolStripMenuItem,
            this.o3De8HorasToolStripMenuItem,
            this.promediosDe1HoraToolStripMenuItem,
            this.promediosDe24HorasToolStripMenuItem});
            this.procesosToolStripMenuItem.Name = "procesosToolStripMenuItem";
            this.procesosToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.procesosToolStripMenuItem.Text = "Procesos";
            // 
            // promediosMensualesToolStripMenuItem
            // 
            this.promediosMensualesToolStripMenuItem.Name = "promediosMensualesToolStripMenuItem";
            this.promediosMensualesToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.promediosMensualesToolStripMenuItem.Text = "Promedios Mensuales";
            this.promediosMensualesToolStripMenuItem.Click += new System.EventHandler(this.promediosMensualesToolStripMenuItem_Click);
            // 
            // pMDe12HorasToolStripMenuItem
            // 
            this.pMDe12HorasToolStripMenuItem.Name = "pMDe12HorasToolStripMenuItem";
            this.pMDe12HorasToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.pMDe12HorasToolStripMenuItem.Text = "PM de 12 horas";
            this.pMDe12HorasToolStripMenuItem.Click += new System.EventHandler(this.pMDe12HorasToolStripMenuItem_Click);
            // 
            // o3De8HorasToolStripMenuItem
            // 
            this.o3De8HorasToolStripMenuItem.Name = "o3De8HorasToolStripMenuItem";
            this.o3De8HorasToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.o3De8HorasToolStripMenuItem.Text = "CO de 8 horas";
            this.o3De8HorasToolStripMenuItem.Click += new System.EventHandler(this.o3De8HorasToolStripMenuItem_Click);
            // 
            // promediosDe1HoraToolStripMenuItem
            // 
            this.promediosDe1HoraToolStripMenuItem.Name = "promediosDe1HoraToolStripMenuItem";
            this.promediosDe1HoraToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.promediosDe1HoraToolStripMenuItem.Text = "Promedios de 1 hora";
            this.promediosDe1HoraToolStripMenuItem.Click += new System.EventHandler(this.promediosDe1HoraToolStripMenuItem_Click);
            // 
            // preferenciasToolStripMenuItem
            // 
            this.preferenciasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graficandoToolStripMenuItem});
            this.preferenciasToolStripMenuItem.Name = "preferenciasToolStripMenuItem";
            this.preferenciasToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.preferenciasToolStripMenuItem.Text = "Preferencias";
            // 
            // graficandoToolStripMenuItem
            // 
            this.graficandoToolStripMenuItem.Name = "graficandoToolStripMenuItem";
            this.graficandoToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.graficandoToolStripMenuItem.Text = "Graficando";
            this.graficandoToolStripMenuItem.Click += new System.EventHandler(this.graficandoToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // promediosDe24HorasToolStripMenuItem
            // 
            this.promediosDe24HorasToolStripMenuItem.Name = "promediosDe24HorasToolStripMenuItem";
            this.promediosDe24HorasToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.promediosDe24HorasToolStripMenuItem.Text = "Promedios de 24 horas";
            this.promediosDe24HorasToolStripMenuItem.Click += new System.EventHandler(this.promediosDe24HorasToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 346);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "RAMA - Red Automática de Monitoreo Ambiental";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ciudadesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferenciasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graficandoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem procesamientoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem procesosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promediosMensualesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pMDe12HorasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem o3De8HorasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promediosDe1HoraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promediosDe24HorasToolStripMenuItem;
    }
}

