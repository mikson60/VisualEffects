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

        Debug.Log("Contacts " + collision.contacts.Length);
        foreach (ContactPoint contact in collision.contacts)
        {
            ps.transform.position = (contact.point);
            //ps.transform.LookAt(contact.normal);
        }

        Debug.Log(collision.relativeVelocity.magnitude);

        ps.Play();
        var emRate = ps.emission.rateOverTime;
        emRate.constantMin = sparks.emission.rateOverTime.constantMin * collision.relativeVelocity.magnitude;
        emRate.constantMax = sparks.emission.rateOverTime.constantMax * collision.relativeVelocity.magnitude;
        var v = ps.velocityOverLifetime.speedModifier;
        v.constantMin = sparks.velocityOverLifetime.speedModifier.constantMin * collision.relativeVelocity.magnitude;
        v.constantMax = sparks.velocityOverLifetime.speedModifier.constantMax * collision.relativeVelocity.magnitude;


    }

    private void OnCollisionStay(Collision collision)
    {
        if (particleSystems.ContainsKey(collision.collider))
        {
            ParticleSystem ps = particleSystems[collision.collider];
            Debug.Log("Contacts " + collision.contacts.Length);
            foreach (ContactPoint contact in collision.contacts)
            {
                ps.transform.position = (contact.point);
                //ps.transform.LookAt(contact.normal);
            }

            Debug.Log(collision.relativeVelocity.magnitude);
            var emRate = ps.emission.rateOverTime;
            emRate.constantMin = sparks.emission.rateOverTime.constantMin * collision.relativeVelocity.magnitude;
            emRate.constantMax = sparks.emission.rateOverTime.constantMax * collision.relativeVelocity.magnitude;
            var v = ps.velocityOverLifetime.speedModifier;
            v.constantMin = sparks.velocityOverLifetime.speedModifier.constantMin * collision.relativeVelocity.magnitude;
            v.constantMax = sparks.velocityOverLifetime.speedModifier.constantMax * collision.relativeVelocity.magnitude;

            Debug.Log(v.constantMin + ", " + v.constantMax);
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
        }
    }
}
