public struct ConfigPlayerInfo
{
    public float PlayerSpeed { get; private set; }
    public float FireCooldown { get; private set;  }
    public int LifesCount { get; private set;  }
    public ConfigPlayerInfo(float playerSpeed, float fireCooldown, int lifesCount)
    {
        PlayerSpeed = playerSpeed;
        FireCooldown = fireCooldown;
        LifesCount = lifesCount;
    }
}
