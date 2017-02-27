using System;
using System.Drawing;
using NUnit.Framework;
using TrigoUtilities;

namespace TrigoUtilitiesTest
{
    [TestFixture]
    class TrigoToolsTest
    {
        private static readonly Point A = new Point(2, 3);
        private static readonly Point B = new Point(10, 10);
        private static readonly Point C = new Point(18, 9);
        private static readonly Point D = new Point(15, 5);
        private static readonly Point E = new Point(4, 10);
        private static readonly Point F = new Point(4, 10);
        

        [SetUp]
        public void Setup()
        {
           

        }

        [Test]
        public void DistanceBetweenTwoPointShouldReturnExpectedDistance()
        {
            Assert.AreEqual(6, EuclidianTools.DistanceBetweenTwoPoint(B, E));
            Assert.AreEqual(6, EuclidianTools.DistanceBetweenTwoPoint(E, B));

            Assert.AreEqual(12.083045973594572d, EuclidianTools.DistanceBetweenTwoPoint(E, D));
            Assert.AreEqual(17.088007490635061d, EuclidianTools.DistanceBetweenTwoPoint(A, C));
        }


        [Test]
        public void RadianToDegreeShouldReturnExpectedValues()
        {
            const double PI = Math.PI;

            Assert.That(EuclidianTools.RadianToDegree(PI), Is.EqualTo(180));
            Assert.That(EuclidianTools.RadianToDegree(PI/2), Is.EqualTo(90));
            Assert.That(EuclidianTools.RadianToDegree((3*PI)/2), Is.EqualTo(270));
            Assert.That(EuclidianTools.RadianToDegree((7*PI)/6), Is.EqualTo(210));
            Assert.That(EuclidianTools.RadianToDegree((5*PI)/4), Is.EqualTo(225));
            Assert.That(EuclidianTools.RadianToDegree(2*PI), Is.EqualTo(360));

        }

        [Test]
        public void DegreeToRadianShouldReturnExpectedValues()
        {
            const double PI = Math.PI;

            Assert.That(EuclidianTools.DegreeToRadian(180), Is.EqualTo(PI));
            Assert.That(EuclidianTools.DegreeToRadian(90), Is.EqualTo(PI / 2));
            Assert.That(EuclidianTools.DegreeToRadian(270), Is.EqualTo((3 * PI) / 2));
            Assert.That(EuclidianTools.DegreeToRadian(225), Is.EqualTo((5 * PI) / 4));
            Assert.That(EuclidianTools.DegreeToRadian(360), Is.EqualTo((2 * PI) ));

        }


        


    }

}
