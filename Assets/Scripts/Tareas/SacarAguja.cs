using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacarAguja : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Hand rightHand;
    [SerializeField] Hand leftHand;
    [SerializeField] Outline outlineEstilete;
    Rigidbody rb;
    Outline outlineAguja;

    private bool estileteBloqueado;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        estileteBloqueado = false;
        outlineAguja = GetComponent<Outline>();
    }

    void Update()
    {
        if (gameManager.fsm.estadoActual == MaquinaEstados.Estado.SacarAguja && !estileteBloqueado) outlineEstilete.enabled = true;

        else if (gameManager.fsm.estadoActual == MaquinaEstados.Estado.SacarAguja && estileteBloqueado) outlineAguja.enabled = true;
        // Cambiar este if al nuevo input------------------------------------------------------------------------------------
        // Cambiar este if al nuevo input------------------------------------------------------------------------------------
        //if (gameManager.fsm.estadoActual == MaquinaEstados.Estado.SacarAguja && estileteBloqueado &&
          //((rightHand._isGrabbing && rightHand._heldObject == gameObject) || (leftHand._isGrabbing && leftHand._heldObject == gameObject))) //Si está cogiendo la aguja
        if(gameManager.fsm.estadoActual == MaquinaEstados.Estado.SacarAguja && estileteBloqueado) // && cogiendome
        {
            Debug.Log("Quitando aguja");
            outlineAguja.enabled = false;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            gameManager.ActivarEstado(MaquinaEstados.Estado.Aposito, "Desinfecta la zona de nuevo y pon una gasa");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Estilete" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.SacarAguja)
        {
            if (!estileteBloqueado)
            {
                outlineEstilete.enabled = false;
                Rigidbody estileteRB = collision.gameObject.GetComponent<Rigidbody>();
                estileteRB.isKinematic = true;
                estileteRB.useGravity = false;
                estileteRB.constraints = RigidbodyConstraints.FreezeAll;
                estileteRB.detectCollisions = false;
                estileteBloqueado = true;
                Debug.Log("Estilete bloqueado");
            }
        }
    }

    /*private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Human")
        {
            collision.gameObject.GetComponent<MeshCollider>().enabled = true;
        }
    }*/
}
