using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class LayerGenerator
{
    private const string LAYER_TAG = "ParallaxLayer";
    private const string LAYER_PARENT_NAME = "Parallax Layers";

    public static ParallaxEffect GenerateLayer(string pName, Sprite pSprite, int pLayer, float pSpeed, bool pRepeatable, bool pRepeatRandom, float pMinHeight, float pMaxHeight, GameObject pLayerParent)
    {

        if (pLayerParent == null)
            pLayerParent = new GameObject(LAYER_PARENT_NAME);

        string objName = "";

        if (pName == "" || pName == string.Empty)
        {
            Debug.Log("Layer name is empty, will set name to 'Parallax Layer'");
            objName = "Parallax Layer";
        }
        else
            objName = pName;

        GameObject obj = new GameObject(objName);

        obj.transform.SetParent(pLayerParent.transform);
        obj.transform.tag = LAYER_TAG;
        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        obj.AddComponent<ParallaxEffect>();

        ParallaxEffect parEffect = obj.GetComponent<ParallaxEffect>();

        renderer.sprite = pSprite;
        renderer.sortingOrder = pLayer;

        parEffect.Speed = pSpeed;
        parEffect.isRepeating = pRepeatable;
        parEffect.isRepeatingRandom = pRepeatRandom;
        parEffect.MinimumHeight = pMinHeight;
        parEffect.MaximumHeight = pMaxHeight;

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

        return parEffect;
    }

    public static bool CreateLayerTag()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        SerializedProperty layersProp = tagManager.FindProperty("layers");

        if (!PropertyExists(layersProp, 0, 31, LAYER_TAG))
        {
            SerializedProperty sp;

            for (int i = 8, j = 31; i < j; i++)
            {
                sp = layersProp.GetArrayElementAtIndex(i);

                if (sp.stringValue == "")
                {
                    sp.stringValue = LAYER_TAG;

                    tagManager.ApplyModifiedProperties();
                    return true;
                }
                if (i == j)
                    Debug.Log("All allowed layers have been filled");
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if the value exists in the property.
    /// </summary>
    private static bool PropertyExists(SerializedProperty property, int start, int end, string value)
    {
        for (int i = start; i < end; i++)
        {
            SerializedProperty t = property.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(value))
            {
                return true;
            }
        }
        return false;
    }
}
