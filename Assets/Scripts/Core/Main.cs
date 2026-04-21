using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private LevelsConfig levelsConfig;
    [SerializeField] private EnemiesConfig enemiesConfig;
    [Header("Game")]
    [SerializeField] private UIController uIController;
    [SerializeField] private Button playButon;
    [SerializeField] private EnemySpawner enemySpawner;

    private SaveSystem saveSystem;
    private GameManager gameManager;
    private ScreenInfo screenInfo;
    private LevelsManager levelsManager;

    private void Start()
    {
        saveSystem = new SaveSystem();
        screenInfo = new ScreenInfo();
        InitializeManagers();
        StartGame();
    }

    private void InitializeManagers()
    {
        levelsManager = new LevelsManager(levelsConfig, saveSystem.IsSaveLoaded, saveSystem.GetSave, saveSystem.Save);
        uIController.Init(levelsManager.GetLevelData, levelsManager.LevelsAmount);
        gameManager = new GameManager(
            playerConfig,
            enemiesConfig,
            enemySpawner,
            screenInfo,
            levelsManager.GetLevelData,
            uIController.UpdateScore,
            uIController.UpdateLifes,
            uIController.OpenGameoverInfoPanel,
            levelsManager.UpdateLevelsState);
        uIController.SetupInfoPanel(gameManager.StartLevel, gameManager.RestartCurrentLevel);
    }

    private void StartGame()
    {
        playButon.onClick.AddListener(uIController.ShowLevelsMenu);
        uIController.ShowStartMenu();
    }
}