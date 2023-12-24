using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;

    [SerializeField] private float movementSpeed = 0.5f;
    [SerializeField] private float movementSpeedMax = 0.5f;
    [SerializeField] private float rotatingSpeed = 0.5f;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * movementSpeed * Time.deltaTime;
        movement = transform.TransformVector(movement);
        rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity + movement, movementSpeedMax);
        
        transform.Rotate(new Vector3(0, 0, rotatingSpeed * -Input.GetAxis("Rotating") * Time.deltaTime));

        float animatorMovement = Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"));
        animator.SetInteger("Movement", (int)animatorMovement);
    }
}
