using UnityEngine;

public class StatueHeadObj : Interaction
{
    [SerializeField] StatueHead statueHead;
    public bool isKillable = false;
    CelebAutoGraphs autoGraphs;

    [SerializeField] AudioSource bonkSfx;
    [SerializeField] AudioSource hitFloorSfx;

    private void Start()
    {
        autoGraphs = FindAnyObjectByType<CelebAutoGraphs>();
    }

    public override void onClick()
    {
        if (statueHead == null)
        {
            statueHead = FindFirstObjectByType<StatueHead>();
        }
        statueHead.ClickedStatueObject(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isKillable == true)
            if (collision.gameObject.tag == "Player")
            {
                bonkSfx.Play();
                hitFloorSfx.volume = 0f;
                autoGraphs.OnFailed();
            }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Untagged")
            hitFloorSfx.Play();
    }
}
