using System.Data;

namespace TallerDiscretas
{
    public partial class Form1 : Form
    {
        bool deteccionMovimiento = false;
        bool bovedaAbierta = false;
        bool barreraLaserInterrumpida = false;
        bool botonPanicoPresionado = false;
        bool esHorarioNocturno = false;
        bool credencialAutorizada = false;

        public Form1()
        {
            InitializeComponent();

            CargarEcuaciones();
            CargarTablaVerdadPredefinida();

            OnBankTransparent(pictureBoxMovimiento);
            OnBankTransparent(pictureBoxBoveda);
            OnBankTransparent(pictureBoxLaser);
            OnBankTransparent(pictureBoxPanico);
            OnBankTransparent(pictureBoxCredencial);
            OnBankTransparent(pictureBoxHorario);
            OnBankTransparent(pictureBoxSonora);
            OnBankTransparent(pictureBoxSilenciosa);
            OnBankTransparent(pictureBoxBloqueo);
            OnBankTransparent(pictureBoxCentral);
        }

        private void OnBankTransparent(PictureBox control)
        {
            Point positionOnForm = control.Location;
            control.Parent = pictureBoxEscena;
            control.Location = new Point(positionOnForm.X - pictureBoxEscena.Left, positionOnForm.Y - pictureBoxEscena.Top);
            control.BackColor = Color.Transparent;
            control.BringToFront();
        }

        private void pictureBoxMovimiento_Click(object sender, EventArgs e)
        {
            deteccionMovimiento = !deteccionMovimiento;
        }

        private void pictureBoxBoveda_Click(object sender, EventArgs e)
        {
            bovedaAbierta = !bovedaAbierta;
        }
        private void pictureBoxLaser_Click(object sender, EventArgs e)
        {
            barreraLaserInterrumpida = !barreraLaserInterrumpida;
        }
        private void pictureBoxPanico_Click(object sender, EventArgs e)
        {
            botonPanicoPresionado = !botonPanicoPresionado;
        }
        private void pictureBoxCredencial_Click(object sender, EventArgs e)
        {
            credencialAutorizada = !credencialAutorizada;
        }
        private void pictureBoxHorario_Click(object sender, EventArgs e)
        {
            esHorarioNocturno = !esHorarioNocturno;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBoxMovimiento.Image = deteccionMovimiento ? pictureBoxMovimiento1.Image : pictureBoxMovimiento0.Image;
            pictureBoxBoveda.Image = bovedaAbierta ? pictureBoxBoveda1.Image : pictureBoxBoveda0.Image;
            pictureBoxLaser.Image = barreraLaserInterrumpida ? pictureBoxLaser1.Image : pictureBoxLaser0.Image;
            pictureBoxPanico.Image = botonPanicoPresionado ? pictureBoxPanico1.Image : pictureBoxPanico0.Image;
            pictureBoxCredencial.Image = credencialAutorizada ? pictureBoxCredencial1.Image : pictureBoxCredencial0.Image;
            pictureBoxHorario.Image = esHorarioNocturno ? pictureBoxHorario1.Image : pictureBoxHorario0.Image;

            bool alarmaSonora =        // S1 = A·F' + C·E
                (deteccionMovimiento && !credencialAutorizada) ||
                (barreraLaserInterrumpida && esHorarioNocturno);

            bool alarmaSilenciosa =    // S2 = B·D·E'
                bovedaAbierta && botonPanicoPresionado && !esHorarioNocturno;

            bool bloqueoAccesos =      // S3 = B·F' + C·A
                (bovedaAbierta && !credencialAutorizada) ||
                (barreraLaserInterrumpida && deteccionMovimiento);

            bool alertaCentral =       // S4 = D·E + A·B·F'
                (botonPanicoPresionado && esHorarioNocturno) ||
                (deteccionMovimiento && bovedaAbierta && !credencialAutorizada);

            pictureBoxSonora.Image = alarmaSonora ? pictureBoxSonora1.Image : pictureBoxSonora0.Image;
            pictureBoxSilenciosa.Image = alarmaSilenciosa ? pictureBoxSilenciosa1.Image : pictureBoxSilenciosa0.Image;
            pictureBoxBloqueo.Image = bloqueoAccesos ? pictureBoxBloqueo1.Image : pictureBoxBloqueo0.Image;
            pictureBoxCentral.Image = alertaCentral ? pictureBoxCentral1.Image : pictureBoxCentral0.Image;

            this.Text = $"S1={(alarmaSonora ? 1 : 0)}  S2={(alarmaSilenciosa ? 1 : 0)}  S3={(bloqueoAccesos ? 1 : 0)}  S4={(alertaCentral ? 1 : 0)}";
            this.BackColor = alarmaSonora ? Color.IndianRed : Color.WhiteSmoke;
        }

        private void CargarEcuaciones()
        {
            txtEcuaciones.Text =
                "SISTEMA DE SEGURIDAD BANCARIO\r\n" +
                "=====================================\r\n\r\n" +
                "VARIABLES DE ENTRADA:\r\n" +
                "  • A: Detección Movimiento\r\n" +
                "  • B: Bóveda Abierta\r\n" +
                "  • C: Barrera Láser Interrumpida\r\n" +
                "  • D: Botón Pánico Presionado\r\n" +
                "  • E: Horario Nocturno\r\n" +
                "  • F: Credencial Autorizada\r\n\r\n" +
                "ECUACIONES DE SALIDA:\r\n" +
                "  • S1 (Alarma Sonora)    = A·F' + C·E\r\n" +
                "  • S2 (Alarma Silenciosa) = B·D·E'\r\n" +
                "  • S3 (Bloqueo Accesos)   = B·F' + C·A\r\n" +
                "  • S4 (Alerta Central)    = D·E + A·B·F'";
        }

        private void CargarTablaVerdadPredefinida()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("A", typeof(int));
            dt.Columns.Add("B", typeof(int));
            dt.Columns.Add("C", typeof(int));
            dt.Columns.Add("D", typeof(int));
            dt.Columns.Add("E", typeof(int));
            dt.Columns.Add("F", typeof(int));

            dt.Columns.Add("S1", typeof(int));
            dt.Columns.Add("S2", typeof(int));
            dt.Columns.Add("S3", typeof(int));
            dt.Columns.Add("S4", typeof(int));

            for (int i = 0; i < 64; i++)
            {
                bool A = (i & 32) != 0;
                bool B = (i & 16) != 0;
                bool C = (i & 8) != 0;
                bool D = (i & 4) != 0;
                bool E = (i & 2) != 0;
                bool F = (i & 1) != 0;

                bool S1 = (A && !F) || (C && E);
                bool S2 = B && D && !E;
                bool S3 = (B && !F) || (C && A);
                bool S4 = (D && E) || (A && B && !F);

                dt.Rows.Add(
                    A ? 1 : 0, B ? 1 : 0, C ? 1 : 0,
                    D ? 1 : 0, E ? 1 : 0, F ? 1 : 0,
                    S1 ? 1 : 0, S2 ? 1 : 0, S3 ? 1 : 0, S4 ? 1 : 0
                );
            }

            dgvTablaVerdad.DataSource = dt;
            dgvTablaVerdad.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTablaVerdad.ReadOnly = true;
            dgvTablaVerdad.AllowUserToAddRows = false;
            dgvTablaVerdad.RowHeadersVisible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarEcuaciones();
            CargarTablaVerdadPredefinida();
        }
    }
}