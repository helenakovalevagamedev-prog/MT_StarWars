using System;

public class GameManager
{
    private EnemySpawner enemySpawner;
    private Player player;
    private int lifesCount;
    private int currentlifesCount;
    private int goalScore;
    private int currentScore;
    private Func<int, LevelData> getLevelData;
    private LevelData currentLevel;
    private Action<int> onScoreChanged;
    private Action<int> onLifesChanged;
    private Action<InfoStates> onGameOver;
    private Action<int> onLevelComplete;

    public GameManager(
        PlayerConfig playerConfig,
        EnemiesConfig enemiesConfig,
        EnemySpawner enemySpawner,
        ScreenInfo screenInfo,
        Func <int, LevelData> getLevelData,
        Action<int> onScoreChanged,
        Action<int> onLifesChanged,
        Action<InfoStates> onGameOver,
        Action<int> onLevelComplete)
    {
        this.enemySpawner = enemySpawner;
        this.onGameOver = onGameOver;
        enemySpawner.Init(enemiesConfig, screenInfo);
        player = new Player(playerConfig, screenInfo, OnShoot, OnHit);
        this.getLevelData = getLevelData;
        this.onLevelComplete = onLevelComplete;
        lifesCount = playerConfig.GetPlayerInfo().LifesCount;
        ResetValues();
        this.onScoreChanged = onScoreChanged;
        this.onLifesChanged = onLifesChanged;
        onLifesChanged?.Invoke(lifesCount);
    }

    public void StartLevel(int levelIdx)
    {
        ResetValues();
        currentLevel = getLevelData.Invoke(levelIdx);
        goalScore = currentLevel.Goal;
        player.Show();
        player.Activate();
        enemySpawner.StartSpawning(currentLevel.PossibleEnemies);
    }

    public void RestartCurrentLevel()
    {
        StartLevel(currentLevel.Idx);
    }

    private void StopGame()
    {
        enemySpawner.StopSpawning();
        player.Deactivate();
    }

    private void ResetValues()
    {
        currentScore = 0;
        goalScore = 0;
        currentlifesCount = lifesCount;
    }

    private void OnShoot(int value)
    {
        currentScore = currentScore + value;
        onScoreChanged?.Invoke(currentScore);
        if (currentScore >= goalScore)
        {
            ApplyWinnedGame();
        }
    }

    private void OnHit()
    {
        currentlifesCount = currentlifesCount - 1;
        onLifesChanged?.Invoke(currentlifesCount);
        if(currentlifesCount <= 0)
        {
            ApplyFailedGame();
        }
    }

    private void ApplyFailedGame()
    {
        player.Hide();
        StopGame();
        onGameOver?.Invoke(InfoStates.Fail);
    }

    private void ApplyWinnedGame()
    {
        StopGame();
        onLevelComplete?.Invoke(currentLevel.Idx);
        onGameOver?.Invoke(InfoStates.Win);
    }
}