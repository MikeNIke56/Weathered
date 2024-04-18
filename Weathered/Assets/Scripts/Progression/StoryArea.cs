using UnityEngine;

public class StoryArea : MonoBehaviour
{
    [SerializeField] Progression.StoryAreas StoryAreaTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Progression.Prog.StoryAreaEnter(StoryAreaTag);
        }
    }
}
