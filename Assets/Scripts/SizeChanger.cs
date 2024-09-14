using UnityEngine;

public class SizeChanger : MonoBehaviour
{
    private int _sizeDevider = 2;

    public void ReduceObjectSize(GameObject gameObject)
    {
        gameObject.transform.localScale = transform.localScale / _sizeDevider;
    }
}
