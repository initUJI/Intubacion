using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeringuillaTrigger : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;

    public GameObject jeringuilla_explicacion;

    public static bool jeringa_no_puesta = true;

    public GameObject tubo2;
    public GameObject tubo;

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

        if (collision.gameObject.tag == "jeringuilla")
        {
            jeringa_no_puesta = false;
            jeringuilla_explicacion.SetActive(false);

            tubo2.SetActive(true);

            tubo.SetActive(false);



        }
    }
}
