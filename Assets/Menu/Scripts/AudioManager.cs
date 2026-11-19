
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Configuración de audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip musicaGeneral;

    private void Awake()
    {
        // Evitar crear varios AudioManagers
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Mantener el objeto entre escenas
        DontDestroyOnLoad(gameObject);

        // Configurar reproducción
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.clip = musicaGeneral;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        audioSource.Play();
    }

    public void CambiarVolumen(float volumen)
    {
        audioSource.volume = Mathf.Clamp01(volumen);
    }

    public void PausarMusica()
    {
        audioSource.Pause();
    }

    public void ReanudarMusica()
    {
        audioSource.UnPause();
    }

    public void DetenerMusica()
    {
        audioSource.Stop();
    }
}

