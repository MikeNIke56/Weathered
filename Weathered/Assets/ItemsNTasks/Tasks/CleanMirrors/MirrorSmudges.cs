using UnityEngine;

public class MirrorSmudges : Interaction
{
    [SerializeField] CleanMirrors cleanMirrors;
    public int cleanCount;
    [SerializeField] AudioSource cleanSqueakSfx;
    //public MirrorRange mirror;

    public override void onClick()
    {
        if (cleanMirrors == null)
        {
            cleanMirrors = FindFirstObjectByType<CleanMirrors>();
        }

        cleanSqueakSfx.Play();
        cleanMirrors.ClickedSmudge(this);
    }
}
