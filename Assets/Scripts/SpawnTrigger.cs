using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private bool _isTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if(_isTriggered || !other.transform.CompareTag("MovablePart"))
        {
            return;
        }

        Debug.Log("Triggered");
        _isTriggered = true;
        ConveyorBelt.Instance.Spawn();
    }

    void OnTriggerExit(Collider coll)
    {
        if (!_isTriggered) return;

        Debug.Log("End trigger");
        _isTriggered = false;
    }
}
