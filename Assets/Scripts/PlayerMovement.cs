using UnityEngine;

// controls finn's movement and attacks
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] float boundary = 8f;   // how far left/right finn can go before hitting the edge
    [SerializeField] float attackRange = 1f; // radius of the attack hitbox around finn

    bool isAttacking = false; // prevents spamming the attack button
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal"); // -1 = left, 0 = nothing, 1 = right
        transform.Translate(Vector2.right * input * speed * Time.deltaTime);

        // clamp position so finn can't walk off screen
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -boundary, boundary);
        transform.position = pos;

        // switch between idle and run animation
        animator.SetBool("isRunning", input != 0);

        // flip finn's sprite based on direction
        if (input < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // facing left
        }
        else if (input > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // facing right
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        AudioManager.Instance.PlayPlayerAttack();

        // check for enemies in a circle around finn
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<ZombieMovement>().Die();
                GameManager.Instance.AddScore(10);
                FindObjectOfType<EnemySpawner>().EnemyKilled();
            }
        }

        Invoke(nameof(ResetAttack), 0.5f); // allow attacking again after 0.5 seconds
    }

    void ResetAttack() => isAttacking = false;
}