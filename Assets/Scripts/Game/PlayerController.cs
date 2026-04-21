using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private const float hiddenOffset = -1f;
    private const float visibleOffset = 1f;
    private const int bulletPoolCapacityValue = 20;
    private const int bulletPoolMaxSizeValue = 50;

    [SerializeField] private Transform shootingPoint;

    private float speed;
    private float fireCooldown;
    private float lastFireTime;
    private Vector2 moveInput;
    private ScreenInfo screenInfo;
    private Action onHit;
    private Action<int> onShoot;
    private bool isInited;
    private ConfigBulletInfo bulletInfo;
    private SimplePool bulletPool;
    private Vector3 hiddenPos;
    private Vector3 visiblePos;

    public void Init(
        GameObject bulletPrefab,
        ConfigPlayerInfo playerInfo,
        ConfigBulletInfo bulletInfo,
        ScreenInfo screenInfo,
        Action<int> onShoot,
        Action onHit)
    {
        speed = playerInfo.PlayerSpeed;
        fireCooldown = playerInfo.FireCooldown;
        this.screenInfo = screenInfo;
        this.bulletInfo = bulletInfo;
        this.onShoot = onShoot;
        this.onHit = onHit;
        bulletPool = new SimplePool(bulletPrefab, bulletPoolCapacityValue, bulletPoolMaxSizeValue);
        hiddenPos = new(0, screenInfo.MinY + hiddenOffset, 0);
        visiblePos = new(0, screenInfo.MinY + visibleOffset, 0);
        isInited = true;
    }

    public void MoveToVisiblePoint()
    {
        transform.position = visiblePos;
    }

    public void MoveToHiddenPoint()
    {
        transform.position = hiddenPos;
    }

    private void Update()
    {
        if (!isInited || !enabled)
        {
            return;
        }
        Move();
    }

    private void Move()
    {
        Vector3 pos = transform.position;
        pos += new Vector3(moveInput.x, moveInput.y, 0) * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, screenInfo.MinX, screenInfo.MaxX);
        pos.y = Mathf.Clamp(pos.y, screenInfo.MinY, screenInfo.MaxY);
        transform.position = pos;
        HandleTouch();
    }
    

    private void HandleTouch()
    {
        if (Touchscreen.current == null)
        {
            return;
        }

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.isPressed)
        {
            float screenX = touch.position.ReadValue().x;
            moveInput.x = screenX < screenInfo.ScreenCenter ? -1 : 1;
        }
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnFire()
    {
        if (!enabled || Time.time < lastFireTime + fireCooldown)
        {
            return;
        }

        lastFireTime = Time.time;

        var bulletGO = bulletPool.Get();
        bulletGO.transform.position = shootingPoint.position;
        bulletGO.GetComponent<Bullet>().Init(bulletInfo.Speed, onShoot, bulletPool.Release, screenInfo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            onHit?.Invoke();
        }
    }
}