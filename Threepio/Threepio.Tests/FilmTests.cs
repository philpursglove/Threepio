using NUnit.Framework;
using System.Collections.Generic;

namespace Threepio.Tests
{
    [TestFixture]
    public class FilmTests
    {
        [Test]
        public async void Film_1_Returns_A_Film()
        {
            Film aNewHope = await Film.Get(1);

            Assert.IsNotNull(aNewHope);
        }


        [Test]
        public async void GetPage_Returns_Multiple_Films()
        {
            var result = await Film.GetPage();

            Assert.IsInstanceOf<List<Film>>(result);
        }

        [Test]
        public async void Episode1_Returns_ThePhantomMenace()
        {
            Film ep1 = await Film.Episode1();
            Assert.IsTrue(ep1.Title == "The Phantom Menace");
        }

        [Test]
        public async void Episode2_Returns_AttackOfTheClones()
        {
            Film ep2 = await Film.Episode2();
            Assert.IsTrue(ep2.Title == "Attack of the Clones");
        }

        [Test]
        public async void Episode3_Returns_RevengeOfTheSith()
        {
            Film ep3 = await Film.Episode3();
            Assert.IsTrue(ep3.Title == "Revenge of the Sith");
        }

        [Test]
        public async void Episode4_Returns_ANewHope()
        {
            Film ep4 = await Film.Episode4();
            Assert.IsTrue(ep4.Title == "A New Hope");
        }

        [Test]
        public async void Episode5_Returns_TheEmpireStrikesBack()
        {
            Film ep5 = await Film.Episode5();
            Assert.IsTrue(ep5.Title == "The Empire Strikes Back");
        }

        [Test]
        public async void Episode6_Returns_ReturnOfTheJedi()
        {
            Film ep6 = await Film.Episode6();
            Assert.IsTrue(ep6.Title == "Return of the Jedi");
        }

        [Test]
        public async void Episode7_Returns_TheForceAwakens()
        {
            Film ep7 = await Film.Episode7();
            Assert.AreEqual("The Force Awakens", ep7.Title);
        }
    }
}
