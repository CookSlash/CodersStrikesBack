using System.Drawing;
using NUnit.Framework;
using TrigoUtilities;

namespace TrigoUtilitiesTest
{
    [TestFixture]
    class EuclidanVectorTest
    {
        private static readonly Point A = new Point(2, 3);
        private static readonly Point B = new Point(10, 10);
        private static readonly Point C = new Point(18, 9);
        private static readonly Point D = new Point(15, 5);
        private static readonly Point E = new Point(4, 10);
        private static readonly Point F = new Point(4, 5);


        [Test]
        public void VectorCtorWithPointTest()
        {
            var v = new EuclidianVector(E, D);
            Assert.That(v.X, Is.EqualTo(11));
            Assert.That(v.Y, Is.EqualTo(-5));
        }

        [Test]
        public void VectorCtorWithCoordTest()
        {
            var v = new EuclidianVector(11, 5);
            Assert.That(v.X, Is.EqualTo(11));
            Assert.That(v.Y, Is.EqualTo(5));
        }

        [Test]
        public void GetVectorNormTest()
        {
            var v = new EuclidianVector(E, D);
            Assert.That(v.Norm, Is.EqualTo(12.083045973594572d));
        }

        [Test]
        public void ScalarProductOfTwoVectorsTest()
        {
            var v0 = new EuclidianVector(2,2); 
            var v1 = new EuclidianVector(0,3); 

            Assert.That(v0.ScalarProductWith(v1),Is.EqualTo(6d));
        }

        [Test]
        public void GetAngleInDegreeWithAnotherVectorsTest()
        {
            var v = new EuclidianVector(E, B);
            var v1 = new EuclidianVector(E, F);

            Assert.That(v.AngleInDegreeWith(v1), Is.EqualTo(90.0d));
        }

    }
}
