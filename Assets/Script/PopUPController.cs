using UnityEngine;

public class PopUPController: MonoBehaviour
{
    public static PopUPController instance;

    public AudioClip spinSound;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpinSound()
    {
        audioSource.PlayOneShot(spinSound);
    }
    public void PauseSpinSound()
    {
        audioSource.Pause();
    }
}
