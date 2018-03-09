using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmazonPricingCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int adjusted_dn = Int32.Parse(text_DN.Text);
            int commPercentage = Int32.Parse(text_Comm.Text);
            int shipping = Int32.Parse(text_Shipping.Text);
            int profitMargin = Int32.Parse(text_PM.Text);

         


            if ((adjusted_dn == 0) || (commPercentage == 0) || (shipping == 0) || (profitMargin == 0))
            {
                label6.Text = "Please enter in valid values in all of the boxes";
            }

        }

       
    }
}
