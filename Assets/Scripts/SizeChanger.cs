using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    private int _sizeDevider = 2;

    public void ReduceObjectSize(Cube cube)
    {
        cube.transform.localScale = transform.localScale / _sizeDevider;
    }
}
