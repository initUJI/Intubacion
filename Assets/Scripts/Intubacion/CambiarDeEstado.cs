using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarDeEstado : MonoBehaviour
{

    [SerializeField] GameManager_Intubation gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (QuitarLaringoscopio.quitarPaloTubo)
        {

            gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.QuitarPaloTubo, "Ahora retire el fiador que se encuentra dentro del tubo endotraqueal.");
        }

        if (RetirarPaloTubo.inflarGlobo)
        {
            gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.InflarGloboTubo, "Ponga la jeringa en la boquilla azul del tubo endotraqueal para inflar el bal�n y as� evitar que el tubo se mueva.");

        }

        if (RetirarJeringa.jeringa_mesa)
        {

            gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.PonerAmbu, "Ahora coloque el Amb� en el lugar indicado.");
        }

        
        if (AmbuTrigger.puesto)
        {
            gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.ConectarAmbuOxigeno, "Conecte el Amb� a la bombona de ox�geno que se encuentra al lado de la mesa con los utensilios.");
        }

        if (ConectarAmbu.cable_conectado)
        {
            gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.UtilizarAmbu, "Ahora apriete el Amb�, f�jese en c�mo el Amb� y el pecho se mueven.");
        }
    }
}
