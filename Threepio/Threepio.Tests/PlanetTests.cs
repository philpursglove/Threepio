using NUnit.Framework;

namespace Threepio.Tests
{
    [TestFixture]
    public class PlanetTests
    {
        [Test]
        public void Planet_1_Returns_Planet()
        {
            Planet planet = Planet.Get(1);

            Assert.IsNotNull(planet);
        }

        [Test]
        public void Planet_Minus1_Returns_Null()
        {
            Planet planet = Planet.Get(-1);

            Assert.IsNull(planet);
        }
    }
}
