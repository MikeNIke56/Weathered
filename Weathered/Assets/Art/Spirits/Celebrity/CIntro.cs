using UnityEngine;

public class CIntro : MonoBehaviour
{
    public void GhostForm(int isBall)
    {
        if (isBall > 0)
        {
            GetComponentInChildren<CelebritySpriteScript>().GhostBall(true);
        }
        else
        {
            GetComponentInChildren<CelebritySpriteScript>().GhostBall(false);
        }

    }

    public void MoveCeleb()
    {
        GetComponent<Animator>().SetTrigger("goDesk");
    }
}
