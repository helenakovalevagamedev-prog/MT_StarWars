using UnityEngine;

public class ScreenInfo
{
    public float MinX {get; private set;}
    public float MaxX { get; private set; }
    public float MinY { get; private set; }
    public float MaxY { get; private set; }
    public float Aspect { get; private set; }
    public float HalfHeight { get; private set; }
    public float HalfWidth { get; private set; }
    public float ScreenCenter { get; private set; }
    public ScreenInfo()
    {
        Aspect = (float)Screen.width / Screen.height;
        HalfHeight = Camera.main.orthographicSize;
        HalfWidth = HalfHeight * Aspect;
        MinX = -HalfWidth;
        MaxX = HalfWidth;
        MinY = -HalfHeight;
        MaxY = HalfHeight;
        ScreenCenter = Screen.width / 2f;
    }
}
