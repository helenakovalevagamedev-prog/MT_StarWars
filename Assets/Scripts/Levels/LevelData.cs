using System.Collections.Generic;

public class LevelData
{
    private readonly List<EnemyTypes> possibleEnemies;

    public int Idx { get; private set; }
    public LevelState State { get; private set; }
    public int Goal { get; private set; }
    public IReadOnlyList<EnemyTypes> PossibleEnemies => possibleEnemies;

    public LevelData(int goal, List<EnemyTypes> possibleEnemies, LevelState state, int idx)
    {
        Goal = goal;
        this.possibleEnemies = possibleEnemies;
        State = state;
        Idx = idx;
    }

    public void UpdateState(LevelState newState)
    {
        if((int)newState < (int)State)
        {
            return;
        }
        State = newState;
    }

    public void UpdateGoal(int newGoal)
    {
        Goal = newGoal;
    }

    public void UpdateEnemies(List<EnemyTypes> newEnemies)
    {
        possibleEnemies.Clear();
        for (int i = 0; i < newEnemies.Count; i++)
        {
            possibleEnemies.Add(newEnemies[i]);
        }
    }
}