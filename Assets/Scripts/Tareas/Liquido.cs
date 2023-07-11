using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquido : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject liquidoTubo;
    private float maxLiquid = 0.005f;
    private float scale;
    void Start()
    {
        scale = 0;
    }

    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        //Si la gota de liquido choca con el tubo, se va rellenando hasta un tope
        if(other.gameObject.tag == "TuboMuestra" && gameManager.fsm.estadoActual == MaquinaEstados.Estado.TuboPresion)
        {
            RellenarTuboMuestra();
        }
    }

    void RellenarTuboMuestra()
    {
        liquidoTubo.SetActive(true);
        scale += 0.001f;
        liquidoTubo.transform.localScale = new Vector3(0.01f, scale, 0.01f);

        var pos = liquidoTubo.transform.localPosition;
        pos.y += 0.001f;
        liquidoTubo.transform.localPosition = pos;

        if (liquidoTubo.transform.localScale.y >= maxLiquid)
        {
            GetComponentInParent<TuboPresion>().CerrarValvula();
        }
    }
}
