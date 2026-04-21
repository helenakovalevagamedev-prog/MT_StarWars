using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Action<int> onShoot;
    private Action<GameObject> onDestroy;
    private float speed;
    private ScreenInfo screenInfo;
    private bool isDestroyed;

    public void Init(float speed, Action<int> onShoot, Action<GameObject> onDestroy, ScreenInfo screenInfo)
    {
        this.speed = speed;
        this.onShoot = onShoot;
        this.onDestroy = onDestroy;
        this.screenInfo = screenInfo;
        isDestroyed = false;
    }

    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (transform.position.y > screenInfo.MaxY + 1)
        {
            isDestroyed = true;
            onDestroy?.Invoke(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroyed)
        {
            return;
        }
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            isDestroyed = true;
            onShoot?.Invoke(enemy.Cost);
            onDestroy?.Invoke(gameObject);
        }
    }
}