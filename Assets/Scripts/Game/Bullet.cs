using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IMovable, IDestroyable, IOutOfBoundsHandler
{
    [SerializeField] private Collider2D thisCollider;
    
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
        thisCollider.enabled = true;
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
        isDestroyed = true;
        // temp
        // централизировать процесс удаления объектов
        thisCollider.enabled = false;
        onDestroy?.Invoke(gameObject);
    }

    private void Update()
    {
        Move();
        if (IsOutOfBounds())
        {
            DestroySelf();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherCollider = other.GetComponent<Collider2D>();
        if (otherCollider.TryGetComponent(out Enemy enemy))
        {
            DestroySelf();
            onShoot?.Invoke(enemy.Cost);
        }
    }
}