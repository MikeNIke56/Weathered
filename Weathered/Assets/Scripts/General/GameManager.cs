using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour, ISavable
{
    static public GameManager GM;
    static GameObject flashPrefab;
    [SerializeField] GameObject pointFlashPrefab;
    static public bool flashDone = false;
    static float deathShotFadeOut = 2f;
    [SerializeField] AudioSource deathImpactSFX;
    [SerializeField] Texture2D defaultCursor;

    public bool tutorialPlayed = false;

    void Start()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        //Cursor.visible = false;
        GM = FindFirstObjectByType<GameManager>();
        flashPrefab = pointFlashPrefab;
    }
    public static void StartDeath(GameObject deathRoot, float animationTimeInSeconds, bool skipFlash)
    {
        FindFirstObjectByType<PlayerController>().StartDeath();
        UIController.UIControl.DeathEventScreen();
        GM.StartCoroutine(DeathAnim(deathRoot, animationTimeInSeconds, skipFlash));
    }

    static IEnumerator DeathAnim(GameObject deathRoot, float animTime, bool skipFlash)
    {
        if (deathRoot != null)
        {
            FindFirstObjectByType<Camera>().enabled = false;
            deathRoot.gameObject.SetActive(true);
            yield return new WaitForSeconds(animTime);
        }
        if (!skipFlash)
        {
            Instantiate(flashPrefab);
            yield return new WaitUntil(() => flashDone);
            GM.deathImpactSFX.Play();
        }
        yield return new WaitForEndOfFrame();
        UIController.UIControl.ShowDeathScreen();
        if (deathRoot != null)
        {
            Image deathShotImage = deathRoot.GetComponentInChildren<Image>();
            float deathStep = deathShotFadeOut / 50;
            while (deathShotImage.color.a > 0)
            {
                deathShotImage.color -= new Color(0f, 0f, 0f, 0.02f);
                yield return new WaitForSeconds(deathStep);
            }
        }
    }

    static public void RestartGame()
    {
        SceneManager.LoadScene("TestPlayer");
    }

    public object CaptureState()
    {
        var saveData = new GameManagerSaveData()
        {
            tutorialPlayed = tutorialPlayed,
        };

        return saveData;
    }

    public void RestoreState(object state)
    {
        var saveData = (GameManagerSaveData)state;
        var tutPlayed = saveData.tutorialPlayed;
        this.tutorialPlayed = tutPlayed;
    }
    [Serializable]
    public class GameManagerSaveData
    {
        public bool tutorialPlayed = false;
    }
}
