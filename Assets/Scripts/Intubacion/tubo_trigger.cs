using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tubo_trigger : MonoBehaviour
{

    public GameObject tubo_puesto;

    public GameObject tubo_explicacion;

    public static bool tubo_no_puesto = true;

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

        if (collision.gameObject.tag == "tubo")
        {
            tubo_no_puesto = false;
            tubo_explicacion.SetActive(false);
            tubo_puesto.SetActive(true);

            gameObject.SetActive(false);



        }
    }

}
