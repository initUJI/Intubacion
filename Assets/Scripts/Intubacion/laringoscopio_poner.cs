using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laringoscopio_poner : MonoBehaviour
{

    [SerializeField] GameManager_Intubation gameManager;

    public GameObject laringoscopio_puesta;
    public GameObject laringoscopio_explicacion;

    public static bool no_puesto = true;

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

        if (collision.gameObject.tag == "laringoscopio")
        {
            no_puesto = false;
            laringoscopio_explicacion.SetActive(false);
            laringoscopio_puesta.SetActive(true);


            gameObject.SetActive(false);



        }
    }
}
