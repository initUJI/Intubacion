using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitarLaringoscopio : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;

    MeshCollider meshColliderLarin;

    int layerIndex;
    
    public GameObject laringoscopio;
    public GameObject soporte1;
    public GameObject soporte2;
    public GameObject soporte3;

    public Animator anim;

    private bool fueraLarin = false;

    public static bool quitarPaloTubo = false;

    Rigidbody rb;

    private float auxlarin = 0f;
    private bool wait_larin = true;

    private float auxpalo = 0f;
    private bool wait_palo = true;

    // Start is called before the first frame update
    void Start()
    {
        meshColliderLarin = GetComponent<MeshCollider>();

        rb = GetComponent<Rigidbody>();

        layerIndex = LayerMask.NameToLayer("Grabbable");

    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.QuitarLaringoscopio)
        {
            if (!fueraLarin)
            {
                Wait_laring();

                if(wait_larin == false)
                {
                    meshColliderLarin.enabled = true;

                    anim.enabled = false;

                    gameObject.layer = layerIndex;
                    rb.isKinematic = false;

                    fueraLarin = true;

                }
                


            }

            if (soporte1.activeSelf)
            {
                Wait_palo();

                if (wait_palo == false)
                {
                    quitarPaloTubo = true;

                  

                }

            }


        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.QuitarLaringoscopio)
        {
            if (collision.gameObject.tag == "mesa")
            {

                soporte1.SetActive(true);
                soporte2.SetActive(true);
                soporte3.SetActive(true);


            }


        }

    }


    void Wait_laring()
    {

        auxlarin += Time.deltaTime;
        if (auxlarin >= 3f) wait_larin = false;
    }

    void Wait_palo()
    {
        auxpalo += Time.deltaTime;
        if (auxpalo >= 1.5f) wait_palo = false;

    }

}
