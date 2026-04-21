using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/GameplayConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float fireCooldown;
    [SerializeField] private int lifesCount;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    public GameObject GetPlayerPrefab()
    {
        return playerPrefab;
    }

    public GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }

    public ConfigPlayerInfo GetPlayerInfo()
    {
        return new ConfigPlayerInfo(playerSpeed, fireCooldown, lifesCount);
    }

    public ConfigBulletInfo GetBulletInfo()
    {
        return new ConfigBulletInfo(bulletSpeed);
    }
}
