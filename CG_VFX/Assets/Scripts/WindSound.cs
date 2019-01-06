using UnityEngine;

public class WindSound : MonoBehaviour {

    [SerializeField] Rigidbody rb;
    [SerializeField] AudioSource windAS;
	
	void Update () {
        windAS.volume = rb.velocity.magnitude / 10f;
	}
}
