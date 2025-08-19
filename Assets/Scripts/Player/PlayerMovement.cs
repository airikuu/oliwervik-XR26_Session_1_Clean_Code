using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float rotationSpeed = 0.5f; // For mouse rotation

    private Rigidbody rb;
    private bool isGrounded;
    private float yaw; // For mouse rotation

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Player needs a Rigidbody component!");
        }
        // Freeze rotation to prevent physics flipping the player
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to screen center
    }

    void Update()
    {
        // Handle rotation with mouse
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        yaw += mouseX;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        // Jumping Logic (Monolithic, handles animation state and physics)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Debug.Log("Player jumped!");
        }
    }

    void FixedUpdate()
    {
        // Handle movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward * v + transform.right * h;
        Vector3 velocity = direction.normalized * moveSpeed;

        Vector3 newPos = rb.position + velocity * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

