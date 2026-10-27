using UnityEngine;

public enum SoundEffect
{
    PlayerHitWall,
    PlayerOpenDoor,
    BuyItem,
    NoMoney,
    EnemyAppear
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Sound Effects")]
    public AudioClip hitWall;
    public AudioClip openDoor;
    public AudioClip buyItem;
    public AudioClip noMoney;
    public AudioClip enemyAppear;

    [Header("Music")]
    public AudioClip backgroundMusic;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        PlayMusic(backgroundMusic);
    }

    
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    
    public void PlaySound(SoundEffect sound)
    {
        switch (sound)
        {
            case SoundEffect.PlayerHitWall:
                PlaySound(hitWall);
                break;

            case SoundEffect.PlayerOpenDoor:
                PlaySound(openDoor);
                break;

            case SoundEffect.BuyItem:
                PlaySound(buyItem);
                break;

            case SoundEffect.NoMoney:
                PlaySound(noMoney);
                break;

            case SoundEffect.EnemyAppear:
                PlaySound(enemyAppear);
                break;
        }
    }

   
    public void PlayMusic(AudioClip music)
    {
        if (musicSource.clip == music) return;

        musicSource.Stop();
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}