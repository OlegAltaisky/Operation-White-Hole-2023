using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private int damage = 1;
    
    private void Update()
    {
        transform.Translate(new Vector2(0, speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<HealthPoints>(out HealthPoints healthPoints))
        {
            healthPoints.TakeDamage(damage);
        }
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 5.0f);
        Destroy(this.gameObject);
    }
}