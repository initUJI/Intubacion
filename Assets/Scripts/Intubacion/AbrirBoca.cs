using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbrirBoca : MonoBehaviour
{

    [SerializeField] GameManager_Intubation gameManager;
    public GameObject paciente;
    public GameObject boca_cerrada;

    public static bool vibrar = false;

    //Viejo
    public GameObject manoDerecha;
    public GameObject manoIzquierda;
    //
    public GameObject manos_explicacion;

    public GameObject _handRight, _handLeft;
    public GameObject _handRightGhost;
    public GameObject _handLeftGhost;

    public GameObject anim_final_mano;

    Vector3 _finalPoseAnimR;
    Vector3 _finalPoseAnimL;

    public static bool mano_terminado = false;
    public static bool _handRLerping = false;
    public static bool _handLLerping = false;
    public static bool girando_manos = false;

    Coroutine interpolatingR = null;
    Coroutine interpolatingL = null;

    public Material _matGhost;
    public Material _matNormal;

    public GameManager_Intubation gameManager_intubacion;

    public AreaDetector _areaDetector;

    Animator _animator;

    string _clipName = "";

    public float normalizedTime = 0;

    public int controllerIndex; // Índice del controlador (0 para izquierdo, 1 para derecho)


    float init_rotation = 0f;
    float old_rotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager_intubacion= FindObjectOfType<GameManager_Intubation>();
        _areaDetector = GetComponent<AreaDetector>();
        _areaDetector.f_enterRight.AddListener(f_enterRight);
        _areaDetector.f_exitRight.AddListener(f_exitRight);
        _areaDetector.f_enterLeft.AddListener(f_enterLeft);
        _areaDetector.f_exitLeft.AddListener(f_exitLeft);

        _finalPoseAnimR = _handRightGhost.transform.position;
        _finalPoseAnimL = _handLeftGhost.transform.position;
    }
    void Update()
    {
        //rotateHandsEval(60f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "mano")
        {
            
            if (gameManager.fsm.estadoActual == MaquinaEstadosIntubation.Estado.AbrirBoca)
            {
                //InterpolacionManos();

                anim_final_mano.SetActive(true);
                _animator = anim_final_mano.GetComponent<Animator>();
                _animator.speed = 0;
                _clipName = currentAnimationClip();
            }
        }
    }

    void f_enterRight(Collider collider)
    {
        if (collider.CompareTag("right"))
        {
            if (!_handRLerping && interpolatingR == null)
            {
                manos_explicacion.SetActive(false);
                interpolatingR = StartCoroutine(c_lerpPositionToAnimation("right"));
                _handRLerping = true;
            }
        }
    }

    void f_exitRight(Collider collider)
    {
        if (collider.CompareTag("right"))
        {
            if (!_handRLerping && interpolatingR == null)
            {
                StartCoroutine(c_lerpPositionToHands("right"));
                _handRLerping = true;
            }
        }
    }

    void f_enterLeft(Collider collider)
    {
        if (collider.CompareTag("left"))
        {
            if (!_handLLerping && interpolatingL == null)
            {
                manos_explicacion.SetActive(false);
                interpolatingL = StartCoroutine(c_lerpPositionToAnimation("left"));
                _handLLerping = true;
            }
        }
    }

    void f_exitLeft(Collider collider)
    {
        if (collider.CompareTag("left"))
        {
            if (!_handLLerping && interpolatingL == null)
            {
                interpolatingL = StartCoroutine(c_lerpPositionToHands("left"));
                _handLLerping = true;
            }
        }
    }

    IEnumerator c_lerpPositionToAnimation(String hand)
    {
        float animationTime = 1f;
        float tiempoTranscurrido = 0f;
        float ratio = 0f;

        Vector3 initPos = Vector3.zero;
        Vector3 endPos = Vector3.zero;

        Action WhenFinish = null;


        GameObject handTrack = null;
        GameObject handGhost = null;

        switch (hand)
        {
            case "right":
                handTrack = _handRight;
                handGhost = _handRightGhost;
                endPos = _finalPoseAnimR;

                WhenFinish = () => { _handRLerping = false; interpolatingR = null; };
                break;
            case "left":
                handTrack = _handLeft;
                handGhost = _handLeftGhost;
                endPos = _finalPoseAnimL;

                WhenFinish = () => { _handLLerping = false; interpolatingL = null; };
                break;
            default: break;
        }

        initPos = handTrack.transform.position;

        handTrack.GetComponentInChildren<SkinnedMeshRenderer>().material = _matGhost;
        handGhost.SetActive(true);


        handGhost.transform.position = handTrack.transform.position;
        print("Empieza Interpolacion");
        while (tiempoTranscurrido < animationTime)
        {
            print("ratio: " + ratio);
            ratio = tiempoTranscurrido / animationTime;

            handGhost.transform.position = Vector3.Lerp(initPos, endPos, ratio);

            yield return new WaitForSeconds(1/60f);

            tiempoTranscurrido += 1/60f;
        }

        print("Termina Interpolacion");

        WhenFinish?.Invoke();

        yield return new WaitForSeconds(0.75f);
        //poniendo_manos = false;
        //girando_manos = true;

        //init_rotation = manoDerecha.transform.rotation.eulerAngles.x;

        yield return null;
    }
    IEnumerator c_lerpPositionToHands(string hand)
    {
        float animationTime = 1f;
        float tiempoTranscurrido = 0f;
        float ratio = 0f;

        Vector3 initPos = Vector3.zero;
        Vector3 endPos = Vector3.zero;

        Action WhenFinish = null;

        GameObject handTrack = null;
        GameObject handGhost = null;

        switch (hand)
        {
            case "right":
                handTrack = _handRight;
                handGhost = _handRightGhost;

                WhenFinish = () => { _handRLerping = false; interpolatingR = null; };
                break;
            case "left":
                handTrack = _handLeft;
                handGhost = _handLeftGhost;

                WhenFinish = () => { _handLLerping = false; interpolatingL = null; };
                break;
            default: break;
        }

        initPos = handGhost.transform.position;
        endPos = handTrack.transform.position;

        print("Empieza Interpolacion");
        while (tiempoTranscurrido < animationTime)
        {
            endPos = handTrack.transform.position;
            print("ratio: " + ratio);
            ratio = tiempoTranscurrido / animationTime;

            handGhost.transform.position = Vector3.Lerp(initPos, endPos, ratio);

            yield return new WaitForSeconds(1 / 60f);

            tiempoTranscurrido += 1 / 60f;
        }

        print("Termina Interpolacion");


        handTrack.GetComponentInChildren<SkinnedMeshRenderer>().material = _matNormal;
        handGhost.SetActive(false);

        WhenFinish?.Invoke();

        yield return new WaitForSeconds(0.75f);
        //girando_manos = true;

        // init_rotation = manoDerecha.transform.rotation.eulerAngles.x;
        yield return null;
    }

    public void f_abriBoca()
    {
        boca_cerrada.SetActive(false);
        paciente.SetActive(true);

        gameManager.ActivarEstado(MaquinaEstadosIntubation.Estado.IntroducirCanulaGuedel, "Introduzca la cánula de Guedel en la boca del paciente para mantener abierta la vía aérea superior y así ventilar al paciente.");

        StartCoroutine(recuperarManos());
    }

    void rotateHandsEval(float finalRot)
    {
        if(girando_manos)
        {
            float auxRot = (Mathf.Abs(manoDerecha.transform.rotation.eulerAngles.x - init_rotation) > finalRot + 2 ? (360 - Mathf.Abs(manoDerecha.transform.rotation.eulerAngles.x - init_rotation)) : Mathf.Abs(manoDerecha.transform.rotation.eulerAngles.x - init_rotation));
            normalizedTime = (auxRot/ finalRot) * 60;
            if(normalizedTime < 0)
            {
                print("Valor negativo");
                return;
            }

            print($"Rotacion derecha: {manoDerecha.transform.rotation.eulerAngles.x};\nRotacion inicial: {init_rotation};\nRotReal: {auxRot} ;Resta rotaciones: {manoDerecha.transform.rotation.eulerAngles.x - init_rotation}, en valor absoluto: {Mathf.Abs(manoDerecha.transform.rotation.eulerAngles.x - init_rotation)};\nTodo entre 90: {(Mathf.Abs(manoDerecha.transform.rotation.eulerAngles.x - init_rotation) / 90)};\nValor por 60: {normalizedTime}");
            jumpToTime(_clipName, normalizedTime / 60);

            if(auxRot > finalRot)
            {
                girando_manos = false;
                mano_terminado = true;
                f_abriBoca();
            }
        }
    }

    void jumpToTime(string name, float nTime)
    {
        _animator.Play(name, 0, nTime);
    }

    string currentAnimationClip()
    {
        var currAnimName = "";
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
            {
                currAnimName = clip.name.ToString();
            }
        }

        return currAnimName;
    }
    
    IEnumerator recuperarManos()
    {
        float animationTime = 2.3f;
        float tiempoTranscurrido = 0f;
        float ratio = 0f;

        Vector3 initPosM_der = _handRightGhost.transform.position;
        Vector3 endPosM_der = manoDerecha.transform.position;

        Vector3 initPosM_izq = _handLeftGhost.transform.position;
        Vector3 endPosM_izq =  manoIzquierda.transform.position;

        print("Empieza Interpolacion");

        yield return new WaitForSeconds(1f);
        while (tiempoTranscurrido < animationTime)
        {
            print("ratio: " + ratio);
            ratio = tiempoTranscurrido / animationTime;

            _handRightGhost.transform.position = Vector3.Lerp(initPosM_der, endPosM_der, ratio);
            _handLeftGhost.transform.position = Vector3.Lerp(initPosM_izq, endPosM_izq, ratio);

            yield return new WaitForSeconds(1 / 75f);

            tiempoTranscurrido += 0.1f;
        }

        anim_final_mano.SetActive(false);
        /*
        Renderer renderer = manoIzquierda_material.GetComponent<Renderer>();
        renderer.material = materialNormal;

        Renderer renderer2 = manoDerecha_material.GetComponent<Renderer>();
        renderer2.material = materialNormal;
        print("Termina INterpolacion");
        */
        yield return null;
    }
}




