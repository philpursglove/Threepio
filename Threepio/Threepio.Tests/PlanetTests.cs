using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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

        [Test]
        public void GetPage_Returns_Multiple_Planets()
        {
            var result = Planet.GetPage();

            Assert.IsInstanceOf<List<Planet>>(result);
        }

        [Test]
        public void GetPage2_Returns_Different_Results_To_GetPage1()
        {
            var result1 = Planet.GetPage(1);
            var result2 = Planet.GetPage(2);

            Assert.AreNotEqual(result1, result2);
        }
    }
}
