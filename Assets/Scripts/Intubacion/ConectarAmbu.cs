using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectarAmbu : MonoBehaviour
{
    public GameObject cable;

    public GameObject posicion;

    public float speed;

    public GameObject ambu;

    public static bool cable_conectado = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enchufar")
        {

            Rigidbody rb =cable.GetComponent<Rigidbody>();

            cable.layer = default;

            rb.isKinematic = true;

            gameObject.transform.parent.parent = ambu.transform;

            cable.transform.position = Vector3.MoveTowards(transform.position, posicion.transform.position, speed);

            cable_conectado = true;
        }


    }
}
