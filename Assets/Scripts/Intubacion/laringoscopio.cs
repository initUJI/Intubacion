using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laringoscopio : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;


    public GameObject cabeza_laringoscopio;

    public GameObject posicion;

    [SerializeField] GameObject Light;
    public static  bool larin_unido = false;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Light.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Light.gameObject.activeSelf)
        {
           
            larin_unido = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mango_laringoscopio")
        {
            Rigidbody rb = cabeza_laringoscopio.GetComponent<Rigidbody>();

            cabeza_laringoscopio.layer = default;

            rb.isKinematic = true;

            gameObject.transform.parent.parent = other.transform.parent;

            cabeza_laringoscopio.transform.position = Vector3.MoveTowards(transform.position, posicion.transform.position, speed);

            Light.gameObject.SetActive(true);


        }





    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "mango_laringoscopio")
        {
            Rigidbody rb = cabeza_laringoscopio.GetComponent<Rigidbody>();

            cabeza_laringoscopio.layer = 6;
            rb.isKinematic = false;

            Light.gameObject.SetActive(false);

        }
    }


}