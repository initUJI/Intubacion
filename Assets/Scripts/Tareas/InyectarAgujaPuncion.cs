using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InyectarAgujaPuncion : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject cuerpo;
    [SerializeField] GameObject vertebra1Collider;
    [SerializeField] GameObject vertebra2Collider;
    [SerializeField] GameObject puncionVista;
    private bool colocandoAguja;
    private bool start;
    Outline outlineAguja;
    void Start()
    {
        colocandoAguja = true;
        start = true;
        outlineAguja = GetComponent<Outline>();
    }
    void Update()
    {
        if (gameManager.fsm.estadoActual == MaquinaEstados.Estado.InyectarAgujaPuncion && start)
        {
            if (puncionVista.activeInHierarchy == false) puncionVista.SetActive(true);
            //Desactivar colliders para detectar vertebra
            vertebra1Collider.SetActive(false);
            vertebra2Collider.SetActive(false);
            cuerpo.GetComponent<MeshCollider>().enabled = false;
            outlineAguja.enabled = true;
            start = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Vertebra" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.InyectarAgujaPuncion)
        {
            //Ricardo: He comentado esto porque daba error con el nuevo sistema, habra que adaptarlo.
            //gameObject.GetComponent<HapticController>().SendHaptics();
        }
    }
    private void OnTriggerEnter(Collider other)
    {      
        if (colocandoAguja && other.gameObject.tag == "Interespinal" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.InyectarAgujaPuncion)
        {
            colocandoAguja = false;
            outlineAguja.enabled = false;
            ColocarAgujaInsercion(other.gameObject);
            
        }
    }

    void ColocarAgujaInsercion(GameObject espacioInterespinal)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
       
        var pos = espacioInterespinal.transform.position;

        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(-espacioInterespinal.transform.forward);

        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = true;

        gameManager.ActivarEstado(MaquinaEstados.Estado.EmpujarHastaObtenerLCR, "Continúa hasta llegar al espacio aracnoideo y obtener LCR. Puedes empujar con la A o con tu mano, no es necesario " +
            "que agarres nada. Ve quitando el estilete para ver si hay líquido. Recuerda que solo puedes mover la aguja si está el estilete introducido por completo.");

    }
}
