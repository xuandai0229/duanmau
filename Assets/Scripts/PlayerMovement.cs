using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 20f;
    [SerializeField] private float climbSpeed = 5f;

    [Header("Jump Settings")]
    [SerializeField] private float maxJumpHoldTime = 0.5f;
    [SerializeField] private float additionalJumpForce = 8f;
    [SerializeField] private float horizontalBoost = 2f;
    private int jumpCount = 0;
    private int maxJumps = 2; // Cho phép nhảy hai lần

    [Header("UI")]
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI coinText;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireCooldown = 0.5f;

    private Vector2 moveInput;
    private Rigidbody2D myRigidbody;
    private CapsuleCollider2D myCapsuleCollider;
    private Animator myAnimator;
    private Vector3 startPosition;
    private int lives = 3;
    private int coinsCollected = 0;

    private bool isJumping = false;
    private float jumpTime = 0f;
    private float originalGravityScale;
    private float lastFireTime;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        startPosition = transform.position;
        originalGravityScale = myRigidbody.gravityScale;

        UpdateUI();
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();

        if (isJumping && jumpTime < maxJumpHoldTime)
        {
            jumpTime += Time.deltaTime;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y + additionalJumpForce * Time.deltaTime);
        }

        if (Keyboard.current.qKey.wasPressedThisFrame && Time.time - lastFireTime >= fireCooldown)
        {
            FireBullet();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && jumpCount < maxJumps)
        {
            isJumping = true;
            jumpTime = 0f;
            myRigidbody.velocity = new Vector2(moveInput.x * horizontalBoost, jumpSpeed);
            jumpCount++;
        }
        else
        {
            isJumping = false;
        }
    }

    // Reset lại số lần nhảy khi chạm đất
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpCount = 0;
        }
    }

    private void FireBullet()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
        lastFireTime = Time.time;
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void ClimbLadder()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = originalGravityScale;
            return;
        }

        myRigidbody.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    public void Respawn()
    {
        lives--;
        if (lives > 0)
        {
            transform.position = startPosition;
            myRigidbody.velocity = Vector2.zero;
        }
        else
        {
            Debug.Log("Game Over!");
        }
        UpdateUI();
    }

    public void CollectCoin()
    {
        coinsCollected++;
        Debug.Log("Coins Collected: " + coinsCollected);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives.ToString();

        if (coinText != null)
            coinText.text = "Coins: " + coinsCollected.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heart"))
        {
            lives++;
            Destroy(collision.gameObject);
            UpdateUI();
            Debug.Log("Gained a life! Total lives: " + lives);
        }
    }
}
