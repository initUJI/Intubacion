using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarVertebra : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Dedo" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.DetectarVertebra)
        {
            //Ricardo: He comentado esto porque daba error con el nuevo sistema, habra que adaptarlo.
            //gameObject.GetComponent<HapticController>().SendHaptics();
            print("has tocado la vertebra crack");
            gameManager.ActivarEstado(MaquinaEstados.Estado.MarcarPunto, "Has detectado las vertebras bien imagino. Cambiamos a lo de marcarlas con el rotu por que es lo siguiente que sale en la maquina de estados :)");
        }
    }
}
