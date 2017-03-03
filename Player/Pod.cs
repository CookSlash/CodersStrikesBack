using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrigoUtilities;

namespace Player
{
    public class Pod
    {
        public int Angle { get; private set; }
        public EuclidianVector Speed { get; private set; }
        public Point Position;
       

        public Pod(int x, int y, int vx, int vy, int angle)
        {
            Position = new Point(x,y);
            Speed = new EuclidianVector(vx,vy);
           
            Angle = angle;
        }
    }
}
