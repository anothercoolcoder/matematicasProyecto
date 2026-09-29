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
    }
}