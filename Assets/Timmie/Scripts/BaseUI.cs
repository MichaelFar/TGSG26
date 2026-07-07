using UnityEngine;

public class BaseUI : MonoBehaviour
{
    protected CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Show()
    {
        canvasGroup.alpha = 1;
    }

    public virtual void Hide()
    {
        canvasGroup.alpha = 0;
    }
}
