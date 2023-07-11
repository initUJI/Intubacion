using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerGloboInflarse : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;

    public GameObject camara;
    public GameObject jeringa_explicacion;

    private bool jeringa_puesta = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.InflarGloboTubo)
        {
            if (!jeringa_puesta)
            {
                camara.SetActive(true);
                jeringa_explicacion.SetActive(true);

                jeringa_puesta = true;

            }
            

        }
        
    }
}
