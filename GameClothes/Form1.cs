using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace GameClothes
{
    public partial class Form1 : Form
    {
        private Random rand = new Random();
        private int correctMatches = 0;
        private readonly Stopwatch stopwatch = new Stopwatch();

        PictureBox pb;
        Label draggedLabel;   

        private Point[] labelPositionArr = new Point[6];
        public Form1()
        {
            InitializeComponent();
        }

        bool isDown = false;
        // Событие на нажатие мыши
        private void label_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            Label lbl = sender as Label;

            if (lbl != null && isDown == true)
            {
                lbl.DoDragDrop(lbl, DragDropEffects.Move);
                lbl.Location = this.PointToClient(Control.MousePosition);
            }

            isDown = false;
        }

        private void label_MouseMove(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            if (isDown)
            {
                Label label = sender as Label;
                
                label.Location = this.PointToClient(Control.MousePosition);

                label.DoDragDrop(label.Text, DragDropEffects.Move);
            }
        }

        private void label_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
        }

        // Сравниваем подходит ли label к PictureBox
        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            pb = (PictureBox)sender;
            draggedLabel = (Label)e.Data.GetData(typeof(Label));

            if (pb == pictureBox1 && draggedLabel == label1)
            {
                draggedLabel.Tag = pb;
                correctMatches++;
            }
            else if (pb == pictureBox2 && draggedLabel == label2)
            {
                draggedLabel.Tag = pb;
                correctMatches++;
            }
            else if (pb == pictureBox3 && draggedLabel == label3)
            {
                draggedLabel.Tag = pb;
                correctMatches++;
            }
            else if (pb == pictureBox4 && draggedLabel == label4)
            {
                draggedLabel.Tag = pb;
                correctMatches++;
            }
            else if (pb == pictureBox5 && draggedLabel == label5)
            {
                draggedLabel.Tag = pb;
                correctMatches++;
            }
            else if (pb == pictureBox6 && draggedLabel == label6)
            {
                draggedLabel.Tag = pb;
                correctMatches++;
            }

            draggedLabel.Enabled = false;
        }

        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            pb = (PictureBox)sender;

            draggedLabel = (Label)e.Data.GetData(typeof(Label));
        }

        private void ResetGame()
        {
            label1.Location = labelPositionArr[0];
            label2.Location = labelPositionArr[1];
            label3.Location = labelPositionArr[2];
            label4.Location = labelPositionArr[3];
            label5.Location = labelPositionArr[4];
            label6.Location = labelPositionArr[5];

            label1.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = true;
            label4.Enabled = true;
            label5.Enabled = true;
            label6.Enabled = true;

            Random rand = new Random();

            PictureBox[] pictureBoxes = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6 };

            // Перемешать PictureBox
            for (int i = pictureBoxes.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                var temp = pictureBoxes[j].Location;
                pictureBoxes[j].Location = pictureBoxes[i].Location;
                pictureBoxes[i].Location = temp;
            }
            stopwatch.Restart();
            // Сброс счетчика правильных совпадений
            correctMatches = 0;
        }       


        private void Form1_Load(object sender, EventArgs e)
        {

            labelPositionArr[0] = label1.Location;
            labelPositionArr[1] = label2.Location;
            labelPositionArr[2] = label3.Location;
            labelPositionArr[3] = label4.Location;
            labelPositionArr[4] = label5.Location;
            labelPositionArr[5] = label6.Location;

            ((Control)pictureBox1).AllowDrop = true;
            ((Control)pictureBox2).AllowDrop = true;
            ((Control)pictureBox3).AllowDrop = true;
            ((Control)pictureBox4).AllowDrop = true;
            ((Control)pictureBox5).AllowDrop = true;
            ((Control)pictureBox6).AllowDrop = true;

            ResetGame();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
            MessageBox.Show($"{correctMatches} out of 6 correct answer!\ntime: {labelTime.Text}", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = stopwatch.Elapsed.ToString("mm\\:ss");
        }
    }
}
