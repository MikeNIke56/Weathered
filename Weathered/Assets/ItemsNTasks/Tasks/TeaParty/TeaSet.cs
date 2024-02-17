using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaSet : Item
{
    public TeaSetObj teaSetObject;
    TeaParty teaParty;

    [SerializeField] GameObject brokenGlass;
    [SerializeField] AudioSource glassBreakingSfx;

    PlayerController player;

    private void Awake()
    {
        teaParty = FindAnyObjectByType<TeaParty>();
        player = FindAnyObjectByType<PlayerController>();
    }

    public void ClickedTeaSetObject(TeaSetObj teaSetClicked)
    {
        ItemController.AddItemToHand(this);
        teaSetClicked.gameObject.SetActive(false);
    }

    public override void OnDropped()
    {
        ClearItem();
        teaParty.deathTriggered = true;
        var brokenGlassCopy = Instantiate(brokenGlass, player.transform);
        brokenGlassCopy.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - .5f);
        glassBreakingSfx.Play();
    }

    public override void ClearItem()
    {
        base.ClearItem();
        teaSetObject.gameObject.SetActive(true);
    }
}
