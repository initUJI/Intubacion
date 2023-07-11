using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EscribirNotas : MonoBehaviour
{
    [SerializeField] GameManager_Diagnostico gameManager;

    [SerializeField] TMP_Text objetoTexto;
    private string notas;

    private void Start()
    {
        notas = gameManager.notas;
    }

    private void Update()
    {
        notas = gameManager.notas;

        //Si se interactua (se coge el objeto), se escribe
        if (gameManager.EstaCogiendoElObjeto(gameObject))
        {
            gameManager.BorrarMensajes();
            Escribir(notas);
        }
    }
    public void Escribir(string texto)
    {
        objetoTexto.text = texto;
    }

}
