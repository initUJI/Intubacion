using Subtegral.DialogueSystem.DataContainers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TareasNotebook : MonoBehaviour
{
    public GameManager_Diagnostico gameManager;
    public GameObject containerTareas;
    public DialogueContainer dialogoFinal;

    private int numTareas;
    private int tareasCompletadas;

    void Start()
    {
        gameManager.onTareaFinalizadaEvent += TareaFinalizada;

        numTareas = containerTareas.transform.childCount;
        tareasCompletadas = 0;
        
    }

    void Update()
    {
        if(tareasCompletadas == numTareas)
        {
            Debug.Log("SE ACABAAA");
            EscribirDialogoDiagnostico();
            //Se ha acabado
            if (gameManager.dialogue != dialogoFinal)
            {
                gameManager.CambiarDialogo(dialogoFinal);
                StartCoroutine(MostrarDialogoFinal());
            }
        }
    }

    void EscribirDialogoDiagnostico()
    {
        //Leer el dialogo
        var narrativeData = dialogoFinal.NodeLinks.First().TargetNodeGUID; //Entrypoint node

        //Leer las posibles respuestas (choices)
        var choices = dialogoFinal.NodeLinks.Where(x => x.BaseNodeGUID == narrativeData);

        foreach (var choice in choices)
        {
            //Si no es el daignostico correcto, escribir "incorrecto" en el siguiente nodo
            if (choice.PortName != Enum.GetName(typeof(Diagnostico), gameManager.diagnostico))
            {
                dialogoFinal.DialogueNodeData.Find(x => x.NodeGUID == choice.TargetNodeGUID).DialogueText = "Incorrecto... Piensa mejor";

                //Reiniciar el dialogo. Avanzar al siguiente nodo.
                var finalChoice = dialogoFinal.NodeLinks.Where(x => x.BaseNodeGUID == choice.TargetNodeGUID);

                foreach (var _choice in finalChoice)
                {
                    dialogoFinal.DialogueNodeData.Find(x => x.NodeGUID == _choice.TargetNodeGUID).DialogueText = "ReiniciarDialogo";
                }
            }

            else
            {
                //Si es el diagnostico, escribir correcto en el siguiente nodo y cerrar el dialogo
                dialogoFinal.DialogueNodeData.Find(x => x.NodeGUID == choice.TargetNodeGUID).DialogueText = "¡Correcto! Has diagnosticado correctamente al paciente. ¡Enhorabuena!";

                var finalChoice = dialogoFinal.NodeLinks.Where(x => x.BaseNodeGUID == choice.TargetNodeGUID);
                foreach (var _choice in finalChoice)
                {
                    dialogoFinal.DialogueNodeData.Find(x => x.NodeGUID == _choice.TargetNodeGUID).DialogueText = "CerrarDialogo";
                }
            }

        }
    }


    IEnumerator MostrarDialogoFinal()
    {
        yield return new WaitForSeconds(2);
        gameManager.AbrirDialogo();
    }

    void TareaFinalizada()
    {
        Debug.Log("TareaFinalizada");
        switch (gameManager.tareaPrevia)
        {
            case Tarea.DialogoInicial:
                containerTareas.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
                tareasCompletadas++;
                break;
            case Tarea.Otoscopio:
                containerTareas.transform.GetChild(1).GetComponent<Toggle>().isOn = true;
                tareasCompletadas++;
                break;
            case Tarea.Estetoscopio:
                containerTareas.transform.GetChild(2).GetComponent<Toggle>().isOn = true;
                tareasCompletadas++;
                break;
            case Tarea.Ninguna:
                break;
            default:
                break;
        }
        
    }
}
