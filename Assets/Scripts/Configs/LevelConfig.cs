using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelConfig
{
    [SerializeField] private int goal;
    [SerializeField] private List<EnemyTypes> possibleEnemies = new List<EnemyTypes>();

    public int GetGoalValue() { return goal; }

    public List<EnemyTypes> PossibleEnemies { get { return possibleEnemies; } }
}
