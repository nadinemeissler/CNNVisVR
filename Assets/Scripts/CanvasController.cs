using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Controlls the UI-Elements   
 */

public class CanvasController : MonoBehaviour
{
    public Canvas canvasMiddle;

    SpriteLoader spriteLoader;
    Button[] filterBtns, fmBtns;

    Sprite[,] filterSprites;

    private void Awake()
    {
        spriteLoader = this.gameObject.GetComponent<SpriteLoader>();

        filterSprites = new Sprite[2,64];

        // Get button components from canvas
        // change to parameter to true if buttons are inactive on start
        filterBtns = canvasMiddle.GetComponentsInChildren<Button>(false);

        

        fmBtns = new Button[64];
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Anz. Buttons: "+filterBtns.Length);

       foreach (var btn in filterBtns)
        {
            Debug.Log("Button gefunden: "+btn.name);
        }

        // Add loaded sprites from resources to buttons
        if (filterBtns.Length > 0)
        {
            int filterSpritesLenght = spriteLoader.filterSpritesConv2.Length;

            for (int i = 0; i < filterSpritesLenght; i++)
            {
                filterBtns[i].image.sprite = spriteLoader.filterSpritesConv2[i];
            }

            // If there are more Buttons than sprites set remaining buttons to inactive
            if(filterBtns.Length > filterSpritesLenght)
            {
                for(int i = filterSpritesLenght; i < filterBtns.Length; i++)
                {
                    filterBtns[i].gameObject.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
