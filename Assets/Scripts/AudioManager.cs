using UnityEngine;

// handles all audio in the game, both music and sound effects
// there's one audiomanager per scene; the startscreen and samplescene both have their own one
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource musicSource; // plays the background music on loop
    [SerializeField] AudioSource sfxSource;   // plays one-off sound effects

    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip zombieHarm;   // plays when the zombie dies
    [SerializeField] AudioClip uiSound;      // plays when a button is clicked
    [SerializeField] AudioClip playerAttack; // plays when finn attacks
    [SerializeField] AudioClip playerHit;    // plays when finn takes damage

    void Awake()
    {
        Instance = this; // making this accessible from other scripts
    }

    void Start()
    {
        // start the background music immediately when the scene loads
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    // each of these just plays the right sound - called from other scripts
    public void PlayZombieHarm() => sfxSource.PlayOneShot(zombieHarm);
    public void PlayUI() => sfxSource.PlayOneShot(uiSound);
    public void PlayPlayerAttack() => sfxSource.PlayOneShot(playerAttack);
    public void PlayPlayerHit() => sfxSource.PlayOneShot(playerHit);
}