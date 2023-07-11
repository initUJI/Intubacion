using UnityEngine;
using UnityEngine.Events;

public class AreaDetector : MonoBehaviour
{
    [SerializeField] private Detector _rightAreaDetector;
    [SerializeField] private Detector _leftAreaDetector;

    [HideInInspector]
    public UnityEvent<Collider> f_enterRight, f_exitRight, f_enterLeft, f_exitLeft;
    private void OnEnable()
    {
        _rightAreaDetector.onTriggerEnter.AddListener(WhenRightTriggerEnter);
        _rightAreaDetector.onTriggerExit.AddListener(WhenRightTriggerExit);
        _leftAreaDetector.onTriggerEnter.AddListener(WhenLeftTriggerEnter);
        _leftAreaDetector.onTriggerExit.AddListener(WhenLeftTriggerExit);
    }

    private void OnDisable()
    {
        _rightAreaDetector.onTriggerEnter.RemoveListener(WhenRightTriggerEnter);
        _rightAreaDetector.onTriggerExit.RemoveListener(WhenRightTriggerExit);
        _leftAreaDetector.onTriggerEnter.RemoveListener(WhenLeftTriggerEnter);
        _leftAreaDetector.onTriggerExit.RemoveListener(WhenLeftTriggerExit);
    }


    public void WhenRightTriggerEnter(Collider collider)
    {
        f_enterRight?.Invoke(collider);
    }

    public void WhenRightTriggerExit(Collider collider)
    {
        Debug.Log("WhenTopTriggerExit");
        f_exitRight?.Invoke(collider);
    }

    public void WhenLeftTriggerEnter(Collider collider)
    {
        Debug.Log("WhenBottomTriggerEnter");
        f_enterLeft?.Invoke(collider);
    }

    public void WhenLeftTriggerExit(Collider collider)
    {
        Debug.Log("WhenBottomTriggerExit");
        f_exitLeft?.Invoke(collider);
    }
}