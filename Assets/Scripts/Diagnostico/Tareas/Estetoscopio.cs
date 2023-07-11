using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Estetoscopio : MonoBehaviour
{
    [SerializeField] GameManager_Diagnostico gameManager;
    private DialogueParser dialogueParser;


    [Header("Tarea estetoscopio")]
    [SerializeField] GameObject paciente;
    [SerializeField] Transform posicionPaciente;
    [SerializeField] AudioSource audioRespiracion;
    public GameObject objetoEstetoscopio;
    
    private Animator animator;
    private bool haEscuchado;
    private bool fin;

    [Header("Respiraciones")]
    private Respiraciones tipoRespiracion;
    public List<AudioClip> listaSonidosRespiraciones;
    public DialogueContainer dialogoEstetoscopio;


    void Start()
    {
        animator = paciente.GetComponent<Animator>();
        haEscuchado = false;
        fin = false;

        tipoRespiracion = gameManager.tipoRespiracion;
        AsignarSonidosRespiracion();
        CrearRespuestasDialogo();

        Debug.Log("RESPIRACION: " + Enum.GetName(typeof(Respiraciones), tipoRespiracion));

        dialogueParser = gameManager.gameObject.GetComponent<DialogueParser>();
        dialogueParser.onDialogueClosedEvent += EventoDialogoCerrado;
        dialogueParser.onDialogueRebootEvent += EventoDialogoReiniciado;

    }

    public void Update()
    {
        if (!fin && gameManager.tareaActual == Tarea.Estetoscopio)
        {

            if (gameManager.LeerMensaje() == "") gameManager.EscribirMensajeAJugador("Acércate al paciente y coloca el estetoscopio en su pecho");



            if (CercaPaciente())
            {
                animator.SetBool("SeLevanta", true);
            }

            //Si suelta el estetoscopio que salgan las preguntas
            if (!gameManager.EstaCogiendoElObjeto(gameObject) && haEscuchado && !gameManager.canvasDialogo.gameObject.activeInHierarchy)
            {
                StartCoroutine(MostrarPregunta());
            }
        }
    }

    bool CercaPaciente()
    {
        if (Vector3.Distance(transform.position, posicionPaciente.position) <= 1.5) return true;
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Paciente" && gameManager.EstaCogiendoElObjeto(gameObject))
        {
            gameManager.EscribirMensajeAJugador("Cuando termines de escuchar, suelta el estetoscopio");
            if (!audioRespiracion.isPlaying) audioRespiracion.Play();

            //Nuevo dialogo

            if (gameManager.dialogue != dialogoEstetoscopio)
            {
                gameManager.CambiarDialogo(dialogoEstetoscopio);
            }

            haEscuchado = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Paciente")
        {
            if (audioRespiracion.isPlaying) audioRespiracion.Stop();
        }
    }

    private void AsignarSonidosRespiracion()
    {

        switch (tipoRespiracion)
        {
            case Respiraciones.Normal:
                audioRespiracion.clip = listaSonidosRespiraciones[0];
                break;
            case Respiraciones.Estridor:
                audioRespiracion.clip = listaSonidosRespiraciones[1];
                break;
            case Respiraciones.Sibilancias:
                audioRespiracion.clip = listaSonidosRespiraciones[2];
                break;
            case Respiraciones.CrepitantesFinos:
                audioRespiracion.clip = listaSonidosRespiraciones[3];
                break;
            case Respiraciones.CrepitantesGruesos:
                audioRespiracion.clip = listaSonidosRespiraciones[4];
                break;
            default:
                break;
        }

    }


    private void CrearRespuestasDialogo()
    {
        //Leer el dialogo
        var narrativeData = dialogoEstetoscopio.NodeLinks.First().TargetNodeGUID; //Entrypoint node

        //Leer las posibles respuestas (choices)
        var choices = dialogoEstetoscopio.NodeLinks.Where(x => x.BaseNodeGUID == narrativeData);

        foreach(var choice in choices)
        {
            //Si no es la respiracion actual, escribir "incorrecto" en el siguiente nodo
            if (choice.PortName != Enum.GetName(typeof(Respiraciones), tipoRespiracion))
            {               
                dialogoEstetoscopio.DialogueNodeData.Find(x => x.NodeGUID == choice.TargetNodeGUID).DialogueText = "¿Seguro? Escucha otra vez...";

                //Reiniciar el dialogo. Avanzar al siguiente nodo.
                var finalChoice = dialogoEstetoscopio.NodeLinks.Where(x => x.BaseNodeGUID == choice.TargetNodeGUID);

                foreach(var _choice in finalChoice)
                {
                    dialogoEstetoscopio.DialogueNodeData.Find(x => x.NodeGUID == _choice.TargetNodeGUID).DialogueText = "ReiniciarDialogo";
                }
            }

            else
            {
                //Si es la respiracion actual, escribir correcto en el siguiente nodo y cerrar el dialogo
                dialogoEstetoscopio.DialogueNodeData.Find(x => x.NodeGUID == choice.TargetNodeGUID).DialogueText = "¡Correcto! Escríbelo en tus notas";

                var finalChoice = dialogoEstetoscopio.NodeLinks.Where(x => x.BaseNodeGUID == choice.TargetNodeGUID);
                foreach (var _choice in finalChoice)
                {
                    dialogoEstetoscopio.DialogueNodeData.Find(x => x.NodeGUID == _choice.TargetNodeGUID).DialogueText = "CerrarDialogo";
                }
            }

        }
    }

    IEnumerator MostrarPregunta()
    {
        Debug.Log("Mostrar pregunta");
        yield return new WaitForSeconds(3);
        gameManager.AbrirDialogo();
    }

    //EVENTOS

    void EventoDialogoCerrado()
    {
        if (gameManager.tareaActual == Tarea.Estetoscopio)
        {
            Debug.Log("Estetoscopio: Cerrar dialogo");
            if (haEscuchado) fin = true;
            gameManager.CambiarTarea(Tarea.Ninguna);
            gameManager.EscribirMensajeAJugador("Bien! Escribe lo que has visto en tus notas");
            gameManager.notas += "\nSe detecta " + Enum.GetName(typeof(Respiraciones), tipoRespiracion) + " en respiraciones";
            Debug.Log("haEscuchado = " + haEscuchado + ", fin = " + fin);
            objetoEstetoscopio.SetActive(false);
        }

    }

    void EventoDialogoReiniciado()
    {
        if (gameManager.tareaActual == Tarea.Estetoscopio)
        {
            Debug.Log("Estetoscopio: Reiniciar dialogo");
            if(haEscuchado) haEscuchado = false;
            fin = false;

        }

    }
}