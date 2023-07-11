using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Animator))]

public class Hand : MonoBehaviour
{
    //Animation
    Animator animator;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    public float speed;

    //Physics Movement
    [SerializeField] private ActionBasedController controller;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [Space]
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    [Space]
    [SerializeField] private Transform palm;
    [SerializeField] float reachDistance = 0.1f, joinDistance = 0.05f;
    [SerializeField] private LayerMask grabbableLayer;

    private Transform _followTarget;
    private Rigidbody _body;

    public bool _isGrabbing;
    public GameObject _heldObject;

    private Transform _grabPoint;
    private FixedJoint _join1, _join2;

    void Start()
    {
        //Animation
        animator = GetComponent<Animator>();

        //Physics movement
        _followTarget = controller.gameObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 20f;
        _body.maxAngularVelocity = 20f;

        // Input Setup
        controller.selectAction.action.started += Grab;
        controller.selectAction.action.canceled += Released;

        // Teleport hands
        _body.position = _followTarget.position;
        _body.rotation = _followTarget.rotation;

    }
    void Update()
    {
        AnimateHand();
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        //Position
        var positionWithOffset = _followTarget.TransformPoint(positionOffset) ;
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        //Rotation
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
    }

    private void Grab(InputAction.CallbackContext context)
    {
        if (_isGrabbing || _heldObject) return;

        Collider[] grababbleColliders = Physics.OverlapSphere(palm.position, reachDistance, grabbableLayer);
        if (grababbleColliders.Length < 1) return;

        var objectToGrab = grababbleColliders[0].transform.gameObject;

        var objectBody = objectToGrab.GetComponent<Rigidbody>();
        if(objectBody != null)
        {
            _heldObject = objectBody.gameObject;
        }
        else
        {
            objectBody = objectToGrab.GetComponentInParent<Rigidbody>();
            if (objectBody != null)
            {
                _heldObject = objectBody.gameObject;
            }
            else
            {
                return;
            }
        }

        StartCoroutine(GrabObject(grababbleColliders[0], objectBody));
    }

    private IEnumerator GrabObject(Collider collider, Rigidbody targetBody)
    {
        _isGrabbing = true;

        //Create a grap point
        _grabPoint = new GameObject().transform;
        _grabPoint.position = collider.ClosestPoint(palm.position);
        _grabPoint.parent = _heldObject.transform;

        //Move hand to grab point
        _followTarget = _grabPoint;

        //Wait for hand to reach grab point
        while(Vector3.Distance(_grabPoint.position, palm.position) > joinDistance && _isGrabbing)
        {
            yield return new WaitForEndOfFrame();
        }

        //Freeze hand and object motion
        _body.velocity = Vector3.zero;
        _body.angularVelocity = Vector3.zero;
        targetBody.velocity = Vector3.zero;
        targetBody.angularVelocity = Vector3.zero;

        targetBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        targetBody.interpolation = RigidbodyInterpolation.Interpolate;

        //Attach joints
        _join1 = gameObject.AddComponent<FixedJoint>();
        _join1.connectedBody = targetBody;
        _join1.breakForce = float.PositiveInfinity;
        _join1.breakTorque = float.PositiveInfinity;

        _join1.connectedMassScale = 1;
        _join1.massScale = 1;
        _join1.enableCollision = false;
        _join1.enablePreprocessing = false;

        _join2 = _heldObject.AddComponent<FixedJoint>();
        _join2.connectedBody = _body;
        _join2.breakForce = float.PositiveInfinity;
        _join2.breakTorque = float.PositiveInfinity;

        _join2.connectedMassScale = 1;
        _join2.massScale = 1;
        _join2.enableCollision = false;
        _join2.enablePreprocessing = false;

        //Reset follow target
        _followTarget = controller.gameObject.transform;
    }

    private void Released(InputAction.CallbackContext context)
    {
        if (_join1 != null) Destroy(_join1);
        if (_join2 != null) Destroy(_join2);
        if (_grabPoint != null) Destroy(_grabPoint.gameObject);

        if(_heldObject != null)
        {
            var targetBody = _heldObject.GetComponent<Rigidbody>();
            targetBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            targetBody.interpolation = RigidbodyInterpolation.None;
            _heldObject = null;
        }

        _isGrabbing = false;
        _followTarget = controller.gameObject.transform;

    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat("Grip", gripCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat("Trigger", triggerCurrent);
        }
    }
}
