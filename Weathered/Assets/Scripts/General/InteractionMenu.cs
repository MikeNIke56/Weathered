using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionMenu : MonoBehaviour
{
    public Image itemImg;
    public Text descriptionText;
    public Text nameText;

    [SerializeField] StartTask acceptTask;
    [SerializeField] GameObject pickupItem;
    public float maxTime;
    public float time;
    public bool acceptIsShowing = false;

    public Item item;
    ItemHUD itemHUD;

    PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>(FindObjectsInactive.Include);
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

    public void DisplayInfo(Task task)
    {
        gameObject.SetActive(true);
        itemImg.sprite = task.InspectIcon;
        descriptionText.text = task.Description;
        nameText.text = task.Name;

        //this.task = task;
        itemHUD.gameObject.SetActive(false);
    }
    public void DisplayInfo(Item item)
    {
        gameObject.SetActive(true);
        itemImg.sprite = item.OverWorldIcon;
        descriptionText.text = item.Description;
        nameText.text = item.Name;

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
                pickupItem.SetActive(true);
                time = maxTime;
                acceptIsShowing = true;
            }
        }       
    }

    public void Yes()
    {
        //picks up item
        ResetElements();

        itemHUD.SetImage(item.OverWorldIcon);

        player.curItem = item;

        //deletes item or removes from scene afterwards
    }

    public void No()
    {
        //exits interaction menu       
        ResetElements();
    }

    void ResetElements()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(false);
        acceptIsShowing = false;
        time = maxTime;
        itemHUD.gameObject.SetActive(true);
    }
}
