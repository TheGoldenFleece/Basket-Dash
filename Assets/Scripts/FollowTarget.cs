using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 _shift;
    private void Start()
    {
        _shift = transform.position - target.position;
    }
    private void Update()
    {

        transform.position = target.position + _shift;
    }
}
