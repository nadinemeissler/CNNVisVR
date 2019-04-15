using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LayerInteraction : MonoBehaviour
{
    public ViveControllerInput viveControllerInput;
    bool collision, selected, triggerWasPressed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (!collision)
        {
            print("Kollision");

            if(other.tag != null) //other.transform.parent.parent
            {
                string tag = other.tag;

                print("tag: " + tag);

                if (other.CompareTag("Dot"))
                {
                    print("Kollision mit Pointer");
                    this.transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
                    collision = true;
                }
            }
                      
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if(collision)
        {
            if((!triggerWasPressed) && SteamVR_Actions._default.InteractUI.GetStateUp(SteamVR_Input_Sources.Any))
            {
                selected = true;
                triggerWasPressed = true;
                print("Layer is selected");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(collision)
        {
            this.transform.localScale -= new Vector3(0.1F, 0.1F, 0.1F);
            collision = false;
        }
    }
}
