using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip zombieHarm;
    [SerializeField] AudioClip uiSound;
    [SerializeField] AudioClip playerAttack;
    [SerializeField] AudioClip playerHit;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayZombieHarm() => sfxSource.PlayOneShot(zombieHarm);
    public void PlayUI() => sfxSource.PlayOneShot(uiSound);
    public void PlayPlayerAttack() => sfxSource.PlayOneShot(playerAttack);
    public void PlayPlayerHit() => sfxSource.PlayOneShot(playerHit);
}