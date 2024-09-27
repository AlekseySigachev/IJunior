using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChecker : MonoBehaviour
{
    [SerializeField] protected LayerMask _layer;
    [SerializeField] protected bool _IsTouchingLayer;

    private Collider2D _collider;

    public bool IsTouchingLayer => _IsTouchingLayer;

    private void Awake() =>
        _collider = GetComponent<Collider2D>();

    private void OnTriggerStay2D(Collider2D collision) =>
        _IsTouchingLayer = _collider.IsTouchingLayers(_layer);

    private void OnTriggerExit2D(Collider2D collision) =>
        _IsTouchingLayer = _collider.IsTouchingLayers(_layer);
}
