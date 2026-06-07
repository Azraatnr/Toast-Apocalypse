using UnityEngine;

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
        FindObjectOfType<EnemySpawner>().EnemyKilled();
        GameManager.Instance.LoseLife();

        if (GameManager.Instance.GetLives() <= 0)
        {
            animator.SetTrigger("Death");
            Invoke(nameof(ShowGameOver), 1f);
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }
}

    void ShowGameOver()
    {
        GameManager.Instance.GameOver();
    }
}