using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimByCode : MonoBehaviour
{
    Animator _animator;

    AnimationClip _animClip;

    string _clipName = "";

    public float normalizedTime = 0;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _animator.speed = 0;
        currentAnimationClip();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            jumpToTime(_clipName, normalizedTime / 60);
            normalizedTime += 1;
            print($" normalized time: {normalizedTime}, {(normalizedTime >= _animClip.length * 60 ? "Finished" : "InProgress")}");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpToTime("DoStuff", normalizedTime / 60);
            normalizedTime -= 1;
            print($" normalized time: {normalizedTime}");
        }
    }

    void jumpToTime(string name, float nTime)
    {
        _animator.Play(name, 0, nTime);
        //_animator.GetCurrentAnimatorStateInfo(0).IsName("DoStuff");
    }

    string currentAnimationClip()
    {
        var currAnimName = "";
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
            {
                currAnimName = clip.name.ToString();
                _animClip = clip;
            }
        }

        return currAnimName;
    }
}