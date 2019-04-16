using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class ViveControllerInput : MonoBehaviour
{
    public bool triggerPressed, triggerDown;
    public Canvas featureMapCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions._default.InteractUI.GetStateUp(SteamVR_Input_Sources.Any))
        {
            print("Trigger is up!");
            triggerDown = false;
            triggerPressed = true;
        }

        if (SteamVR_Actions._default.InteractUI.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("Trigger is down!");
            triggerPressed = false;
            triggerDown = true;
        }

        // Test um Feature Map über Drücken des Triggers vergrößert anzuzeigen
        /*if (triggerPressed)
        {
            if (featureMapCanvas.gameObject.activeSelf)
            {
                featureMapCanvas.gameObject.SetActive(false);
            } else
            {
                featureMapCanvas.gameObject.SetActive(true);
            }
            
            triggerPressed = false;
        }*/

        // Mit Taste "s" Screenshot aus HMD Sicht auf Desktop speichern
        if (Input.GetKeyDown(KeyCode.S))
        {
            TakeScreenshot();
        }
    }

    // Screenshot wird auf dem Desktop unter "Unity_Screenshot_[Zeitstempel].png" gespeichert
    private void TakeScreenshot()
    {
        string destination = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/Unity_Screenshot_" + System.DateTime.Now.ToString("dd_MM_yyy_HH_mm_ss") + ".png";
        ScreenCapture.CaptureScreenshot(destination);
        Debug.Log("Screenshot gespeichert!");
    }
}
