using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

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
        isDestroyed = false;
    }

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y < screenInfo.MinY - 1)
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
        if (other.GetComponent<Bullet>() != null || other.GetComponent<PlayerController>() != null)
        {
            isDestroyed = true;
            onDestroy?.Invoke(gameObject);
        }
    }
}