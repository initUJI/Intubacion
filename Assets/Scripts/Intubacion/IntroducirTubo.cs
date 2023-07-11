using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroducirTubo : MonoBehaviour
{


    [SerializeField] GameManager_Intubation gameManager;


    public GameObject explicacion_tubo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.IntroducirTubo)
        {
            if (tubo_trigger.tubo_no_puesto)
            {
                explicacion_tubo.SetActive(true);
                
            }
                


        }
        if (!tubo_trigger.tubo_no_puesto)
        {

            gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.QuitarLaringoscopio, "Retire el laringoscopio de la boca del paciente.");


        }


    }
}