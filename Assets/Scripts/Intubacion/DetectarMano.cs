using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarMano : MonoBehaviour
{
    public GameObject mano_i;
    public GameObject mano_d;

    public GameObject paciente;
    public GameObject boca_cerrada;

    public GameObject manos_paciente;

    Rigidbody rb_d;
    Rigidbody rb_i;
    // Start is called before the first frame update
    void Start()
    {
        rb_d = mano_d.GetComponent<Rigidbody>();
        rb_i = mano_i.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if(other.tag == "mano_quieta")
        {
            rb_d.isKinematic = true;
            rb_i.isKinematic = true;
            boca_cerrada.SetActive(false);
            paciente.SetActive(true);
            manos_paciente.SetActive(false);
        }
        */
    }
}
