using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogoController : MonoBehaviour
{
    [SerializeField] Canvas canvasDialogo;
    [SerializeField] TMP_Text textoDialogo;

    private CambiarInteractor cambiarInteractor;
    void Start()
    {
        cambiarInteractor = GetComponent<CambiarInteractor>();
    }

    void Update()
    {
        //Cerrar diálogo
        if(textoDialogo.text == "CerrarDialogo")
        {
            canvasDialogo.gameObject.SetActive(false);
            cambiarInteractor.ActivateDirectInteraction();
        }

        //Abrir diálogo
    }
}
