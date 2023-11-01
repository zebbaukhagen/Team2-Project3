using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private List<AudioClip> listOfLevelMusic = new();
    [SerializeField] private AudioSource musicSource;
     
    private void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayLevelMusic(int indexOfSongToPlay)
    {
        musicSource.clip = listOfLevelMusic[indexOfSongToPlay];
        musicSource.Play();
    }
}
