using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;
        public bool loop = false;
        
        [HideInInspector]
        public AudioSource source;
    }
    
    [Header("Sound Effects")]
    public Sound brickDestroy;
    public Sound playerCollision;
    public Sound levelComplete;
    public Sound gameOver;
    public Sound victory;
    
    [Header("Background Music")]
    public Sound backgroundMusic;
    
    private Dictionary<string, Sound> soundDictionary;
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    void InitializeAudio()
    {
        soundDictionary = new Dictionary<string, Sound>();
        
        // Configura todos os sons
        SetupSound("BrickDestroy", brickDestroy);
        SetupSound("PlayerCollision", playerCollision);
        SetupSound("LevelComplete", levelComplete);
        SetupSound("GameOver", gameOver);
        SetupSound("Victory", victory);
        SetupSound("BackgroundMusic", backgroundMusic);
        
        // Toca música de fundo
        Play("BackgroundMusic");
    }
    
    void SetupSound(string key, Sound sound)
    {
        if (sound.clip == null) return;
        
        // Cria AudioSource para este som
        sound.source = gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        
        soundDictionary[key] = sound;
    }
    
    public void Play(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            Sound sound = soundDictionary[soundName];
            if (sound.source != null)
            {
                sound.source.Play();
            }
        }
    }
    
    public void Stop(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            Sound sound = soundDictionary[soundName];
            if (sound.source != null && sound.source.isPlaying)
            {
                sound.source.Stop();
            }
        }
    }
    
    // Métodos específicos para cada tipo de som
    public void PlayBrickDestroy() => Play("BrickDestroy");
    public void PlayPlayerCollision() => Play("PlayerCollision");
    public void PlayLevelComplete() => Play("LevelComplete");
    public void PlayGameOver() => Play("GameOver");
    public void PlayVictory() => Play("Victory");
    
    public void StopBackgroundMusic() => Stop("BackgroundMusic");
    public void PlayBackgroundMusic() => Play("BackgroundMusic");
}