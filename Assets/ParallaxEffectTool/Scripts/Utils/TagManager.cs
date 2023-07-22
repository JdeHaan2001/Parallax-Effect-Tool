using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class TagManager
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

    public static Dictionary<string, int> GetAllTags()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset"));
        SerializedProperty layers = tagManager.FindProperty("tags");
        int layerSize = layers.arraySize;

        Dictionary<string, int> LayerDictionary = new Dictionary<string, int>();

        for (int i = 0; i < layerSize; i++)
        {
            SerializedProperty element = layers.GetArrayElementAtIndex(i);
            string layerName = element.stringValue;

            if (!string.IsNullOrEmpty(layerName))
                LayerDictionary.Add(layerName, i);
        }

        return LayerDictionary;
    }

    public static void CreateTag(string pName)
    {
        Dictionary<string, int> dic = GetAllTags();
        if (!dic.ContainsKey(pName))
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layers = tagManager.FindProperty("tags");

            for (int i = 0; i < 31; i++)
            {
                Debug.Log(i);
                SerializedProperty element;

                if (i > (layers.arraySize - 1))
                {
                    layers.arraySize++;

                    element = layers.GetArrayElementAtIndex(i);
                    element.stringValue = pName;

                    tagManager.ApplyModifiedProperties();
                    Debug.Log(i.ToString() + " Layer created: " + pName);
                    break;
                }
            }
        }
    }
}
