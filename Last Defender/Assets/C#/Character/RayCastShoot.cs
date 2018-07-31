using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour {

    public int gunDamage = 1;
    public float weaponRange = 50;
    public float hitForce = 160;
    public Transform gunEnd1;
    private CharacterMotor _characterMotor;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private LineRenderer[] laserLine;
    private PShoot _pShoot;
    [SerializeField] private GameObject _gunLight;
    // Use this for initialization
    

    private void Awake()
    {
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _gunLight.SetActive(false);
        _pShoot = GetComponent<PShoot>();
    }

    public void RayShoot(int c)
    {
        if (c == 1)
        {
            StartCoroutine(ShotEffect(1));
        }

        //gets point at exact center of viewport
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        laserLine[0].SetPosition(0, gunEnd1.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {

            laserLine[0].SetPosition(1, hit.point);
            //gets script from hit object
            StrongDemon enemyCollider = hit.collider.GetComponent<StrongDemon>();
            FastDemon enemyCollider2 = hit.collider.GetComponent<FastDemon>();
            RangeDemon enemyCollider3 = hit.collider.GetComponent<RangeDemon>();
            //DemonController demonController = hit.collider.GetComponent<DemonController>();

            //checks if there is a shootablebox script
            if (enemyCollider != null)
            {
                GameEvents.ReportEnemyHit();
                enemyCollider.CurrentHealth -= _pShoot.currentDamage;

                if (_pShoot.currentWeapon == 3)
                    enemyCollider.HitActivate();
            }

            if (enemyCollider2 != null)
            {
                enemyCollider2.CurrentHealth -= _pShoot.currentDamage;

                if (_pShoot.currentWeapon == 3)
                    enemyCollider2.HitActivate();
            }

            if (enemyCollider3 != null)
            {
                enemyCollider3.CurrentHealth -= _pShoot.currentDamage;
            }

            if (hit.rigidbody != null)
            {
                //hit.normal is direction away from surface we hit
                //use -hit.normal to make the box move away from the surface it was hit * hitforce amount
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }


        }
        else
        {   //continue past point
            laserLine[0].SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }


    }

    IEnumerator ShotEffect(int c)
    {
        if (c == 1)
        {
            laserLine[0].enabled = true;
            _gunLight.SetActive(true);
            yield return new WaitForSeconds(0.08f);
            laserLine[0].enabled = false;
            _gunLight.SetActive(false);
            StopCoroutine(ShotEffect(1));
        }
   
    }

    private void OnEnable()
    {
        GameEvents.EventEnemyHit += EnemyHit;
    }

    private void OnDisable()
    {
        GameEvents.EventEnemyHit -= EnemyHit;
    }

    void EnemyHit()
    {
        Debug.Log("EnemyHit");
       //Dot image fx
       //
    }
}
