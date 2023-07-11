using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarInteractor : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] GameObject RightDirect_Controller;
    [SerializeField] GameObject LeftDirect_Controller;
    [SerializeField] GameObject RightRay_Controller;
    [SerializeField] GameObject LeftRay_Controller;

    [Header("Hands")]
    [SerializeField] GameObject RightDirect_Hand;
    [SerializeField] GameObject LeftDirect_Hand;
    [SerializeField] GameObject RightRay_Hand;
    [SerializeField] GameObject LeftRay_Hand;

    private List<GameObject> DirectInteractors;
    private List<GameObject> RayInteractors;

    [SerializeField] GameObject button_play;

    void Start()
    {
        DirectInteractors = new List<GameObject>();
        RayInteractors = new List<GameObject>();

        DirectInteractors.Add(RightDirect_Controller);
        DirectInteractors.Add(LeftDirect_Controller);
        DirectInteractors.Add(RightDirect_Hand);
        DirectInteractors.Add(LeftDirect_Hand);

        RayInteractors.Add(RightRay_Controller);
        RayInteractors.Add(LeftRay_Controller);
        RayInteractors.Add(RightRay_Hand);
        RayInteractors.Add(LeftRay_Hand);

        //Al comenzar, desactivar el direct despues de haber asignado las variables. Esto es así porque se empieza la escena con un diálogo.
        ActivateRayInteraction();
    }

    private void Update()
    {
        if (button_play.activeSelf == false)
        {
            ActivateDirectInteraction();
        }
    }

    public void ActivateDirectInteraction()
    {
        //Desactivar ray
        foreach(GameObject interactor in RayInteractors)
        {
            interactor.SetActive(false);
        }
        //Activar direct
        foreach(GameObject interactor in DirectInteractors)
        {
            interactor.SetActive(true);
        }
    }
    public void ActivateRayInteraction()
    {
        //Desactivar direct
        foreach (GameObject interactor in DirectInteractors)
        {
            interactor.SetActive(false);
        }
        //Activar ray
        foreach (GameObject interactor in RayInteractors)
        {
            interactor.SetActive(true);
        }
    }
}
