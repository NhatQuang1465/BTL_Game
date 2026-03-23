using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int currentEnergy;
    [SerializeField] private int energyThreshold = 3;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject enemySpawner;
    private bool bossCalled = false;
    [SerializeField] private Image energyBar;
    [SerializeField] GameObject gameUi;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject shopButton;
    [SerializeField] private GameObject coinIcon;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    private int score = 0;

    void Start()
    {
        InitWeaponData();

        UpdateCoinUI();
        UpdateScore();
        currentEnergy = 0;
        UpdateEnergyBar();
        boss.SetActive(false);
        MainMenu();
        audioManager.StopAudioGame();
    }

    private void InitWeaponData()
    {
        if (!PlayerPrefs.HasKey("Gun_Default_Unlocked"))
        {
            PlayerPrefs.SetInt("Gun_Default_Unlocked", 1);
        }

        if (!PlayerPrefs.HasKey("CurrentGun"))
        {
            PlayerPrefs.SetString("CurrentGun", "DefaultGun");
        }

        if (!PlayerPrefs.HasKey("GunDamage_DefaultGun"))
        {
            PlayerPrefs.SetInt("GunDamage_DefaultGun", 10);
        }

        if (!PlayerPrefs.HasKey("GunDamage_GunLv1"))
        {
            PlayerPrefs.SetInt("GunDamage_GunLv1", 20);
        }

        PlayerPrefs.Save();
    }

    private void UpdateCoinUI()
    {
        if (coinText != null && CoinManager.Instance != null)
        {
            coinText.text = CoinManager.Instance.coinInRun.ToString();
        }
    }

    public void AddCoin(int amount)
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.AddCoinInRun(amount);
        }

        UpdateCoinUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void AddEnergy()
    {
        if (bossCalled) return;

        currentEnergy += 1;
        UpdateEnergyBar();

        if (currentEnergy == energyThreshold)
        {
            CallBoss();
        }
    }

    private void CallBoss()
    {
        bossCalled = true;
        boss.SetActive(true);
        enemySpawner.SetActive(false);
        audioManager.PlayBossSound();
    }

    private void UpdateEnergyBar()
    {
        if (energyBar != null)
        {
            float fillAmount = Mathf.Clamp01((float)currentEnergy / (float)energyThreshold);
            energyBar.fillAmount = fillAmount;
        }
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        scoreUI.SetActive(false);

        if (shopButton != null)
        {
            shopButton.SetActive(true);
        }

        Time.timeScale = 0f;
        if (coinIcon != null)
        {
            coinIcon.SetActive(false);
        }
    }


    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        scoreUI.SetActive(false);
        Time.timeScale = 0f;
        if (coinIcon != null)
        {
            coinIcon.SetActive(false);
        }
    }


    public void GameOverMenu()
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.SaveRunCoinToTotal();
        }

        gameOverMenu.SetActive(true);
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        scoreUI.SetActive(false);
        Time.timeScale = 0f;
        if (coinIcon != null)
        {
            coinIcon.SetActive(false);
        }
    }

    public void PauseGameMenu()
    {
        pauseMenu.SetActive(true);
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void WinGameMenu()
    {
        winMenu.SetActive(true);
        pauseMenu.SetActive(false);
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        scoreUI.SetActive(true);

        if (shopButton != null)
        {
            shopButton.SetActive(false);
        }

        Time.timeScale = 1f;
        audioManager.PlayDefaultSound();
        if (coinIcon != null)
        {
            coinIcon.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);

        if (shopButton != null)
        {
            shopButton.SetActive(false);
        }

        Time.timeScale = 1f;
        if (coinIcon != null)
        {
            coinIcon.SetActive(true);
        }
    }
}