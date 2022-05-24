A very small package that helps to fake (a.k.a. mock) system `TimeZone`.

This package may be useful for testing DateTime and local/UTC conversions.

Usage
=

In order to use the library, wrap `FakeLocalTimeZone` in `using` keyword and provide local time zone ID that you want to switch to.

Example:

```
using (new FakeLocalTimeZone(TimeZoneInfo.FindSystemTimeZoneById("UTC+12")))
{
    var localDateTime = new DateTime(2020, 12, 31, 23, 59, 59, DateTimeKind.Local);
    var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime);

    Assert.Multiple(() =>
    {
        Assert.That(utcDateTime, Is.EqualTo(localDateTime.AddHours(-12))); // Passes
    });
}
```

Supported frameworks
=

For the main library, following frameworks are supported:

* netcoreapp2.0
* net6.0
* netstandard2.0
* net45

The lib has been tested for the following frameworks:

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

* The code of Fake comes from the StackOverflow: https://stackoverflow.com/q/44413407/297823.
* To find out all TimeZones in your system, you may use the `TimeZoneInfo.GetSystemTimeZones())` code.