using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestigateMenu : MonoBehaviour
{
    static public InvestigateMenu investMenu;
    public GameObject visualRoot;
    public Image descBox;
    public Text descText;
    public Text nameText;
    InvestigateMenu()
    {
        if (investMenu == null)
        {
            investMenu = this;
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
        while (visualRoot.transform.childCount > 0)
        {
            Destroy(visualRoot.transform.GetChild(0).gameObject);
        }
    }
}
