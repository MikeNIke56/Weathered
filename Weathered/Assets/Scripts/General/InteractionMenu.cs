using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionMenu : MonoBehaviour
{
    public Image itemImg;
    public Text descriptionText;

    private void Start()
    {

    }

    private void Update()
    {
        HandleUpdate();
    }

    public void DisplayInfo(ItemBase item)
    {
        gameObject.SetActive(true);
        itemImg.sprite = item.InspectIcon;
        descriptionText.text = item.Description;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameObject.SetActive(false);
        }
    }
}
