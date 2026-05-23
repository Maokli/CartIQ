namespace CartIQ.Application.Common.Utils;
using System;

public static class AvatarColorGenerator
{
    public static (string BackgroundColor, string TextColor) Generate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentNullException(nameof(input));
        }

        int hash = 0;
        foreach (char c in input)
        {
            hash = c + ((hash << 5) - hash);
        }

        int baseHash = Math.Abs(hash);

        // --- Background HSL ---
        int hue = baseHash % 360;
        int saturation = 65 + (baseHash % 20); // 65–85%
        int lightness = 45 + (baseHash % 10);  // 45–55%

        var (r, g, b) = HslToRgb(hue, saturation / 100.0, lightness / 100.0);
        string background = $"#{r:X2}{g:X2}{b:X2}";

        // --- Text color: same hue, darker ---
        int textLightness = Math.Max(15, lightness - 25); // push it darker
        int textSaturation = Math.Min(100, saturation + 5); // slightly richer

        var (tr, tg, tb) = HslToRgb(hue, textSaturation / 100.0, textLightness / 100.0);
        string textColor = $"#{tr:X2}{tg:X2}{tb:X2}";

        return (background, textColor);
    }

    private static (int r, int g, int b) HslToRgb(double h, double s, double l)
    {
        double c = (1 - Math.Abs(2 * l - 1)) * s;
        double x = c * (1 - Math.Abs((h / 60.0 % 2) - 1));
        double m = l - c / 2;

        double rPrime = 0, gPrime = 0, bPrime = 0;

        if (h < 60) { rPrime = c; gPrime = x; }
        else if (h < 120) { rPrime = x; gPrime = c; }
        else if (h < 180) { gPrime = c; bPrime = x; }
        else if (h < 240) { gPrime = x; bPrime = c; }
        else if (h < 300) { rPrime = x; bPrime = c; }
        else { rPrime = c; bPrime = x; }

        int r = (int)Math.Round((rPrime + m) * 255);
        int g = (int)Math.Round((gPrime + m) * 255);
        int b = (int)Math.Round((bPrime + m) * 255);

        return (r, g, b);
    }
}