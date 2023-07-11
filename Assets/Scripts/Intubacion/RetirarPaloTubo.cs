using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetirarPaloTubo : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;

    public BoxCollider box1;
    public BoxCollider box2;

    int layerIndex;

    public Animator anim;

    public Animator animTubo;

    private bool fueraPalo = false;

    public static bool inflarGlobo = false;

    public GameObject base_cuerpo;

    Rigidbody rigidbodyHijo;

    public GameObject soporte1;
    public GameObject soporte2;
    public GameObject soporte3;


    // Start is called before the first frame update
    void Start()
    {
        
        rigidbodyHijo = GetComponent<Rigidbody>();


        layerIndex = LayerMask.NameToLayer("Grabbable");

    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.QuitarPaloTubo)
        {
            if (!fueraPalo)
            {
                box1.enabled = true;
                box2.enabled = true;

                gameObject.layer = layerIndex;

                base_cuerpo.SetActive(false);

                rigidbodyHijo.isKinematic = false;
                

                fueraPalo = true;


            }

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.QuitarPaloTubo)
        {
            if (collision.gameObject.tag == "mesa")
            {
                soporte1.SetActive(false);
                soporte2.SetActive(false);
                soporte3.SetActive(false);

                inflarGlobo = true;

                base_cuerpo.SetActive(true);
            }


        }

    }
}
