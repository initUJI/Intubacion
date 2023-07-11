using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarEstilete : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    Outline outlineEstilete;
    private void Start()
    {
        outlineEstilete = GetComponent<Outline>();
    }
    private void Update()
    {
        if(gameManager.fsm.estadoActual == MaquinaEstados.Estado.ColocarEstilete)
        {
            outlineEstilete.enabled = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Si el estilete colisiona con la aguja, se hace hijo de ésta y se reinicia su transform para copiar la posicion exacta.
        if(collision.gameObject.tag == "Aguja" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.ColocarEstilete)
        {
            gameObject.transform.parent = collision.transform;
            transform.localEulerAngles = Vector3.zero;
            transform.localPosition = Vector3.zero;

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.detectCollisions = false;

            outlineEstilete.enabled = false;
            gameManager.ActivarEstado(MaquinaEstados.Estado.InyectarAgujaPuncion, "Inyecta la aguja de la punción en el espacio interespinal. Si tocas una vértebra, vibrará el mando. Ayúdate de la pantalla lateral");
        }
    }
}
