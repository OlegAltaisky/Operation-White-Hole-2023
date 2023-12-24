using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammo = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerShooter>(out PlayerShooter playerShooter))
        {
            playerShooter.GiveAmmo(ammo);
            Destroy(this.gameObject);
        }
    }
}
