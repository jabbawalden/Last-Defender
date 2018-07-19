using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBox : MonoBehaviour {

    public int currentHealth;
    private DemonController demonController;
    

    private void Start()
    {
        demonController = GetComponent<DemonController>();
        currentHealth = 6;
    }
    public void Update()
    {
        if (currentHealth <= 0)
        {
            //calls OnKill function
            demonController.OnKill();
            Destroy(this.gameObject);
        }
    }

    /*
    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    */
}
