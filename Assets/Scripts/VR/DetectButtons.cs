using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class DetectButtons : MonoBehaviour
{
    public ActionBasedController rightHand;
    public InputHelpers.Button buttonA;
    public InputHelpers.Button buttonB;
    public bool pressedA;
    bool pressedB;
    void Start()
    {
        pressedA = false;
    }
    void Update()
    {
        //A pressed
        if (rightHand && rightHand.currentControllerState.uiPressInteractionState.active) pressedA = true;
        else pressedA = false;
        
    }
}
