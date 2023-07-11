using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class EmpujarHastaObtenerLCR : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    Rigidbody rb;
    private bool liquidoEncontrado = false;
    bool agujaYEstileteJuntos = false;

    GameObject LCR, estilete;
    Rigidbody estileteRB;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (gameManager.fsm.estadoActual == MaquinaEstados.Estado.EmpujarHastaObtenerLCR)
        {
            if (!liquidoEncontrado)
            {
                SacarEstileteParaObtenerLCR();
                EmpujarAguja();
                
            }
        }
    }

    //Solo se puede empujar si aguja y estilete estan juntos
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Estilete" && !liquidoEncontrado && gameManager.fsm.estadoActual == MaquinaEstados.Estado.EmpujarHastaObtenerLCR)
        {
            agujaYEstileteJuntos = true;
        }
    }

    //Empujar la aguja hacia dentro con la A, o empujando con la mano
    void EmpujarAguja()
    {
        if (agujaYEstileteJuntos)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;

            if (gameManager.GetComponent<DetectButtons>().pressedA)
            {          
                rb.AddForce(-transform.forward * 0.001f, ForceMode.Impulse);
                if (estileteRB != null) estileteRB.AddForce(-transform.forward * 0.001f, ForceMode.Impulse);
            }
        }

    }

    //Desbloquear el estilete y sacar con físicas
    void SacarEstileteParaObtenerLCR()
    {
        estilete = transform.GetChild(2).gameObject;
        estileteRB = estilete.GetComponent<Rigidbody>();
        if(estileteRB != null)
        {
            estileteRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            estileteRB.isKinematic = false;
            estileteRB.detectCollisions = true;
        }

    }

    //Si la aguja colisiona con el espacio aracnoideo, se llama a EncontrarLiquido y eso bloquea la aguja
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EspacioAracnoideo" && !liquidoEncontrado && gameManager.fsm.estadoActual == MaquinaEstados.Estado.EmpujarHastaObtenerLCR)
        {
            //Si colisiona con el espacio aracnoideo se encuentra el líquido y se bloquea la aguja
            EncontrarLiquido();
        }
    }
    void EncontrarLiquido()
    {
        //Ricardo: He comentado esto porque daba error con el nuevo sistema, habra que adaptarlo.
        //gameObject.GetComponent<HapticController>().SendHaptics();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.isKinematic = true;

        liquidoEncontrado = true;
    }

    //Cuando se saca el estilete (deja de colisionar el estilete con la aguja) sale el líquido
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Estilete" && liquidoEncontrado && gameManager.fsm.estadoActual == MaquinaEstados.Estado.EmpujarHastaObtenerLCR)
        {
            LCR = transform.GetChild(1).gameObject;
            Animator animator = LCR.GetComponent<Animator>();
            animator.SetBool("LCRencontrado", true);
            gameManager.ActivarEstado(MaquinaEstados.Estado.TuboPresion, "Retira el estilete y pon el tubo para medir la presión. Si tocas la válvula se abrirá y comenzará a caer LCR. Con el tubo " +
                "de muestra, ponlo debajo y recógelo. Cuando la cantidad sea suficiente, la válvula se cerrará automáticamente y tendrás que retirar el tubo. Guarda la muestra en la mesa.");
        }

        else if (collision.gameObject.tag == "Estilete" && !liquidoEncontrado && gameManager.fsm.estadoActual == MaquinaEstados.Estado.EmpujarHastaObtenerLCR)
        {
            agujaYEstileteJuntos = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }





}