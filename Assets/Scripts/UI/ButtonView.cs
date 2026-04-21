using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    [SerializeField] protected Image image;
    [SerializeField] private TextMeshProUGUI title;

    public void Show()
    {
        image.enabled = true;
        if (title != null)
        {
            title.enabled = true;
        }
    }

    public void Hide()
    {
        image.enabled = false;
        if (title != null)
        {
            title.enabled = false;
        }
    }
    public void SetTitle(string title)
    {
        if (title != null)
        {
            this.title.text = title;
        }
    }
}