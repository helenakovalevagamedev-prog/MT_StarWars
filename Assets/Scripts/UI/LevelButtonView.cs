using UnityEngine;

public class LevelButtonView : ButtonView
{
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Sprite closedSprite;

    public void Init(int number, LevelState state)
    {
        SetTitle($"Level {number}");
        UpdateButtonView(state);
    }

    public void UpdateButtonView(LevelState state)
    {
        image.sprite = state == LevelState.Locked ? closedSprite : openedSprite;
    }
}