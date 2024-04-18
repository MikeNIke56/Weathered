using System.Collections;
using UnityEngine;

public class TaxidermyStairs : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Progression.Prog.HasFixedStairs)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Progression.Prog.HasFixedStairs = true;
                collision.gameObject.transform.position = new Vector3(2f, 13.8f, 0f);
                StartCoroutine(FixedStairsDialog());
            }
        }
    }

    IEnumerator FixedStairsDialog()
    {
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();

        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Worker, "Hey");
        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Worker, "Stairs are fixed");

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
    }
}
