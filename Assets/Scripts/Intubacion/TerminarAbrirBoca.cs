using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class TerminarAbrirBoca : MonoBehaviour
{
    [SerializeField] GameManager_Intubation gameManager;

    public GameObject mano_derecha;
    public GameObject mano_izquierda;

    [SerializeField] GameObject anim_final_mano;

    private float aux = 0f;
    private bool wait_t = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
/*
        if (AbrirBoca.mano_terminado)
        {
            
            mano_izquierda.SetActive(false);
            mano_derecha.SetActive(false);

            Wait();

            if(wait_t == false)
            {
                mano_izquierda.SetActive(true);
                mano_derecha.SetActive(true);
                anim_final_mano.SetActive(false);
                print("termino manos");

                gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.IntroducirCanulaGuedel, "Introduzca la cánula de Guedel en la boca del paciente para mantener abierta la vía aérea superior y así ventilar al paciente.");

            }
            

        }
*/
    }

    void Wait()
    {

        aux += Time.deltaTime;
        if (aux >= 2f) wait_t = false;
    }
}
