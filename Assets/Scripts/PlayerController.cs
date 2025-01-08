using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public variables
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float jumpForce = 400f;
    public float gravity = -9.81f;
    
    // Private Variables
    Rigidbody rb;
    Vector3 moveDirection = Vector3.zero;
    bool isGrounded = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(0.0f, gravity, 0.0f);
        RaycastHit hitInfo;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hitInfo, 1.5f, LayerMask.GetMask("Ground"));
            
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection *= moveSpeed * Time.deltaTime;
        
        rb.transform.Translate(moveDirection);
        
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
