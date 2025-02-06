using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;  // Tăng giá trị này để nhảy cao hơn
    [SerializeField] private float climbSpeed = 5f;

    [Header("Jump Settings")]
    [SerializeField] private float maxJumpHoldTime = 0.5f; // Thời gian tối đa giữ phím nhảy
    [SerializeField] private float additionalJumpForce = 8f; // Tăng lực nhảy bổ sung để nhảy cao hơn
    [SerializeField] private float horizontalBoost = 2f; // Tăng tốc ngang khi nhảy

    [Header("UI")]
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI coinText;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab; // Prefab đạn
    [SerializeField] private Transform firePoint;     // Điểm xuất phát của đạn
    [SerializeField] private float fireCooldown = 0.5f; // Thời gian chờ giữa các lần bắn

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

    private float lastFireTime; // Thời gian bắn gần nhất

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

        // Thêm lực nhảy khi giữ phím cách
        if (isJumping && jumpTime < maxJumpHoldTime)
        {
            jumpTime += Time.deltaTime;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y + additionalJumpForce * Time.deltaTime);
        }

        // Kiểm tra phím bắn
        if (Keyboard.current.qKey.wasPressedThisFrame && Time.time - lastFireTime >= fireCooldown)
        {
            FireBullet(); // Bắn đạn khi nhấn Q
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed)
        {
            isJumping = true;
            jumpTime = 0f;

            // Thêm vận tốc ngang và lực nhảy ban đầu
            myRigidbody.velocity = new Vector2(moveInput.x * horizontalBoost, jumpSpeed);
        }
        else
        {
            isJumping = false;
        }
    }

    private void FireBullet()
    {
        // Xác định hướng bắn dựa vào hướng nhân vật
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Tạo viên đạn
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);

        // Ghi nhận thời gian bắn
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
            myRigidbody.gravityScale = originalGravityScale; // Khôi phục trọng lực
            return;
        }

        myRigidbody.gravityScale = 0f; // Vô hiệu hóa trọng lực khi leo thang

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
            // Thêm logic game over nếu cần
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
        livesText.text = "Lives: " + lives.ToString();
        coinText.text = "Coins: " + coinsCollected.ToString();
    }
}
