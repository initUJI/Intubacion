using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeringa : MonoBehaviour
{
    [SerializeField] Hand leftHand;
    [SerializeField] Hand rightHand;
    bool tocado = false;

    public Animator animator;

    public static bool hinchar_globo = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if (!tocado)
        {
            if (leftHand._isGrabbing && leftHand._heldObject == gameObject)
            {
                animator.SetBool("entra", false);
                tocado = true;
            }
            else if (rightHand._isGrabbing && rightHand._heldObject == gameObject)
            {
                animator.SetBool("entra", false);
                tocado = true;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boquilla" )
        {

            hinchar_globo = true;

            animator.SetBool("entra", true);

            

        }
    }
}
