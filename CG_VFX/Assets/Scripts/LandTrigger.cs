using UnityEngine;

public class LandTrigger : MonoBehaviour {

    [SerializeField] Rigidbody rb;
    [SerializeField] AudioSource landAS;

    private void OnTriggerEnter(Collider other)
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            landAS.Play();
        }
    }
}
