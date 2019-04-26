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
    // Canvas for input selection
    public Canvas canvasInputSelect;

    // Images in scene which display input image
    public Image[] inputImages;

    // Array for GameObjects which have the layer buttons as children
    public GameObject[] layerObjs;    

    SpriteLoader spriteLoader;
    // Array for buttons from 3D-Model
    Button[] layerBtns;
    Layer[] layers;
    Layer selectedLayer;
    int selectedInput;
    Text filterNum, outNum, inNum;
    Image filterPanel, fmPanelIn, fmPanelOut, inputImgPanel;
    Button[] filterBtns, fmBtnsIn, fmBtnsOut;

    bool inputChanged;

    private void Awake()
    {
        spriteLoader = this.gameObject.GetComponent<SpriteLoader>();

        // Get button components from canvas
        // change the parameter to true if buttons are inactive on start
        filterPanel = canvasOperation[0].transform.Find("Filter_Panel").GetComponent<Image>();
        fmPanelIn = canvasInOut[0].transform.Find("FM_Panel").GetComponent<Image>();
        fmPanelOut = canvasInOut[1].transform.Find("FM_Panel").GetComponent<Image>();

        filterBtns = filterPanel.GetComponentsInChildren<Button>(true);

        fmBtnsIn = fmPanelIn.GetComponentsInChildren<Button>(true);
        fmBtnsOut = fmPanelOut.GetComponentsInChildren<Button>(true);

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
        // get text component for number of output fms from canvas
        outNum = canvasInOut[1].transform.Find("FeatureM_Text").GetComponent<Text>();

        // get button component for input image button from canvas_middle_input
        inputImgPanel = canvasInOut[0].transform.Find("InputImg_Panel").GetComponent<Image>();

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
    }

    // Update is called once per frame
    void Update()
    {
        if(inputChanged)
        {
            inputChanged = false;
            switch (selectedLayer.layerType)
            {
                case "input":
                    break;
                case "conv":
                    UpdateConv();
                    break;
                case "pool":
                    break;
                case "fc":
                    break;
                case "output":
                    break;
                default:
                    Debug.Log("CanvasController: Unknown layerType");
                    break;
            }
        }
    }

    // Called from layer buttons on 3D model
    public void ShowLayerDetails()
    {
        var go = EventSystem.current.currentSelectedGameObject;

        selectedLayer = go.GetComponent<Layer>();

        // Set layer name on UI and show it
        canvasLayerName.GetComponentInChildren<Text>().text = selectedLayer.layerName;
        canvasLayerName.gameObject.SetActive(true);


        string type = selectedLayer.layerType;

        // call next functions depending on layer type
        switch (type)
        {
            case "input":
                Debug.Log("Layer: " + type);
                break;
            case "conv":
                UpdateConv();
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
    }

    // Called from buttons from Canvas_Middle_Input
    public void ShowInputSelection()
    {
        // Show input selection canvas
        canvasInputSelect.gameObject.SetActive(true);
    }

    // Called from buttons from input selection canvas
    public void ChangeInputImg()
    {
        var go = EventSystem.current.currentSelectedGameObject;

        // Detect which image was selected and save ID in selectedInput
        switch (go.name)
        {
            case "Input_Image_0":
                selectedInput = 0;
                break;
            case "Input_Image_1":
                selectedInput = 1;
                break;
            case "Input_Image_2":
                selectedInput = 2;
                break;
            case "Input_Image_3":
                selectedInput = 3;
                break;
            case "Input_Image_4":
                selectedInput = 4;
                break;
            case "Input_Image_5":
                selectedInput = 5;
                break;
        }

        Debug.Log("New input img: "+selectedInput);

        // Change input images in scene to new input image
        if(spriteLoader.inputImg.Length > selectedInput)
        {
            for(int i = 0; i < inputImages.Length; i++)
            {
                inputImages[i].sprite = spriteLoader.inputImg[selectedInput];
            }   
        }

        // TO-DO
        // Change fms according to new input
        // Update and boolean for changes??
        // own functions for filter and fms with layer type as input?
        // called here or with boolean for change in update
        inputChanged = true;

        // Hide input selection canvas
        // Add animation?
        canvasInputSelect.gameObject.SetActive(false);
        
    }

    private void ShowInput()
    {
        // set values on Input layer canvas

        

        // Show Canvas
    }

    private void UpdateConv()
    {
        // set values on operation canvas
        // filter
        // #filter --> größe des arrays abfragen
        // filter size --> beibehalten auf canvas


        // Load sprites from conv1 or conv2 filters and fms depending on layer name
        Sprite[] sprites;
        Sprite[] spritesOut;
        // number of filter/fms
        int fmNum = 0;

        switch (selectedLayer.layerName)
        {
            case "Convolution 1":
                sprites = spriteLoader.filterSpritesConv1;
                spritesOut = spriteLoader.fmSpritesConv1;
                Debug.Log("Conv1: sprites length "+sprites.Length);
                Debug.Log("spritesout length " + spritesOut.Length);
                break;
            case "Convolution 2":
                sprites = spriteLoader.filterSpritesConv2;
                spritesOut = spriteLoader.fmSpritesConv2;
                Debug.Log("Conv2: sprites length " + sprites.Length);
                Debug.Log("spritesout length " + spritesOut.Length);
                break;
            default:
                sprites = new Sprite[1];
                spritesOut = new Sprite[1];
                break;
        }

        int spritesLength = sprites.Length;

        // length of array with filters = number of filters/fms
        fmNum = sprites.Length;

        if(spritesLength < 2)
        {
            Debug.Log("CanvasController - ShowConv(): No filter sprites loaded");
            return;
        }

        if (spritesOut.Length < 2)
        {
            Debug.Log("CanvasController - ShowConv(): No fm out sprites loaded");
            return;
        }

        // Set text for number of filters
        filterNum.text = spritesLength.ToString()+" Filter";
        // Set text for number of output fms
        outNum.text = spritesLength.ToString()+" Feature Maps";

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

        // show FMs depending on input image and layer
        // selectedInput gives ID for input image

        // Set values for output canvas
        // new fms

        // Add loaded fm sprites from resources to buttons
        if (fmBtnsOut != null)
        {
            // depending on input ID show the fms for input
            switch (selectedInput)
            {
                case 0:
                    for (int i = 0; i < fmNum; i++)
                    {
                        fmBtnsOut[i].image.sprite = spritesOut[i];
                        fmBtnsOut[i].gameObject.SetActive(true);
                    }
                    break;
                case 1:
                    for (int i = fmNum, j = 0; i < (fmNum * 2); i++, j++)
                    {
                        fmBtnsOut[j].image.sprite = spritesOut[i];
                        fmBtnsOut[j].gameObject.SetActive(true);
                    }
                    break;
                case 2:
                    for (int i = (fmNum * 2), j = 0; i < (fmNum * 3); i++, j++)
                    {
                        fmBtnsOut[j].image.sprite = spritesOut[i];
                        fmBtnsOut[j].gameObject.SetActive(true);
                    }
                    break;
                case 3:
                    for (int i = (fmNum * 3), j = 0; i < (fmNum * 4); i++, j++)
                    {
                        fmBtnsOut[j].image.sprite = spritesOut[i];
                        fmBtnsOut[j].gameObject.SetActive(true);
                    }
                    break;
                case 4:
                    for (int i = (fmNum * 4), j = 0; i < (fmNum * 5); i++, j++)
                    {
                        fmBtnsOut[j].image.sprite = spritesOut[i];
                        fmBtnsOut[j].gameObject.SetActive(true);
                    }
                    break;
                case 5:
                    for (int i = (fmNum * 5), j = 0; i < (fmNum * 6); i++, j++)
                    {
                        fmBtnsOut[j].image.sprite = spritesOut[i];
                        fmBtnsOut[j].gameObject.SetActive(true);
                    }
                    break;
                default:
                    Debug.Log("No valid input index selected");
                    break;
            }

            // If there are more Buttons than sprites set remaining buttons to inactive
            if (fmBtnsOut.Length > fmNum)
            {
                for (int i = fmNum; i < fmBtnsOut.Length; i++)
                {
                    fmBtnsOut[i].gameObject.SetActive(false);
                }
            }
        }

        
    }

    // Show all UI elements for Conv layer
    private void ShowConv()
    {
        // if selected layer is first layer after input
        // show input image else show fm buttons
        if (selectedLayer.layerID == 1)
        {
            // deactivate feature map buttons
            fmPanelIn.gameObject.SetActive(false);

            // ativate input image buttons
            inputImgPanel.gameObject.SetActive(true);
        }
        else
        {
            // activate feature map buttons
            fmPanelIn.gameObject.SetActive(true);

            // deativate input image buttons
            inputImgPanel.gameObject.SetActive(false);
        }

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
}
