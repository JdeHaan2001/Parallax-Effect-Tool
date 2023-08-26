using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageMerger
{
    public static Texture2D MergeTextures(Texture2D[] pTextures)
    {
        Vector2 newTextureSize = GetBiggestTextureSize(pTextures);

        Texture2D finalTexture = new Texture2D((int)newTextureSize.x, (int)newTextureSize.y);
        Color[] colorArray = new Color[finalTexture.width * finalTexture.height];

        Color[][] srcArray = new Color[pTextures.Length][];

        //populate array
        for (int i = 0; i < pTextures.Length; i++)
        {
            srcArray[i] = pTextures[i].GetPixels();
        }

        for (int x = 0; x < finalTexture.width; x++)
        {
            for (int y = 0; y < finalTexture.height; y++)
            {
                int pixelIndex = x + (y * finalTexture.width);
                for (int i = 0; i < pTextures.Length; i++)
                {                    
                    Color srcPixel = srcArray[i][pixelIndex];
                    if(srcPixel.a > 0)
                        colorArray[pixelIndex] = srcPixel;
                }                
            }
        }

        finalTexture.SetPixels(colorArray);
        finalTexture.Apply();

        return finalTexture;
    }

    private static Vector2 GetBiggestTextureSize(Texture2D[] pTextures)
    {
        float biggestWidth = 0;
        float biggestHeight = 0;

        for (int i = 0; i < pTextures.Length; i++)
        {
            Texture2D tex = pTextures[i];

            if (tex.width > biggestWidth)
                biggestWidth = tex.width;
            if (tex.height > biggestHeight)
                biggestHeight = tex.height;
        }

        return new Vector2(biggestWidth, biggestHeight);
    }
}
