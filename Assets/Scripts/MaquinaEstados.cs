using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaquinaEstados : MonoBehaviour
{
    public enum Estado
    {
        DetectarVertebra,
        MarcarPunto,
        DesinfectarZona,
        ColocarBata,
        InyectarAnestesia,
        ColocarEstilete,
        InyectarAgujaPuncion,
        EmpujarHastaObtenerLCR,
        TuboPresion,
        SacarAguja,
        Aposito
        
    }

    public Estado estadoInicial;
    public Estado estadoActual;

    void Start()
    {
        estadoActual = estadoInicial ;
    }

}
