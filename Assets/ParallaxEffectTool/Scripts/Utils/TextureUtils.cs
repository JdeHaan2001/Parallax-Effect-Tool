using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class TextureUtils
{
    public static Texture2D MergeTextures(Texture2D[] pTextures)
    {
        Resources.UnloadUnusedAssets();

        Vector2 textureSize = GetBiggestTextureSizeInArray(pTextures);

        Texture2D texture = new Texture2D((int)textureSize.x, (int)textureSize.y);

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, new Color(1, 1, 1, 0));
            }
        }

        for (int i = 0; i < pTextures.Length; i++)
        {
            for (int x = 0; x < pTextures[i].width; x++)
            {
                for (int y = 0; y < pTextures[i].height; y++)
                {
                    Color color = pTextures[i].GetPixel(x, y).a == 0 ? texture.GetPixel(x, y) : pTextures[i].GetPixel(x, y);

                    texture.SetPixel(x, y, color);
                }
            }
        }

        texture.Apply();

        return texture;
    }

    public static Sprite Texture2DToSprite(Texture2D pTexture)
    {
        return Sprite.Create(pTexture, new Rect(0, 0, pTexture.width, pTexture.height), new Vector2(0.5f, 0.5f));
    }

    public static Sprite[] TextureArrayToSpriteArray(Texture2D[] pTextures)
    {
        Sprite[] sprites = new Sprite[pTextures.Length];

        for (int i = 0; i < pTextures.Length; i++)
        {
            sprites[i] = Texture2DToSprite(pTextures[i]);
        }

        return sprites;
    }

    public static Vector2 GetBiggestTextureSizeInArray(Texture2D[] pTextures)
    {
        if (pTextures.Length <= 0)
        {
            Debug.LogWarning("Given texture2D array is empty");
            return Vector2.zero;
        }

        Vector2 biggestSize = Vector2.zero;

        for (int i = 0; i < pTextures.Length; i++)
        {
            Texture2D texture = pTextures[i];

            if (texture.width > biggestSize.x)
                biggestSize.x = texture.width;

            if (texture.height > biggestSize.y)
                biggestSize.y = texture.height;
        }

        return biggestSize;
    }
}
