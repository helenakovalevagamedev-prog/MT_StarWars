using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IMovable, IDestroyable, IOutOfBoundsHandler
{
    private Action<int> onShoot;
    private Action<GameObject> onDestroy;
    private float speed;
    private ScreenInfo screenInfo;
    private bool isDestroyed;
    private Collider2D otherCollider;

    public void Init(float speed, Action<int> onShoot, Action<GameObject> onDestroy, ScreenInfo screenInfo)
    {
        this.speed = speed;
        this.onShoot = onShoot;
        this.onDestroy = onDestroy;
        this.screenInfo = screenInfo;
        isDestroyed = false;
    }

    public void Move()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public bool IsOutOfBounds()
    {
        return transform.position.y > screenInfo.MaxY + 1;
    }

    public void DestroySelf()
    {
        if (isDestroyed)
        {
            return;
        }
        if(otherCollider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            isDestroyed = true;
            // temp
            // централизировать процесс удаления объектов
            var col = GetComponent<Collider2D>();
            col.enabled = false;
            onShoot?.Invoke(enemy.Cost);
            onDestroy?.Invoke(gameObject);
        }
    }

    private void Update()
    {
        Move();
        if (IsOutOfBounds())
        {
            isDestroyed = true;
            onDestroy?.Invoke(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        otherCollider = other.GetComponent<Collider2D>();
        DestroySelf();
        otherCollider = null;
    }
}