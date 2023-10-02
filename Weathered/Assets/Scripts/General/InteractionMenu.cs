using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionMenu : MonoBehaviour
{
    public Image itemImg;
    public Text descriptionText;
    public Text nameText;

    [SerializeField] PickupItem pickUpItem;
    public float maxTime;
    public float time;
    public bool acceptIsShowing = false;

    ItemBase item;
    ItemHUD itemHUD;

    private void Awake()
    {
        itemHUD = FindAnyObjectByType<ItemHUD>(FindObjectsInactive.Include);
    }

    private void Start()
    {
        time = maxTime;
    }

    private void Update()
    {
        ShowAcceptUI();
    }

    public void DisplayInfo(ItemBase item)
    {
        gameObject.SetActive(true);
        itemImg.sprite = item.InspectIcon;
        descriptionText.text = item.Description;
        nameText.text = item.Name;

        this.item = item;
        itemHUD.gameObject.SetActive(false);
    }

    void ShowAcceptUI()
    {
        if (acceptIsShowing == false)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                pickUpItem.gameObject.SetActive(true);
                time = maxTime;
                acceptIsShowing = true;
            }
        }       
    }

    public ItemBase Item => item;

}
