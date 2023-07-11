using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuboPresion : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Hand rightHand;
    [SerializeField] Hand leftHand;
    [SerializeField] GameObject visionPuncionLumbar;
    [SerializeField] Outline outlineTuboMuestra;

    Rigidbody rb;
    GameObject punto;
    ParticleSystem liquido;
    Outline tuboOutline;
    public bool tuboColocado;
    public bool valvulaCerrada;

    //Animacion liquido
    [SerializeField] Animator LCRAnimator;
    //Animacion valvula
    [SerializeField] Animator valvulaAnimator;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        liquido = GetComponentInChildren<ParticleSystem>();
        tuboColocado = false;
        valvulaCerrada = false;
        tuboOutline = GetComponent<Outline>();
    }

    private void Update()
    {
        if(gameManager.fsm.estadoActual == MaquinaEstados.Estado.TuboPresion)
        {
            if(!tuboColocado) tuboOutline.enabled = true;

            //Si ya se ha recogido el líquido y cerrado la válvula, pero el tubo sigue colocado, hay que quitar el tubo
            if (valvulaCerrada && tuboColocado)
            {
                QuitarTubo();
            }        
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Cuando el tubo de presion toque la aguja en la espalda se colocará para sacar el liquido
        if(collision.gameObject.tag == "Aguja" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.TuboPresion && !tuboColocado & !valvulaCerrada)
        {
            collision.transform.GetChild(1).gameObject.SetActive(false); //Quitar el punto del líquido saliendo por la aguja

            punto = collision.transform.GetChild(0).gameObject; //El punto donde se coloca el tubo
            ColocarTuboEnAguja(punto);
            tuboOutline.enabled = false;
            outlineTuboMuestra.enabled = true;
            tuboColocado = true;
        }

        //Si el tubo está colocado en la aguja y el tubo lo toca, se abrirá la válvula

        /*else if (collision.gameObject.tag == "TuboMuestra" && tuboColocado && !valvulaCerrada && gameManager.fsm.estadoActual == MaquinaEstados.Estado.TuboPresion)
        {
            outlineTuboMuestra.enabled = false;
            AbrirValvula();
        }*/

    }

    void ColocarTuboEnAguja(GameObject punto)
    {      
        transform.position = punto.transform.position;
        transform.up = punto.transform.forward;
        transform.forward = punto.transform.up;
        transform.localEulerAngles = new Vector3(-90f, -180f, 0);
        rb.isKinematic = true;
        rb.useGravity = false;
        LCRAnimator.SetBool("MedirPresion", true); //El liquido sube por el tubo de presion

    }

    public void AbrirValvula()
    {
        outlineTuboMuestra.enabled = false;
        valvulaAnimator.SetBool("AbrirValvula", true);
        liquido.Play();
    }

    //Cuando el líquido llega a cierto nivel en el tubo de muestra, se cierra la válvula
    public void CerrarValvula()
    {
        valvulaAnimator.SetBool("AbrirValvula", false);
        liquido.Stop();
        valvulaCerrada = true;
    }


    void QuitarTubo()
    {
        visionPuncionLumbar.SetActive(false);
        //AQUI, hay que cambiar el if para detectar si coges con el nuevo input
        //if (gameManager.fsm.estadoActual == MaquinaEstados.Estado.TuboPresion &&
          //  ((rightHand._isGrabbing && rightHand._heldObject == gameObject) || (leftHand._isGrabbing && leftHand._heldObject == gameObject))) //Si está cogiendo el tubo
        if (gameManager.fsm.estadoActual == MaquinaEstados.Estado.TuboPresion) // && "Agarrando con el nuevo input"
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            gameManager.ActivarEstado(MaquinaEstados.Estado.SacarAguja, "Coloca el estilete de nuevo y saca la aguja");
        }
    }

}
