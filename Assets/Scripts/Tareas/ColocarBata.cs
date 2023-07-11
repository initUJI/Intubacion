using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarBata : MonoBehaviour
{
    private Outline outline;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject bataAbierta;
    [SerializeField] GameObject ropaInterior;
    void Start()
    {
        outline = gameObject.GetComponent<Outline>();
    }

    void Update()
    {
        if(gameManager.fsm.estadoActual == MaquinaEstados.Estado.ColocarBata && outline.enabled == false)
        {
            outline.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dedo" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.ColocarBata)
        {
            bataAbierta.SetActive(true);
            ropaInterior.SetActive(false);
            gameManager.ActivarEstado(MaquinaEstados.Estado.InyectarAnestesia, "Crea un habón de anestésico en el sitio de entrada de la aguja de calibre 25 y luego" +
                    " anestesiar más profundamente en los tejidos blandos a lo largo de la trayectoria prevista para la inserción de la aguja.");
            Destroy(gameObject);
        }
    }
}
