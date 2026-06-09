using UnityEngine;

// handles what happens when finn gets hit by a zombie
public class PlayerHealth : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<EnemySpawner>().EnemyKilled(); // count this as a kill for the wave
            GameManager.Instance.LoseLife();

            if (GameManager.Instance.GetLives() <= 0)
            {
                animator.SetTrigger("Death");
                GetComponent<PlayerMovement>().enabled = false; // stop finn from moving after death
                Invoke(nameof(ShowGameOver), 1f); // wait for the death animation before  gameover
            }
            else
            {
                animator.SetTrigger("Hurt");
                AudioManager.Instance.PlayPlayerHit();
            }
        }
    }

    void ShowGameOver()
    {
        GameManager.Instance.GameOver();
    }
}