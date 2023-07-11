using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetirarJeringa : MonoBehaviour
{

    [SerializeField] GameManager_Intubation gameManager;

    public static bool jeringa_mesa = false;

    public GameObject ambu_explicacion;

    public GameObject pantalla;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }



    void OnCollisionEnter(Collision collision)
    {
        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.InflarGloboTubo)
        {
            if (collision.gameObject.tag == "mesa" && Globo.globo_hincahdo)
            {

                jeringa_mesa = true;

                pantalla.SetActive(false);
                ambu_explicacion.SetActive(true);

            }


        }

    }
}
