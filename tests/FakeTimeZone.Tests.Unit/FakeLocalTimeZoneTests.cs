using System;
using NUnit.Framework;

namespace FakeTimeZone.Tests.Unit
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

        [TestCase("UTC+12")]
        [TestCase("UTC-11")] // < In case UTC+12 is a de-facto local timezone
        public void local_timezone_is_switched_into_faked_one(string timeZoneId)
        {
            Assume.That(TimeZoneInfo.Local.Id, Is.Not.EqualTo(timeZoneId));
            
            using (new FakeLocalTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)))
            {
                Assert.That(TimeZoneInfo.Local.Id, Is.EqualTo(timeZoneId));
            }
        }

        [TestCase("UTC+12")]
        [TestCase("UTC-11")] // < In case UTC+12 is a de-facto local timezone
        public void local_timezone_is_reverted_into_original_one(string timeZoneId)
        {
            var localTimeZone = TimeZoneInfo.Local;

            Assume.That(localTimeZone.Id, Is.Not.EqualTo(timeZoneId));

            using (new FakeLocalTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZoneId))) {}
            
            var localTimeZoneAfterUsingFake = TimeZoneInfo.Local;
            
            Assert.That(localTimeZone, Is.EqualTo(localTimeZoneAfterUsingFake));
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            // Double ensure Local timezone is set back
            using (new FakeLocalTimeZone(TimeZoneInfo.Local)) {}
        }
    }
}