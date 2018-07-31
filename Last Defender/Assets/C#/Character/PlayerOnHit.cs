using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHit : MonoBehaviour {

    private Animator playerHitAnim;

	// Use this for initialization
	void Start ()
    {
        playerHitAnim = GetComponent<Animator>();
	}

    private void OnEnable()
    {
        GameEvents.EventPlayerHit += PlayerUponHit;
    }

    private void OnDisable()
    {
        GameEvents.EventPlayerHit -= PlayerUponHit;
    }

    private void PlayerUponHit()
    {
        //play sound
        StartCoroutine(OnPlayerHit());
    }

    IEnumerator OnPlayerHit()
    {
        playerHitAnim.SetBool("PlayerHit", true);
        yield return new WaitForSeconds(0.02f);
        playerHitAnim.SetBool("PlayerHit", false);
    }
}
