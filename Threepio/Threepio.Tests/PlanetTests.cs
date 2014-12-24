using FluentAssertions;
using NUnit.Framework;
using System;
using System.Net;

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
        public void Planet_Minus1_Throws_404()
        {
            Action act = () => Planet.Get(-1);

            act.ShouldThrow<WebException>();
        }
    }
}
