using UnityEngine;

/// <summary>
/// A list of utility classes
/// </summary>
public static class UtilityFunctions
{
    /// <summary>
    /// Clamps an input position to within the bounds of a defined Transform Rect. (used for bounding Touch input within a UI canvas area)
    /// </summary>
    /// <param name="position">The Vector2 position of the input</param>
    /// <param name="boundingCanvas">The Bounding Canvas to clamp the Input to</param>
    /// <param name="clampedPosition">The out var to assign the recieving var</param>
    /// <returns>Clamped Position to the Bounding Canvas</returns>
    public static Vector2 ClampToWindow(Vector2 position, RectTransform boundingCanvas, out Vector2 clampedPosition)
    {
        Debug.Log("Clamping to window");
        Vector2 rawPointerPosition = position;

        Vector3[] canvasCorners = new Vector3[4];
        
        boundingCanvas.GetWorldCorners(canvasCorners);
        float clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        float clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

        clampedPosition = new Vector2(clampedX, clampedY);
        return clampedPosition;
    }

    public static Vector2 ClampToWindow(Vector2 position, Vector2[] botLeftTopRight, out Vector2 clampedPosition)
    {
        Vector2 rawPointerPosition = position;

        
        float clampedX = Mathf.Clamp(rawPointerPosition.x, botLeftTopRight[0].x, botLeftTopRight[1].x);
        float clampedY = Mathf.Clamp(rawPointerPosition.y, botLeftTopRight[0].y, botLeftTopRight[1].y);

        clampedPosition = new Vector2(clampedX, clampedY);
        return clampedPosition;
    }

    public static Texture2D SaveAsFlippedTexture2D(Texture2D input, bool vertical, bool horizontal)
    {
        Texture2D flipped = new Texture2D(input.width, input.height);
        Color[] data = input.GetPixels();
        Color[] flipped_data = new Color[data.Length];

        for (int x = 0; x < input.width; x++)
        {
            for (int y = 0; y < input.height; y++)
            {
                int index = 0;
                if (horizontal && vertical)
                    index = input.width - 1 - x + (input.height - 1 - y) * input.width;
                else if (horizontal && !vertical)
                    index = input.width - 1 - x + y * input.width;
                else if (!horizontal && vertical)
                    index = x + (input.height - 1 - y) * input.width;
                else if (!horizontal && !vertical)
                    index = x + y * input.width;

                flipped_data[x + y * input.width] = data[index];
            }
        }
        flipped.SetPixels(flipped_data);
        flipped.Apply();
        return flipped;
    }

    public static Texture2D rotateTexture(Texture2D tex, float angle)
    {
        Debug.Log("rotating");
        Texture2D rotImage = new Texture2D(tex.width, tex.height);
        int x, y;
        float x1, y1, x2, y2;

        int w = tex.width;
        int h = tex.height;
        float x0 = rot_x(angle, -w / 2.0f, -h / 2.0f) + w / 2.0f;
        float y0 = rot_y(angle, -w / 2.0f, -h / 2.0f) + h / 2.0f;

        float dx_x = rot_x(angle, 1.0f, 0.0f);
        float dx_y = rot_y(angle, 1.0f, 0.0f);
        float dy_x = rot_x(angle, 0.0f, 1.0f);
        float dy_y = rot_y(angle, 0.0f, 1.0f);


        x1 = x0;
        y1 = y0;

        for (x = 0; x < tex.width; x++)
        {
            x2 = x1;
            y2 = y1;
            for (y = 0; y < tex.height; y++)
            {
                //rotImage.SetPixel (x1, y1, Color.clear);          

                x2 += dx_x;//rot_x(angle, x1, y1);
                y2 += dx_y;//rot_y(angle, x1, y1);
                rotImage.SetPixel((int)Mathf.Floor(x), (int)Mathf.Floor(y), getPixel(tex, x2, y2));
            }

            x1 += dy_x;
            y1 += dy_y;

        }

        rotImage.Apply();
        return rotImage;
    }

    private static float rot_x(float angle, float x, float y)
    {
        float cos = Mathf.Cos(angle / 180.0f * Mathf.PI);
        float sin = Mathf.Sin(angle / 180.0f * Mathf.PI);
        return (x * cos + y * (-sin));
    }

    private static float rot_y(float angle, float x, float y)
    {
        float cos = Mathf.Cos(angle / 180.0f * Mathf.PI);
        float sin = Mathf.Sin(angle / 180.0f * Mathf.PI);
        return (x * sin + y * cos);
    }

    private static Color getPixel(Texture2D tex, float x, float y)
    {
        Color pix;
        int x1 = (int)Mathf.Floor(x);
        int y1 = (int)Mathf.Floor(y);

        if (x1 > tex.width || x1 < 0 ||
           y1 > tex.height || y1 < 0)
        {
            pix = Color.clear;
        }
        else {
            pix = tex.GetPixel(x1, y1);
        }

        return pix;
    }
}
