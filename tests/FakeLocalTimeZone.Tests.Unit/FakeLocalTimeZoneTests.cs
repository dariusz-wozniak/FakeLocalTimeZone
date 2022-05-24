using System;
using NUnit.Framework;

namespace FakeLocalTimeZone.Tests.Unit
{
    public class FakeLocalTimeZoneTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            // For diagnostic purposes, let's list all timezones to see if these are different than expected
            // (Windows Server instances has different timezones, for example)
            foreach (var tz in TimeZoneInfo.GetSystemTimeZones()) TestContext.Out.WriteLine(tz.Id);
        }

        [Test]
        public void local_timezone_is_switched_into_faked_one()
        {
            using (new FakeLocalTimeZone(TimeZoneInfo.FindSystemTimeZoneById("UTC+12")))
            {
                var localDateTime = new DateTime(2020, 12, 31, 23, 59, 59, DateTimeKind.Local);
                var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime);

                Assert.That(utcDateTime, Is.EqualTo(localDateTime.AddHours(-12)));
            }
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            // Double ensure Local timezone is set back
            using (new FakeLocalTimeZone(TimeZoneInfo.Local)) {}
        }
    }
}