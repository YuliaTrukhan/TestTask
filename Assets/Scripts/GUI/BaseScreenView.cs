using UnityEngine;

public class BaseScreenView : MonoBehaviour
{

    [SerializeField] private Canvas canvas;
    public virtual void Show()
    {
        canvas.enabled = true;
    }

    public virtual void Hide()
    {
        canvas.enabled = false;
    }
}
