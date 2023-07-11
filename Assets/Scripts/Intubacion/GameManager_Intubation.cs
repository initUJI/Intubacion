using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager_Intubation : MonoBehaviour
{
    public MaquinaEstadosIntubation fsm;
    [SerializeField] Canvas Tv;
    //AudioSource correctAudio;

    public XRBaseController leftController, rightController;

    void Start()
    {
        fsm = GetComponent<MaquinaEstadosIntubation>();
        //correctAudio = Tv.GetComponent<AudioSource>();
    }

    void Update()
    {
        //Debug.Log(AbrirBoca.vibrar);
        if (AbrirBoca.vibrar)
        {
            // Activa la vibración en el controlador

           SendHaptics(0.5f, 0.5f);
        }
    }

    public void ActivarEstado(MaquinaEstadosIntubation.Estado estado, string instruccion)
    {
        fsm.estadoActual = estado;
        UpdateTv(instruccion);
    }

    //---TV---
    void UpdateTv(string instructions)
    {
        Text texto = Tv.GetComponentInChildren<Text>();
        texto.text = instructions;
        //correctAudio.Play();
    }

    public void SendHaptics(float defaultAmplitude, float defaultDuration)
    {
        leftController.SendHapticImpulse(defaultAmplitude, defaultDuration);
        rightController.SendHapticImpulse(defaultAmplitude, defaultDuration);
    }
}
