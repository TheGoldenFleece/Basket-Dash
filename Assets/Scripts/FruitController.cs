using UnityEngine;

public class FruitController : MonoBehaviour
{
    private Transform _binding;
    private bool _isTriggered;
    private string _triggerTag = "Player";
    private Rigidbody _rigidbody;
    public bool IsGrabbed { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isTriggered) return;

        if (!other.CompareTag(_triggerTag)) return;
        _isTriggered = true;

        _rigidbody.isKinematic = true;

        _binding = other.transform.parent;
        transform.SetParent(_binding);

        transform.position = other.transform.position;
        IsGrabbed = true;
    }
}
