using FluentAssertions;
using NUnit.Framework;
using System;
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

    }
}
