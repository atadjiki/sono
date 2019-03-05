using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingFunctions
{
    public static float EaseOutQuint(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value * value * value + 1) + start;
    }

    public static float EaseInSine(float start, float end, float value)
    {
        end -= start;
        return -end * Mathf.Cos(value * (Mathf.PI * 0.5f)) + end + start;
    }

    public static float EaseInOutQuad(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value + start;
        value--;
        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }

    public static float EaseInOutQuint(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value * value + start;
        value -= 2;
        return end * 0.5f * (value * value * value * value * value + 2) + start;
    }

    public static Vector3 EaseInOutQuad(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(
            EaseInOutQuad(start.x, end.x, value),
            EaseInOutQuad(start.y, end.y, value),
            EaseInOutQuad(start.z, end.z, value)
            );
    }

    public static Vector3 EaseInOutQuint(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(
            EaseInOutQuint(start.x, end.x, value),
            EaseInOutQuint(start.y, end.y, value),
            EaseInOutQuint(start.z, end.z, value)
            );
    }
}
