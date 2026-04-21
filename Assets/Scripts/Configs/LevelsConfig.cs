using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelsConfig", menuName = "Scriptable Objects/LevelsConfig")]
public class LevelsConfig : ScriptableObject
{
    [SerializeField] private List<LevelConfig> levels = new List<LevelConfig>();

    public List<LevelConfig> Levels { get { return levels; } }
}
