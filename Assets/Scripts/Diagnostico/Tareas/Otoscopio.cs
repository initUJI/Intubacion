using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otoscopio : MonoBehaviour
{
    [SerializeField] GameManager_Diagnostico gameManager;
    [SerializeField] GameObject PanelVisionOtoscopio;
    [SerializeField] GameObject paciente;
    [SerializeField] GameObject body;
    /*private Mesh mesh;
    private MeshCollider meshCollider;*/
    [SerializeField] Transform posicionPaciente;
    private Animator animator;
    private Light luz;
    private bool terminado = false;

    void Start()
    {
        luz = GetComponentInChildren<Light>();
        animator = paciente.GetComponent<Animator>();
    }

    void Update()
    {

        if (gameManager.EstaCogiendoElObjeto(gameObject))
        {
            gameManager.CambiarTarea(Tarea.Otoscopio);
            animator.SetBool("SeLevanta", true);          
            PausarAnimacionCuandoEsteCerca();

            if(gameManager.LeerMensaje() == "") gameManager.EscribirMensajeAJugador("Enciende la luz presionando A");

            PanelVisionOtoscopio.SetActive(true);

            //Encender la luz con A
            if (gameManager.GetComponent<DetectButtons>().pressedA)
            {
                gameManager.BorrarMensajes();
                if (!luz.enabled) luz.enabled = true;
                else luz.enabled = false;
            }
        }

        else
        {
            PanelVisionOtoscopio.SetActive(false);
            animator.speed = 1;
        }

    }

    private void PausarAnimacionCuandoEsteCerca()
    {
        //Que el paciente se quede quieto cuando esté cerca para que sea más facil
        if (Vector3.Distance(transform.position, posicionPaciente.position) <= 1.1)
        {
            animator.speed = 0;
            Debug.Log("CERCA DE PACIENTE");
        } 
        else
        { 
            animator.speed = 1;
            /*if (body.TryGetComponent<MeshCollider>(out MeshCollider collider))
            {
                Destroy(collider);
            }*/
        }


        //Si no tiene mesh collider, añadir
        /*if(!body.TryGetComponent<MeshCollider>(out MeshCollider collder))
        {
            meshCollider = body.AddComponent<MeshCollider>();
            mesh = body.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            meshCollider.sharedMesh = mesh;
        }*/


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Paciente")
        {
            if (!terminado)
            {
                gameManager.EscribirMensajeAJugador("Nueva nota desbloqueada!");
                gameManager.notas += "\nOido normal";
                gameManager.CambiarTarea(Tarea.Ninguna);
            }


            terminado = true;
        }
    }
}
