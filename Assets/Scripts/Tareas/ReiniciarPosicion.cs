using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiniciarPosicion : MonoBehaviour
{
    Vector3 initialPos;
    private void Start()
    {
        initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            transform.position = initialPos;
        }
    }
}
