using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class AddForceDownEscalier : MonoBehaviour
    {

        private Rigidbody rb;

        public float x;
        public float y;
        public float z;

        public int speedWalk;
        public int speedRun;
        private int increase;

        private Vector3 dir;

        private FirstPersonController fps;

        private bool redonnerKinematic;

        void Start()
        {
            dir = new Vector3(x, y, z);
            increase = 0;
        }



/*        void Update()
        {
            if(redonnerKinematic == true)
            {
                if(increase>= 20)
                {
                    rb.isKinematic = true;
                    increase = 0;
                    redonnerKinematic = false;
                }
                increase++;
            }
        }*/

        private void OnTriggerEnter(Collider other)
        {
            rb = other.GetComponent<Rigidbody>();

            if (other.gameObject.tag == "Player")
            {               
                rb.isKinematic = false;
                rb.AddForce(dir * speedWalk);
            }
        }

        private void OnTriggerStay(Collider other)
        {

            rb = other.GetComponent<Rigidbody>();
            fps = other.GetComponent<FirstPersonController>();

            if (other.gameObject.tag == "Player")
            {
                if(fps.m_IsWalking == true)
                {
                    rb.AddForce(dir * speedWalk);
                }
                else
                {
                    rb.AddForce(dir * speedRun);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                rb.AddForce(new Vector3(x, -y, z) * speedWalk * 200);
                //redonnerKinematic = true;
                rb.isKinematic = true;
            }
        }
    }
}