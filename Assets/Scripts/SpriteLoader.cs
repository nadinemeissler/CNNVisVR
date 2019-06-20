using System;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    /*
     * Author: Nadine Meißler
     * Date: June 2019
     * Description:
     * Loads the filter and feature map sprites from the resources folder into arrays
     * Loads the input images into an array
     * Loads the values of the filters into an array
     * 
     */

    private Sprite[] filterSpritesConv1;
    private Sprite[] inputImg;

    private Sprite[] fmSpritesConv1, fmSpritesPool1;

    private string[,] m_FilterValConv1;

    private void Awake()
    {
        /*
         * Load sprites from resources folder and store them in arrays
         */

        try
        {
            // Load filter sprites for conv1 layer
            filterSpritesConv1 = Resources.LoadAll<Sprite>("Sprites/Filter/conv1");

            // Load all feature map sprites for conv1 layer
            fmSpritesConv1 = Resources.LoadAll<Sprite>("Sprites/FeatureMaps/conv1");

            // Load all feature map sprites for pool1 layer
            fmSpritesPool1 = Resources.LoadAll<Sprite>("Sprites/FeatureMaps/pool1");

            // Load all input images
            inputImg = Resources.LoadAll<Sprite>("Sprites/InputImg");
        }
        catch (Exception e)
        {
            Debug.Log("Loading of sprites in SpriteLoader failed with the following exception: ");
            Debug.Log(e);
        }

        /*
         * load filter values from text file
         */
         
        try
        {
            // load text files with filter values as TextAssets
            TextAsset filterVal1 = Resources.Load<TextAsset>("TextData/weights_conv1");

            string[] filterValConv1;

            // get text from TextAssets and load them into string arrays
            // split at ';'
            // last value in file must not have a ; or last value is empty
            filterValConv1 = filterVal1.ToString().Split(';');

            /*
             * store filter values in two dimensional array for easy access
             */

            m_FilterValConv1 = new string[filterValConv1.Length/9, 9];

            int index = 0;

            for (int i = 0; i < m_FilterValConv1.GetLength(0); i++)
            {
                for(int j = 0; j < m_FilterValConv1.GetLength(1); j++)
                {
                    m_FilterValConv1[i, j] = filterValConv1[index];
                    index++;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Loading of filter values in SpriteLoader failed with the following exception: ");
            Debug.Log(e);
        }
    }

    /*
     * Get functions 
     */ 

    public Sprite[] GetInputImages()
    {
        return inputImg;
    }

    public Sprite[] GetFilterSprites(string layername)
    {
        switch(layername)
        {
            case "Convolution 1":
                return filterSpritesConv1;
            default:
                Debug.Log("SpriteLoader - GetFilterSprites(): invalid layername: "+layername);
                return new Sprite[1];
        }
    }

    public Sprite[] GetFmSprites(string layername)
    {
        switch (layername)
        {
            case "Convolution 1":
                return fmSpritesConv1;
            case "Pooling 1":
                return fmSpritesPool1;
            default:
                Debug.Log("SpriteLoader - GetFmSprites(): invalid layername: "+layername);
                return new Sprite[1];
        }
    }

    public string[,] GetFilterValues(string layername)
    {
        switch(layername)
        {
            case "Convolution 1":
                return m_FilterValConv1;
            default:
                Debug.Log("SpriteLoader - GetFilterValues(): invalid layername: " + layername);
                return new string[1,1];
        }
    }
}
