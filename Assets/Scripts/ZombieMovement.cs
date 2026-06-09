using UnityEngine;

// makes the zombie walk towards finn and handles death
public class ZombieMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    Transform player;
    Animator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return; // stop if finn is gone > gameover

        // calculate direction from zombie to finn and move towards him
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);

        // flip the sprite based on which way the zombie is walking
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // walking right
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // walking left
        }
    }

    // called when finn hits this zombie
    public void Die()
    {
        animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false; // disable collider so it doesnt keep triggering
        enabled = false;  // stop this script so the zombie stops moving
        AudioManager.Instance.PlayZombieHarm();
        Destroy(gameObject, 0.8f); // wait for the death animation before removing the toast enemy
    }
}