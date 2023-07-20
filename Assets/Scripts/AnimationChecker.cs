using System;
using UnityEngine;

public class AnimationChecker : MonoBehaviour
{
    public static AnimationChecker Instance { get; private set; }

    private void OnEnable()
    {
        if (Instance != null) return;

        Instance = this;
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
