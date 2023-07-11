using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Path_aux : MonoBehaviour
{
    public PathCreator pathCreator; // Referencia al componente PathCreator que contiene la curva

    public float movementSpeed = 2f;

    private Rigidbody rb;
    private bool isOnLane = false;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponentInParent<Rigidbody>();

    }

    private void Update()
    {
        if (isOnLane)
        {
            MoveAlongLane();
        }
    }

    private void MoveAlongLane()
    {
        Debug.Log("MoveAlong");
       // Obtener la posición más cercana en la curva
        Vector3 closestPoint = pathCreator.path.GetClosestPointOnPath(transform.position);

        // Calcular la dirección hacia el punto más cercano
        Vector3 direction = closestPoint - transform.position;
        direction.y = 0f; // Opcionalmente, asegúrate de mantener el objeto en el mismo plano horizontal

        // Mover el objeto hacia la posición más cercana en el carril
        rb.MovePosition(transform.position + direction.normalized * movementSpeed * Time.deltaTime);

        // Comprobar si el objeto ha llegado al final de la curva
        if (closestPoint == pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1))
        {
            isOnLane = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            audioSource.Play();
            // Realizar acciones adicionales cuando el objeto llega al final de la curva
            // Ejemplo: desactivar controles, reproducir una animación, etc.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si el jugador colisiona con un objeto de activación del carril
        if (other.CompareTag("curva"))
        {
            Debug.Log("DENTRO");
            isOnLane = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("curva"))
        {
            isOnLane = false;
        }
    }

}
