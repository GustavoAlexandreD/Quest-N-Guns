using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Configurações")]
    [Range(0f, 1f)]
    public float defaultVolume = 1f;

    private AudioSource sourceA;
    private AudioSource sourceB;
    private bool isUsingA = true;

    private const string PREF_KEY = "music_volume";

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeSources();
        LoadVolume();
    }

    private void InitializeSources()
    {
        sourceA = gameObject.AddComponent<AudioSource>();
        sourceB = gameObject.AddComponent<AudioSource>();

        ConfigureSource(sourceA);
        ConfigureSource(sourceB);
    }

    private void ConfigureSource(AudioSource src)
    {
        src.loop = true;
        src.playOnAwake = false;
        src.spatialBlend = 0f;
        src.volume = 0f;
    }

    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(PREF_KEY, defaultVolume);
        SetVolume(savedVolume);
    }

    private void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat(PREF_KEY, value);
        PlayerPrefs.Save();
    }

    public void SetVolume(float value)
    {
        sourceA.volume = value;
        sourceB.volume = value;
        SaveVolume(value);
    }

    public float GetVolume()
    {
        return isUsingA ? sourceA.volume : sourceB.volume;
    }

    // --- API PRINCIPAL ---

    /// <summary>
    /// Troca a música instantaneamente (sem fade).
    /// </summary>
    public void SetMusic(AudioClip newClip)
    {
        GetCurrentSource().clip = newClip;
        GetCurrentSource().Play();
        GetOtherSource().Stop();
    }

    /// <summary>
    /// Troca de música com crossfade suave.
    /// </summary>
    public void SetMusicCrossfade(AudioClip newClip, float fadeDuration = 1f)
    {
        StartCoroutine(CrossfadeRoutine(newClip, fadeDuration));
    }

    private IEnumerator CrossfadeRoutine(AudioClip newClip, float fadeTime)
    {
        AudioSource current = GetCurrentSource();
        AudioSource next = GetOtherSource();

        next.clip = newClip;
        next.volume = 0f;
        next.Play();

        float startVolume = GetVolume();
        float time = 0f;

        while (time < fadeTime)
        {
            float t = time / fadeTime;

            current.volume = Mathf.Lerp(startVolume, 0f, t);
            next.volume = Mathf.Lerp(0f, startVolume, t);

            time += Time.deltaTime;
            yield return null;
        }

        current.volume = 0f;
        next.volume = startVolume;

        current.Stop();
        isUsingA = !isUsingA; // Alterna o canal ativo
    }

    private AudioSource GetCurrentSource()
    {
        return isUsingA ? sourceA : sourceB;
    }

    private AudioSource GetOtherSource()
    {
        return isUsingA ? sourceB : sourceA;
    }
}
