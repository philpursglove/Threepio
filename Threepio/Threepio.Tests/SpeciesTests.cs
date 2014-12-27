using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace Threepio.Tests
{
    [TestFixture]
    class SpeciesTests
    {
        [Test]
        public void Species_1_Returns_A_Species()
        {
            Species species = Species.Get(1);

            Assert.IsNotNull(species);
        }

        [Test]
        public void Species_Minus1_Throws_A_404()
        {
            Action act = () => Species.Get(-1);

            act.ShouldThrow<WebException>();
        }

        [Test]
        public void GetPage_Returns_Multiple_Species()
        {
            var result = Species.GetPage();

            Assert.IsInstanceOf<List<Species>>(result);
        }

        [Test]
        public void GetPage2_Returns_Different_Results_To_GetPage1()
        {
            var result1 = Species.GetPage(1);
            var result2 = Species.GetPage(2);

            Assert.AreNotEqual(result1, result2);
        }
    }
}
