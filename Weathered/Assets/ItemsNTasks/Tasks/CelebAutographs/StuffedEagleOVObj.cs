using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffedEagleOVObj : Interaction
{
    CelebAutoGraphs autoGraphs;
    [SerializeField] StatueHeadObj headObj;
    [SerializeField] float waitTime;

    public override void onClick()
    {
        if (autoGraphs == null)
        {
            autoGraphs = FindFirstObjectByType<CelebAutoGraphs>();
        }
        autoGraphs.ClickedEagleObject(this);
    }

    public IEnumerator DropHead()
    {
        gameObject.tag = "Untagged";
        GetComponent<BoxCollider2D>().enabled = false;
        headObj.gameObject.GetComponent<Rigidbody2D>().gravityScale = 2f;
        headObj.isKillable = true;

        yield return new WaitForSeconds(1f);
        headObj.isKillable = false;

        yield return new WaitForSeconds(waitTime);
        headObj.gameObject.tag = "Interactable";
        headObj.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
}
