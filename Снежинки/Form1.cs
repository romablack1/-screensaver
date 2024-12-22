using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

namespace Снежинки
{
    public partial class Form1 : Form
    {
        private List<Snowflake> snowflakes = new List<Snowflake>();
        private System.Timers.Timer timer;
        private int Count = 200; // Увеличиваем количество снежинок
        private const int TimerInterval = 35; // Интервал таймера в миллисекундах

        public Form1()
        {
            InitializeComponent();
            Cursor.Hide();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized; 

            for (int i = 0; i < Count; i++)
            {
                snowflakes.Add(new Snowflake());
            }

            timer = new System.Timers.Timer(); 
            timer.Interval = TimerInterval;
            timer.Elapsed += TimerElapsed; 
            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                foreach (var snowflake in snowflakes)
                {
                    snowflake.Y += snowflake.Speed;

                    if (snowflake.Y > this.ClientSize.Height)
                    {
                        snowflake.SpawnSnowflake();
                    }
                }

                this.Invalidate(); 
            }));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            foreach (var snowflake in snowflakes)
            {
                g.DrawImage(snowflake.Image, snowflake.X, snowflake.Y, snowflake.Size, snowflake.Size);
            }
        }

        public class Snowflake
        {
            private static readonly Random rand = new Random(); // Статический Random для улучшения производительности
            public int X { get; private set; }
            public float Y { get; set; }
            public float Speed { get; private set; }
            public int Size { get; private set; }
            public Image Image { get; private set; }

            public Snowflake()
            {
                Image = Properties.Resources.snowflake; 
                Size = rand.Next(10, 50); 
                SpawnSnowflake();
            }

            public void SpawnSnowflake()
            {
                this.X = rand.Next(0, Screen.PrimaryScreen.Bounds.Width);
                this.Y = rand.Next(0, 150);

                
                if (Size < 25) 
                {
                    this.Speed = (float)rand.Next(2, 5) / 2; 
                }
                else 
                {
                    this.Speed = (float)rand.Next(3, 6); 
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}