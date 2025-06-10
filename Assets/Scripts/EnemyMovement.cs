using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed = 3f;
    public float jumpForce = 50f;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded = true;
    private bool isJumping = false;
    private Vector2 jumpDirection = Vector2.up;
    private float groundY = -3.5f;
    private bool canAct = true;

    private float rightLimit = 6.66f;
    private float leftLimit = -6.55f;

    public Transform playerTarget; // Referensi ke posisi Player

    private List<string> animationList = new List<string>();

    // Knockback system
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;
    private float knockbackTimer;
    private Vector2 knockbackDirection;
    private bool isKnockedBack = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        LoadAnimations();
        PlayRandomAnimation();
    }

    private void Update()
    {
        animator.SetBool("isGrounded", isGrounded);

        if (isKnockedBack)
        {
            // Apply knockback movement
            transform.position += (Vector3)(knockbackDirection * knockbackForce * Time.deltaTime);
            knockbackTimer -= Time.deltaTime;

            if (knockbackTimer <= 0)
            {
                isKnockedBack = false;
            }

            return; // Jangan gerakkan enemy saat knockback
        }

        if (!canAct || isJumping) return;

        // Batasi pergerakan enemy dan ubah arah ketika mencapai batas
        if (transform.position.x >= rightLimit)
        {
            enemySpeed = -Mathf.Abs(enemySpeed); // Berbalik ke kiri
        }
        else if (transform.position.x <= leftLimit)
        {
            enemySpeed = Mathf.Abs(enemySpeed); // Berbalik ke kanan
        }

        // Enemy movement (otomatis maju)
        rb.velocity = new Vector2(enemySpeed, rb.velocity.y);
        animator.SetBool("isMoving", true);

        // Pastikan musuh selalu menghadap ke player
        FlipSprite();

        // Lompat secara acak
        if (Random.Range(0f, 1f) < 0.01f && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= groundY)
        {
            isGrounded = true;
            isJumping = false;
            animator.SetBool("isGrounded", true);

            Vector3 pos = transform.position;
            pos.y = groundY;
            transform.position = pos;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        isJumping = true;
        animator.SetBool("isGrounded", false);
        animator.SetBool("isMoving", false);
    }

    private void LoadAnimations()
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in controller.animationClips)
        {
            animationList.Add(clip.name);
        }
    }

    private void PlayRandomAnimation()
    {
        if (animationList.Count > 0)
        {
            string randomAnimation = animationList[Random.Range(0, animationList.Count)];
            animator.Play(randomAnimation);
        }
    }

    private void FlipSprite()
    {
        if (playerTarget == null) return;

        Vector3 scale = transform.localScale;

        if (transform.position.x > playerTarget.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }

    public void GetKicked(Vector2 attackerPosition)
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;

        // Hit dari kanan = dorong ke kanan (karena player di kiri)
        knockbackDirection = transform.position.x < attackerPosition.x ? Vector2.left : Vector2.right;

        //animator.SetTrigger("enemy hurt"); // Optional: tambahkan animasi Hit
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("tendangan"))
        {
            Vector2 attackerPos = other.transform.position;
            GetKicked(attackerPos);
        }
    }
}
