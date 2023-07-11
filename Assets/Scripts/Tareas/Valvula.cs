using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valvula : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TuboPresion tuboPresion;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Dedo" && tuboPresion.tuboColocado && !tuboPresion.valvulaCerrada && gameManager.fsm.estadoActual == MaquinaEstados.Estado.TuboPresion)
        {
            tuboPresion.AbrirValvula();
        }    
    }
}
