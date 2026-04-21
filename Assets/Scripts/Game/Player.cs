using System;
using UnityEngine;

public class Player
{
    private PlayerController controller;
    private PlayerView view;
    private Action<int> onShoot;
    private Action onHit;

    public Player(PlayerConfig config, ScreenInfo screenInfo, Action<int> onShoot, Action onHit)
    {
        this.onHit = onHit;
        this.onShoot = onShoot;
        
        CreatePlayer(config, screenInfo);
    }

    public void Activate()
    {
        controller.enabled = true;
        controller.MoveToVisiblePoint();
    }

    public void Deactivate()
    {
        controller.MoveToHiddenPoint();
        controller.enabled = false;
    }

    public void Show()
    {
        view.Show();
    }

    public void Hide()
    {
        view.Hide();
    }

    private void CreatePlayer(PlayerConfig config, ScreenInfo screenInfo)
    {
        var playerGO = GameObject.Instantiate(config.GetPlayerPrefab(), Vector3.zero, Quaternion.identity);
        controller = playerGO.GetComponent<PlayerController>();
        view = playerGO.GetComponent<PlayerView>();
        controller.Init(config.GetBulletPrefab(), config.GetPlayerInfo(), config.GetBulletInfo(), screenInfo, OnShoot, OnHit);
        controller.MoveToHiddenPoint();
        Hide();
        Deactivate();
    }

    private void OnHit()
    {
        onHit?.Invoke();
    }

    private void OnShoot(int cost)
    {
        onShoot?.Invoke(cost);
    }
}
