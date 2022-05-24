using System;
using System.Reflection;
using ReflectionMagic;

namespace FakeLocalTimeZone
{
    /// <summary>
    /// Fake local time zone to use to switch to a different time zone. Use wisely!
    /// </summary>
    /// <remarks>
    /// Code comes from https://stackoverflow.com/questions/44413407/mock-the-country-timezone-you-are-running-unit-test-from
    /// </remarks>
    public class FakeLocalTimeZone : IDisposable
    {
        private readonly TimeZoneInfo _actualLocalTimeZoneInfo;

        private static void SetLocalTimeZone(TimeZoneInfo timeZoneInfo)
        {
            try
            {
                // .NET Framework:
                typeof(TimeZoneInfo).AsDynamicType().s_cachedData.m_localTimeZone = timeZoneInfo;
            }
            catch
            {
                try
                {
                    // Yet another .NET Framework:
                    typeof(TimeZoneInfo).AsDynamicType()._cachedData.m_localTimeZone = timeZoneInfo;
                }
                catch
                {
                    // .NET Core:
                    var info = typeof(TimeZoneInfo).GetField("s_cachedData", BindingFlags.NonPublic | BindingFlags.Static);
                    object cachedData = info.GetValue(null);

                    var field = cachedData.GetType().GetField("_localTimeZone", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Instance);
                    field.SetValue(cachedData, timeZoneInfo);
                }
            }
        }

        public FakeLocalTimeZone(TimeZoneInfo timeZoneInfo)
        {
            _actualLocalTimeZoneInfo = TimeZoneInfo.Local;
            SetLocalTimeZone(timeZoneInfo);
        }

        public void Dispose() => SetLocalTimeZone(_actualLocalTimeZoneInfo);
    }
}