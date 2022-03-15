using System;

using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        Board board = null;
        Button start = null;
        Button stop = null;
        Label lblSteps = null;
        TextBox steps = null;
        Button start1 = null;
        int count = 32;
        int squareSize = 25;
        int topMargin = 40;

        Thread runner = null;



        public Form1()
        {
            InitializeComponent();

            int winWidth = (count + 1) * (squareSize + 1);
            this.Width = winWidth;
            int titleHeight = this.Height - this.ClientRectangle.Height;
            this.Height = count * (squareSize + 1) + topMargin + titleHeight;
            this.BackColor = Color.Gray;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new Board(count, squareSize, topMargin);
            for(int i = 0; i < count*count; i++)
            {
                this.Controls.Add(board.square(i).panel());
            }

            start = new Button();
            start.Text = "Старт";
            start.Height = topMargin;
            start.Width = 100;
            start.BackColor = Color.Gray;
            start.Click += Start_Click;
            this.Controls.Add(start);

            stop = new Button();
            stop.Text = "Стоп";
            stop.Height = topMargin;
            stop.Location = new Point(200, 0);
            stop.Width = 100;
            stop.BackColor = Color.Gray;
            stop.Click += Stop_Click;
            this.Controls.Add(stop);

            lblSteps = new Label();
            lblSteps.Height = topMargin;
            lblSteps.Location = new Point(400, 0);
            lblSteps.Width = 100;
            lblSteps.Text = "Количество шагов:";
            this.Controls.Add(lblSteps);

            steps = new TextBox();
            steps.Height = topMargin;
            steps.Location = new Point(500, 0);
            steps.Width = 100;
            steps.Text = "50";
            this.Controls.Add(steps);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (runner == null || !runner.IsAlive)
            {
                runner = new Thread(board.run);
                if (steps.Text == "")
                {
                    MessageBox.Show("Number of steps is empty!");
                }
                else
                {
                    int s;
                    if (Int32.TryParse(steps.Text, out s))
                    {
                        runner.Start(s);
                    }
                    else
                    {
                        MessageBox.Show("Number of steps must only contain an integer!");
                    }
                }
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if(runner != null && runner.IsAlive)
            {
                runner.Abort();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
