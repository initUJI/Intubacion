using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introducir_laring : MonoBehaviour
{

    [SerializeField] GameManager_Intubation gameManager;

    public GameObject explicacion_laringo;

    private float aux_tubo = 0;
    private bool wait_tubo = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.IntroducirLaringoscopio)
        {
            if (laringoscopio_poner.no_puesto)
                explicacion_laringo.SetActive(true);


        }
        if (!laringoscopio_poner.no_puesto)
        {
            Wait_tubo();

            if(wait_tubo == false)
                gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.IntroducirTubo, "Ahora, sin quitar el laringoscopio, introduzca el tubo endotraqueal.");
           

        }

    }

    void Wait_tubo()
    {

        aux_tubo += Time.deltaTime;
        if (aux_tubo >= 4f) wait_tubo = false;
    }

}
