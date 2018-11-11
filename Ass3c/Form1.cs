using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Ass3c
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private int cnt = 0;
        private int moveCnt = 0;
        private int numCnt = 0;
        public bool killMe = true;


        public Form1()
        {
            InitializeComponent();
        }

        // Button click that makes the mouse move to random positions on the screen, the commented out code is to make the mouse also click when its moving
        private void button1_Click(object sender, EventArgs e)
        {


            //  System.Threading.Thread.Sleep(2000);
            //  DoMouseClick();
            //  System.Threading.Thread.Sleep(1000);
            while (killMe)
            {
                System.Threading.Thread.Sleep(1000);
                MoveCursor();
                //System.Threading.Thread.Sleep(2000);
                //DoMouseClick();
                cnt++;
                numCnt++;
                
                if(numCnt > 15)
                {
                    break;
                   
                }
           
                
            }
            numCnt = 0;
        }

        //Button click to show the position of the mouse currently and the number of movements that the move method has done
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = Cursor.Position.ToString();
            countKeystrokes();
        }

        private void countKeystrokes()
        {
                cnt++;
            //Counts how many times the mouse has moved by the move method
                label1.Text = "Number of Mouse Movements = " + cnt.ToString();
                if (cnt == 1) // first time only
                {
                    label1.Text = "1"; // clear it
                }
          
        }

        private void MoveCursor()
        {
           

            //Sets a random X and Y Position for the mouse
            Random rnd = new Random();
            int rndY = rnd.Next(-75, 75);
            int rndX = rnd.Next(-80, 80);


            //Method so that the mouse can return to the center after a number of movements
            if(moveCnt > 25)
            {
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Position = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
                moveCnt = 0;
            }
            else
            {

                this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Position = new Point(Cursor.Position.X - rndX, Cursor.Position.Y - rndY);
                moveCnt++;
                //  Cursor.Clip = new Rectangle(this.Location, this.Size);
            }

        }


        //A method that makes the mouse click
        public void DoMouseClick()
        {

            //Call the imported function with the cursor's current position
            this.Cursor = new Cursor(Cursor.Current.Handle);
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }
    }
}
