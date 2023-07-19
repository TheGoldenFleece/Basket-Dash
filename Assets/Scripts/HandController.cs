using System;
using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandController : MonoBehaviour
{
    [SerializeField] private Transform _targetToSet;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _rightHandTarget;
    [SerializeField] private float _speed = 1;

    private bool IsGrabbingReady { get; set; }

    private Vector3 _default;
    private string _putFruitTrigger = "PutFruit";

    [SerializeField] private Transform _radiusChecker;

    private void Awake()
    {
        _targetToSet = null;
        _default = _rightHandTarget.position;
        IsGrabbingReady = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsGrabbingReady) {
            Fire();
        }
    }

    private void Fire(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Fruit") && IsTargetInRadius(hit.transform))
            {
                _targetToSet = hit.transform;
                StartCoroutine(GrabCoroutine());
            }
        }
    }

    private bool IsTargetInRadius(Transform target)
    {
        if (Vector3.Distance(target.position, _radiusChecker.position) > .2f) return false;

        return true;
    }
    
    IEnumerator GrabCoroutine()
    {
        IsGrabbingReady = false;

        yield return StartCoroutine(GrabTarget());
        yield return StartCoroutine(BackHand());

        if (_targetToSet.GetComponent<FruitController>().IsGrabbed)
        {
            yield return StartCoroutine(PutFruitInBasket());
        }

        IsGrabbingReady = true;

        yield break;
    }

    IEnumerator GrabTarget()
    {
        FruitController fc = _targetToSet.GetComponent<FruitController>();

        while (!fc.IsGrabbed && IsTargetInRadius(_targetToSet))
        {
            _rightHandTarget.position = Vector3.Lerp(_rightHandTarget.position, _targetToSet.position, Time.deltaTime * _speed);

            yield return null;
        }

        yield break;
    }

    IEnumerator BackHand()
    {
        float timer = 0;
        float timeToGrab = 1 / _speed;

        while (timer < timeToGrab)
        {
            _rightHandTarget.position = Vector3.Lerp(_targetToSet.position, _default, timer / timeToGrab);
            timer += Time.deltaTime;

            yield return null;
        }

        yield break;
    }

    IEnumerator PutFruitInBasket()
    {
        _animator.SetTrigger(_putFruitTrigger);
        yield return new WaitUntil(() => IsAnimationOver(_animator, "PutInTheBasket"));

        yield break;
    }

    public bool IsAnimationOver(Animator animator, string clipName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        try
        {
            // Перевірка, чи кліп закінчився
            if (stateInfo.IsName(clipName) && stateInfo.normalizedTime >= 1.0f)
                return true;

            //return false;
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
        }

        return false;
    }
}
