using UnityEngine;

public class GeigerClickGenerator : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Call this function from your GeigerCounter script
    public void PlayClick(float intensity)
    {
        int samplerate = 44100;
        // A click is only 0.005 seconds long
        float[] samples = new float[(int)(samplerate * 0.005f)];

        for (int i = 0; i < samples.Length; i++)
        {
            // Create white noise
            samples[i] = Random.Range(-1f, 1f);
            // Apply a "Fade out" so it sounds like a sharp pop, not a buzz
            samples[i] *= 1f - ((float)i / samples.Length);
        }

        AudioClip clip = AudioClip.Create("Click", samples.Length, 1, samplerate, false);
        clip.SetData(samples, 0);

        audioSource.pitch = Random.Range(0.8f, 1.2f); // Variance makes it feel analog
        audioSource.PlayOneShot(clip, 0.5f + (intensity * 0.5f));
    }
}