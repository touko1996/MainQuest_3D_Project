using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 6f;          
    public float rotationSpeed = 10f;     

    [Header("점프 설정")]
    public float jumpHeight = 1.5f;      
    public float gravity = -9.81f;        //유니티 기본 중력값

    [Header("바닥 체크")]
    public Transform groundCheck;         
    public float groundDistance = 0.3f;   
    public LayerMask groundMask;         

    private CharacterController controller;
    private Animator anim;
    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 바닥체크 - 레이캐스트로
        isGrounded = Physics.Raycast(
            groundCheck.position,
            Vector3.down,
            groundDistance,
            groundMask
        );

        anim.SetBool("IsGrounded", isGrounded);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; // 바닥에 붙어 있게 약한 힘으로 눌러줌. 유니티 공식적인 해결책
        }

        // 플레이어 이동처리
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, 0f, z).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            controller.Move(moveDir * moveSpeed * Time.deltaTime);

            // 회전- 보간을 통해 부드러운 회전 적용
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }

        anim.SetFloat("Speed", moveDir.magnitude);

        // 플레이어 점프
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //점프공식
            anim.SetTrigger("Jump");
        }

        // 캐릭터컨트롤러는 중력설정이 안되니 직접 업데이트에서 중력적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // 바닥 체크 기즈모
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            groundCheck.position,
            groundCheck.position + Vector3.down * groundDistance
        );
    }
}
