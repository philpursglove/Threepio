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
        public void Starship_10_Returns_A_Starship()
        {
            Starship ship = Starship.Get(10);

            Assert.IsNotNull(ship);
        }

        [Test]
        public void Starship_Minus1_Throws_A_404()
        {
            Action act = () => Starship.Get(-1);

            act.ShouldThrow<WebException>();
        }

        [Test]
        public void GetAll_Returns_Multiple_Starships()
        {
            var result = Starship.GetAll();

            Assert.IsInstanceOf<List<Starship>>(result);
        }
    }
}