using System.Runtime.CompilerServices;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    static public ConveyorBelt Instance;

    [SerializeField] private Transform _spawnPosition;

    [SerializeField] private Fruits _fruitsScriptableObject;
    private GameObject[] _fruitPrefabs;
    [SerializeField] private Vector2 _spawnRange;

    private void OnEnable()
    {
        if (Instance != null) return;
        Instance = this;
    }

    private void Awake()
    {
        _fruitPrefabs = _fruitsScriptableObject.fruits;
    }

    private void Start()
    {
        InvokeRepeating("Spawn", 0f, 1f);
    }

    public void Spawn()
    {
        int random = Random.Range(0, _fruitPrefabs.Length);

        float randomX = Random.Range(_spawnRange.x, _spawnRange.y);

        GameObject fruit = Instantiate(_fruitPrefabs[random], _spawnPosition);
        fruit.transform.localPosition = new Vector3(randomX, fruit.transform.localPosition.y, fruit.transform.localPosition.z); 
    }
}
