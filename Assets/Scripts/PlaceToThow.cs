using System.Collections;
using UnityEngine;

public class PlaceToThow : MonoBehaviour
{
    private bool _isTriggered;
    private string _triggerTag = "Fruit";
    [SerializeField] private Transform _placeToLocate;
    private Transform _fruit;

    private void OnTriggerEnter(Collider other)
    {
        if (_isTriggered) return;

        if (!other.CompareTag(_triggerTag)) return;

        Debug.Log("Triggered");

        _isTriggered = true;

        _fruit = other.transform;
        _fruit.SetParent(_placeToLocate);
        _fruit.localPosition = Vector3.zero;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!_isTriggered) return;

        if (!other.CompareTag(_triggerTag)) return;

        _isTriggered = false;
    }
}
