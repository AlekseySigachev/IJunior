using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<Cube> CubeFounded;
    private Camera _camera;
    private int _indexLeftMouseButton = 0;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_indexLeftMouseButton))
        {
            IdentifyExplosiveCube();
        }
    }

    private void IdentifyExplosiveCube()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out Cube explosiveCube))
            {
                CubeFounded?.Invoke(explosiveCube);
                Destroy(explosiveCube.gameObject);
            }
        }
    }
}
