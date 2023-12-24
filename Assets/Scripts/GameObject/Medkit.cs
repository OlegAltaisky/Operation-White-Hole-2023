using UnityEngine;

public class Medkit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<HealthPoints>(out HealthPoints healthPoints))
        {
            healthPoints.Heal();
            Destroy(this.gameObject);
        }
    }
}
