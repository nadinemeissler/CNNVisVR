using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Controlls the UI-Elements   
 */

public class CanvasController : MonoBehaviour
{
    // Array for the three different operation canvas, conv - pool - fc
    public Canvas[] canvasOperation;
    // Array for the input and output canvas, input - output
    public Canvas[] canvasInOut;
    // Canvas for layer name
    public Canvas canvasLayerName;

    // Array for GameObjects with the layer buttons as children
    public GameObject[] layerObjs;    

    SpriteLoader spriteLoader;
    // Array for buttons from 3D-Model
    Button[] layerBtns;
    Layer[] layers;
    Layer selectedLayer;
    int selectedInput;
    Text filterNum;

    Image filterPanel, fmPanel;
    Button[] filterBtns, fmBtns;

    private void Awake()
    {
        spriteLoader = this.gameObject.GetComponent<SpriteLoader>();

        // Get button components from canvas
        // change the parameter to true if buttons are inactive on start
        filterPanel = canvasOperation[0].transform.Find("Filter_Panel").GetComponent<Image>();
        filterBtns = filterPanel.GetComponentsInChildren<Button>(true);

        fmBtns = canvasOperation[2].GetComponentsInChildren<Button>(true);

        layerBtns = new Button[layerObjs.Length];
        layers = new Layer[layerBtns.Length];

        // get buttons from layer gameobjects
        for (int i = 0; i < layerObjs.Length; i++)
        {
            layerBtns[i] = layerObjs[i].GetComponentInChildren<Button>();
        }

        // reference layer scripts in Array
        for(int i = 0; i < layerBtns.Length; i++)
        {
            layers[i] = layerBtns[i].GetComponent<Layer>();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // get text component for number of filters from canvas
        filterNum = canvasOperation[0].transform.Find("FilterNum_Text").GetComponent<Text>();

        // set layer IDs, layer types and names
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].layerID = i;

            switch (layers[i].layerID)
            {
                case 0:
                    layers[i].layerType = "input";
                    layers[i].layerName = "Input";
                    break;
                case 1:
                    layers[i].layerType = "conv";
                    layers[i].layerName = "Convolution 1";
                    break;
                case 2:
                    layers[i].layerType = "conv";
                    layers[i].layerName = "Convolution 2";
                    break;
                case 3:
                    layers[i].layerType = "pool";
                    layers[i].layerName = "Pooling 1";
                    break;
                case 4:
                    layers[i].layerType = "fc";
                    layers[i].layerName = "Fully Connected 1";
                    break;
                case 5:
                    layers[i].layerType = "output";
                    layers[i].layerName = "Output";
                    break;
            }
        }

        // For tests!!
        // Delete!
        //ShowConv();


        Debug.Log("Anz. Buttons: "+filterBtns.Length);

       foreach (var btn in filterBtns)
        {
            Debug.Log("Button gefunden: "+btn.name);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowInput()
    {
        // set values on Input middle canvas
    }

    private void ShowConv()
    {
        // set values on operation canvas
        // filter
        // #filter --> größe des arrays abfragen
        // filter size --> beibehalten auf canvas

        
        // Load sprites from conv1 or conv2 filters depending on layer name
        Sprite[] sprites;

        switch(selectedLayer.layerName)
        {
            case "Convolution 1":
                sprites = spriteLoader.filterSpritesConv1;
                break;
            case "Convolution 2":
                sprites = spriteLoader.filterSpritesConv2;
                break;
            default:
                sprites = new Sprite[1];
                break;
        }

        int spritesLength = sprites.Length;

        if(spritesLength == 1)
        {
            Debug.Log("CanvasController - ShowConv(): No filter sprites loaded");
            return;
        }

        // Set text for number of filters
        filterNum.text = spritesLength.ToString()+" Filter";

        // Add loaded sprites from resources to buttons
        if (filterBtns.Length > 0)
        {
            for (int i = 0; i < spritesLength; i++)
            {
                filterBtns[i].image.sprite = sprites[i];
                filterBtns[i].gameObject.SetActive(true);
            }

            // If there are more Buttons than sprites set remaining buttons to inactive
            if (filterBtns.Length > spritesLength)
            {
                for (int i = spritesLength; i < filterBtns.Length; i++)
                {
                    filterBtns[i].gameObject.SetActive(false);
                }
            }
        }

        // set values on input canvas
        // image or fms



        //set values for output canvas
        // new fms

        // show input canvas
        if (canvasInOut[0] != null && !canvasInOut[0].gameObject.activeSelf)
        {
            canvasInOut[0].gameObject.SetActive(true);
        }

        // show Operation Canvas
        if (canvasOperation[0] != null && !canvasOperation[0].gameObject.activeSelf)
        {
            canvasOperation[0].gameObject.SetActive(true);
        }

        // show output canvas
        if (canvasInOut[1] != null && !canvasInOut[1].gameObject.activeSelf)
        {
            canvasInOut[1].gameObject.SetActive(true);
        }
    }

    private void ShowPool()
    {
        // set values on operation canvas
        // set values on input canvas
        //set values for output canvas

        // show input canvas
        // show Operation Canvas
        // show output canvas
    }

    private void ShowFC()
    {
        // set values on operation canvas
        // set values on input canvas
        //set values for output canvas

        // show input canvas
        // show Operation Canvas
        // show output canvas
    }

    private void ShowOutput()
    {
        // Set values on Output middle Canvas
    }

    public void ShowLayerDetails()
    {
        var go = EventSystem.current.currentSelectedGameObject;

        selectedLayer = go.GetComponent<Layer>();

        // Set layer name on UI and show it
        canvasLayerName.GetComponentInChildren<Text>().text = selectedLayer.layerName;
        canvasLayerName.gameObject.SetActive(true);


        string type = selectedLayer.layerType;

        switch (type)
        {
            case "input":
                Debug.Log("Layer: "+ type);
                break;
            case "conv":
                ShowConv();
                break;
            case "pool":
                Debug.Log("Layer: " + type);
                ShowPool();
                break;
            case "fc":
                Debug.Log("Layer: " + type);
                break;
            case "output":
                Debug.Log("Layer: " + type);
                break;
            default:
                Debug.Log("CanvasController: Unknown layerType");
                break;
        }

        /*
        foreach (var canvas in canvasOperation)
        {
            if(!canvas.gameObject.activeSelf)
            {
                canvas.gameObject.SetActive(true);
            }
        }*/
    }
}
