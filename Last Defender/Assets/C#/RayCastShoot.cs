using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour {

    public int gunDamage = 1;
    public float weaponRange = 50;
    public float hitForce = 160;
    public Transform gunEnd1, gunEnd2;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private LineRenderer[] laserLine;

	// Use this for initialization
	void Start ()
    {

	}
	
    public void RayShoot(int c)
    {
        if (c == 1)
        {
            StartCoroutine(ShotEffect(1));
        }
       
        //gets point at exact center of viewport
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3 (0.5f, 0.5f, 0));
        RaycastHit hit;
        laserLine[0].SetPosition(0, gunEnd1.position);
        laserLine[1].SetPosition(0, gunEnd2.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            laserLine[0].SetPosition(1, hit.point);
            laserLine[1].SetPosition(1, hit.point);

            //gets script from hit object
            ShootableBox health = hit.collider.GetComponent<ShootableBox>();

            //checks if there is a shootablebox script
            if (health != null)
            {
                health.Damage(gunDamage);
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
            laserLine[1].SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }
    }

    IEnumerator ShotEffect(int c)
    {
        if (c == 1)
        {
            laserLine[0].enabled = true;
            laserLine[1].enabled = true;
            yield return new WaitForSeconds(0.07f);
            laserLine[0].enabled = false;
            laserLine[1].enabled = false;
        }
   
    }


}
