using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcarPunto : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Outline outlineRotulador;

    bool _penPainted = false;

    private void Start()
    {
        outlineRotulador.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pen") // && gameManager.fsm.estadoActual == MaquinaEstados.Estado.MarcarPunto && !_penPainted
        {

            _penPainted = true;
            outlineRotulador.enabled = false;
            gameManager.ActivarEstado(MaquinaEstados.Estado.DesinfectarZona, "Limpiar el sitio de inserción con solución antiséptica mediante una serie de círculos concéntricos crecientes " +
                "que alcanzan unos 20 cm de diámetro. Usa la gasa con el yodo y desinfecta la zona.");
        }
    }
}
