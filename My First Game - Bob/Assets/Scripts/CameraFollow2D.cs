using System;
using UnityEngine;

//copied from book
//road level uses cinemachine which I found out about from this:
//https://www.youtube.com/watch?v=2jTY11Am0Ig&ab_channel=Brackeys
namespace UnityStandardAssets._2D
{
    public class CameraFollow2D : MonoBehaviour
    {
        //i don't really know what most of this does since i copied it from the book but it's supposed to allow the camera to follow the player throughout the game
        public Transform target;
        public float damping = 0;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 1f;
        public float lookAheadMoveThreshold = 1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
    }
}