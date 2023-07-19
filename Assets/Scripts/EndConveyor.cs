using System.Collections;
using UnityEngine;

public class EndConveyor : MonoBehaviour
{
    [SerializeField]
    private string _tagToDestroy;
    [SerializeField]
    private float _destroyDelay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("MovablePart"))
        {
            return;
        }

        Destroy(other.gameObject, _destroyDelay);
    }
}
