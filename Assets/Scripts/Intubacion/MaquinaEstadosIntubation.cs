using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaEstadosIntubation : MonoBehaviour
{

    public enum Estado
    {
        Comenzar,
        AbrirBoca,
        IntroducirCanulaGuedel,
        PrepararLaringoscopio,
        QuitarCanulaGuedel,
        IntroducirLaringoscopio,
        IntroducirTubo,
        QuitarLaringoscopio,
        QuitarPaloTubo,
        InflarGloboTubo,
        PonerAmbu,
        ConectarAmbuOxigeno,
        UtilizarAmbu,
        FijarTubo

    }

    public Estado estadoInicial;
    public Estado estadoActual;

    // Start is called before the first frame update
    void Start()
    {

        estadoActual = estadoInicial;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
