using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour {

    public ParticleSystem sparks;
    public float MinRelVelocity;

    private Dictionary<Collider, List<ParticleSystem>> particleSystems = new Dictionary<Collider, List<ParticleSystem>>();

    // Use this for initialization
    void Start () {
        string name = this.name;
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " collided with " + collision.collider.name);
        //Debug.Log("Contacts on enter" + collision.contacts.Length);

        List<ParticleSystem> localSystems;
        if (!particleSystems.ContainsKey(collision.collider))
        {
            localSystems = new List<ParticleSystem>();

            foreach (ContactPoint contact in collision.contacts)
            {
                ParticleSystem ps;
                ps = ParticleSystem.Instantiate<ParticleSystem>(sparks);
                localSystems.Add(ps);
            }
            particleSystems.Add(collision.collider, localSystems);
        }
        else
        {
            localSystems = particleSystems[collision.collider];
        }

        for (int i = 0; i < collision.contacts.Length; i++)
        {
            localSystems[i].transform.position = collision.contacts[i].point;
            scaleEmissionRateWithSpeed(localSystems[i], collision);

            localSystems[i].Play();

            //localSystems[i].transform.LookAt(contact.normal);
        }


        //Debug.Log("Relative velocity on enter " + collision.relativeVelocity.magnitude);
        
        //ps.Play();
        //var emRate = ps.emission.rateOverTime;
        //emRate.constantMin = sparks.emission.rateOverTime.constantMin * collision.relativeVelocity.magnitude;
        //emRate.constantMax = sparks.emission.rateOverTime.constantMax * collision.relativeVelocity.magnitude;
        //var v = ps.velocityOverLifetime.speedModifier;
        //v.constantMin = sparks.velocityOverLifetime.speedModifier.constantMin * collision.relativeVelocity.magnitude;
        //v.constantMax = sparks.velocityOverLifetime.speedModifier.constantMax * collision.relativeVelocity.magnitude;


    }

    private void OnCollisionStay(Collision collision)
    {
        if (particleSystems.ContainsKey(collision.collider))
        {
            //Debug.Log("Contacts on stay " + collision.contacts.Length);

            //ParticleSystem ps = particleSystems[collision.collider];
            List<ParticleSystem> localSystems = particleSystems[collision.collider];
            for (int i = 0; i < collision.contacts.Length || i < localSystems.Count; i++)
            {
                
                
                if (i >= collision.contacts.Length)
                {
                    localSystems[i].Stop();
                } else
                {
                    if (localSystems[i] == null)
                    {
                        ParticleSystem ps;
                        ps = ParticleSystem.Instantiate<ParticleSystem>(sparks);
                        ps.Play();
                        localSystems.Add(ps);
                    }
                    localSystems[i].transform.position = collision.contacts[i].point;

                    //localSystems[i].velocityOverLifetime.s

                    scaleEmissionRateWithSpeed(localSystems[i], collision);

                }
            }

            //Debug.Log("Relative velocity on stay " + collision.relativeVelocity.magnitude);
            //Debug.Log(sparks.emission.rateOverTime.constant);
            //Debug.Log(sparks.emission.rateOverTime.constant * collision.relativeVelocity.magnitude);
            //Debug.Log(sparks.emission.rateOverTime.constantMax);
            //Debug.Log(sparks.emission.rateOverTime.constantMax * collision.relativeVelocity.magnitude);

            //var em = ps.emission;
            //var emRate = em.rateOverTime;
            //float newVel = sparks.emission.rateOverTime.constant * collision.relativeVelocity.magnitude;
            //emRate.constant = newVel;
            ////emRate.constantMax = sparks.emission.rateOverTime.constantMax * collision.relativeVelocity.magnitude;
            //em.rateOverTime = emRate;
            //Debug.Log("Result " + newVel);
            //Debug.Log("Actual " + ps.emission.rateOverTime.constant);

            //var v = ps.velocityOverLifetime.speedModifier;
            //v.constantMin = sparks.velocityOverLifetime.speedModifier.constantMin * collision.relativeVelocity.magnitude;
            //v.constantMax = sparks.velocityOverLifetime.speedModifier.constantMax * collision.relativeVelocity.magnitude;

            //Debug.Log(v.constantMin + ", " + v.constantMax);
            //Debug.Log(collision.contacts.Length);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (particleSystems.ContainsKey(collision.collider))
        {
            Debug.Log(name + " stopped colliding with " + collision.collider.name);
            foreach(ParticleSystem ps in particleSystems[collision.collider])
            {
                ps.Stop();
                //particleSystems.Remove(collision.collider);
            }
        }
    }

    private void scaleEmissionRateWithSpeed(ParticleSystem ps, Collision collision)
    {
        //Debug.Log("Old " + localSystems[i].emission.rateOverTime.constant);
        var em = ps.emission;
        var emRate = em.rateOverTime;
        float newVel = sparks.emission.rateOverTime.constant * collision.relativeVelocity.magnitude - MinRelVelocity;
        emRate.constant = newVel;
        //emRate.constantMax = sparks.emission.rateOverTime.constantMax * collision.relativeVelocity.magnitude;
        em.rateOverTime = emRate;
        //Debug.Log("New " + localSystems[i].emission.rateOverTime.constant);
    }
}
