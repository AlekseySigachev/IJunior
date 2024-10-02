using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int _healPower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out HealthComponent healthComponent))
        {
            healthComponent.ModifyHealth(_healPower);
            Destroy(gameObject);
        }
    }
}
