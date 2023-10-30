using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObservationMenu : MonoBehaviour
{
    static public ObservationMenu observeMenu;
    public GameObject visualRoot;
    public Image descBox;
    public Text descText;
    public Text nameText;
    ObservationMenu()
    {
        if (observeMenu == null)
        {
            observeMenu = this;
        }
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
        foreach (Transform childTransform in  visualRoot.transform)
        {
            Destroy(childTransform.gameObject);
        }
    }
}
