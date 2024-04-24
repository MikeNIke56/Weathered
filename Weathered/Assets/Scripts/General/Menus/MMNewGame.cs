using UnityEngine;

public class MMNewGame : MonoBehaviour
{
    [SerializeField] MainMenuManager MMM;
    [SerializeField] SpriteRenderer SR;
    [SerializeField] Color32 DefaultColor;
    [SerializeField] Color32 HoverColor;
    [SerializeField] Color32 ClickedColor;
    [SerializeField] GameObject creditsButton;

    void Awake()
    {
        SR.color = DefaultColor;
    }
    void OnMouseEnter()
    {
        SR.color = HoverColor;
    }
    void OnMouseExit()
    {
        SR.color = DefaultColor;
    }

    void OnMouseDown()
    {
        SR.color = ClickedColor;
    }

    void OnMouseUpAsButton()
    {
        SR.color = HoverColor;
        MMM.gameFileSelectUI.SetActive(true);
        creditsButton.SetActive(false);
    }
}
