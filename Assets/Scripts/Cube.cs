using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _chanceToSpawn = 1.0f;

    public float GetSpawnChance() => _chanceToSpawn;

    public void ReduceSpawnChance(float value)
    {
        _chanceToSpawn /= value;
    }
}
