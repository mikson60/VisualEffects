using UnityEngine;

public class WindTrail : MonoBehaviour {

    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem windTrailPS;

    private ParticleSystem.MainModule main;
    private ParticleSystem.MinMaxCurve minMaxCurve;
    private ParticleSystem.EmissionModule emissionModule;

    private bool isPlaying;

    private void Start()
    {
        if (windTrailPS != null)
        {
            main = windTrailPS.main;
            minMaxCurve = main.startSize;
            emissionModule = windTrailPS.emission;
        }
        if (rb.velocity.magnitude < 0.5f)
        {
            emissionModule.enabled = false;
            isPlaying = false;
            windTrailPS.Clear();
        }
    }

    void Update () {

        float rbSpeed = rb.velocity.magnitude;
        
        if (rbSpeed > 8f)
        {
            if (!isPlaying)
            {
                windTrailPS.Play();
                emissionModule.enabled = true;
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying)
            {
                emissionModule.enabled = false;
                isPlaying = false;
                windTrailPS.Clear();
            }
        }

        if (isPlaying)
        {
            transform.forward = rb.velocity.normalized;

            main.simulationSpeed = rbSpeed / 15f;
            minMaxCurve.constant = Mathf.Clamp(rbSpeed / 10f, 0f, 1f);
        }
	}
}
