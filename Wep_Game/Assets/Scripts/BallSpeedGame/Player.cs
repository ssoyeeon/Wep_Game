using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Rigidbody playerRigidbody;

    [SerializeField] private Vector3 playerJump = new Vector3(0f, 5f, 0f);

    [Header("수동 점프 세팅")]
    [SerializeField] private float manualJumpUpSpeed = 10f; // 빠르게 확! 올라가는 속도
    [SerializeField] private float manualFallGravity = 20f; // 떨어질 때 확! 끌어내리는 추가 중력 

    [SerializeField] private float moveSpeed = 5f;

    private bool isGrounded = false;
    private bool isDead = false;

    private bool isManualJumping = false;

    public GameObject OverCanvas;

    private void Awake()
    {
        OverCanvas.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(DefaultJump());
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        playerRigidbody.linearVelocity = new Vector3(horizontalInput * moveSpeed, playerRigidbody.linearVelocity.y, 0f);

        if (Input.GetKeyDown(KeyCode.Space) && !isDead)
        {
            isGrounded = false;
            isManualJumping = true; // 수동 점프 발동!

            playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, manualJumpUpSpeed, 0f);
        }

        if (isManualJumping && playerRigidbody.linearVelocity.y < 0)
        {
            playerRigidbody.linearVelocity += Vector3.down * manualFallGravity * Time.deltaTime;
        }
    }

    public IEnumerator DefaultJump()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (isGrounded && !isDead)
            {
                isGrounded = false;
                isManualJumping = false; // 자동 점프는 '빠른 낙하'를 적용하지 않음
                playerRigidbody.AddForce(playerJump, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isManualJumping = false; // 바닥에 닿으면 수동 점프 상태 해제
        }
        else if (collision.gameObject.CompareTag("Map"))
        {
            Debug.Log("쥬금 ㅠ");
            isDead = true;
            OverCanvas.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}