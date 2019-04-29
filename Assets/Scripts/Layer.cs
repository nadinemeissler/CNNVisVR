using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    // TO-DO
    // Add set and get methods
    private int layerID = -1;
    private int inputLength = -1;
    private int outputLength = -1;
    private string layerType;
    private string layerName;

    public void SetLayerID(int newID)
    {
        if(newID > -1)
        {
            layerID = newID;
        } else
        {
            Debug.Log("Layer - SetLayerID(): invalid layerID");
        }
    }

    public int GetLayerID()
    {
        return layerID;
    }

    public void SetInputOutputLength(int newInputLength, int newOutputLength)
    {
        switch(layerType)
        {
            case "conv":
            case "pool":
                inputLength = newInputLength;
                outputLength = newOutputLength;
                break;
            default:
                Debug.Log("Layer - SetInputLength: invalid layerType or layerType has no inputLenght");
                break;
        }
    }

    public int GetInputLength()
    {
        return inputLength;
    }

    public int GetOutputLength()
    {
        return outputLength;
    }

    public void SetLayerType(string newType)
    {
        if(newType.Equals("conv") || newType.Equals("pool") || newType.Equals("input") || newType.Equals("fc") || newType.Equals("output"))
        {
            layerType = newType;
        } else
        {
            Debug.Log("Layer - SetLayerType(): unknown layerType");
        }
    }

    public string GetLayerType()
    {
        return layerType;
    }

    public void SetLayerName(string newName)
    {
        layerName = newName;
    }

    public string GetLayerName()
    {
        return layerName;
    }
}
