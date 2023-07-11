using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour
{
    [SerializeField] float radioDeDeteccion;
    [SerializeField] GameObject cartel;
    [SerializeField] Outline outline;
    [SerializeField] GameObject player;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= radioDeDeteccion)
        {
            outline.enabled = true;
            cartel.SetActive(true);
        }
        else
        {
            outline.enabled = false;
            cartel.SetActive(false);
        }
    }
}
