using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Rigidbody playerRigidbody;

    [SerializeField] private Vector3 playerJump = new Vector3(0f, 5f, 0f);
    [SerializeField] private float playerHighJump = 5f;

    [SerializeField] private float moveSpeed = 5f;

    private bool isGrounded = false;
    private bool isDead = false;

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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isDead)
        {
            isGrounded = false;
            playerRigidbody.AddForce(new Vector3(0f, playerHighJump, 0f), ForceMode.Impulse);
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
                playerRigidbody.AddForce(playerJump, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if(collision.gameObject.CompareTag("Map"))
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