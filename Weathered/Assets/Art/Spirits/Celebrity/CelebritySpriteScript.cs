using UnityEngine;

public class CelebritySpriteScript : MonoBehaviour
{
    [SerializeField] Animator SpriteAnimator;
    [SerializeField] ParticleSystem BallParticles;
    public void GhostBall(bool isBall)
    {
        SpriteAnimator.SetBool("isBall", isBall);
        if (isBall)
        {
            BallParticles.Play();
        }
        else
        {
            BallParticles.Stop();
        }
    }
}
