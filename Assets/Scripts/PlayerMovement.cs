using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputSystem_Actions actions;
    public float speed;
    public float jumpForce;
    public Transform groundCheckTransform;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public float stepDelay = 0.35f;
    public float footstepVolume = 0.5f;
    public float jumpVolume = 0.5f;
    public float coinVolume = 0.5f;
    float stepTimer;
    AudioManager audioManager;
    ScoreSystem score;
    private Animator anim;
    bool isGrounded;
    float move;
    Rigidbody2D rb;

    void Awake()
    {
        actions = new InputSystem_Actions();
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += Movement;
        actions.Player.Jump.performed += Jumping;

        actions.Player.Move.canceled += Movement;
        actions.Player.Jump.canceled += Jumping;
    }

    void OnDisable()
    {
        actions.Player.Enable();

        actions.Player.Move.performed -= Movement;
        actions.Player.Jump.performed -= Jumping;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            move = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            audioManager.PlaySFX(audioManager.coin, coinVolume);
        }

    }

    void Movement(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>().x;
    }

    void Jumping(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (isGrounded)
            {
                rb.linearVelocityY = jumpForce;
                audioManager.PlaySFX(audioManager.jump, jumpVolume);
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);

        if (move > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (move < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }

        anim.SetBool("Run", move != 0);

        if (move != 0 && isGrounded)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                audioManager.PlaySFX(audioManager.tap, footstepVolume);
                stepTimer = stepDelay;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }
}