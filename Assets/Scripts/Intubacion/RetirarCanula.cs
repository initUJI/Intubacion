using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetirarCanula : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;

    MeshCollider meshCollider;
    
    int layerIndex;
    //public GameObject canula_puesta;
    public GameObject canula;
    
    //public Animator anim;
    private bool fuera = false;

    public static bool ponerLaringoscopio = false;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();

        rb = GetComponent<Rigidbody>();


        layerIndex = LayerMask.NameToLayer("Grabbable");

    }

    // Update is called once per frame
    void Update()
    {

        

        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.QuitarCanulaGuedel)
        {
            if (!fuera)
            { 
                meshCollider.enabled = true;
                //anim.enabled = false;

                gameObject.layer = layerIndex;
                rb.isKinematic = false;

                fuera = true;


            }
           

        }
           
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.QuitarCanulaGuedel)
        {
            if(collision.gameObject.tag == "mesa")
            {
                
                ponerLaringoscopio = true;
            }


        }

    }
}
