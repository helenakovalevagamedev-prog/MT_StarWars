using System;

[Serializable]
public class LevelSaveData
{
    public int levelIdx;
    public int state;
    public int goal;
    public int[] enemiesTypes;
}