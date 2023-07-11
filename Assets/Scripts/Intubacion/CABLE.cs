using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Misc
{


    public class CABLE : MonoBehaviour
    {
        [SerializeField] Transform startTransform, endTransform;
        [SerializeField] int segmentCount = 10;
        [SerializeField] float totalLength = 10;   //espacio entre esferas

        [SerializeField] float radius = 0.5f;

        [SerializeField] float totalWeight = 10;
        [SerializeField] float drag = 1;
        [SerializeField] float angularDrag = 1;

        [SerializeField] bool usePhysics = false;

        Transform[] segments = new Transform[0];
        [SerializeField] Transform segmentParent;

        private int prevSegmentCount;
        private float prevDrag;
        private float prevTotalWeight;
        private float prevAngularDrag;
        private float prevTotalLength;

        private float prevRadius;

        // Start is called before the first frame update
        private void Start()
        {
           
        }

        private void Update()
        {

            //Updating weigths and update segments

            if (prevSegmentCount != segmentCount)
            {
                RemoveSegments();
                segments = new Transform[segmentCount];
                GenerateSegments();
            }

            prevSegmentCount = segmentCount;

            if (totalWeight != prevTotalLength || prevDrag != drag || prevTotalWeight != totalWeight || prevAngularDrag != angularDrag)
            {
                UpdateCable();
            }

            prevTotalLength = totalLength;
            prevDrag = drag;
            prevTotalWeight = totalWeight;
            prevAngularDrag = angularDrag;

            if(prevRadius!= radius && usePhysics)
            {
                UpdateRadius();
            }
            prevRadius = radius;

        }

        private void UpdateRadius()
        {
            for(int i=0; i < segments.Length; i++)
            {
                SetRadiusOnSegment(segments[i], radius);

            }
        }

        private void SetRadiusOnSegment(Transform transform, float radius)
        {

            SphereCollider sphereCollider = transform.GetComponent<SphereCollider>();
            sphereCollider.radius = radius;
        }

        private void UpdateCable()
        {
            for(int i = 0; i<segments.Length; i++)
            {
                if(i != 0)
                {
                    UpdateLenghtOnSegment(segments[i], totalLength / segmentCount);
                }
                UpdateWeightOnSegment(segments[i], totalWeight, drag, angularDrag);
            }
        }

        private void UpdateWeightOnSegment(Transform transform, float totalWeight, float drag,float angularDrag)
        {
            Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
            rigidbody.mass = totalWeight / segmentCount;
            rigidbody.drag = drag;
            rigidbody.angularDrag = angularDrag;
        }


        private void UpdateLenghtOnSegment(Transform transform,float v)
        {
            ConfigurableJoint joint = transform.GetComponent<ConfigurableJoint>();
            if (joint != null)
            {
                joint.connectedAnchor = Vector3.forward * (totalLength / segmentCount);
            }
        }
        private void RemoveSegments()
        {
            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i] != null)
                {
                    Destroy(segments[i].gameObject);
                }

            }
        }

        void OnDrawGizmos()
        {
            for (int i = 0; i < segments.Length; i++)
            {
                if(segments[i] != null)
                {

                    Gizmos.DrawWireSphere(segments[i].position, radius);
                }
                

            }
        }

        private void GenerateSegments()
        {
            JoinSegment(startTransform, null, true);
            Transform prevTransform = startTransform;

            Vector3 direction = (endTransform.position - startTransform.position);

            for (int i = 0; i < segmentCount; i++)
            {
                
                //Crear los segmentos entre start y end
                GameObject segment = new GameObject($"segment_{i}");

                segment.transform.SetParent(segmentParent);

                segments[i] = segment.transform;


                // Calcular la posición de cada momento para que los segmentes entre start y end se muevan

                Vector3 pos = prevTransform.position + (direction / segmentCount);
                segment.transform.position = pos;

                JoinSegment(segment.transform,prevTransform);

                prevTransform = segment.transform;

  

            }
            JoinSegment(endTransform, prevTransform, true, true);
        }

        private void JoinSegment(Transform current, Transform connectedTransform, bool isKinetic = false, bool isCloseConnected = false )
        {
            if (current.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rigidbody = current.gameObject.AddComponent<Rigidbody>();
                rigidbody.isKinematic = isKinetic;
                rigidbody.mass = totalWeight / segmentCount;
                rigidbody.drag = drag;
                rigidbody.angularDrag = angularDrag;

            }

            

            if (usePhysics)
            {

                SphereCollider sphereCollider = current.gameObject.AddComponent<SphereCollider>();

                sphereCollider.radius = radius;
                  
            }

            //CONECTAMOS UNOS TRANSFORMS CON OTROS PARA QUE ESTEN UNIDOS

            if (connectedTransform != null)  //si estan conectados a otro objeto
            {
                ConfigurableJoint joint = current.GetComponent<ConfigurableJoint>();

                if (joint == null)
                {
                    joint = current.gameObject.AddComponent<ConfigurableJoint>();

                }

                joint.connectedBody = connectedTransform.GetComponent<Rigidbody>();

                joint.autoConfigureConnectedAnchor = false;


                if (isCloseConnected)
                {

                    joint.connectedAnchor = Vector3.forward * 0.1f;

                }
                else
                {
                    joint.connectedAnchor = Vector3.forward * (totalLength / segmentCount);
                }

                joint.xMotion = ConfigurableJointMotion.Locked;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;

                joint.angularXMotion = ConfigurableJointMotion.Free;
                joint.angularYMotion = ConfigurableJointMotion.Free;
                joint.angularZMotion = ConfigurableJointMotion.Limited;

                SoftJointLimit softJointLimit = new SoftJointLimit();
                softJointLimit.limit = 0;
                joint.angularZLimit = softJointLimit;

                JointDrive jointDrive = new JointDrive();
                jointDrive.positionDamper = 0;
                jointDrive.positionSpring = 0;
                joint.angularXDrive = jointDrive;
                joint.angularYZDrive = jointDrive;


            }
        }


    }


}

