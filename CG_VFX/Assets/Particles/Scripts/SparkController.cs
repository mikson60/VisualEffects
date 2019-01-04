using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour {

    public ParticleSystem sparks;

    private Dictionary<Collider, ParticleSystem> particleSystems = new Dictionary<Collider, ParticleSystem>();

    // Use this for initialization
    void Start () {
        string name = this.name;
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " collided with " + collision.collider.name);
        
        Debug.Log("Contacts on enter" + collision.contacts.Length);
        //if (collision.contacts.Length == 1)
        //{
            ParticleSystem ps;
            if (!particleSystems.ContainsKey(collision.collider))
            {
                ps = ParticleSystem.Instantiate<ParticleSystem>(sparks);
                particleSystems.Add(collision.collider, ps);
            }
            else
            {
                ps = particleSystems[collision.collider];
            }

            ps.transform.position = (collision.contacts[0].point);

            ps.Play();
        //}
        //else
        //{
            
        //}
        
        foreach (ContactPoint contact in collision.contacts)
        {
            
            //ps.transform.LookAt(contact.normal);
        }

        Debug.Log("Relative velocity on enter " + collision.relativeVelocity.magnitude);
        
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
            Debug.Log("Relative velocity on stay " + collision.relativeVelocity.magnitude);
            ParticleSystem ps = particleSystems[collision.collider];
            Debug.Log("Contacts on stay " + collision.contacts.Length);
            foreach (ContactPoint contact in collision.contacts)
            {
                ps.transform.position = (contact.point);
                //ps.transform.LookAt(contact.normal);
            }

            //Debug.Log(sparks.emission.rateOverTime.constant);
            //Debug.Log(sparks.emission.rateOverTime.constant * collision.relativeVelocity.magnitude);
            //Debug.Log(sparks.emission.rateOverTime.constantMax);
            //Debug.Log(sparks.emission.rateOverTime.constantMax * collision.relativeVelocity.magnitude);

            var em = ps.emission;
            var emRate = em.rateOverTime;
            emRate.constant = sparks.emission.rateOverTime.constant * collision.relativeVelocity.magnitude;
            //emRate.constantMax = sparks.emission.rateOverTime.constantMax * collision.relativeVelocity.magnitude;
            em.rateOverTime = emRate;
            Debug.Log("Result " + em.rateOverTime.constant);
            Debug.Log("Actual " + ps.emission.rateOverTime.constant);
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
            ParticleSystem ps = particleSystems[collision.collider];
            ps.Stop();
            particleSystems.Remove(collision.collider);
        }
    }

    //Testcommit
}
