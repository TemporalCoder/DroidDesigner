using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Floodfill
{
    public partial class Form1 : Form
    {
        int wall = 1; 
        int[,] canvas = new int[,] {

            { 1,1,1,1,1,1,1,1,1,1 },
            { 1,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,1 },
            { 1,0,1,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,0,1,1,1 },
            { 1,0,0,0,0,1,0,0,0,1 },
            { 1,0,0,1,1,1,0,0,0,1 },
            { 1,0,0,0,0,1,0,0,0,1 },
            { 1,1,1,1,1,1,1,1,1,1 }

        };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            displayCanvas();
        }

        //Recursive Method
        private void button1_Click(object sender, EventArgs e)
        {
            flood_fill(3, 3, 2, 0);
            displayCanvas();
            Refresh();

        }
    
        //Stack Method - no recursion
        private void button2_Click(object sender, EventArgs e)
        {
            flood_fillStack(3, 3);
            displayCanvas();
            Refresh();
        }

        void flood_fill(int x, int y, int target_colour, int color)
        {
            if (x < 0 || x >= 10) return; //outside boundaries
            if (y < 0 || y >= 10) return;

            if (canvas[x, y] == color)  //if current colour(?) paint and check neighbours
            {
                canvas[x, y] = target_colour; // paint current pixel 

                flood_fill(x + 1, y, target_colour, color);  // down?
                flood_fill(x - 1, y, target_colour, color);  // up
                flood_fill(x, y + 1, target_colour, color);  // left
                flood_fill(x, y - 1, target_colour, color);  // right
            }
            displayCanvas();
            Refresh(); //remove to speed up
            return;
        }

        void flood_fillStack(int x, int y)
        {
            Stack<Point> pointStack= new Stack<Point>(); //store pixel coords to check and paint
            pointStack.Push(new Point(x, y));           //store staring pixel
                        
            while(pointStack.Count>0)                   //if the stack still has pixels, continue
            {
                Point tempPoint = pointStack.Pop();     //get the pixel on the top of the stack, removes!
                x = tempPoint.X;                        //store values to local variable, makes following code shorter!
                y = tempPoint.Y;
                canvas[x, y] = 2;                       //Paint the pixel
                
                if (checkNeighbour(x + 1, y))
                    pointStack.Push(new Point(x+1, y));
                if (checkNeighbour(x - 1, y))
                    pointStack.Push(new Point(x-1, y));
                if (checkNeighbour(x, y + 1))
                    pointStack.Push(new Point(x, y+1));
                if (checkNeighbour(x, y - 1))
                    pointStack.Push(new Point(x, y-1));
                
                displayCanvas();
                Refresh(); //remove to speed up visualisation
            }     
        }

        bool checkNeighbour(int x, int y)
        {
            if (x < 0 || x >= 10) return false;
            if (y < 0 || y >= 10) return false;

            if (canvas[x,y]==0){return true;}
            else{return false;}
            //return false;
        }


        void displayCanvas()
        {
            textBox1.Clear();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    textBox1.AppendText(canvas[i, j].ToString());
                }
                textBox1.AppendText("\r\n");
            }
        }

        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //Not efficient to create these every iteration!
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);
            Pen bluePen = new Pen(Color.Blue, 3);
            //Create Brushes
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            SolidBrush greenBrush = new SolidBrush(Color.Green);
            SolidBrush whiteBrush = new SolidBrush(Color.White);

            int tw = 20; int th = 20; //tile width and height
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int tx = i * tw; //calulate position on screen 
                    int ty = j * th;

                    Rectangle rect = new Rectangle(tx, ty, tw,th);
                    
                    if(canvas[j,i]==1)
                        e.Graphics.FillRectangle(blackBrush, rect);
                    if (canvas[j, i] == 0)
                        e.Graphics.FillRectangle(whiteBrush, rect);
                    if (canvas[j, i] == 2)
                        e.Graphics.FillRectangle(greenBrush, rect);
                    if (canvas[j, i] == 3)
                        e.Graphics.FillRectangle(blueBrush, rect);

                    e.Graphics.DrawRectangle(blackPen, rect);

                }
            }
        }
    }
}
