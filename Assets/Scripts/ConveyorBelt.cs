using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    //[Range(0f, 2f)] private float _spawnMovablePartDelay;
    [SerializeField] private GameObject[] _fruitPrefabs;

    static public ConveyorBelt Instance;

    private void OnEnable()
    {
        if (Instance != null) return;
        Instance = this;
    }

    private void Start()
    {
        InvokeRepeating("Spawn", 0f, 1f);
    }

    public void Spawn()
    {
        int random = Random.Range(0, _fruitPrefabs.Length);
        Instantiate(_fruitPrefabs[random], _spawnPosition);
    }
}
