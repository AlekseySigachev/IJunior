using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public event Action DamageDealed;

    private int _health;

    private void Awake() =>
        _health = _maxHealth;

    public void ModifyHealth(int value)
    {
        if (_health <= 0) 
            return;

        _health += value;

        if (value < 0)
            DamageDealed?.Invoke();
    }
}
