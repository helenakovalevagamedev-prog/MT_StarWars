using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private ButtonView playButtonView;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private SpriteRenderer starsBackground;
    [SerializeField] private SpriteRenderer cloudBackground;
    [SerializeField] private Transform levelButtonsContainer;
    [SerializeField] private InfoPanel infoPanel;

    [Header("Gameplay")]
    [SerializeField] private List<Image> lifesImage;
    [SerializeField] private TextMeshProUGUI scoreAmount;
    [SerializeField] private GameObject gameplayUI;

    private Action<int> onLevelChoosen;
    private Func<int, LevelData> getLevelData;
    private List<LevelButtonView> levelButtonViews;
    private Action onRestartLevelTap;

    public void Init(Func<int, LevelData> getLevelData, int levelsAmount)
    {
        UpdateLifes(lifesImage.Count + 1);
        this.getLevelData = getLevelData;
        CreateLevelButtons(levelsAmount);
        UpdateScore(0);
    }

    public void SetupInfoPanel(Action<int> onLevelChoosen,  Action onRestartLevelTap)
    {
        this.onLevelChoosen = onLevelChoosen;
        this.onRestartLevelTap = onRestartLevelTap;
        infoPanel.Init(OnStartLevelTap, OnPanelMenuTap, OnRestartLevelTap);
    }

    public void UpdateScore(int score)
    {
        scoreAmount.text = score.ToString();
    }

    public void UpdateLifes(int lifes)
    {
        for (int i = 0; i < lifesImage.Count; i++)
        {
            lifesImage[i].enabled = (i + 1) <= lifes;
        }
    }

    private void CreateLevelButtons(int levelsAmount)
    {
        levelButtonViews = new List<LevelButtonView>();
        for (int i = 0; i < levelsAmount; i++)
        {
            var buttonGO = Instantiate(levelButtonPrefab, levelButtonsContainer);
            var button = buttonGO.GetComponent<Button>();
            var view = buttonGO.GetComponent<LevelButtonView>();
            var levelData = getLevelData.Invoke(i);
            int iDx = i;
            button.onClick.AddListener(() => OpenStartInfoPanel(iDx));
            view.Init(i + 1, levelData.State);
            levelButtonViews.Add(view);
        }
    }

    public void OpenStartInfoPanel(int levelIdx)
    {
        var levelData = getLevelData.Invoke(levelIdx);
        infoPanel.SetupStartView(levelIdx, levelData);
    }

    public void OpenGameoverInfoPanel(InfoStates state)
    {
        switch (state)
        {
            case InfoStates.Win:
                infoPanel.SetupWinView();
                break;
            case InfoStates.Fail:
                infoPanel.SetupFailView();
                break;
            case InfoStates.Start:
            case InfoStates.None:
            default:
                break;
        }
    }

    public void ShowStartMenu()
    {
        HideAllUI();
        ShowMenuUI();
        ShowPlayButton();
    }

    public void ShowLevelsMenu()
    {
        HideAllUI();
        ShowMenuUI();
        ShowLevelButtons();
    }

    private void OnStartLevelTap(int levelIdx)
    {
        ShowGameplayUI();
        onLevelChoosen?.Invoke(levelIdx);
    }

    private void OnPanelMenuTap()
    {
        ShowLevelsMenu();
    }

    private void OnRestartLevelTap()
    {
        ShowGameplayUI();
        onRestartLevelTap?.Invoke();
    }

    private void ShowMenuUI()
    {
        starsBackground.enabled = true;
        cloudBackground.enabled = true;
    }

    private void ShowPlayButton()
    {
        playButtonView.Show();
    }

    private void ShowLevelButtons()
    {
        for (int i = 0; i < levelButtonViews.Count; i++)
        {
            levelButtonViews[i].UpdateButtonView(getLevelData.Invoke(i).State);
            levelButtonViews[i].Show();
        }
    }

    private void HideLevelButtons()
    {
        for (int i = 0; i < levelButtonViews.Count; i++)
        {
            levelButtonViews[i].Hide();
        }
    }

    public void ShowGameplayUI()
    {
        HideAllUI();
        UpdateLifes(lifesImage.Count + 1);
        starsBackground.enabled = true;
        gameplayUI.SetActive(true);
    }

    private void HideAllUI()
    {
        HideMainMenu();
        HideGameplayUI();
        playButtonView.Hide();
        HideLevelButtons();
    }

    private void HideMainMenu()
    {
        cloudBackground.enabled = false;
    }

    private void HideGameplayUI()
    {
        gameplayUI.SetActive(false);
    }
}