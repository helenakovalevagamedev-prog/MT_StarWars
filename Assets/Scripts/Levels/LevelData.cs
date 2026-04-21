using System.Collections.Generic;

public struct LevelData
{
    public int Idx { get; private set; }
    public LevelState State { get; private set; }
    public int Goal { get; private set; }
    public List<EnemyTypes> PossibleEnemies { get; private set; }

    public LevelData(int goal, List<EnemyTypes> possibleEnemies, LevelState state, int idx)
    {
        Goal = goal;
        PossibleEnemies = possibleEnemies;
        State = state;
        Idx = idx;
    }
}