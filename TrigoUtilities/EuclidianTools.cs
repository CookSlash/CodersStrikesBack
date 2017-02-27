using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace TrigoUtilities
{
    public static class EuclidianTools
    {

        public static double DistanceBetweenTwoPoint(Point p0, Point p1)
        {
            return Math.Sqrt(Math.Pow(p1.X - p0.X, 2) + Math.Pow(p1.Y - p0.Y, 2));
        }




        public static double RadianToDegree(double angle)
        {
            return angle * (180 / Math.PI);
        }

        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0d;
        }



    }
}
