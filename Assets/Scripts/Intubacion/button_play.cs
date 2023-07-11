using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button_play : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;

    public Button boton;

    public GameObject Hands;

    // Start is called before the first frame update
    void Start()
    {
        boton.onClick.AddListener(OnClick);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        Hands.SetActive(true);

        boton.gameObject.SetActive(false);

        gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.AbrirBoca, "Ábrale la boca al paciente.");

    }
}
