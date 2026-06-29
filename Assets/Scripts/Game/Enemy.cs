using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IMovable, IDestroyable, IOutOfBoundsHandler
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D thisCollider;

    private float speed;
    private Action<GameObject> onDestroy;
    private ScreenInfo screenInfo;
    private bool isDestroyed;

    public int Cost {  get; private set; }

    public void Init(int cost, float speed, Sprite sprite, Action<GameObject> onDestroy, ScreenInfo screenInfo)
    {
        Cost = cost;
        spriteRenderer.sprite = sprite;
        this.speed = speed;
        this.onDestroy = onDestroy;
        this.screenInfo = screenInfo;
        thisCollider.enabled = true;
        isDestroyed = false;
    }

    public void Move()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    public bool IsOutOfBounds()
    {
        return transform.position.y < screenInfo.MinY - 1;
    }

    public void DestroySelf()
    {
        if (isDestroyed)
        {
            return;
        }
        isDestroyed = true;
        // temp
        // TODO centrilize destroy pipeline
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
        if (isDestroyed)
        {
            return;
        }
        if (other.TryGetComponent(out Bullet bullet) || other.TryGetComponent(out PlayerController player))
        {
            DestroySelf();
        }
    }
}