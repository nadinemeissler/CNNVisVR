using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LayerInteraction : MonoBehaviour
{
    public ViveControllerInput viveControllerInput;
    public GameObject pointerObj;
    Pointer pointer;
    public bool collision, selected, triggerWasPressed;


    void Awake()
    {
        pointer = pointerObj.gameObject.GetComponent<Pointer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if pointer collides with object
        if(pointer.m_PointerCollision && !collision)
        {
            collision = true;
            HandlePointerEnter();            
        }

        // Check if pointer exits after collision
        if(pointer.m_PointerExit && collision)
        {
            collision = false;
            HandlePointerExit();            
        }

        // Check for controller input while pointer is colliding
        if(collision && SteamVR_Actions._default.InteractUI.GetStateUp(SteamVR_Input_Sources.Any))
        {
            HandlePointerInteraction();
        }
    }

    private void HandlePointerEnter()
    {
        print("Pointer Enter: scale +0.1");
        this.transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
    }

    private void HandlePointerExit()
    {
        print("Pointer Exit: scale -0.1");
        this.transform.localScale -= new Vector3(0.1F, 0.1F, 0.1F);
        triggerWasPressed = false;
    }

    private void HandlePointerInteraction()
    {
        if ((!triggerWasPressed) && SteamVR_Actions._default.InteractUI.GetStateUp(SteamVR_Input_Sources.Any))
        {
            selected = true;
            triggerWasPressed = true;
            print("Layer is selected");
        }
    }

}
