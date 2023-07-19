using UnityEngine;

public class MovablePart : MonoBehaviour
{
    [SerializeField][Range(0.01f, 2f)]
    private float _speed;
    [SerializeField]
    private MeshCollider _meshCollider;
    private Rigidbody _rigidbody;
    [SerializeField] private float _force = 2;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 rigidbodyPosition = _rigidbody.position;
        _rigidbody.position = _rigidbody.position + Vector3.left * _force * Time.fixedDeltaTime;
        _rigidbody.MovePosition(rigidbodyPosition);
    }
}
