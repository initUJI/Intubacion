using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;
using Subtegral.DialogueSystem.Runtime;

public enum Tarea
{
    DialogoInicial,
    Otoscopio,
    Estetoscopio,
    Ninguna
}

public enum Respiraciones
{
    Normal,
    Estridor,
    Sibilancias,
    CrepitantesGruesos,
    CrepitantesFinos
}

public enum Diagnostico
{
    Obstruccion,
    Asma,
    Neumonía,
    Fibrosis
}

//DIAGNOSTICO:
//Estridor inspiratorio -> Obstrucción de vía aérea superior
//Sibilancias -> Asma y epoc
//Crepitantes gruesos -> Neumonía y edema pulmonar
//Crepitantes finos -> Fibrosis pulmonar

public class GameManager_Diagnostico : MonoBehaviour
{
    

    [Header("Player (XR Origin)")]
    [SerializeField] GameObject player;

    [Header("Controllers")]
    [Header("CAMBIAR INTERACTOR")]
    
    [SerializeField] GameObject RightDirect_Controller;
    [SerializeField] GameObject LeftDirect_Controller;
    [SerializeField] GameObject RightRay_Controller;
    [SerializeField] GameObject LeftRay_Controller;

    [Header("Hands")]
    [SerializeField] GameObject RightDirect_Hand;
    [SerializeField] GameObject LeftDirect_Hand;
    [SerializeField] GameObject RightRay_Hand;
    [SerializeField] GameObject LeftRay_Hand;

    private List<GameObject> DirectInteractors;
    private List<GameObject> RayInteractors;
    private bool rayInteractorActive;
    private bool directInteractorActive;
    private Hand leftHand;
    private Hand rightHand;

    [Header("Tareas y diagnostico")]
    public Tarea tareaActual;
    public Tarea tareaPrevia;

    public Diagnostico diagnostico;
    public Respiraciones tipoRespiracion;
    public Dictionary<Respiraciones, Diagnostico> diccionarioDiagnostico;


    [Header("DIÁLOGOS")]
    [SerializeField] public Canvas canvasDialogo;
    [SerializeField] TMP_Text textoDialogo;
    public DialogueContainer dialogue;
    private string dialogueName;

    [Header("Canvas principal")]
    [SerializeField] Canvas canvasPrincipal;
    private TMP_Text texto;

    [Header("Notas")]
    public string notas;

    [Header("Debug")]
    [SerializeField] GameObject debugContainer;
    [SerializeField] GameObject debugText;
    [SerializeField] ScrollRect scroll;

    //Evento cambio de dialogo
    public delegate void OnDialogueEvent();
    public event OnDialogueEvent onDialogueEvent;

    //Evento tarea finalizada
    public delegate void OnTareaFinalizadaEvent();
    public event OnTareaFinalizadaEvent onTareaFinalizadaEvent;


    private void Start()
    {
        //------CAMBIAR INTERACTOR--------------
        DirectInteractors = new List<GameObject>();
        RayInteractors = new List<GameObject>();

        DirectInteractors.Add(RightDirect_Controller);
        DirectInteractors.Add(LeftDirect_Controller);
        DirectInteractors.Add(RightDirect_Hand);
        DirectInteractors.Add(LeftDirect_Hand);

        RayInteractors.Add(RightRay_Controller);
        RayInteractors.Add(LeftRay_Controller);
        RayInteractors.Add(RightRay_Hand);
        RayInteractors.Add(LeftRay_Hand);

        leftHand = LeftDirect_Hand.GetComponent<Hand>();
        rightHand = RightDirect_Hand.GetComponent<Hand>();

        //Al comenzar, desactivar el direct despues de haber asignado las variables. Esto es así porque se empieza la escena con un diálogo.
        ActivateRayInteraction();

        texto = canvasPrincipal.GetComponentInChildren<TMP_Text>();

        foreach (var exposedProperty in dialogue.ExposedProperties)
        {
            if (exposedProperty.PropertyName == "Name") dialogueName = exposedProperty.PropertyValue;
        }

        tareaActual = Tarea.DialogoInicial;
        ElegirTipoRespiracionRandom();

        //Diccionario diagnostico
        diccionarioDiagnostico = new Dictionary<Respiraciones, Diagnostico>();
        diccionarioDiagnostico.Add(Respiraciones.Estridor, Diagnostico.Obstruccion);
        diccionarioDiagnostico.Add(Respiraciones.Sibilancias, Diagnostico.Asma);
        diccionarioDiagnostico.Add(Respiraciones.CrepitantesGruesos, Diagnostico.Neumonía);
        diccionarioDiagnostico.Add(Respiraciones.CrepitantesFinos, Diagnostico.Fibrosis);

    }

    private void Update()
    {

        //Detectar errores para debug
        Application.logMessageReceived += LogCallback;


    }

    public void CambiarTarea(Tarea tarea)
    {
        tareaPrevia = tareaActual;
        tareaActual = tarea;
        Debug.Log("Cambiar tarea a " + tareaActual);

        //Si se cambia a ninguna es que se acaba de terminar una tarea
        if(tarea == Tarea.Ninguna)
        {
            if (onTareaFinalizadaEvent != null) onTareaFinalizadaEvent();
        }
    }

    private void ElegirTipoRespiracionRandom()
    {
        int rand = UnityEngine.Random.Range(2, 6);

        switch (rand)
        {
            case 1:
                tipoRespiracion = Respiraciones.Normal;
                break;
            case 2:
                tipoRespiracion = Respiraciones.Estridor;
                break;
            case 3:
                tipoRespiracion = Respiraciones.Sibilancias;
                break;
            case 4:
                tipoRespiracion = Respiraciones.CrepitantesFinos;
                break;
            case 5:
                tipoRespiracion = Respiraciones.CrepitantesGruesos;
                break;
            default:
                break;
        }

        diagnostico = diccionarioDiagnostico[tipoRespiracion];

    }


    //----------------FUNCIONES---------------------

    //Cambiar interactor: Para cambiar entre Ray y Direct interactor según se tenga que interactuar con el diálogo/UI o con objetos del entorno.
    public void ActivateDirectInteraction()
    {
        //Si es la primera vez que se le llama es porque se ha terminado el dialogo inicial
        if(tareaActual == Tarea.DialogoInicial)
        {
            CambiarTarea(Tarea.Ninguna);
        }

        //Desactivar ray
        foreach (GameObject interactor in RayInteractors)
        {
            interactor.SetActive(false);
        }
        //Activar direct
        foreach (GameObject interactor in DirectInteractors)
        {
            interactor.SetActive(true);
        }
        rayInteractorActive = false;
        directInteractorActive = true;
    }
    public void ActivateRayInteraction()
    {
        //Desactivar direct
        foreach (GameObject interactor in DirectInteractors)
        {
            interactor.SetActive(false);
        }
        //Activar ray
        foreach (GameObject interactor in RayInteractors)
        {
            interactor.SetActive(true);
        }
        rayInteractorActive = true;
        directInteractorActive = false;
    }

    //Dialogo controller: Para abrir o cerrar la ventana de diálogo según sea necesario.

    public void AbrirDialogo()
    {
        canvasDialogo.gameObject.SetActive(true);
        ActivateRayInteraction();
    }

    public void CerrarDialogo()
    {
        canvasDialogo.gameObject.SetActive(false);
        ActivateDirectInteraction();
    }

    public void CambiarDialogo(DialogueContainer dialogoNuevo)
    {
        
        //Sustituir el dialogo nuevo
        dialogue = dialogoNuevo;

        //Actualizar el nombre del dialogo actual
        foreach (var exposedProperty in dialogue.ExposedProperties)
        {
            if (exposedProperty.PropertyName == "Name") dialogueName = exposedProperty.PropertyValue;
        }

        //Cambiarlo en el dialogue parser
        if (onDialogueEvent != null) onDialogueEvent();

    }

    //EstaCogiendoObjeto: Devuelve true siempre y cuando o la mano izquierda o la mano derecha esté agarrando el objeto que se le pasa.
    public bool EstaCogiendoElObjeto(GameObject objeto)
    {
        if (rayInteractorActive) return false;

        else
        {
            if (leftHand._isGrabbing && leftHand._heldObject == objeto)
            {
                return true;
            }

            else if (rightHand._isGrabbing && rightHand._heldObject == objeto)
            {
                return true;
            }

            else return false;
        }
    }

    //EscribirMensajeAJugador: Escribe el mensaje indicado en el canvas principal, en la parte superior derecha.

    public void EscribirMensajeAJugador(string mensaje)
    {
        texto.text = mensaje;
    }

    public string LeerMensaje()
    {
        return texto.text;
    }
    public void BorrarMensajes()
    {
        texto.text = "";
    }

    //Debug: Funciones para debuggear en una pantalla dentro del juego para facilitar el desarrollo

    public void LogCallback(string condition, string stackTrace, LogType type)
    {
        string textoAEscribir = type + ": " + condition;
        string ultimoTexto = "";

        if(debugContainer.transform.childCount != 0)
        {
            ultimoTexto = debugContainer.transform.GetChild(debugContainer.transform.childCount - 1).GetComponent<TMP_Text>().text;
        }
        
        if (ultimoTexto == textoAEscribir) return;

        //Destruir más antiguos para que no pete
        if (debugContainer.transform.childCount >= 10)
        {
            DestroyImmediate(debugContainer.transform.GetChild(0).gameObject);
            return;
        }
        var t = Instantiate(debugText, debugContainer.transform);
        TMP_Text texto = t.GetComponent<TMP_Text>();

        texto.text = textoAEscribir;
        scroll.normalizedPosition = new Vector2(0, 0);

        switch (type)
        {
            case LogType.Error:
                texto.color = new Color32(254, 9, 0, 255); //red
                break;
            case LogType.Assert:
                texto.color = new Color32(254, 9, 0, 255); //red
                break;
            case LogType.Warning:
                texto.color = new Color32(255, 255, 0, 255); //yellow
                break;
            case LogType.Log:
                texto.color = new Color32(0, 102, 204, 255); //blue
                break;
            case LogType.Exception:
                texto.color = new Color32(254, 9, 0, 255); //red
                break;
            default:
                break;
        }
    }

}
