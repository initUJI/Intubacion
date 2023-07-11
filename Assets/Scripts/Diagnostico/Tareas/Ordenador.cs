using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ordenador : MonoBehaviour
{
    [SerializeField] GameManager_Diagnostico gameManager;
    [SerializeField] GameObject historialPC;
    [SerializeField] GameObject cartel;

    void Start()
    {
        
    }

    void Update()
    {
        //gameManager.MostrarObjetoCercano(transform.position, 1.5f, cartel, GetComponent<Outline>());

    }
    private void OnCollisionEnter(Collision collision)
    {
        historialPC.SetActive(true);
    }
}
