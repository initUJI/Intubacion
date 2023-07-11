using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InyectarAnestesia : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameManager gameManager;
    private Outline outline;
    private bool agujaLlena = false;
    void Start()
    {
        outline = gameObject.GetComponentInParent<Outline>();
    }

    void Update()
    {
        if (gameManager.fsm.estadoActual == MaquinaEstados.Estado.InyectarAnestesia && outline.enabled == false)
        {
            outline.enabled = true;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si la jeringa colisiona con la anestesia, se activa la animación de sacar líquido
        if(gameManager.fsm.estadoActual == MaquinaEstados.Estado.InyectarAnestesia && collision.gameObject.tag == "Anestesia")
        {
            Debug.Log("colision");
            SacarLiquido();
            //Ricardo: He comentado esto porque daba error con el nuevo sistema, habra que adaptarlo.
            //gameObject.GetComponent<HapticController>().SendHaptics();
            agujaLlena = true;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Si la jeringa colisiona con la zona de punción, se activa la animación de vaciar la aguja
        if(other.gameObject.name == "ZonaPuncion" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.InyectarAnestesia && agujaLlena)
        {
            Inyectar();
            //Ricardo: He comentado esto porque daba error con el nuevo sistema, habra que adaptarlo.
            //gameObject.GetComponent<HapticController>().SendHaptics();
            outline.enabled = false;
            agujaLlena = false;
            gameManager.ActivarEstado(MaquinaEstados.Estado.ColocarEstilete, "Coloca el estilete en la aguja");
        }
    }

    private void Inyectar()
    {
        animator.SetBool("SacarLiquido", false);
        animator.SetBool("ExpulsarLiquido", true);
    }
    private void SacarLiquido()
    {
        animator.SetBool("SacarLiquido", true);
        animator.SetBool("ExpulsarLiquido", false);
    }
}
