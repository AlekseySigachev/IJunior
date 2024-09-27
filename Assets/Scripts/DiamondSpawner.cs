using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    [SerializeField] private Diamond _diamond;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start() =>
        Spawn();

    private void Spawn()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
            Instantiate(_diamond, _spawnPoints[i].position, Quaternion.identity);
    }
}
