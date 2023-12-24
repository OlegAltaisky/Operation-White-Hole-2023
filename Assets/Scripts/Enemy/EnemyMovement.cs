using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private EnemyShooter enemyShooter;
    
    [SerializeField] private Transform player;

    [SerializeField] private bool active;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float rotationModifier = 90.0f;

    [SerializeField] private float shootingDistance = 10.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask testMask;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyShooter = GetComponent<EnemyShooter>();
    }

    private void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, direction, shootingDistance * 5, testMask);
        Debug.Log(raycastHit.transform.name);

        if(raycastHit.transform == player.transform)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * rotationSpeed);
            if(Physics2D.Raycast(transform.position, direction, shootingDistance, layerMask))
            {
                enemyShooter.Shoot();
                rigidBody.velocity = Vector2.zero;
                animator.SetInteger("Movement", 0);
            }
            else
            {
                rigidBody.velocity = transform.up * speed;
                animator.SetInteger("Movement", 1);
            }
        }
    }

    public void DestroyShip()
    {
        Destroy(rigidBody);
        Destroy(enemyShooter);
        Destroy(this);
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, player.transform.position - transform.position);
    }
    */
}