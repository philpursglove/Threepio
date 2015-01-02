using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace Threepio.Tests
{
    [TestFixture]
    class VehicleTests
    {
        [Test]
        public async void Vehicle_18_Returns_A_Vehicle()
        {
            Vehicle vehicle = await Vehicle.Get(18);

            Assert.IsNotNull(vehicle);
        }

        [Test]
        public void Vehicle_Minus1_Throws_A_404()
        {
            Action act = () => Vehicle.Get(-1);

            act.ShouldThrow<WebException>();
        }

        [Test]
        public async void GetPage_Returns_Multiple_Vehicles()
        {
            var result = await Vehicle.GetPage();

            Assert.IsInstanceOf<List<Vehicle>>(result);
        }

        [Test]
        public async void GetPage2_Returns_Different_Results_To_GetPage1()
        {
            var result1 = await Vehicle.GetPage(1);
            var result2 = await Vehicle.GetPage(2);

            Assert.AreNotEqual(result1, result2);
        }
    }
}