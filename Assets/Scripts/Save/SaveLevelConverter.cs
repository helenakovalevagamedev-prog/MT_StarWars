using System.Collections.Generic;

public class SaveLevelConverter
{
    public LevelSaveData ConvertToSave(LevelData runtimeData)
    {
        return new LevelSaveData
        {
            levelIdx = runtimeData.Idx,
            state = (int)runtimeData.State,
            goal = runtimeData.Goal,
            enemiesTypes = ConvertEnemiesToInt(runtimeData.PossibleEnemies)
        };
    }

    public LevelData ConvertToRuntime(LevelSaveData save)
    {
       return new LevelData(
                save.goal,
                new List<EnemyTypes>(ConvertEnemies(save.enemiesTypes)),
                (LevelState) save.state,
                save.levelIdx); 
    }

    private EnemyTypes[] ConvertEnemies(int[] arr)
    {
        var result = new EnemyTypes[arr.Length];
        for (int i = 0; i < arr.Length; i++)
            result[i] = (EnemyTypes)arr[i];
        return result;
    }

    private int[] ConvertEnemiesToInt(List<EnemyTypes> list)
    {
        var result = new int[list.Count];
        for (int i = 0; i < list.Count; i++)
            result[i] = (int)list[i];
        return result;
    }
}