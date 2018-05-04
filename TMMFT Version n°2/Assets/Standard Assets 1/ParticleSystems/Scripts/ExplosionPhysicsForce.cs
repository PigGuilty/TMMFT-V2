using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ExplosionPhysicsForce : MonoBehaviour
    {
        public float explosionForce = 4;
        private float Degat;

        private IEnumerator Start()
        {
            // wait one frame because some explosions instantiate debris which should then
            // be pushed by physics force
            yield return null;

            float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;

            float r = 10*multiplier;
            var cols = Physics.OverlapSphere(transform.position, r);
            var rigidbodies = new List<Rigidbody>();
            foreach (var col in cols)
            {
                if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody))
                {
                    rigidbodies.Add(col.attachedRigidbody);
                }
            }
            foreach (var rb in rigidbodies)
            {
                Target target = rb.transform.GetComponent<Target>();
                if (target != null)
                {
                    float OldPos = rb.velocity.magnitude;
                    rb.AddExplosionForce(explosionForce*multiplier, transform.position, r, 1*multiplier, ForceMode.Impulse);
                    yield return new WaitForSeconds(0.1f);
                    float NewPos = rb.velocity.magnitude;

                    if (NewPos > OldPos)
                    {
                        Degat = NewPos - OldPos;
                    }
                    else
                    {
                        Degat =  OldPos - NewPos;
                    }
                    

                    print(OldPos);
                    print(NewPos);
                    print(Degat);

                    target.TakeDamage(Degat * explosionForce);
                }
                else
                {
                    rb.AddExplosionForce(explosionForce * multiplier, transform.position, r, 1 * multiplier, ForceMode.Impulse);
                }
            }
        }
    }
}
