using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    /*
     * Loads the filter and feature map sprites from the resources folder into arrays
     * 
     */

    public Sprite[] filterSpritesConv1, filterSpritesConv2;
    public Sprite[] fmSpritesConv1, fmSpritesConv2, fmSpritesPool1;
    
    private void Awake()
    {
        // Load sprites from resources folder and store them in arrays

        try
        {
            // Load filter sprites for conv1 layer
            filterSpritesConv1 = Resources.LoadAll<Sprite>("Sprites/Filter/conv1");

            // Load filter sprites for conv2 layer
            filterSpritesConv2 = Resources.LoadAll<Sprite>("Sprites/Filter/conv2");

            // Load all feature map sprites for conv1 layer
            fmSpritesConv1 = Resources.LoadAll<Sprite>("Sprites/FeatureMaps/conv1");

            // Load all feature map sprites for conv2 layer
            fmSpritesConv2 = Resources.LoadAll<Sprite>("Sprites/FeatureMaps/conv2");

            // Load all feature map sprites for pool1 layer
            fmSpritesPool1 = Resources.LoadAll<Sprite>("Sprites/FeatureMaps/pool1");

            /*foreach (var s in filterSpritesConv1)
            {
                Debug.Log(s.name);
            }*/
        }
        catch (Exception e)
        {
            Debug.Log("Loading of sprites in SpriteLoader failed with the following exception: ");
            Debug.Log(e);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Resources.UnloadUnusedAssets
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
