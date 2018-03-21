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
    public partial class SingleRepricer : Form
    {
        public SingleRepricer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double adjusted_dn = Double.Parse(text_DN.Text);
            double commInput = Double.Parse(text_Comm.Text);
            double shipping = 0;
            if (!string.IsNullOrEmpty(text_Shipping.Text)) { shipping = Double.Parse(text_Shipping.Text); }
            double profitMarginInput = Double.Parse(text_PM.Text);
            label7.ForeColor = Color.Black;
            label7.Text = "Amazon Price:";

            if (comboBox1.Text != "" && shipping != 0)
            {
                label7.ForeColor = Color.Red; 
                label7.Text = "Please only enter a shipping template OR a shipping cost!";
            }
            else
            {
                double commPercentage = commInput / 100;
                double profitMargin = profitMarginInput / 100;
                double PM_Lower_Range = profitMargin;
                double PM_Upper_Range = profitMargin + .0014;

                if (shipping > 0)
                {
                    double tempTotal = adjusted_dn + shipping;
                    double commission = tempTotal * commPercentage;
                    double total = (tempTotal + commission);
                    double price = total + (total * .25);
                    double PM = (price - total) / price;
                    int count = 0;

                    while ((PM < PM_Lower_Range) || (PM > PM_Upper_Range))
                    {
                        label13.Text = "Loop Iterations: " + count;
                        if (PM > PM_Upper_Range)
                        {
                            price = price - .01;
                            commission = Math.Round((price * commPercentage), 2);
                            total = tempTotal + commission;
                            PM = (price - total) / price;
                        }
                        else if (PM < PM_Lower_Range)
                        {
                            price = price + .01;
                            commission = Math.Round((price * commPercentage), 2);
                            total = tempTotal + commission;
                            PM = (price - total) / price;
                        }
                        count++;
                        price = Math.Round(price, 2);
                    }

                    price = Math.Round(price, 2);
                    label7.Text = "Amazon Price: " + price;
                }
                else
                {
                    double commission2 = adjusted_dn * commPercentage;
                    double total2 = (adjusted_dn + commission2);
                    string templateType = comboBox1.Text;
                    double currShipping = template_values(total2, templateType);
                    total2 = total2 + currShipping;
                    double price2 = total2 + (total2 * .25);
                    double PM = (price2 - total2) / price2;

                    int count = 0;

                    while ((PM < PM_Lower_Range) || (PM > PM_Upper_Range))
                    {
                        label13.Text = "Loop Iterations: " + count;
                        if (PM >= PM_Upper_Range)
                        {
                            price2 = price2 - shipping;
                            price2 = price2 - .01;
                            shipping = template_values(price2, templateType);
                            price2 = price2 + shipping;
                            commission2 = Math.Round((price2 * commPercentage), 2);
                            total2 = commission2 + adjusted_dn + shipping;
                            PM = (price2 - total2) / price2;
                        }
                        else if (PM <= PM_Lower_Range)
                        {
                            price2 = price2 - shipping;
                            price2 = price2 + .01;
                            shipping = template_values(price2, templateType);
                            price2 = price2 + shipping;
                            commission2 = Math.Round((price2 * commPercentage), 2);
                            total2 = commission2 + adjusted_dn + shipping;
                            PM = (price2 - total2) / price2;
                        }
                        count++;
                        //price2 = Math.Round(price2, 2);
                    }

                    price2 = Math.Round(price2, 2);
                    double finalPrice = price2 - shipping;
                    label7.Text = "Amazon Price: " + finalPrice + " + " + shipping;
                }
            }
        }

        private double template_values(double price, string template)
        {
            if (template == "Main-Products-ShippingAddedByTemplate")
            {
                if (price <= 10) { return 7.99; }
                else if (price > 10 && price <= 20) { return 8.99; }
                else if (price > 20 && price <= 30) { return 10.99; }
                else { return 12.99; }
            }
            else
            {
                if (price <= 10) { return 4.99; }
                else if (price > 10 && price <= 20) { return 6.99; }
                else if (price > 20 && price <= 30) { return 8.99; }
                else { return 10.99; }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Text = "Amazon Price:";
            text_DN.Text = "";
            text_Comm.Text = "";
            text_Shipping.Text = "";
            text_PM.Text = "";
            comboBox1.Text = "";
            label13.Text = "Loop Iterations:";
        }



    }
}
