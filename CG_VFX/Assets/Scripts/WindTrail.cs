using UnityEngine;

public class WindTrail : MonoBehaviour {

    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem windTrailPS;
	
	void Update () {

        float rbSpeed = rb.velocity.magnitude;

        transform.forward = rb.velocity.normalized;
	}
}
