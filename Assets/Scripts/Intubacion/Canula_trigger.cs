using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canula_trigger : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;

    public GameObject detector_canula;
    public GameObject canula_explicacion;

    public GameObject canula_mano;

    public static bool no_puesta = true;
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

        if (collision.gameObject.tag == "canula")
        {
            canula_explicacion.SetActive(false);
            detector_canula.SetActive(true);
            canula_mano.SetActive(false);
            no_puesta = false;

  

        }
    }
}