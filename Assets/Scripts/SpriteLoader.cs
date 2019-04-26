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
    public Sprite[,] fmSprites;
    public Sprite[] inputImg;

    public Sprite[] fmSpritesConv1, fmSpritesConv2, fmSpritesPool1;

    int maxFms = 64;

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

            // Load all input images
            inputImg = Resources.LoadAll<Sprite>("Sprites/InputImg");

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

        /*
        fmSprites = new Sprite[3, maxFms];

        if((fmSpritesConv1 != null) && (fmSpritesConv2 != null) && (fmSpritesPool1 != null))
        {
            // save fm sprites in 2 dimensional array to get easy access
            for(int i = 0; i < maxFms; i++)
            {
                if(fmSpritesConv1.Length > i)
                {
                    fmSprites[0, i] = fmSpritesConv1[i];
                }
                if (fmSpritesConv2.Length > i)
                {
                    fmSprites[1, i] = fmSpritesConv2[i];
                }
                if (fmSpritesPool1.Length > i)
                {
                    fmSprites[2, i] = fmSpritesPool1[i];
                }
            }
        } else
        {
            Debug.Log("No fm sprites to load into array");
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        // Resources.UnloadUnusedAssets
    }
}
