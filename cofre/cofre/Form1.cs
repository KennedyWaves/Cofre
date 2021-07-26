using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace cofre
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int qt1 = 0;
        int qt050 = 0;
        int qt025 = 0;
        int qt010 = 0;
        int qt005 = 0;
        private void txt_add1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    qt1 += Convert.ToInt32(txt_add1.Text);
                    txt_qt1.Text = qt1.ToString();
                    txt_t1.Text = "R$ " + qt1.ToString() + ",00";
                    txt_add1.Text = "";
                    update_total();
                }
                catch { }
            }
            if (e.KeyCode == Keys.Down)
            {
                txt_add050.Focus();
            }
        }

        private void txt_add050_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    qt050 += Convert.ToInt32(txt_add050.Text);
                    txt_qt050.Text = qt050.ToString();
                    txt_t050.Text = "R$ " + String.Format("{0:0.00}",Math.Round((qt050 * 0.5), 2));
                    txt_add050.Text = "";
                    update_total();
                }
                catch { }
            }
            if (e.KeyCode == Keys.Up)
            {
                txt_add1.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                txt_add025.Focus();
            }
        }

        void update_total()
        {
            double total = qt1 + (qt050 * 0.5) + (qt025 * 0.25) + (qt010 * 0.1) + (qt005 * 0.05);
            double tcoins = qt1 + qt050 + qt025 + qt010 + qt005;
            txt_tcoins.Text = tcoins.ToString();
            txt_totalAbs.Text = "R$ " + String.Format("{0:0.00}", total);
        }

        private void txt_add025_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    qt025 += Convert.ToInt32(txt_add025.Text);
                    txt_qt025.Text = qt025.ToString();
                    txt_t025.Text = "R$ " + String.Format("{0:0.00}", Math.Round((qt025 * 0.25), 2));
                    txt_add025.Text = "";
                    update_total();
                }
                catch { }
            }
            if (e.KeyCode == Keys.Up)
            {
                txt_add050.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                txt_add010.Focus();
            }
        }

        private void txt_add010_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    qt010 += Convert.ToInt32(txt_add010.Text);
                    txt_qt010.Text = qt010.ToString();
                    txt_t010.Text = "R$ " + String.Format("{0:0.00}", Math.Round((qt010 * 0.10), 2));
                    txt_add010.Text = "";
                    update_total();
                }
                catch { }
            }
            if (e.KeyCode == Keys.Up)
            {
                txt_add025.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                txt_add005.Focus();
            }
        }

        private void txt_add005_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    qt005 += Convert.ToInt32(txt_add005.Text);
                    txt_qt005.Text = qt005.ToString();
                    txt_t005.Text = "R$ " + String.Format("{0:0.00}", Math.Round((qt005 * 0.05), 2));
                    txt_add005.Text = "";
                    update_total();
                }
                catch { }
            }
            if (e.KeyCode == Keys.Up)
            {
                txt_add010.Focus();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt_add1.Focus();
            XmlDocument data = new XmlDocument();
            string path = @"coins.xml";
            data.Load(path);
            qt1 = Convert.ToInt32(data.SelectSingleNode("/coins/r1").Attributes["quant"].Value);
            txt_qt1.Text = qt1.ToString();
            txt_t1.Text = "R$ " + String.Format("{0:0.00}", Math.Round((qt1*1.0), 2));

            qt050 = Convert.ToInt32(data.SelectSingleNode("/coins/r050").Attributes["quant"].Value);
            txt_qt050.Text = qt050.ToString();
            txt_t050.Text = "R$ " + String.Format("{0:0.00}", Math.Round((qt050 * 0.5), 2));

            qt025 = Convert.ToInt32(data.SelectSingleNode("/coins/r025").Attributes["quant"].Value);
            txt_qt025.Text = qt025.ToString();
            txt_t025.Text = "R$ " + String.Format("{0:0.00}", Math.Round((qt025 * 0.25), 2));

            qt010 = Convert.ToInt32(data.SelectSingleNode("/coins/r010").Attributes["quant"].Value);
            txt_qt010.Text = qt010.ToString();
            txt_t010.Text = "R$ " + String.Format("{0:0.00}", Math.Round((qt010 * 0.1), 2));

            qt005 = Convert.ToInt32(data.SelectSingleNode("/coins/r005").Attributes["quant"].Value);
            txt_qt005.Text = qt005.ToString();
            txt_t005.Text = "R$ " + String.Format("{0:0.00}", Math.Round((qt005 * 0.05), 2));
            update_total();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cmd_save_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument data = new XmlDocument();
                XmlNode docNode = data.CreateXmlDeclaration("1.0", "UTF-8", null);
                data.AppendChild(docNode);
                XmlElement coin_Elem = data.CreateElement("coins");
                data.AppendChild(coin_Elem);
                XmlNode coin1Node = data.CreateElement("r1");
                XmlNode coin050Node = data.CreateElement("r050");
                XmlNode coin025Node = data.CreateElement("r025");
                XmlNode coin010Node = data.CreateElement("r010");
                XmlNode coin005Node = data.CreateElement("r005");

                coin_Elem.AppendChild(coin1Node);
                XmlAttribute save1 = data.CreateAttribute("quant");
                save1.InnerText = txt_qt1.Text;
                coin1Node.Attributes.Append(save1);

                coin_Elem.AppendChild(coin050Node);
                XmlAttribute save050 = data.CreateAttribute("quant");
                save050.InnerText = txt_qt050.Text;
                coin050Node.Attributes.Append(save050);

                coin_Elem.AppendChild(coin025Node);
                XmlAttribute save025 = data.CreateAttribute("quant");
                save025.InnerText = txt_qt025.Text;
                coin025Node.Attributes.Append(save025);

                coin_Elem.AppendChild(coin010Node);
                XmlAttribute save010 = data.CreateAttribute("quant");
                save010.InnerText = txt_qt010.Text;
                coin010Node.Attributes.Append(save010);

                coin_Elem.AppendChild(coin005Node);
                XmlAttribute save005 = data.CreateAttribute("quant");
                save005.InnerText = txt_qt005.Text;
                coin005Node.Attributes.Append(save005);

                data.Save(@"coins.xml");
            }
            catch { }
        }
    }
}
