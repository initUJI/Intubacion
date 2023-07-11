using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbuTrigger : MonoBehaviour
{

    public GameObject ambu_explicacion;

    public GameObject ambu_puesto;

    public static bool puesto = false;

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

        if (collision.gameObject.tag == "ambu")
        {
            ambu_explicacion.SetActive(false);

            gameObject.SetActive(false);

            ambu_puesto.SetActive(true);

            puesto = true;
        }

    }
}