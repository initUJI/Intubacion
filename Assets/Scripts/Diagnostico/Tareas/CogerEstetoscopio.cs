using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerEstetoscopio : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "CampanaEstetoscopio")
        {
            //hacer que se detecte solo si esta agarrando con el trigger
            var joint = gameObject.AddComponent<DistanceJoint3D>();
            joint.ConnectedRigidbody = collision.gameObject.transform; //el padre es EL ESTETOSCOPIO

            joint.Distance = Vector3.Distance(gameObject.transform.position, collision.gameObject.transform.position);
        }
    }
}
