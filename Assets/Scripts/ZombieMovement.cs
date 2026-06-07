using UnityEngine;

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
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Die()
    {
        animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
        Destroy(gameObject, 0.8f);
    }
}