using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globo : MonoBehaviour
{

    public Animator animGlobo;

    public static bool globo_hincahdo = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Jeringa.hinchar_globo)
        {

            animGlobo.SetBool("hinchar",true);

            globo_hincahdo = true;
        }
    }

}
