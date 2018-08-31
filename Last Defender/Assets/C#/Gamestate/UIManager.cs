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
    [SerializeField] private Text _rezoidDisplay, _blastCannonDisplay, _miniCannonDisplay, _hyperBlasterDisplay;
    [SerializeField] private Text _ArmorDisplay, _MobilityDisplay, _LightDisplay;
    public int _ArmorLevel, _MobilityLevel, _LightLevel;
    public Text interactE;
    public GameObject interactBG;
    private CharacterMotor _characterMotor;
    private GameManager _gameManager;
    private PShoot _pShoot;
    //[SerializeField] private Text bCAmmoDisplay, mCAmmoDisplay, hBAmmoDisplay;
    public AmmoRefill ammoRefill;
    public GameObject uIammoRefill, uIplayerUpgrades;
    public GameObject suitInstructions;
    public GameObject inGameMenu;
    public GameObject deathScreen;
    public GameObject winScreen;
    public Slider healthBar;
    public Slider powerBar;
    public bool menuShown;

    public Text loadGame;
    public GameObject startNew;

    public GameObject playUI;
    public GameObject mainMenu;

    private void OnEnable()
    {
        GameEvents.EventGameWon += GameWonUI;
        GameEvents.EventSaveGameData += SaveGameDisplay;
    }
    private void OnDisable()
    {
        GameEvents.EventGameWon -= GameWonUI;
        GameEvents.EventSaveGameData -= SaveGameDisplay;
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
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        DoorPowerDisplay("", Color.black);
        interactE.text = "";
        /*
        bCAmmoDisplay.text = "Blast Cannon + " + ammoRefill.blastCannonA;
        mCAmmoDisplay.text = "Mini Cannon + " + ammoRefill.miniCannonA;
        hBAmmoDisplay.text = "Hyper Blaster + " + ammoRefill.hyperBlasterA;
        */
        if (_gameManager.gameExists)
        {
            startNew.SetActive(true);
            loadGame.text = "Load Last Checkpoint";
        }
        else
        {
            startNew.SetActive(false);
            loadGame.text = "Begin";
        }

        if (_gameManager.gameState == GameState.Menu)
        {
            playUI.SetActive(false);
            mainMenu.SetActive(true);
        } 
        else if (_gameManager.gameState == GameState.Play)
        {
            playUI.SetActive(true);
            mainMenu.SetActive(false);
        }

    }

    void Update()
    {
        HealthBarDisplay();
        PowerBarDisplay();
        //Light UI
        _lightPowerDisplay.text = "Light Power:";

        AmmoDisplay();
        SuitInstructionsController();
        GunReveal();

        _ArmorDisplay.text = "+ " + _ArmorLevel;
        _MobilityDisplay.text = "+ " + _MobilityLevel;
        _LightDisplay.text = "+ " + _LightLevel;

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

    public void SaveGameDisplay()
    {
        ItemAcquiredDisplay("Check Point Reached " +
            "(Game Saved)");
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

    public void GunReveal()
    {
        
        if (_pShoot.blastCannonPickUp)
            _blastCannonDisplay.text = "Blast Cannon";
        else
            _blastCannonDisplay.text = "";

        if (_pShoot.miniCannonPickUp)
            _miniCannonDisplay.text = "Mini Cannon";
        else
            _miniCannonDisplay.text = "";

        if (_pShoot.hyperBlasterPickUp)
            _hyperBlasterDisplay.text = "Hyper Blaster";
        else
            _hyperBlasterDisplay.text = "";
            

    }
    public void GunDisplay(int c)
    {
        
        //highlight display if == c, else dim
        if (c == 1)
        {
            //bright white
            _rezoidDisplay.color = Color.white;
        }
        else
        {
            //dim grey
            _rezoidDisplay.color = Color.grey;
        }

        if (c == 2)
        {
            _blastCannonDisplay.color = Color.white;
        }
        else
        {
            _blastCannonDisplay.color = Color.grey;
        }

        if (c == 3)
        {
            _miniCannonDisplay.color = Color.white;
        } 
        else
        {
            _miniCannonDisplay.color = Color.grey;
        }

        if (c == 4)
        {
            _hyperBlasterDisplay.color = Color.white;
        }
        else
        {
            _hyperBlasterDisplay.color = Color.grey;
        }
        
    }

    public void DeleteData()
    {
        _gameManager.DeleteData();
    }
    
    public void RestartGame()
    {
        _gameManager.gameState = GameState.Play;
        mainMenu.SetActive(false);

        GameManager.Instance.deadEnemies.Clear();
        GameManager.Instance.usedAmmo.Clear();
        GameManager.Instance.usedPowerCore.Clear();
        GameManager.Instance.usedHealthPack.Clear();
        GameManager.Instance.usedRepairKits.Clear();
        GameManager.Instance.triggeredEnemyGroups.Clear();

        SceneManager.LoadScene(0);
        GameManager.Instance.shouldLoad = true;
        //GameEvents.ReportLoadLastSave();
    }

    public void LoadGame()
    {
        GameManager.Instance.deadEnemies.Clear();
        GameManager.Instance.usedAmmo.Clear();
        GameManager.Instance.usedPowerCore.Clear();
        GameManager.Instance.usedHealthPack.Clear();
        GameManager.Instance.usedRepairKits.Clear();
        GameManager.Instance.triggeredEnemyGroups.Clear();

        if (_gameManager.gameExists)
        {
            _gameManager.gameState = GameState.Play;
            var load = SceneManager.LoadSceneAsync(0);
            GameManager.Instance.shouldLoad = true;
            //GameEvents.ReportLoadLastSave();
        }
        else
        {
            _gameManager.gameState = GameState.Play;
            SceneManager.LoadScene(0);
        }
    }

    public void StartNew()
    {
        _gameManager.gameState = GameState.Play;
        _gameManager.DeleteData();
        _gameManager.gameExists = false;

        GameManager.Instance.deadEnemies.Clear();
        GameManager.Instance.usedAmmo.Clear();
        GameManager.Instance.usedPowerCore.Clear();
        GameManager.Instance.usedHealthPack.Clear();
        GameManager.Instance.usedRepairKits.Clear();
        GameManager.Instance.triggeredEnemyGroups.Clear();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
    //     public List<string> deadEnemies = new List<string>();
    //public List<string> usedAmmo = new List<string>();
    //public List<string> usedPowerCore = new List<string>();
    //public List<string> usedHealthPack = new List<string>();
    //public List<string> usedRepairKits = new List<string>();
    //public List<string> triggeredEnemyGroups = new List<string>();

    //public List<string> gm_deadEnemies = new List<string>();
    //public List<string> gm_usedAmmo = new List<string>();
    //public List<string> gm_usedPowerCore = new List<string>();
    //public List<string> gm_usedHealthPack = new List<string>();
    //public List<string> gm_usedRepairKits = new List<string>();
    //public List<string> gm_triggeredEnemyGroups = new List<string>();

        _gameManager.gameState = GameState.Menu;
        GameManager.Instance.deadEnemies.Clear();
        GameManager.Instance.usedAmmo.Clear();
        GameManager.Instance.usedPowerCore.Clear();
        GameManager.Instance.usedHealthPack.Clear();
        GameManager.Instance.usedRepairKits.Clear();
        GameManager.Instance.triggeredEnemyGroups.Clear();

        SceneManager.LoadScene(0);
    }

    public void GameWonUI()
    {
        winScreen.SetActive(true);
    }
}
