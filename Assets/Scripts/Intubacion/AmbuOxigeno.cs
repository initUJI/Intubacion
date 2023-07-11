using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmbuOxigeno : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;

    public Animator animManta;
    public Animator animAmbu;
    public Animator animCuerpo;

    bool colision = false;

    bool wait_ambu = true;
    float aux_ambu = 0f;

    bool primera = true;

    public GameObject contador;
    public GameObject veces;
    public Text textVeces;
    public Text textContador;

    int numContador;
    int numVeces;
    private void Start()
    {
               
    }
    void Update()
    {

        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.UtilizarAmbu && primera)
        {

            animManta.SetBool("alzar", true);
            contador.SetActive(true);
            veces.SetActive(true);
        }

        if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.UtilizarAmbu && colision)
        {
            if (primera)
            {
                animAmbu.SetBool("presionar", true);
                animCuerpo.SetBool("inflar", true);
                numVeces+= 1;
                primera = false;

                textVeces.text = numVeces.ToString();
            }

            
            if (wait_ambu == false)
            {
                animAmbu.SetBool("presionar", true);
                animCuerpo.SetBool("inflar", true);
                numVeces+=1;

                wait_ambu = true;
                aux_ambu = 0f;

                textVeces.text = numVeces.ToString();
            }
            

        }
        else if(gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.UtilizarAmbu && colision == false)
        {
            WaitAmbu();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "mano")
        {
           
            colision = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "mano")
        {
            
            colision = false;
            animAmbu.SetBool("presionar", false);
            animCuerpo.SetBool("inflar", false);
        }
    }


    void WaitAmbu()
    {
        aux_ambu += Time.deltaTime;
        textContador.text = "Espere: " + aux_ambu.ToString("F0");

        if (aux_ambu > 3f)
        {
            wait_ambu = false;

            textContador.text = "Presione";
        }
    }
}
