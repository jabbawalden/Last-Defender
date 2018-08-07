using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAcquiredAnimation : MonoBehaviour {

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameEvents.EventItemAcquired += AnimationTrigger;
    }

    private void OnDisable()
    {
        GameEvents.EventItemAcquired -= AnimationTrigger;
    }


    public void AnimationTrigger()
    {
        StartCoroutine(AnimateTiming());
    }

    IEnumerator AnimateTiming()
    {
        _animator.SetBool("HealthAcquired", true);
        yield return new WaitForSeconds(3);
        _animator.SetBool("HealthAcquired", false);
    }
}
