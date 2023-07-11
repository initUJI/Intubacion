using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerCable : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] Hand leftHand;
    [SerializeField] Hand rightHand;

    private bool tocado = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!tocado)
        {

            if (leftHand._isGrabbing && leftHand._heldObject == gameObject)
            {
                rb.isKinematic = false;
                tocado = true;
            }
            else if (rightHand._isGrabbing && rightHand._heldObject == gameObject)
            {

                rb.isKinematic = false;
                tocado = true;
            }

        }

    }
}
