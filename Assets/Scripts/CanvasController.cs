﻿using System.Collections;
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

        // Set layer details here, the script SpriteLoader ??
        // needs to set references to layer input, output and filters in Start()

    }

    // Start is called before the first frame update
    void Start()
    {
        // get text component for number of filters from canvas
        filterNum = canvasOperation[0].transform.Find("FilterNum_Text").GetComponent<Text>();
        // get text component for number of output fms from canvas
        outNum = canvasInOut[1].transform.Find("FeatureM_Text").GetComponent<Text>();
        // get text component for number of input fms from canvas
        inNum = fmPanelIn.transform.Find("FeatureM_Text").GetComponent<Text>();

        // get button component for input image button from canvas_middle_input
        inputImgPanel = canvasInOut[0].transform.Find("InputImg_Panel").GetComponent<Image>();

        // set layer IDs, layer types and names
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].SetLayerID(i);

            switch (i)
            {
                case 0:
                    layers[i].SetLayerType("input");
                    layers[i].SetLayerName("Input");
                    break;
                case 1:
                    layers[i].SetLayerType("conv");
                    layers[i].SetLayerName("Convolution 1");
                    layers[i].SetInputOutputLength(1, 32);
                    break;
                case 2:
                    layers[i].SetLayerType("conv");
                    layers[i].SetLayerName("Convolution 2");
                    layers[i].SetInputOutputLength(32, 64);
                    break;
                case 3:
                    layers[i].SetLayerType("pool");
                    layers[i].SetLayerName("Pooling 1");
                    layers[i].SetInputOutputLength(64, 64);
                    break;
                case 4:
                    layers[i].SetLayerType("fc");
                    layers[i].SetLayerName("Fully Connected 1");
                    break;
                case 5:
                    layers[i].SetLayerType("output");
                    layers[i].SetLayerName("Output");
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
        // Update shown layer details when a new input image was selected
        if(inputChanged)
        {
            inputChanged = false;
            switch (selectedLayer.GetLayerType())
            {
                case "input":
                    break;
                case "conv":
                    UpdateFms("conv","in");
                    UpdateFms("conv", "out");
                    break;
                case "pool":
                    UpdateFms("pool", "in");
                    UpdateFms("pool", "out");
                    break;
                case "fc":
                    break;
                case "output":
                    break;
                default:
                    Debug.Log("CanvasController - Update(): Unknown layerType");
                    break;
            }
        }
    }

    // Called from layer buttons on 3D model
    // Handles the display of the details of the selected layer
    public void ShowLayerDetails()
    {
        var go = EventSystem.current.currentSelectedGameObject;

        selectedLayer = go.GetComponent<Layer>();

        // Set layer name on UI and show it
        canvasLayerName.GetComponentInChildren<Text>().text = selectedLayer.GetLayerName();
        canvasLayerName.gameObject.SetActive(true);


        string type = selectedLayer.GetLayerType();

        // call next functions depending on layer type
        switch (type)
        {
            case "input":
                Debug.Log("Layer: " + type);
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
    }

    // Called from buttons from Canvas_Middle_LayerInput
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
        if(spriteLoader.GetInputImages().Length > selectedInput)
        {
            for(int i = 0; i < inputImages.Length; i++)
            {
                inputImages[i].sprite = spriteLoader.GetInputImages()[selectedInput];
            }   
        }

        inputChanged = true;

        // Hide input selection canvas
        // Add animation?
        canvasInputSelect.gameObject.SetActive(false);
        
    }

    private void ShowInput()
    {
        // TO-DO
        // set values on Input layer canvas

        

        // Show Canvas
    }

    private void UpdateFilters()
    {
        // Load sprites from conv1 or conv2 filters depending on layer name
        Sprite[] filterSprites = spriteLoader.GetFilterSprites(selectedLayer.GetLayerName());

        // number of filter/fms
        // length of array with filters = number of filters/fms
        int filterLength = filterSprites.Length;

        if (filterLength < 2)
        {
            Debug.Log("CanvasController - UpdateFilters(): No filter sprites loaded");
            return;
        }

        // Set text for number of filters on canvas
        filterNum.text = filterLength.ToString() + " Filter";

        // Add loaded sprites from resources to buttons on canvas
        if (filterBtns.Length > 0)
        {
            for (int i = 0; i < filterLength; i++)
            {
                filterBtns[i].image.sprite = filterSprites[i];
                filterBtns[i].gameObject.SetActive(true);
            }

            // If there are more Buttons than sprites set remaining buttons to inactive
            if (filterBtns.Length > filterLength)
            {
                for (int i = filterLength; i < filterBtns.Length; i++)
                {
                    filterBtns[i].gameObject.SetActive(false);
                }
            }
        }        
    }

    private void UpdateFms(string layertype, string inOrOut)
    {
        switch(layertype)
        {
            case "conv":
            case "pool":
                break;
            default:
                Debug.Log("CanvasController - UpdateFms: invalid layertype: "+layertype+". Valid layertypes: conv, pool");
                return;
        }

        Sprite[] fmSprites;
        Button[] fmBtns;
        int fmNum = 0;

        // Check if fms for input or output should be updated
        // load fms from Sprite Loader for input and/or output
        switch (inOrOut)
        {
            case "in":
                // if selectedLayer is first conv layer no fms have to be loaded
                // input is input image
                if(selectedLayer.GetLayerID() == 1) { return; }

                string layername = layers[selectedLayer.GetLayerID()-1].GetLayerName();
                Debug.Log("Vorherige Layer: "+layername);

                fmSprites = spriteLoader.GetFmSprites(layername);
                fmBtns = fmBtnsIn;
                fmNum = selectedLayer.GetInputLength();

                // Set text for number of input fms
                inNum.text = fmNum.ToString() + " Feature Maps";
                break;
            case "out":
                fmSprites = spriteLoader.GetFmSprites(selectedLayer.GetLayerName());
                fmBtns = fmBtnsOut;
                fmNum = selectedLayer.GetOutputLength();

                // Set text for number of output fms
                outNum.text = fmNum.ToString() + " Feature Maps";
                break;
            default:
                fmSprites = new Sprite[1];
                fmBtns = new Button[1];
                break;
        }

        if (fmSprites.Length < 2)
        {
            Debug.Log("CanvasController - UpdateFms(): No fm sprites loaded");
            return;
        }
        if(fmBtns.Length < 2)
        {
            Debug.Log("CanvasController - UpdateFms(): No Buttons for fms found");
            return;
        }

        // Add loaded fm sprites from resources to buttons
        if (fmBtns != null)
        {
            // depending on input ID show the fms for input
            switch (selectedInput)
            {
                case 0:
                    for (int i = 0; i < fmNum; i++)
                    {
                        fmBtns[i].image.sprite = fmSprites[i];
                        fmBtns[i].gameObject.SetActive(true);
                    }
                    break;
                case 1:
                    for (int i = fmNum, j = 0; i < (fmNum * 2); i++, j++)
                    {
                        fmBtns[j].image.sprite = fmSprites[i];
                        fmBtns[j].gameObject.SetActive(true);
                    }
                    break;
                case 2:
                    for (int i = (fmNum * 2), j = 0; i < (fmNum * 3); i++, j++)
                    {
                        fmBtns[j].image.sprite = fmSprites[i];
                        fmBtns[j].gameObject.SetActive(true);
                    }
                    break;
                case 3:
                    for (int i = (fmNum * 3), j = 0; i < (fmNum * 4); i++, j++)
                    {
                        fmBtns[j].image.sprite = fmSprites[i];
                        fmBtns[j].gameObject.SetActive(true);
                    }
                    break;
                case 4:
                    for (int i = (fmNum * 4), j = 0; i < (fmNum * 5); i++, j++)
                    {
                        fmBtns[j].image.sprite = fmSprites[i];
                        fmBtns[j].gameObject.SetActive(true);
                    }
                    break;
                case 5:
                    for (int i = (fmNum * 5), j = 0; i < (fmNum * 6); i++, j++)
                    {
                        fmBtns[j].image.sprite = fmSprites[i];
                        fmBtns[j].gameObject.SetActive(true);
                    }
                    break;
                default:
                    Debug.Log("No valid input index selected");
                    break;
            }

            // If there are more Buttons than sprites set remaining buttons to inactive
            if (fmBtns.Length > fmNum)
            {
                for (int i = fmNum; i < fmBtns.Length; i++)
                {
                    fmBtns[i].gameObject.SetActive(false);
                }
            }
        }
    }

    // Show all UI elements for Conv layer
    private void ShowConv()
    {
        // Update input canvas for layer (input image or fms)

        // if selected layer is first layer after input
        // show input image else show fm buttons
        if (selectedLayer.GetLayerID() == 1)
        {
            // deactivate feature map buttons
            fmPanelIn.gameObject.SetActive(false);

            // ativate input image buttons
            inputImgPanel.gameObject.SetActive(true);
        }
        else
        {
            // update input fm buttons for layer (feature maps from layer before)
            UpdateFms("conv", "in");

            // activate feature map buttons
            fmPanelIn.gameObject.SetActive(true);

            // deativate input image buttons
            inputImgPanel.gameObject.SetActive(false);
        }

        // update filters for layer
        UpdateFilters();

        // update output for layer (new feature maps)
        UpdateFms("conv", "out");

        // show input canvas
        if (canvasInOut[0] != null && !canvasInOut[0].gameObject.activeSelf)
        {
            canvasInOut[0].gameObject.SetActive(true);
        }

        // hide other operation canvas if one is active
        foreach (var opcanvas in canvasOperation)
        {
            if (opcanvas.gameObject.activeSelf) { opcanvas.gameObject.SetActive(false); }
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
        // Update input canvas for layer (feature maps from layer before)
        
        // activate feature map buttons
        fmPanelIn.gameObject.SetActive(true);

        // deativate input image buttons
        inputImgPanel.gameObject.SetActive(false);

        // update sprites of fm buttons with right feature maps
        UpdateFms("pool", "in");

        // TO-DO
        // set values on operation canvas

        // Update output canvas for layer (feature maps from pooling layer)
        UpdateFms("pool","out");

        // show input canvas
        if (canvasInOut[0] != null && !canvasInOut[0].gameObject.activeSelf)
        {
            canvasInOut[0].gameObject.SetActive(true);
        }

        // hide other operation canvas if one is active
        foreach (var opcanvas in canvasOperation)
        {
            if(opcanvas.gameObject.activeSelf) { opcanvas.gameObject.SetActive(false); }
        }

        // show Operation Canvas for active layer
        if (canvasOperation[1] != null && !canvasOperation[1].gameObject.activeSelf)
        {
            canvasOperation[1].gameObject.SetActive(true);
        }

        // show output canvas
        if (canvasInOut[1] != null && !canvasInOut[1].gameObject.activeSelf)
        {
            canvasInOut[1].gameObject.SetActive(true);
        }
    }

    private void ShowFC()
    {
        // TO-DO
        // set values on operation canvas
        // set values on input canvas
        // set values for output canvas

        // show input canvas
        // show Operation Canvas
        // show output canvas
    }

    private void ShowOutput()
    {
        // TO-DO
        // Set values on model Output Canvas
    }
}
