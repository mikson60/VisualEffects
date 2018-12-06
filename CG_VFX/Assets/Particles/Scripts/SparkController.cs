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

        foreach (ContactPoint contact in collision.contacts)
        {
            ps.transform.position = (contact.point);
            //ps.transform.LookAt(contact.normal);
        }
        ps.Play();
        
        //if (collision.relativeVelocity.magnitude > 2)
            
    }

    private void OnCollisionStay(Collision collision)
    {
        if (particleSystems.ContainsKey(collision.collider))
        {
            ParticleSystem ps = particleSystems[collision.collider];
            foreach (ContactPoint contact in collision.contacts)
            {
                ps.transform.position = (contact.point);
                //ps.transform.LookAt(contact.normal);
            }
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

    //Testcommit
}
