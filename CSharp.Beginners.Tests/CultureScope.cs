using System.Globalization;

namespace CSharp.Beginners.Tests;

/// <summary>
/// Pins the current culture for the lifetime of a test so currency and number formatting are predictable.
/// </summary>
public abstract class CultureScope : IDisposable
{
    private readonly CultureInfo _originalCulture = CultureInfo.CurrentCulture;

    protected CultureScope(string cultureName = "en-US")
    {
        CultureInfo.CurrentCulture = new CultureInfo(cultureName);
    }

    public void Dispose()
    {
        CultureInfo.CurrentCulture = _originalCulture;
    }
}
