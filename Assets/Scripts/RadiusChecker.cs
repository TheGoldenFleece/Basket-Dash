using UnityEngine;

public class RadiusChecker : MonoBehaviour
{
    public static RadiusChecker Instance;

    public float Radius { get => _radius; }
    [SerializeField][Range(0.1f, 5.0f)] private float _radius;
    [SerializeField] private Color _color;

    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;

        GetComponent<SphereCollider>().radius = Radius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
