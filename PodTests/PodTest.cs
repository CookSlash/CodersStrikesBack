
using NUnit.Framework;
using Player;

namespace PodTests
{
    [TestFixture]
    public class PodTest
    {
        [Test]
        public void CtorShouldCreateaPod()
        {
            const int x = 100;
            const int y = 152;
            const int vx = -745;
            const int vy = 542;
            const int angle = 75;
            var pod = new Player.Pod( x,  y,  vx,  vy, angle);

            Assert.That(pod.Position.X,Is.EqualTo(x));
            Assert.That(pod.Position.Y,Is.EqualTo(y));
            Assert.That(pod.Speed.X,Is.EqualTo(vx));
            Assert.That(pod.Speed.Y,Is.EqualTo(vy));
            Assert.That(pod.Angle,Is.EqualTo(angle));
           
        }
    }
}
