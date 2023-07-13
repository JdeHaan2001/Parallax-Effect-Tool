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

        Debug.Log("Creating object");
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
}
