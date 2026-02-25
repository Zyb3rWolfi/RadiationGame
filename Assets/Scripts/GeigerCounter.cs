using UnityEngine;

public class GeigerCounter : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource geigerAudioSource; // Drag your Geiger counter audio source here
    public float minClickInterval = 0.1f; // Minimum time between clicks at max radiation
    public float maxClickInterval = 1f; // Maximum time between clicks at low radiation
    
    [Header("Detection Settings")]
    public float detectionRadius = 5f; // Radius within which radiation sources are detected
    public LayerMask radiationLayer; // Layer for radiation sources
    [SerializeField] private GameObject clickGenerator; // Drag your Click Generator prefab here
    
    private float nextClickTime; // Time when the next click should occur
    
    GeigerClickGenerator clickGen;
    
    [Header("Health Reference")]
    [SerializeField] private Health playerHealth; // Reference to the Health script
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clickGen = clickGenerator.GetComponent<GeigerClickGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        float intensity = GetRadiationIntensity();
        print("Current radiation intensity: " + intensity);
        if (intensity > 0)
        {
            float currentDelay = Mathf.Lerp(maxClickInterval, minClickInterval, intensity);
            
            if (Time.time >= nextClickTime)
            {
                print("Radiation detected with intensity: " + intensity);
                clickGen.PlayClick(intensity);
                nextClickTime = Time.time + currentDelay;
                int radiationAmount = Mathf.CeilToInt(intensity * 10); // Scale intensity to a radiation amount
                playerHealth.TakeRadiation(radiationAmount);
            }
        }
        
    }

    float GetRadiationIntensity()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, radiationLayer);
        float highestIntensity = 0f;

        foreach (var hit in hits)
        {
            print ("Detected object: " + hit.name);
            if (hit.CompareTag("RadiationSource"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                float intensity = Mathf.Clamp01(1 - (distance / detectionRadius)); // Intensity decreases with distance
                if (intensity > highestIntensity)
                {
                    highestIntensity = intensity;
                }
            }
        }
        
        return highestIntensity;
    }
    
    void PlayClick(float intensity)
    {
        geigerAudioSource.pitch = Random.Range(0.9f, 1.1f) + (intensity * 0.5f);
        geigerAudioSource.PlayOneShot(geigerAudioSource.clip);
    }
}
