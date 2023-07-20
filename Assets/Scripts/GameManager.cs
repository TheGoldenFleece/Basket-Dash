using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Fruits _fruitsScriptableObject;
    [SerializeField] private GameObject _completeLevelUI;
    [SerializeField] private GameObject _gameOverUI;

    [SerializeField] private GameObject _ui;
    [SerializeField] private TextMeshProUGUI _tastText;

    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _conveyorAnimator;
    [SerializeField] private Animator _cameraAnimator;

    [SerializeField] private RigBuilder _rigBuilder;

    private string _danceTrigger = "Dance";
    private string _disappearTrigger = "Disappear";
    private string _cameraTransitionTrigger = "Transition";

    private GameObject[] _fruits;

    public static Task FruitTask { get; private set; }
    public static int PickedAmount { private set; get; }

    private void OnEnable()
    {
        if (Instance != null) return;

        Instance = this;
    }
    private void Awake()
    {
        PickedAmount = 0;
        _fruits = _fruitsScriptableObject.fruits;
        _completeLevelUI.SetActive(false);
        _gameOverUI.SetActive(false);
    }

    private void Start()
    {
        FruitTask = new Task(_fruits);
        _ui.SetActive(true);

        DisplayTask();
    }

    private void DisplayTask()
    {
        int remained = FruitTask.Count - PickedAmount;
        string text = $"Collect {remained} ";

        if (remained == 1)
        {
            _tastText.text = text + FruitTask.Fruit;
            return;
        }

        string fruit = FruitTask.Fruit.ToString();
        char ending = fruit[fruit.Length - 1];

        switch (ending)
        {
            case 's':
                {
                    text += fruit;
                    break;
                }
            case 'y':
                {
                    text += fruit.Remove(fruit.Length - 1) + "ies"; ;
                    break;
                }
            default:
                {
                    text += fruit + "s";
                    break;
                }
        }

        _tastText.text = text;
    }

    public void AddFruit(Fruit fruit)
    {
        if (FruitTask.Fruit == fruit)
        {
            PickedAmount++;
            DisplayTask();
        }
        
        if (PickedAmount == FruitTask.Count)
        {
            CompleteLevel();
            return;
        }

        if (PlaceToThow.CellIndex == 5)
        {
            GameOver();
            return;
        }
    }

    private void GameOver()
    {
        _gameOverUI.SetActive(true);
        _ui.SetActive(false);
        this.enabled = false;
    }

    private void CompleteLevel()
    {
        _completeLevelUI.SetActive(true);
        _ui.SetActive(false);
        _animator.SetTrigger(_danceTrigger);
        _conveyorAnimator.SetTrigger(_disappearTrigger);
        _rigBuilder.enabled = false;

        _cameraAnimator.SetTrigger(_cameraTransitionTrigger);

        this.enabled = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public class Task {
    public Fruit Fruit { private set; get; }
    public int Count { private set; get; }

    public Task(GameObject[] fruits)
    {
        int randomName = Random.Range(0, fruits.Length);
        int randomCount = Random.Range(1, 6);

        Fruit = fruits[randomName].GetComponent<FruitController>().Fruit;
        Count = randomCount;
    }

}

