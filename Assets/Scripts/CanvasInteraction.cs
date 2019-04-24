using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInteraction : MonoBehaviour
{
    public Canvas featureMapCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


    public void ShowFM()
    {
        print("Button was clicked!");

        if(featureMapCanvas != null)
        {
            if (featureMapCanvas.gameObject.activeSelf)
            {
                featureMapCanvas.gameObject.SetActive(false);
            }
            else
            {
                featureMapCanvas.gameObject.SetActive(true);
            }
        } else
        {
            print("No Canvas attached");
        }
        
    }
}
