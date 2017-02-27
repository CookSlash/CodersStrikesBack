using System;
using System.Collections.Generic;
using System.Drawing;
using TrigoUtilities;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
namespace Player
{
    class Player
    {
        static void Main(string[] args)
        {
            string[] inputs;
            bool isBoostUsed = false;
            Point lastPoint = new Point();
            Point lastTarget = new Point();
            EuclidianVector speedVector = null;
            Point myTarget = new Point();
            bool firstRound = true;
            Dictionary<Point, Point> Targets = new Dictionary<Point, Point>();



            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int x = int.Parse(inputs[0]);
                int y = int.Parse(inputs[1]);

                int nextCheckpointX = int.Parse(inputs[2]); // x position of the next check point
                int nextCheckpointY = int.Parse(inputs[3]); // y position of the next check point
                int nextCheckpointDist = int.Parse(inputs[4]); // distance to the next checkpoint
                int nextCheckpointAngle = int.Parse(inputs[5]); // angle between your pod orientation and the direction of the next checkpoint
                inputs = Console.ReadLine().Split(' ');
                int opponentX = int.Parse(inputs[0]);
                int opponentY = int.Parse(inputs[1]);

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                var nextCheckPoint = new Point(nextCheckpointX, nextCheckpointY);
                if (firstRound && !lastTarget.IsEmpty && lastTarget != nextCheckPoint)
                {
                    if (!Targets.ContainsKey(lastTarget))
                    {
                        Targets.Add(lastTarget, nextCheckPoint);

                    }
                    else
                    {
                        firstRound = false;
                    }
                }


                var opPosition = new Point(opponentX, opponentY);
                var currentPosition = new Point(x, y);

                if (!lastPoint.IsEmpty)
                {
                    speedVector = new EuclidianVector(lastPoint, currentPosition);
                    var angleBetweenSpeedAndDirection =
                        (speedVector.AngleInDegreeWith(new EuclidianVector(currentPosition, nextCheckPoint))) % 360;




                    if (!isBoostUsed && nextCheckpointDist > 5000 && (nextCheckpointAngle < 5 && nextCheckpointAngle > -5))
                    {

                        Console.Error.WriteLine("Angle when Boost: " + nextCheckpointAngle);
                        Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " BOOST");
                        isBoostUsed = true;
                        lastPoint = new Point(x, y);
                        continue;
                    }

                }
                var thrust = 100.0d;



                var target = CalcIdealTarget(nextCheckpointAngle, nextCheckPoint, currentPosition, nextCheckpointDist, speedVector, Targets, firstRound);


                var posTNextCheckPoint = new EuclidianVector(currentPosition, nextCheckPoint);
                var posToIdealTarget = new EuclidianVector(currentPosition, target);

                var IdealTargetAngle = target.Equals(nextCheckPoint) ? nextCheckpointAngle : posToIdealTarget.AngleInDegreeWith(posTNextCheckPoint) - nextCheckpointAngle;



                thrust = CalculatePerfectThrust(currentPosition, nextCheckpointAngle, nextCheckPoint, nextCheckpointDist, speedVector);



                var distOponant = EuclidianTools.DistanceBetweenTwoPoint(currentPosition, opPosition);

                if (DoINeedActivateShield(speedVector, currentPosition, opPosition, nextCheckPoint))
                {
                    Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " SHIELD");
                    continue;
                }






                Console.WriteLine(target.X + " " + target.Y + " " + (int)thrust);

                lastPoint = new Point(x, y);
                lastTarget = nextCheckPoint;

                // You have to output the target position
                // followed by the power (0 <= thrust <= 100)
                // i.e.: "x y thrust"

            }



        }

        private static bool DoINeedActivateShield(EuclidianVector speedVector, Point currentPosition, Point opPosition, Point nextCheckPoint)
        {
            if (speedVector == null)
            {
                return false;
            }

            var distanceToOp = EuclidianTools.DistanceBetweenTwoPoint(currentPosition, opPosition);
            var OpDirection = new EuclidianVector(opPosition, nextCheckPoint);
            var MyDirectiobn = new EuclidianVector(currentPosition, nextCheckPoint);

            var angleBetweenMeAndOp = (MyDirectiobn.AngleInDegreeWith(OpDirection)) % 360;


            return false;// (distanceToOp < 810   );
        }

        private static Point CalcIdealTarget(int dirAngle, Point nextCheckPoint, Point currentPosition, int distance, EuclidianVector speedV, Dictionary<Point, Point> Targets, bool firstRound)
        {


            if (speedV == null)
            {
                return nextCheckPoint;
            }



            var referenceVector = new EuclidianVector(100, 0);
            var dirVector = new EuclidianVector(currentPosition, nextCheckPoint);
            var angleBetwenReferenceAndDirection = referenceVector.AngleInDegreeWith(dirVector);
            var angleBetweenReferenceAndSpeed = referenceVector.AngleInDegreeWith(speedV);
            var angleBetwenDirectionAndSpeed = dirVector.AngleInDegreeWith(speedV);

            var angleBetwenSpeedAnDir = speedV.AngleInDegreeWith(dirVector);


            //double offsetAngle = angleBetwenReferenceAndDirection - dirAngle-angleBetwenDirectionAndSpeed;
            double offsetAngle = angleBetwenReferenceAndDirection - dirAngle - angleBetwenDirectionAndSpeed;

            //  a;
            Point idealTarget = new Point((int)(nextCheckPoint.X + 300 * Math.Cos(EuclidianTools.DegreeToRadian(offsetAngle))),
                (int)(nextCheckPoint.Y + 300 * Math.Sin(EuclidianTools.DegreeToRadian(offsetAngle))));

            // Point idealTarget = new Point(  (int) (nextCheckPoint.X + 400*Math.Cos(EuclidianTools.DegreeToRadian(-angleBetweenReferenceAndSpeed -2*angleBetwenDirectionAndSpeed))),
            //                           (int) (nextCheckPoint.Y + 400*Math.Sin(EuclidianTools.DegreeToRadian(-angleBetweenReferenceAndSpeed -2*angleBetwenDirectionAndSpeed)))); 


            Console.Error.WriteLine("dirAngle   " + dirAngle);
            Console.Error.WriteLine("distance   " + distance);
            Console.Error.WriteLine("offsetAngle  " + offsetAngle);


            if (dirAngle > 45 || dirAngle < -45)
            {
                return idealTarget;
            }
            /* else
          {

              dirVector.Normalise();
              dirVector.ScaleBy(-400);
              idealTarget = dirVector.ApplyTo(nextCheckPoint);


          }*/




            if (!firstRound && distance < 1800 && speedV.Norm > 300)
            {
                return Targets[nextCheckPoint];

            }

            return nextCheckPoint;



        }




        private static double CalculatePerfectThrust(Point currentPosition, int dirAngle, Point nextCheckPoint, int nextCheckpointDist, EuclidianVector speedVector)
        {
            var maxThrust = 100.0d;
            if (speedVector == null)
            {
                return maxThrust;
            }




            if (dirAngle > 90 || dirAngle < -90)
                return 0;//Math.Abs(30 * Math.Sin(EuclidianTools.DegreeToRadian(dirAngle)));;


            if (nextCheckpointDist > 1600)
                return 100;


            var t = Math.Abs(100 * Math.Cos(EuclidianTools.DegreeToRadian(dirAngle)));




            return (int)t;





        }


    }
}

