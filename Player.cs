using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
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
        Dictionary<Point,Point> Targets = new Dictionary<Point,Point> ();
        
        

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
            
             var nextCheckPoint = new Point(nextCheckpointX,nextCheckpointY);
            if(firstRound && !lastTarget.IsEmpty && lastTarget!=nextCheckPoint)
            {
                if(!Targets.ContainsKey(lastTarget)) 
                {
                   Targets.Add(lastTarget,nextCheckPoint);
                  
                }
                else
                {
                   firstRound=false; 
                }
            }
           
           
            var opPosition = new Point(opponentX, opponentY);
            var currentPosition = new Point(x, y);
            
            if (!lastPoint.IsEmpty)
            {
                speedVector = new EuclidianVector(lastPoint, currentPosition);
                 var angleBetweenSpeedAndDirection =
                (speedVector.AngleInDegreeWith(new EuclidianVector(currentPosition, nextCheckPoint)))% 360;
               
                
               
                
                if (!isBoostUsed && nextCheckpointDist > 5000 && (nextCheckpointAngle<5 && nextCheckpointAngle > -5 ))
                {
                   
                    Console.Error.WriteLine("Angle when Boost: "+ nextCheckpointAngle);
                    Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " BOOST");
                    isBoostUsed = true;
                    lastPoint = new Point(x, y);
                    continue;
                }
            
            }
            var thrust = 100.0d;


            
                  var target = CalcIdealTarget(nextCheckpointAngle, nextCheckPoint,currentPosition, nextCheckpointDist, speedVector, Targets,firstRound);

                
                var posTNextCheckPoint = new EuclidianVector(currentPosition,nextCheckPoint);
                var posToIdealTarget = new EuclidianVector(currentPosition,target);
                
                var IdealTargetAngle = target.Equals(nextCheckPoint)?nextCheckpointAngle:posToIdealTarget.AngleInDegreeWith(posTNextCheckPoint) - nextCheckpointAngle;
                
                 
                
                thrust = CalculatePerfectThrust(currentPosition, nextCheckpointAngle, nextCheckPoint, nextCheckpointDist,speedVector);
                
                
                
                var distOponant = EuclidianTools.DistanceBetweenTwoPoint(currentPosition, opPosition);

                if (DoINeedActivateShield(speedVector, currentPosition,opPosition,nextCheckPoint))
                {
                    Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " SHIELD");
                    continue;
                }

           


        
         
            Console.WriteLine(target.X + " " + target.Y + " " + (int)thrust);

            lastPoint = new Point(x, y);
            lastTarget =nextCheckPoint;

            // You have to output the target position
            // followed by the power (0 <= thrust <= 100)
            // i.e.: "x y thrust"

        }



    }

    private static bool DoINeedActivateShield(EuclidianVector speedVector, Point currentPosition,Point opPosition, Point nextCheckPoint)
    {
        if (speedVector == null)
        {
           return false;
        }

        var distanceToOp = EuclidianTools.DistanceBetweenTwoPoint(currentPosition, opPosition);
        var OpDirection = new EuclidianVector(opPosition,nextCheckPoint);
         var MyDirectiobn = new EuclidianVector(currentPosition,nextCheckPoint);

        var angleBetweenMeAndOp = (MyDirectiobn.AngleInDegreeWith(OpDirection))%360;
        

        return false;// (distanceToOp < 810   );
    }

    private static Point CalcIdealTarget(int dirAngle,Point nextCheckPoint, Point currentPosition,int distance, EuclidianVector speedV,Dictionary<Point,Point> Targets, bool firstRound )
    {
      
      
      if (speedV == null)
        {
           return nextCheckPoint;
        }
        
    
      
      var referenceVector = new EuclidianVector(100,0);
      var dirVector = new EuclidianVector(currentPosition, nextCheckPoint);
      var angleBetwenReferenceAndDirection = referenceVector.AngleInDegreeWith(dirVector);
      var angleBetweenReferenceAndSpeed =referenceVector.AngleInDegreeWith(speedV);
      var angleBetwenDirectionAndSpeed = dirVector.AngleInDegreeWith(speedV);
      
      var angleBetwenSpeedAnDir = speedV.AngleInDegreeWith(dirVector);
      
     
        //double offsetAngle = angleBetwenReferenceAndDirection - dirAngle-angleBetwenDirectionAndSpeed;
        double offsetAngle =  angleBetwenReferenceAndDirection - dirAngle-angleBetwenDirectionAndSpeed;
         
   //  a;
         Point idealTarget = new Point(  (int) (nextCheckPoint.X + 300*Math.Cos(EuclidianTools.DegreeToRadian(offsetAngle))),
                                      (int) (nextCheckPoint.Y + 300*Math.Sin(EuclidianTools.DegreeToRadian(offsetAngle))));   
          
         // Point idealTarget = new Point(  (int) (nextCheckPoint.X + 400*Math.Cos(EuclidianTools.DegreeToRadian(-angleBetweenReferenceAndSpeed -2*angleBetwenDirectionAndSpeed))),
           //                           (int) (nextCheckPoint.Y + 400*Math.Sin(EuclidianTools.DegreeToRadian(-angleBetweenReferenceAndSpeed -2*angleBetwenDirectionAndSpeed)))); 
        
        
      Console.Error.WriteLine("dirAngle   " + dirAngle);
          Console.Error.WriteLine("distance   " + distance);
          Console.Error.WriteLine("offsetAngle  " + offsetAngle); 
       
     
    if(dirAngle >  45|| dirAngle < -45 )
       {  
          return idealTarget;
        }
      /* else
        {
           
            dirVector.Normalise();
            dirVector.ScaleBy(-400);
            idealTarget = dirVector.ApplyTo(nextCheckPoint);
            
            
        }*/
         
        
         
         
        if (!firstRound  && distance < 1800 && speedV.Norm > 300) 
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
        

       
        
        if (dirAngle > 90 || dirAngle < -90 )
          return 0;//Math.Abs(30 * Math.Sin(EuclidianTools.DegreeToRadian(dirAngle)));;
        
       
         if (nextCheckpointDist>1600)
          return 100;
       
          
        var t =   Math.Abs(100 * Math.Cos(EuclidianTools.DegreeToRadian(dirAngle)));
        
         
        
         
         return (int)t;
       
       



    }
    
    
}
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
            Y= Y/(int) Norm;
            
            
            
        }
        
        public void ScaleBy(int factor)
        
        {
            X*=factor;
            Y*=factor;
            
           
        }
        
        
        public Point ApplyTo(Point p)
        {
            return new Point ((int)(p.X+X),(int)(p.Y+Y));    
        }
        public double ScalarProductWith(EuclidianVector v1)
        {
            return X * v1.X + Y * v1.Y;
        }

        public double AngleInDegreeWith(EuclidianVector v1)
        {
            var cosAngle = ScalarProductWith(v1) / (Norm * v1.Norm) ;
            var angleInGradian =Math.Acos(cosAngle);
            var angleInDegree =  EuclidianTools.RadianToDegree(angleInGradian)%360;
            if(angleInDegree>180)
            {
                angleInDegree = -(360-angleInDegree);
            }
            
            return angleInDegree;
        }
    }

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
