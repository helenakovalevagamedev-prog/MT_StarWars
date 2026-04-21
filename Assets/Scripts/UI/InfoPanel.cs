using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image background;
    [SerializeField] private Image fade;
    [SerializeField] private ButtonView startButtonView;
    [SerializeField] private Button startButton;
    [SerializeField] private ButtonView menuButtonView;
    [SerializeField] private Button menuButton;
    [SerializeField] private ButtonView restartButtonView;
    [SerializeField] private Button restartButton;
    [SerializeField] private ButtonView closeButtonView;
    [SerializeField] private Button closeButton;

    private Action<int> onStartTap;
    private Action onMenuTap;
    private Action onRestartTap;
    private int currenLevelIdx;

    public void Init(Action<int> onStartTap, Action onMenuTap, Action onRestartTap)
    {
        ResetValues();
        this.onStartTap = onStartTap;
        this.onMenuTap = onMenuTap;
        this.onRestartTap = onRestartTap;
        startButton.onClick.AddListener(OnStartLevelTap);
        menuButton.onClick.AddListener(OnMenuTap);
        restartButton.onClick.AddListener(OnRestartTap);
        closeButton.onClick.AddListener(HideAll);
        HideAll();
    }

    public void SetupStartView(int levelIdx, LevelData levelData)
    {
        HideAll();
        currenLevelIdx = levelIdx;
        ShowBase();
        SetTitle($"Level {levelIdx + 1}");
        closeButton.interactable = true;
        closeButtonView.Show();
        string descriptionText = "???";
        if (levelData.State == LevelState.InProgress || levelData.State == LevelState.Completed)
        {
            var enemies = " ";
            for (int i = 0; i < levelData.PossibleEnemies.Count; i++)
            {
                enemies = $"{enemies} {levelData.PossibleEnemies[i]}";
            }
            descriptionText = $"Goal: {levelData.Goal} \n Possible Enemies: {enemies}";
            startButton.interactable = true;
            startButtonView.Show();
        }
        SetDescription(descriptionText);
    }

    public void SetupFailView()
    {
        HideAll();
        ShowBase();
        SetTitle("Try Again!");
        SetDescription(":(");
        restartButton.interactable = true;
        restartButtonView.Show();
        menuButton.interactable = true;
        menuButtonView.Show();
    }

    public void SetupWinView()
    {
        HideAll();
        ShowBase();
        SetTitle("Win!");
        SetDescription(":)");
        menuButton.interactable = true;
        menuButtonView.Show();
    }

    private void HideAll()
    {
        background.enabled = false;
        fade.enabled = false;
        title.enabled = false;
        description.enabled = false;
        startButton.interactable = false;
        startButtonView.Hide();
        menuButton.interactable = false;
        menuButtonView.Hide();
        restartButton.interactable = false;
        restartButtonView.Hide();
        closeButton.interactable = false;
        closeButtonView.Hide();
    }
    private void ShowBase()
    {
        background.enabled = true;
        fade.enabled = true;
        title.enabled = true;
        description.enabled = true;
    }

    private void OnStartLevelTap()
    {
        HideAll();
        onStartTap?.Invoke(currenLevelIdx);
    }

    private void OnRestartTap()
    {
        HideAll();
        onRestartTap?.Invoke();
    }

    private void OnMenuTap()
    {
        HideAll();
        onMenuTap?.Invoke();
    }

    private void ResetValues()
    {
        currenLevelIdx = 0;
    }

    private void SetTitle(string title)
    {
        this.title.text = title;
    }

    private void SetDescription(string description)
    {
        this.description.text = description;
    }
}
