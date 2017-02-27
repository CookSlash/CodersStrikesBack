using System;
using System.Drawing;

namespace TrigoUtilities
{
    public class EuclidianVector
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Norm { get; private set; }

        public EuclidianVector(Point p1, Point p2)
        {
            GetVectorFromPoints(p1, p2);
        }

        public EuclidianVector(int x, int y)
        {
            X = x;
            Y = y;

            Norm = GetNorm();
        }

        private void GetVectorFromPoints(Point p0, Point p1)
        {
            X = p1.X - p0.X;
            Y = p1.Y - p0.Y;

            Norm = GetNorm();
        }

        private double GetNorm()
        {
            return Math.Sqrt(X * X + Y * Y);
        }


        public void Normalise()
        {
            X = X / (int)Norm;
            Y = Y / (int)Norm;



        }

        public void ScaleBy(int factor)

        {
            X *= factor;
            Y *= factor;


        }


        public Point ApplyTo(Point p)
        {
            return new Point((int)(p.X + X), (int)(p.Y + Y));
        }
        public double ScalarProductWith(EuclidianVector v1)
        {
            return X * v1.X + Y * v1.Y;
        }

        public double AngleInDegreeWith(EuclidianVector v1)
        {
            var cosAngle = ScalarProductWith(v1) / (Norm * v1.Norm);
            var angleInGradian = Math.Acos(cosAngle);
            var angleInDegree = EuclidianTools.RadianToDegree(angleInGradian) % 360;
            if (angleInDegree > 180)
            {
                angleInDegree = -(360 - angleInDegree);
            }

            return angleInDegree;
        }
    }
}
