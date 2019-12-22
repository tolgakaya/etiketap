using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EtikeTAP
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            this.KeyPreview = true;// this.FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            var progreassBar = new ProgressBar()
            {
                Style = ProgressBarStyle.Marquee,
                Size = new System.Drawing.Size(200, 20),
                ForeColor = Color.Orange,
                MarqueeAnimationSpeed = 40
            };
            Size = progreassBar.Size;
            Controls.Add(progreassBar);
        }
    }
}
