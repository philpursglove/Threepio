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
        public async void Planet_1_Returns_Planet()
        {
            Planet planet = await Planet.Get(1);

            Assert.IsNotNull(planet);
        }

       
        [Test]
        public async void GetPage_Returns_Multiple_Planets()
        {
            var result = await Planet.GetPage();

            Assert.IsInstanceOf<List<Planet>>(result);
        }

        [Test]
        public async void GetPage2_Returns_Different_Results_To_GetPage1()
        {
            var result1 = await Planet.GetPage(1);
            var result2 = await Planet.GetPage(2);

            Assert.AreNotEqual(result1, result2);
        }
    }
}
