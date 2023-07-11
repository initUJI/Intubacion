using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitarCanula : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;

    public GameObject base_paciente;

    private float aux = 0f;
    private bool wait_t = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (laringoscopio.larin_unido)
        { 

            base_paciente.SetActive(true);

            Wait_canula();

            if(wait_t ==false)
                gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.QuitarCanulaGuedel, "Ahora retire la cánula de Guedel de la boca del paciente.");
        }

        if (RetirarCanula.ponerLaringoscopio)
        {
            gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.IntroducirLaringoscopio, "Pase a introducir el laringoscopio en la boca del paciente.");
        }

    }

    void Wait_canula()
    {

        aux += Time.deltaTime;
        if (aux >= 1f) wait_t = false;
    }
}
