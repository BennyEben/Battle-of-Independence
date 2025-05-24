using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    public Joystick movementJoystick;
    public float playerSpeed = 5f;
    public float jumpForce = 75f;
    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded = true;
    private bool jumpPressed = false;
    private Vector2 jumpDirection = Vector2.zero;

    private float groundY = -3.5f;

    private bool isDucking = false;
    private bool isJumping = false;
    private bool canAct = true;

    public float actionCooldown = 0.5f; // jeda antar aksi
    public Transform enemyTarget; // Referensi ke musuh


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDucking", isDucking);

        if (!canAct || isJumping) return;

        Vector2 direction = movementJoystick.Direction;

        // Pastikan player selalu menghadap musuh
        FlipSprite();

        // 🔽 Ducking
        if (direction.magnitude > 0.5f && direction.y < -0.5f && Mathf.Abs(direction.x) < 0.4f)
        {
            if (!isDucking)
            {
                isDucking = true;
                StartCoroutine(ActionCooldown());
            }
            return;
        }
        else if (isDucking && direction.y < -0.5f)
        {
            return;
        }
        else
        {
            isDucking = false;
        }

        if (!isGrounded) return;

        if (direction.magnitude > 0.5f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360f;

            if (angle >= 22.5f && angle < 67.5f)
            {
                jumpDirection = new Vector2(1, 1).normalized;
                jumpPressed = true;
            }
            else if (angle >= 67.5f && angle < 112.5f)
            {
                jumpDirection = Vector2.up;
                jumpPressed = true;
            }
            else if (angle >= 112.5f && angle < 157.5f)
            {
                jumpDirection = new Vector2(-1, 1).normalized;
                jumpPressed = true;
            }
            else if (direction.y < -0.5f && direction.x > 0.5f)
            {
                jumpPressed = false;
            }
            else if (direction.y < -0.5f && direction.x < -0.5f)
            {
                jumpPressed = false;
            }

            if (jumpPressed)
            {
                StartCoroutine(ActionCooldown());
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = movementJoystick.Direction;

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

        if (!isGrounded) return;

        if (isDucking)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
            return;
        }

        bool isMoving = Mathf.Abs(direction.x) > 0.1f;
        animator.SetBool("isMoving", isMoving);

        if (jumpPressed)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            isJumping = true;
            animator.SetBool("isGrounded", false);
            animator.SetBool("isMoving", false);
            jumpPressed = false;
            return;
        }

        Vector2 horizontalMove = new Vector2(direction.x * playerSpeed, rb.velocity.y);
        rb.velocity = horizontalMove;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -9f, 9f);
        transform.position = clampedPosition;
    }

    private IEnumerator ActionCooldown()
    {
        canAct = false;
        yield return new WaitForSeconds(actionCooldown);
        canAct = true;
    }

    private void FlipSprite()
    {
        if (enemyTarget == null) return; // Pastikan musuh tersedia

        Vector3 scale = transform.localScale;

        // Player selalu menghadap ke musuh
        scale.x = (transform.position.x < enemyTarget.position.x) ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }


    //  Fungsi ini dipanggil dari Button A
    public void Punch()
    {
        if (!canAct || !isGrounded || isJumping || isDucking) return;

        Vector2 direction = movementJoystick.Direction;
        if (Mathf.Abs(direction.x) > 0.1f) return; // Sedang jalan

        animator.SetTrigger("Punch");
        StartCoroutine(ActionCooldown());
    }
    // Fungsi ini dipanggil dari Button B
    public void Kick()
    {
        if (!canAct || !isGrounded || isJumping || isDucking) return;

        Vector2 direction = movementJoystick.Direction;
        if (Mathf.Abs(direction.x) > 0.1f) return; // Sedang jalan

        animator.SetTrigger("Kick");
        StartCoroutine(ActionCooldown());
    }

}
