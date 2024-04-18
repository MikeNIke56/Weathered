using UnityEngine;
using UnityEngine.UI;

public class ObservationMenu : MonoBehaviour
{
    static public ObservationMenu observeMenu;
    public GameObject visualRoot;
    public Image descBox;
    public Text descText;
    public Text nameText;

    PlayerController player;
    ObservationMenu()
    {
        if (observeMenu == null)
        {
            observeMenu = this;
        }
    }

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
    }
    public void SetDescText(string textToSet)
    {
        descText.text = textToSet;
        if (textToSet.Equals(""))
        {
            descBox.gameObject.SetActive(false);
        }
        else
        {
            descBox.gameObject.SetActive(true);
        }
    }

    public void ClearVisualRoot()
    {
        foreach (Transform childTransform in visualRoot.transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}
