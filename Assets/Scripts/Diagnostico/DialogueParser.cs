using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;

namespace Subtegral.DialogueSystem.Runtime
{
    public class DialogueParser : MonoBehaviour
    {
        public GameManager_Diagnostico gameManager;

        public DialogueContainer dialogue;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Button choicePrefab;
        [SerializeField] private Transform buttonContainer;

        //Evento dialogo cerrado
        public delegate void OnDialogueClosedEvent();
        public event OnDialogueClosedEvent onDialogueClosedEvent;

        //Evento dialogo reiniciado
        public delegate void OnDialogueRebootEvent();
        public event OnDialogueRebootEvent onDialogueRebootEvent;

        private void Start()
        {
            gameManager.onDialogueEvent += EventoDialogo;

            var narrativeData = dialogue.NodeLinks.First(); //Entrypoint node
            ProceedToNarrative(narrativeData.TargetNodeGUID);
        }

        private void ProceedToNarrative(string narrativeDataGUID)
        {
            var text = dialogue.DialogueNodeData.Find(x => x.NodeGUID == narrativeDataGUID).DialogueText;

            if(text == "CerrarDialogo")
            {
                gameManager.CerrarDialogo();
                Debug.Log("DP: Cerrar dialogo");

                if (onDialogueClosedEvent != null) onDialogueClosedEvent();
                return;
            }

            else if(text == "ReiniciarDialogo")
            {
                gameManager.CerrarDialogo();
                Debug.Log("DP: Reiniciar dialogo");
                ProceedToNarrative(dialogue.NodeLinks.First().TargetNodeGUID);

                if (onDialogueRebootEvent != null) onDialogueRebootEvent();
                return;
            }

            var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGUID == narrativeDataGUID);
            dialogueText.text = ProcessProperties(text);
            var buttons = buttonContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
            }

            foreach (var choice in choices)
            {
                var button = Instantiate(choicePrefab, buttonContainer);
                button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
                button.onClick.AddListener(() => ProceedToNarrative(choice.TargetNodeGUID));
            }
        }

        private string ProcessProperties(string text)
        {
            foreach (var exposedProperty in dialogue.ExposedProperties)
            {
                text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
            }
            return text;
        }

        void EventoDialogo()
        {
            Debug.Log("dialogoNuevo");

            dialogue = gameManager.dialogue;
            ProceedToNarrative(dialogue.NodeLinks.First().TargetNodeGUID);

        } 
    }
}