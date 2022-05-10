using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetups;

    public enum AnimationType
    {
        RUN,
        INDLE,
        DEATH
    }

    public void Play(AnimationType type)
    {
        foreach (var animation in animatorSetups)
        {
            if (animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                break;
            }
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Play(AnimationType.RUN);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Play(AnimationType.DEATH);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Play(AnimationType.INDLE);
        }
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
}