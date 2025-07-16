using System;
using UnityEngine;

public class MouseCursorManager : Singleton<MouseCursorManager>
{
    /**
     * Enables read/write access for cursor textures at runtime.
     * This setting is required to resize the texture or access its pixel data via scripts.
     */
    [Header("[Cursor Image]")] 
    [SerializeField] private CursorInfo basicCursorInfo;
    [SerializeField] private CursorInfo selectCursorInfo;
    [SerializeField] private CursorInfo mouseEnterCursorInfo;
    [Serializable]
    public struct CursorInfo
    {
        public Texture2D cursorImage;
        public Vector2 resizeRatio;
        public Vector2 hotspotRatio;
        public CursorMode cursorMode;
    }

    protected override void Awake()
    {
        base.Awake();
        
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitCursorImage();
        SetBasicCursor();
    }

    private void InitCursorImage()
    {
        CursorImageResize(ref basicCursorInfo);
        CursorImageResize(ref selectCursorInfo);
        CursorImageResize(ref mouseEnterCursorInfo);
    }

    private void CursorImageResize(ref CursorInfo cursorInfo)
    {
        Texture2D cursorTexture = cursorInfo.cursorImage;
        Vector2 resizeRatio = cursorInfo.resizeRatio;
        
        if(!cursorTexture) { return; }
        
        Debug.Assert(resizeRatio.x > 0f, "ResizeRatio.x must be greater than 0");
        Debug.Assert(resizeRatio.y > 0f, "ResizeRatio.y must be greater than 0");
        
        cursorInfo.cursorImage = ChangTextureSize
        (
            cursorTexture,
            (int)(cursorTexture.width * resizeRatio.x),
            (int)(cursorTexture.height * resizeRatio.y)
        );
    }
    
    public void SetCustomImageCursor(CursorInfo cursorInfo)
    {
        Texture2D cursorTexture = cursorInfo.cursorImage;
        Vector2 hotspot = cursorInfo.hotspotRatio;
        
        if(!cursorTexture) { return; }
        
        hotspot = new Vector2
        (
            cursorTexture.width * hotspot.x, 
            cursorTexture.height * hotspot.y
        );
        
        Cursor.SetCursor(cursorTexture, hotspot, cursorInfo.cursorMode);
    }

    private Texture2D ChangTextureSize(Texture2D texture, int newWidth, int newHeight)
    {
        texture = TextureScale.Bilinear(texture, newWidth, newHeight);

        return texture;
    }
    
    public void SetBasicCursor()
    {
        SetCustomImageCursor(basicCursorInfo);
    }
    
    public void SetSelectCursor()
    {
        SetCustomImageCursor(selectCursorInfo);
    }
    
    public void SetOnMouseEnterCursor()
    {
        SetCustomImageCursor(mouseEnterCursorInfo);
    }
}

public static class TextureScale
{
    public static Texture2D Bilinear(Texture2D source, int newWidth, int newHeight)
    {
        Texture2D result = new Texture2D(newWidth, newHeight);
        Color[] pixels = new Color[newWidth * newHeight];

        float xRatio = (float)source.width / newWidth;
        float yRatio = (float)source.height / newHeight;
        
        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                float xLerp = x * xRatio;
                float yLerp = y * yRatio;

                int x1 = Mathf.FloorToInt(xLerp);
                int y1 = Mathf.FloorToInt(yLerp);
                int x2 = Mathf.Min(x1 + 1, source.width - 1);
                int y2 = Mathf.Min(y1 + 1, source.height - 1);

                Color colorA = source.GetPixel(x1, y1);
                Color colorB = source.GetPixel(x2, y1);
                Color colorC = source.GetPixel(x1, y2);
                Color colorD = source.GetPixel(x2, y2);

                float xFraction = xLerp - x1;
                float yFraction = yLerp - y1;

                Color top = Color.Lerp(colorA, colorB, xFraction);
                Color bottom = Color.Lerp(colorC, colorD, xFraction);
                pixels[y * newWidth + x] = Color.Lerp(top, bottom, yFraction);
            }
        }

        result.SetPixels(pixels);
        result.Apply();

        return result;
    }
}
