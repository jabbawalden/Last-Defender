using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour {

    public int health;
    //create variables
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _jump;

    public GameObject spotLight;
    public float lightPower;
    public bool lightOn;

    float _translation;
    float _strafe;

    [SerializeField] private Animator _playerAnim;

    public GameObject[] hitPos;

   

    // Use this for initialization
    void Start ()
    {
        lightOn = false;
        spotLight.SetActive(false);
        _speed *= Time.deltaTime;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        _translation = Input.GetAxis("Vertical") * _speed;
        _strafe = Input.GetAxis("Horizontal") * _speed;

        //movementfunction
        if (health >= 1)
        {
            MovementInput();
            LightEnable();
        }
        else
        {

        }
        
        if (IsWalking())
        {
            _playerAnim.SetBool("IsWalking", true);
        }
        else
        {
            _playerAnim.SetBool("IsWalking", false);
        }
        
    }


    private void MovementInput()
    {
        //WASD movement
  

        //force
        transform.Translate(_strafe, 0, _translation);

        //add mouse back in during play
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (CanJump())
        {
            _rb.AddForce(_jump * transform.up, ForceMode.Impulse);
        }
        
    }

    bool CanJump()
    {
        Ray ray = new Ray(transform.position, transform.up * -1);

        RaycastHit hit;

        //localscale.y is maxdistance of ray
        if (Physics.Raycast(ray, out hit, transform.localScale.y + 0.2f))
        {
            return true;
            
        }

        return false;
    }

 
    bool IsWalking()
    {
        if (Mathf.Abs(_strafe) == 0 && Mathf.Abs(_translation) == 0)
        {
            _playerAnim.SetBool("IsWalking", false);
            return false;
        }
        //animate
        


        return true;
    }

    private void LightEnable()
    {
        if (Input.GetKeyDown(KeyCode.E) && !lightOn)
        {
            lightOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && lightOn)
        {
            lightOn = false;
        }

        if (lightOn)
        {
            spotLight.SetActive(true);
            lightPower--;

        }
        else if (!lightOn)
        {
            spotLight.SetActive(false);

            if (lightPower < 500)
            {
                lightPower++;
            }
        }


        if (lightPower <= 0)
        {
            lightOn = false;
        }

    }

}
