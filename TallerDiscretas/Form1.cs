namespace TallerDiscretas
{
    public partial class Form1 : Form
    {

        private bool deteccionMovimiento = false;
        private bool bovedaAbierta = false;
        private bool barreraLaserInterrumpida = false;
        private bool botonPanicoPresionado = false;
        private bool esHorarioNocturno = false;
        private bool credencialAutorizada = false;


        private bool alarmaSonora = false;
        private bool alarmaSilenciosa = false;
        private bool bloqueoAccesos = false;
        private bool alertaCentralSeguridad = false;


        public Form1()
        {
            InitializeComponent();
        }



        public bool EvaluarAlarmaSonora()
        {
            return (deteccionMovimiento && !credencialAutorizada) || (barreraLaserInterrumpida && esHorarioNocturno);
        }

        public bool EvaluarAlarmaSilenciosa()
        {
            return !esHorarioNocturno && botonPanicoPresionado && bovedaAbierta;
        }
        public bool EvaluarBloqueoAccesos()
        {
            return (bovedaAbierta && !credencialAutorizada) || (barreraLaserInterrumpida && deteccionMovimiento);
        }

        public bool EvaluarAlertaCentral()
        {
            return (botonPanicoPresionado && esHorarioNocturno) || (bovedaAbierta && deteccionMovimiento && !credencialAutorizada);
        }

        private void VerificarEstadoSistema()
        {
            alarmaSonora = EvaluarAlarmaSonora();
            alarmaSilenciosa = EvaluarAlarmaSilenciosa();
            bloqueoAccesos = EvaluarBloqueoAccesos();
            alertaCentralSeguridad = EvaluarAlertaCentral();

            if (alarmaSonora)
            {
                MessageBox.Show("Se activó la alarma sonora");
            }

            if (alarmaSilenciosa)
            {
                MessageBox.Show("Se activó la alarma silenciosa");
            }

            if (bloqueoAccesos)
            {
                MessageBox.Show("Se activó el bloqueo automático de accesos");
            }

            if (alertaCentralSeguridad)
            {
                MessageBox.Show("Se activó el envío de alerta a la central de seguridad o policía");
            }
        }

        private void HorarioDiurno_Click(object sender, EventArgs e)
        {
            esHorarioNocturno = false;
            MessageBox.Show("Horario: DIURNO");
            VerificarEstadoSistema();
        }

        private void HorarioNocturno_Click(object sender, EventArgs e)
        {
            esHorarioNocturno = true;
            MessageBox.Show("Horario: NOCTURNO");
            VerificarEstadoSistema();
        }

        private void BovedaOpen_Click(object sender, EventArgs e)
        {
            bovedaAbierta = true;
            MessageBox.Show("Puerta de Boveda: ABIERTA");
            VerificarEstadoSistema();
        }

        private void BovedaClose_Click(object sender, EventArgs e)
        {
            bovedaAbierta = false;
            MessageBox.Show("Puerta de Boveda: CERRADA");
            VerificarEstadoSistema();
        }

        private void LaserClose_Click(object sender, EventArgs e)
        {
            barreraLaserInterrumpida = true;
            MessageBox.Show("Laser: INTERRUMPIDO");
            VerificarEstadoSistema();
        }

        private void LaserOpen_Click(object sender, EventArgs e)
        {
            barreraLaserInterrumpida = false;
            MessageBox.Show("Laser: NO INTERRUMPIDO");
            VerificarEstadoSistema();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            botonPanicoPresionado = true;
            MessageBox.Show("Botón de Pánico: ON");
            VerificarEstadoSistema();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            botonPanicoPresionado = false;
            MessageBox.Show("Botón de Pánico: OFF");
            VerificarEstadoSistema();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            credencialAutorizada = true;
            MessageBox.Show("Credencial autorizada: ON");
            VerificarEstadoSistema();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            credencialAutorizada = false;
            MessageBox.Show("Credencial autorizada: OFF");
            VerificarEstadoSistema();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            deteccionMovimiento = true;
            MessageBox.Show("Deteccion de movimiento: ON");
            VerificarEstadoSistema();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            deteccionMovimiento = false;
            MessageBox.Show("Deteccion de movimiento: OFF");
            VerificarEstadoSistema();
        }
    }
}
