using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    public void Show()
    {
        sprite.enabled = true;
    }

    public void Hide()
    {
        sprite.enabled = false;
    }
}
