using UnityEngine;


public struct HSL
{
    private float _h;
    private float _s;
    private float _l;

    public HSL(float h, float s, float l)
    {
        this._h = h;
        this._s = s;
        this._l = l;
    }

    public float H
    {
        get { return this._h; }
        set { this._h = value; }
    }

    public float S
    {
        get { return this._s; }
        set { this._s = value; }
    }

    public float L
    {
        get { return this._l; }
        set { this._l = value; }
    }

    public bool Equals(HSL hsl)
    {
        return (this.H == hsl.H) && (this.S == hsl.S) && (this.L == hsl.L);
    }
}

public class HSLColor {
    public static Color HSLToRGB(HSL hsl)
    {
        float r = 0;
        float g = 0;
        float b = 0;

        if (hsl.S == 0)
        {
            r = g = b = (byte)(hsl.L);
        }
        else
        {
            float v1, v2;

            v2 = (hsl.L < 0.5) ? (hsl.L * (1 + hsl.S)) : ((hsl.L + hsl.S) - (hsl.L * hsl.S));
            v1 = 2 * hsl.L - v2;

            r = (float)(HueToRGB(v1, v2, hsl.H + (1.0f / 3)));
            g = (float)(HueToRGB(v1, v2, hsl.H));
            b = (float)(HueToRGB(v1, v2, hsl.H - (1.0f / 3)));
        }

        return new Color(r, g, b);
    }

    private static float HueToRGB(float v1, float v2, float vH)
    {
        if (vH < 0)
            vH += 1;

        if (vH > 1)
            vH -= 1;

        if ((6 * vH) < 1)
            return (v1 + (v2 - v1) * 6 * vH);

        if ((2 * vH) < 1)
            return v2;

        if ((3 * vH) < 2)
            return (v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6);

        return v1;
    }
}
