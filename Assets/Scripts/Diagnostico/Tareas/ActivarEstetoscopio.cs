using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarEstetoscopio : MonoBehaviour
{
    [SerializeField] GameManager_Diagnostico gameManager;
    [SerializeField] GameObject estetoscopioReal;

    // Update is called once per frame
    void Update()
    {
        //Al coger el objeto, desaparece y se activa el real
        if (gameManager.EstaCogiendoElObjeto(gameObject))
        {          
            estetoscopioReal.SetActive(true);
            gameObject.SetActive(false);
            gameManager.CambiarTarea(Tarea.Estetoscopio);
        }
    }
}
