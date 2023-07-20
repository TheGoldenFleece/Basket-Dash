using System.Collections;
using UnityEngine;

public class PlaceToThow : MonoBehaviour
{
    private bool _isTriggered;
    private string _triggerTag = "Fruit";
    [SerializeField] private Transform _placeToLocate;
    [SerializeField] private GameObject _collectedMessageCanvas;
    [SerializeField] private Animator _animator;
    private Transform _fruit;
    private Transform[] _cells;
    public static int CellIndex { private set; get; }

    private void Awake()
    {
        _collectedMessageCanvas.SetActive(false);
        _cells = new Transform[_placeToLocate.childCount];
        CellIndex = 0;

        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i] = _placeToLocate.GetChild(i);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isTriggered) return;

        if (!other.CompareTag(_triggerTag)) return;

        Debug.Log("Triggered");

        _isTriggered = true;

        _fruit = other.transform;
        _fruit.SetParent(_placeToLocate);

        _fruit.position = _cells[CellIndex].position;
        CellIndex++;

        if (CellIndex == _cells.Length)
        {
            Debug.Log("Max cells");
        }

        GameManager.Instance.AddFruit(_fruit.GetComponent<FruitController>().Fruit);

        StartCoroutine(DisplayCollectedMessage());
    }

    IEnumerator DisplayCollectedMessage()
    {
        _collectedMessageCanvas.SetActive(true);

        yield return new WaitUntil(() => AnimationChecker.Instance.IsAnimationOver(_animator, "DisplayText"));

        _collectedMessageCanvas.SetActive(false);

        yield break;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isTriggered) return;

        if (!other.CompareTag(_triggerTag)) return;

        _isTriggered = false;
    }
}
