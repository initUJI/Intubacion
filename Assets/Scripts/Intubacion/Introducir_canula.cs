using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introducir_canula : MonoBehaviour
{

    [SerializeField] GameManager_Intubation gameManager;

    public GameObject explicacion_canula;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.IntroducirCanulaGuedel)
        {
            if (Canula_trigger.no_puesta)
                explicacion_canula.SetActive(true);


        }
        if (!Canula_trigger.no_puesta)
        {

            gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.PrepararLaringoscopio, "Una el cabezal del laringoscopio con el mango del mismo.");


        }
            
        


    }
}
