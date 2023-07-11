using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class HapticController : MonoBehaviour
{
    public XRBaseController leftController, rightController;
    public float defaultAmplitude = 0.2f;
    public float defaultDuration = 0.5f;

    public void SendHaptics()
    {
        //Ricardo: He comentado esto porque daba error con el nuevo sistema, habra que adaptarlo.
        //leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
        //leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
    }

}
