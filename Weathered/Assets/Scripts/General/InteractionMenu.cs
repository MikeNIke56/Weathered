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
    public float maxTime;
    public float time;
    public bool acceptIsShowing = false;

    Task task;
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

    public void DisplayInfo(Task task)
    {
        gameObject.SetActive(true);
        itemImg.sprite = task.InspectIcon;
        descriptionText.text = task.Description;
        nameText.text = task.Name;

        this.task = task;
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
                acceptTask.gameObject.SetActive(true);
                time = maxTime;
                acceptIsShowing = true;
            }
        }       
    }

    public Task Task => task;

}
