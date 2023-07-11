using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesinfectarZona : MonoBehaviour
{
    Color32 firstColor;
    Color32 mediumColor;
    Color32 finalColor;
    Material material;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject yodo;
    private Outline outlineYodo, outlineGasa;
    void Start()
    {
        firstColor = new Color32(250, 196, 159, 1);
        mediumColor = new Color32(193, 121, 72, 1);
        finalColor = new Color32(132, 54, 0, 1);
        material = gameObject.GetComponent<MeshRenderer>().material;
        outlineYodo = yodo.GetComponent<Outline>();
        outlineGasa = gameObject.GetComponent<Outline>();
    }

    private void Update()
    {
        if (gameManager.fsm.estadoActual == MaquinaEstados.Estado.DesinfectarZona && outlineYodo.enabled == false && outlineGasa.enabled == false)
        {
            outlineYodo.enabled = true;
            outlineGasa.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(gameManager.fsm.estadoActual == MaquinaEstados.Estado.DesinfectarZona)
        {
            if (other.gameObject.tag == "Yodo")
            {
                if (material.color == Color.white)
                {
                    material.color = firstColor;
                    //Ricardo: He comentado esto porque daba error con el nuevo sistema, habra que adaptarlo.
                    //gameObject.GetComponent<HapticController>().SendHaptics();
                }
                else if (material.color == firstColor)
                {
                    material.color = mediumColor;
                    //Ricardo: He comentado esto porque daba error con el nuevo sistema, habra que adaptarlo.
                    //gameObject.GetComponent<HapticController>().SendHaptics();
                }
                else if (material.color == mediumColor)
                {
                    material.color = finalColor;
                    //Ricardo: He comentado esto porque daba error con el nuevo sistema, habra que adaptarlo.
                    //gameObject.GetComponent<HapticController>().SendHaptics();
                }
            }
            if (material.color == finalColor)
            {
                outlineYodo.enabled = false;
            } 
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Human" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.DesinfectarZona)
        {
            outlineGasa.enabled = false;
            gameManager.ActivarEstado(MaquinaEstados.Estado.ColocarBata, "Coloca una bata con una abertura detrás al paciente para crear una zona estéril");
        }
    }
}