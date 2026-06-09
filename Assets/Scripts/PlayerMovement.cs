using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] float boundary = 8f;
    [SerializeField] float attackRange = 1f;

    bool isAttacking = false;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * input * speed * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -boundary, boundary);
        transform.position = pos;

        animator.SetBool("isRunning", input != 0);

        if (input < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (input > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
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
        Invoke(nameof(ResetAttack), 0.5f);
        AudioManager.Instance.PlayPlayerAttack();
    }

    void ResetAttack() => isAttacking = false;
}