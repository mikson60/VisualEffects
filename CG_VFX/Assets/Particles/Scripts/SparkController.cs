using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour {

    public ParticleSystem sparks;

    // Use this for initialization
    void Start () {
        string name = this.name;
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " collided with " + collision.collider.name);
        sparks.Play();

        foreach (ContactPoint contact in collision.contacts)
        {
            sparks.transform.localPosition = transform.InverseTransformPoint(contact.point);
            
        }
        //if (collision.relativeVelocity.magnitude > 2)
            
    }

    private void OnCollisionStay(Collision collision)
    {

        foreach (ContactPoint contact in collision.contacts)
        {
            sparks.transform.localPosition = transform.InverseTransformPoint(contact.point);
            sparks.transform.LookAt(contact.point);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(name + " stopped with " + collision.collider.name);
        sparks.Stop();
    }
}
