using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace Threepio.Tests
{
    [TestFixture]
    public class FilmTests
    {
        [Test]
        public void Film_1_Returns_A_Film()
        {
            Film aNewHope = Film.Get(1);

            Assert.IsNotNull(aNewHope);
        }

        [Test]
        public void Film_Minus1_Throws_404()
        {
            Action act = () => Film.Get(-1);

            act.ShouldThrow<WebException>();
        }

        public void GetAll_Returns_Multiple_Films()
        {
            var result = Film.GetAll();

            Assert.IsInstanceOf<List<Film>>(result);
        }

        [Test]
        public void Episode1_Returns_ThePhantomMenace()
        {
            Film ep1 = Film.Episode1();
            Assert.IsTrue(ep1.Title == "The Phantom Menace");
        }

        [Test]
        public void Episode2_Returns_AttackOfTheClones()
        {
            Film ep2 = Film.Episode2();
            Assert.IsTrue(ep2.Title == "Attack of the Clones");
        }

        [Test]
        public void Episode3_Returns_RevengeOfTheSith()
        {
            Film ep3 = Film.Episode3();
            Assert.IsTrue(ep3.Title == "Revenge of the Sith");
        }

        [Test]
        public void Episode4_Returns_ANewHope()
        {
            Film ep4 = Film.Episode4();
            Assert.IsTrue(ep4.Title == "A New Hope");
        }

        [Test]
        public void Episode5_Returns_TheEmpireStrikesBack()
        {
            Film ep5 = Film.Episode5();
            Assert.IsTrue(ep5.Title == "The Empire Strikes Back");
        }

        [Test]
        public void Episode6_Returns_ReturnOfTheJedi()
        {
            Film ep6 = Film.Episode6();
            Assert.IsTrue(ep6.Title == "Return of the Jedi");
        }
    }
}
