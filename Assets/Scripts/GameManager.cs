using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public MaquinaEstados fsm;
    [SerializeField] Canvas tablet;
    AudioSource correctAudio;

    void Start()
    {
        fsm = GetComponent<MaquinaEstados>();
        correctAudio = tablet.GetComponent<AudioSource>();
    }

    void Update() {}

    //Activa estado
    public void ActivarEstado(MaquinaEstados.Estado estado, string instruccion)
    {
        fsm.estadoActual = estado;
        UpdateTablet(instruccion);
    }

    //---TABLET--- 
    void UpdateTablet(string instructions)
    {
        Text texto = tablet.GetComponentInChildren<Text>();
        texto.text = instructions;
        correctAudio.Play();
    }

}
