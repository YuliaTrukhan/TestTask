
using UnityEngine;
using UnityEngine.UI;

public class ObjectIconView : MonoBehaviour
{
    [SerializeField] private Image objectIconImage;

    public void Init(Sprite icon)
    {
        objectIconImage.sprite = icon;
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
