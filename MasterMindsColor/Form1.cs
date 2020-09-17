using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MasterMindsColor
{
    public partial class Form1 : Form
    {
        private Random random;
        private List<Color> ListOfColors;
        private Color[] colors;
        private Dictionary<Button, Panel> keyValuePairs;
        private Panel[] playerPanels;
        private Panel[] guessingPanels;
        public Form1()
        {
            InitializeComponent();
            ListOfColors = new List<Color>();
            ListOfColors.Add(Color.Red);
            ListOfColors.Add(Color.Green);
            ListOfColors.Add(Color.Blue);

            colors = new Color[] { ListOfColors[0], ListOfColors[1], ListOfColors[2]};

            random = new Random();

            playerPanels = new Panel[] { panel1, panel2, panel3 };
            guessingPanels = new Panel[] { panel4, panel5, panel6 };

            keyValuePairs = new Dictionary<Button, Panel>();
            keyValuePairs.Add(button1, panel1);
            keyValuePairs.Add(button2, panel2);
            keyValuePairs.Add(button3, panel3);

            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;


            for (int i = 0; i < guessingPanels.Length; i++)
            {
                guessingPanels[i].BackColor = colors[i];
            }

            colorDialog1.AllowFullOpen = false;
            colorDialog1.AnyColor = false;
        }

        private void GenerateColorArray()
        {
            timer1.Enabled = false;
            for (int i = 0; i < guessingPanels.Length; i++)
            {
                colors[i] = ListOfColors[random.Next(ListOfColors.Count - 1)];
                guessingPanels[i].BackColor = colors[i];
                guessingPanels[i].Visible = false;
            }            
        }

        private void button_Click(object sender, EventArgs e)
        {
            var s = sender as Button;
            colorDialog1.ShowDialog();
            keyValuePairs[s].BackColor = colorDialog1.Color;
        }
        
        private bool CheckMatchingColors(Panel panel, Panel panelToCheck)
        {
            if (panel.BackColor == panelToCheck.BackColor)
            {
                panelToCheck.Visible = true;
                return true;
            }
            return false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            bool isPanel1Matched = CheckMatchingColors(panel1, panel4);
            bool isPanel2Matched = CheckMatchingColors(panel2, panel5);
            bool isPanel3Matched = CheckMatchingColors(panel3, panel6);

            if (isPanel1Matched && isPanel2Matched && isPanel3Matched)
            {
                timer1.Enabled = true;
                timer1.Interval = 2000;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
            => GenerateColorArray();

        private void Hint_Button_MouseDown(object sender, MouseEventArgs e)
        {
            timer2.Enabled = true;
            timer2.Interval = 1000;

            for (int i = 0; i < guessingPanels.Length; i++)
            {
                guessingPanels[i].Visible = true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            for (int i = 0; i < guessingPanels.Length; i++)
            {
                guessingPanels[i].Visible = false;
            }
        }

    }
}
