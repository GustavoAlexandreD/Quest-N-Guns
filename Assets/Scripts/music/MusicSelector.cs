using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicLoader : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    private void Start()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "MenuQuest'nGuns")
            MusicManager.Instance.SetMusicCrossfade(menuMusic);
        else if (scene == "SampleScene")
            MusicManager.Instance.SetMusicCrossfade(gameMusic);
    }
}