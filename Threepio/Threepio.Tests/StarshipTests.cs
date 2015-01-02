using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace Threepio.Tests
{
    [TestFixture]
    class StarshipTests
    {
        [Test]
        public async void Starship_10_Returns_A_Starship()
        {
            Starship ship = await Starship.Get(10);

            Assert.IsNotNull(ship);
        }

        [Test]
        public void Starship_Minus1_Throws_A_404()
        {
            Action act = () => Starship.Get(-1);

            act.ShouldThrow<WebException>();
        }

        [Test]
        public async void GetPage_Returns_Multiple_Starships()
        {
            var result = await Starship.GetPage();

            Assert.IsInstanceOf<List<Starship>>(result);
        }

        [Test]
        public async void GetPage2_Returns_Different_Results_To_GetPage1()
        {
            var result1 = await Starship.GetPage(1);
            var result2 = await Starship.GetPage(2);

            Assert.AreNotEqual(result1, result2);
        }
    }
}