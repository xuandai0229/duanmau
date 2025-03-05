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
    private int maxJumps = 2;
    private bool isJumping = false;
    private float jumpTime = 0f;

    [Header("UI")]
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI coinText;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireCooldown = 0.5f;
    private float lastFireTime;

    private Vector2 moveInput;
    private Rigidbody2D myRigidbody;
    private CapsuleCollider2D myCapsuleCollider;
    private Animator myAnimator;
    private Vector3 checkpointPosition;
    private int lives = 3;
    private int coinsCollected = 0;
    private float originalGravityScale;
    private GameManager gameManager;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
        checkpointPosition = transform.position;
        originalGravityScale = myRigidbody.gravityScale;

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
            Debug.LogError("❌ Không tìm thấy GameManager!");

        UpdateUI();
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
        HandleJump();

        if (Keyboard.current.qKey.wasPressedThisFrame && Time.time - lastFireTime >= fireCooldown)
        {
            FireBullet();
        }
    }

    private void HandleJump()
    {
        if (isJumping && jumpTime < maxJumpHoldTime)
        {
            jumpTime += Time.deltaTime;
            myRigidbody.velocity += new Vector2(0, additionalJumpForce * Time.deltaTime);
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
        myAnimator.SetBool("isRunning", Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
    }

    private void FlipSprite()
    {
        if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
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
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myAnimator.SetBool("isClimbing", Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon);
    }

    public void Respawn()
    {
        lives--;
        if (lives > 0)
        {
            transform.position = checkpointPosition;
            myRigidbody.velocity = Vector2.zero;
        }
        else
        {
            if (gameManager != null)
                gameManager.GameOver(false);
        }
        UpdateUI();
    }

    public void CollectCoin()
    {
        if (gameManager != null)
        {
            coinsCollected++;
            gameManager.AddCoin(1);
            UpdateUI();
        }
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
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            UpdateCheckpoint(collision.transform.position);
        }
        
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }
}
