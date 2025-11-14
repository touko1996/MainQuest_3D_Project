using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;

        // 이동
        if (move.magnitude > 0.1f)
        {
            Vector3 moveDir = move * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + moveDir);

            transform.forward = move;
        }

        // velocity 대신 입력 기반 Speed 사용
        float speed = move.magnitude * moveSpeed;
        anim.SetFloat("Speed", speed);

        // groundCheck
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        anim.SetBool("IsGrounded", isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }


    private void Jump()
    {
        // y 방향 속도 초기화 후 점프
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        anim.SetTrigger("Jump");
    }
}
