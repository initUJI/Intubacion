using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aposito : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    Rigidbody rb;
    Outline outline;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
    }

    void Update()
    {
        if(gameManager.fsm.estadoActual == MaquinaEstados.Estado.Aposito)
        {
            outline.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PuntoAposito" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.Aposito)
        {

            GameObject punto = other.gameObject;

            transform.position = punto.transform.position;
            transform.rotation = Quaternion.LookRotation(punto.transform.forward);

            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.isKinematic = true;
            rb.useGravity = false;

            outline.enabled = false;

            gameManager.ActivarEstado(MaquinaEstados.Estado.DetectarVertebra, "¡Enhorabuena! Ha finalizado su tarea correctamente");
        }
    }

}
