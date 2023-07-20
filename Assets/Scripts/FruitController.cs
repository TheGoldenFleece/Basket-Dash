using Unity.VisualScripting;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    private Transform _binding;
    private bool _isTriggered;
    private string _triggerTag = "Player";
    private string _radiusCheckerTag = "RadiusChecker";
    private Rigidbody _rigidbody;
    [SerializeField] private Fruit _fruit;

    private Material _defaultMaterial;
    [SerializeField] private Material _onMouseEnterMaterial;
    [SerializeField] private Material _enablePickingMaterial;
    private Renderer _renderer;

    public Fruit Fruit { get => _fruit; }
    public bool IsGrabbed { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defaultMaterial = _renderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_radiusCheckerTag))
        {
            _renderer.material = _enablePickingMaterial;
        }

        if (_isTriggered) return;

        if (!other.CompareTag(_triggerTag)) return;
        _isTriggered = true;

        _rigidbody.isKinematic = true;

        _binding = other.transform.parent;
        transform.SetParent(_binding);

        transform.position = other.transform.position;
        IsGrabbed = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_radiusCheckerTag))
        {
            _renderer.material = _defaultMaterial;
        }
    }

    private void OnMouseEnter()
    {

        _renderer.material = _enablePickingMaterial;
    }

    private void OnMouseExit()
    {
        _renderer.material = _defaultMaterial;

    }
}
