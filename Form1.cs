using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ShutdownRestartApp2
{
    public partial class Form1 : Form
    {
        TimeSpan timeLeft;

        public Form1()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            CheckTextBoxValues();

            bool isStartable = true;

            try
            {
                timeLeft = new TimeSpan(Convert.ToInt32(txtHours.Text), Convert.ToInt32(txtMinutes.Text), Convert.ToInt32(txtSeconds.Text));
            }
            catch (FormatException ex)
            {
                isStartable = false;
                MessageBox.Show(ex.Message);
            }

            if (isStartable == true)
            {
                Timer.Start();
                lblTimer.Text = timeLeft.ToString(@"hh\:mm\:ss");
            }
        }

        private void CheckTextBoxValues()
        {
            if (txtHours.Text.Count() == 0)
                txtHours.Text = "0";

            if (txtMinutes.Text.Count() == 0)
                txtMinutes.Text = "0";

            if (txtSeconds.Text.Count() == 0)
                txtSeconds.Text = "0";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
            lblTimer.Text = timeLeft.ToString(@"hh\:mm\:ss");

            if (timeLeft.TotalSeconds <= 0)
            {
                Timer.Stop();
                PerformAction();
            }
        }

        private void PerformAction()
        {
            if (rbShutdown.Checked)
                Process.Start("shutdown", "/s");
            else if (rbRestart.Checked == true)
                Process.Start("shutdown", "/r");
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Timer.Stop();
            lblTimer.Text = "00:00:00";
        }
    }
}
