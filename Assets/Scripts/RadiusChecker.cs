using UnityEngine;

public class RadiusChecker : MonoBehaviour
{
    [SerializeField][Range(0.1f, 5.0f)] private float _radius;
    [SerializeField] private Color _color;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
