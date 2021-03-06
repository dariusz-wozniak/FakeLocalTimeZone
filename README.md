A very small package that helps to fake (a.k.a. mock) system `TimeZone`.

This package may be useful for testing DateTime and local/UTC conversions.

[![NuGet](https://buildstats.info/nuget/FakeLocalTimeZone)](https://www.nuget.org/packages/FakeLocalTimeZone)

Usage
=

In order to use the library, wrap `FakeLocalTimeZone` in `using` keyword and provide local time zone ID that you want to switch to.

Example:

```csharp
using (new FakeLocalTimeZone(TimeZoneInfo.FindSystemTimeZoneById("UTC+12")))
{
    var localDateTime = new DateTime(2020, 12, 31, 23, 59, 59, DateTimeKind.Local);
    var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime);

    Assert.That(TimeZoneInfo.Local.Id, Is.EqualTo("UTC+12")); // ✅ Assertion passes
    Assert.That(localDateTime, Is.EqualTo(utcDateTime.AddHours(12))); // ✅ Assertion passes
}

// Now, TimeZoneInfo.Local is the one before setup
```

Supported frameworks
=

For the main library, following frameworks are supported:

* netcoreapp2.1
* net6.0
* netstandard2.0
* net45

The library has been tested with the following frameworks:

* netcoreapp2.1
* netcoreapp2.2
* netcoreapp3.0
* netcoreapp3.1
* net5.0
* net6.0
* net45
* net451
* net452
* net46
* net461
* net462
* net47
* net471
* net472
* net48

Remarks
=

* The code of fake comes from the StackOverflow: https://stackoverflow.com/q/44413407/297823.
* To find out all TimeZones in your system, you may use the `TimeZoneInfo.GetSystemTimeZones()` code.
