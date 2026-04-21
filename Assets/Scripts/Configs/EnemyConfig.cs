using System;
using UnityEngine;

[Serializable]
public class EnemyConfig
{
    [SerializeField] private EnemyTypes type;
    [SerializeField] private float speed;
    [SerializeField] private int cost;
    [SerializeField] private Sprite sprite;

    public EnemyTypes Type => type;
    public float Speed => speed;
    public int Cost => cost;
    public Sprite Sprite => sprite;
}
