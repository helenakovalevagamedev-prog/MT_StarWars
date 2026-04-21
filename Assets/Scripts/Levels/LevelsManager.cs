using System;
using System.Collections.Generic;
using System.Linq;

public class LevelsManager
{
    private const int goalChange = 50;

    private List<LevelData> levels;
    private LevelsConfig config;
    private Action<SaveData> saveData;
    private SaveLevelConverter converter;

    public int LevelsAmount => levels.Count;

    public LevelsManager(LevelsConfig config, bool isSaveLoaded, Func<SaveData> getSave, Action<SaveData> saveData)
    {
        this.config = config;
        this.saveData = saveData;
        converter = new SaveLevelConverter();
        
        levels = new List<LevelData>();
        if (isSaveLoaded)
        {
            levels = getSave.Invoke().levels.Select(converter.ConvertToRuntime).ToList();
            return;
        }

        for (int i = 0; i < config.Levels.Count; i++)
        {
            levels.Add(new LevelData(
                GenerateGoal(config.Levels[i].GetGoalValue()),
                GenerateEnemies(config.Levels[i].PossibleEnemies),
                i == 0 ? LevelState.InProgress : LevelState.Locked,
                i));
        }
    }

    public LevelData GetLevelData(int levelIdx)
    {
        return levels[levelIdx];
    }

    public void UpdateLevelsState(int levelIdx)
    {
        // regenerate completed level data
        var configLevelData = config.Levels[levelIdx];
        var newCompletedLevelData = new LevelData(
            GenerateGoal(configLevelData.GetGoalValue()),
            GenerateEnemies(configLevelData.PossibleEnemies),
            LevelState.Completed,
            levelIdx);
        levels[levelIdx] = newCompletedLevelData;

        //update next level state
        if (levelIdx + 1 < levels.Count)
        {
            var nextLevelData = levels[levelIdx + 1];
            var newNextLevelData = new LevelData(
                    nextLevelData.Goal,
                    nextLevelData.PossibleEnemies,
                    LevelState.InProgress,
                    nextLevelData.Idx);
            levels[levelIdx + 1] = newNextLevelData;
        }

        OnDataUpdated();
    }

    private void OnDataUpdated()
    {
        var save = new SaveData();
        save.levels = levels.Select(converter.ConvertToSave).ToList();
        saveData?.Invoke(save);
    }

    private int GenerateGoal(int configGoal)
    {
        return UnityEngine.Random.Range(configGoal, configGoal + goalChange);
    }

    private List<EnemyTypes> GenerateEnemies(List<EnemyTypes> possibleEnemies)
    {
        int count = UnityEngine.Random.Range(1, possibleEnemies.Count + 1);

        return possibleEnemies
            .OrderBy(x => UnityEngine.Random.value)
            .Take(count)
            .ToList();
    }
}
