using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField] private Text _ammoDisplay;
    [SerializeField] private Text _lightPowerDisplay;
    [SerializeField] private Text _doorPowerDisplay;
    [SerializeField] private Text _itemAcquiredDisplay;
    public Text interactE;
    public GameObject interactBG;
    private CharacterMotor _characterMotor;
    private PShoot _pShoot;
    [SerializeField] private Text bCAmmoDisplay, mCAmmoDisplay, hBAmmoDisplay;
    public AmmoRefill ammoRefill;
    public GameObject uIammoRefill, uIplayerUpgrades;
    public GameObject suitInstructions;
    public GameObject inGameMenu;
    public GameObject deathScreen;
    public GameObject winScreen;
    public Slider healthBar;
    public Slider powerBar;
    public bool menuShown;

    private void OnEnable()
    {
        GameEvents.EventGameWon += GameWonUI;
    }
    private void OnDisable()
    {
        GameEvents.EventGameWon -= GameWonUI;
    }

    void Start()
    {
        menuShown = false;
        winScreen.SetActive(false);
        deathScreen.SetActive(false);
        inGameMenu.SetActive(false);
        suitInstructions.SetActive(false);
        interactE.gameObject.SetActive(false);
        interactBG.gameObject.SetActive(false);
        uIammoRefill.SetActive(false);
        uIplayerUpgrades.SetActive(false);
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _pShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
        DoorPowerDisplay("", Color.black);
        interactE.text = "";
        bCAmmoDisplay.text = "Blast Cannon + " + ammoRefill.blastCannonA;
        mCAmmoDisplay.text = "Mini Cannon + " + ammoRefill.miniCannonA;
        hBAmmoDisplay.text = "Hyper Blaster + " + ammoRefill.hyperBlasterA;
    }

    void Update()
    {
        HealthBarDisplay();
        PowerBarDisplay();
        //Light UI
        _lightPowerDisplay.text = "Light Power:";

        AmmoDisplay();
        SuitInstructionsController();

    }

    public void HealthBarDisplay()
    {
        healthBar.value = Mathf.Lerp(healthBar.value, _characterMotor.health, Time.deltaTime * 10F);

    }

    public void PowerBarDisplay()
    {
        float percentageAmount = _characterMotor.lightPower / _characterMotor.maxLightPower;
        powerBar.value = percentageAmount;
    }

    void AmmoDisplay()
    {

        //Ammo UI
        if (_pShoot.bCannonFire)
            _ammoDisplay.text = "Blast Cannon: " + _pShoot.bAmmo;

        if (_pShoot.miniCannonFire)
            _ammoDisplay.text = "Mini Cannon: " + _pShoot.mAmmo;

        if (_pShoot.hyperBlasterFire)
            _ammoDisplay.text = "Hyper Blaster: " + _pShoot.hAmmo;

        if (_pShoot.rezoidFire)
            _ammoDisplay.text = "Rezoid: Infinite";
    }

    public void DoorPowerDisplay(string powerState, Color colour)
    {
        _doorPowerDisplay.text = powerState;
        _doorPowerDisplay.color = colour;
    }

    public void ItemAcquiredDisplay(string itemType)
    {
        _itemAcquiredDisplay.text = itemType;
    }

    public void SuitInstructionsController()
    {
        if (suitInstructions.activeInHierarchy == false)
        {
            interactE.text = "Interact (E)";
            
        }
        else if (suitInstructions.activeInHierarchy == true)
        {
            interactE.text = "Exit Application (E)";
            
        }
    }

    
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void GameWonUI()
    {
        winScreen.SetActive(true);
    }
}
