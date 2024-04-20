using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour, ISavable
{
    static public GameManager GM;
    static GameObject flashPrefab;
    [SerializeField] GameObject pointFlashPrefab;
    static public bool flashDone = false;
    static float deathShotFadeOut = 2f;
    [SerializeField] AudioSource deathImpactSFX;
    [SerializeField] Texture2D defaultCursor;
    static public PlayerController PC;
    [SerializeField] GameObject deathScreen;

    public bool addedBlockers = false;
    public bool tutorialPlayed = false;
    static bool isDeathDone = false;

    public bool hasReloaded = false;
    Snowglobe placeHolderSG;
    GameObject shelfObj;
    GameObject snowglobesUI;
    GameObject snowglobesUnder;
    GameObject quitButton;
    SnowglobeObj slotOriginal;
    GameObject slotParent;
    GameObject snowGlobesObjUnder;
    List<Snowglobe> snowglobes;
    List<Snowglobe> snowglobeItems = new List<Snowglobe>();

    List<Item> toysItems = new List<Item>();
    List<Item> tapeItems = new List<Item>();
    List<Item> tpItems = new List<Item>();

    private void Awake()
    {
        if (GM != null)
        {
            Destroy(gameObject);
        }
        else
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        flashPrefab = pointFlashPrefab;
        PC = FindFirstObjectByType<PlayerController>();     
    }
    public static void StartDeath(GameObject deathRoot, float animationTimeInSeconds, bool skipFlash)
    {
        PC.StartDeath();
        UIController.UIControl.DeathEventScreen();
        GM.StartCoroutine(DeathAnim(deathRoot, animationTimeInSeconds, skipFlash));
    }

    static IEnumerator DeathAnim(GameObject deathRoot, float animTime, bool skipFlash)
    {
        isDeathDone = false;
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
            isDeathDone = true;
        }
    }

    public void RestartGame()
    {
        hasReloaded = true;
        HandleDeathLoad();
    }

    public void HideDeathScreen()
    {
        deathScreen.SetActive(false);
    }
    public void HandleDeathLoad()
    {
        if(isDeathDone == true)
        {
            SavingSystem.i.Save($"SaveSlot" + ReloadScene.i.slot.ToString());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            UIController.UIControl.OpenBaseUI();
        }
    }

    public void SetGlobeData(Snowglobe placeHolderSG, GameObject shelfObj, GameObject snowglobesUI, GameObject snowglobesUnder,
            GameObject quitButton, SnowglobeObj slotOriginal, GameObject slotParent, GameObject snowGlobesObjUnder, List<Snowglobe> snowglobes)
    {
        if(hasReloaded == false)
        {
            this.placeHolderSG = placeHolderSG;
            this.shelfObj = shelfObj;
            this.slotParent = slotParent;
            this.snowglobesUI = snowglobesUI;
            this.snowglobesUnder = snowglobesUnder;
            this.snowGlobesObjUnder = snowGlobesObjUnder;
            this.quitButton = quitButton;
            this.slotOriginal = slotOriginal;
            this.snowglobes = snowglobes; 
        }
    }
    public void AddItem(Snowglobe item)
    {
        snowglobeItems.Add(item);
    }
    public void ClearGlobeItem()
    {
        snowglobeItems.Clear();
    }
    public List<Snowglobe> GetItems()
    {
        return snowglobeItems;
    }
    public Snowglobe GetPlaceholder()
    {
        return this.placeHolderSG;
    }
    public GameObject GetShelfObj()
    {
        return this.shelfObj;
    }
    public GameObject GetSGUI()
    {
        return this.snowglobesUI;
    }
    public GameObject GetSGUnder()
    {
        return this.snowglobesUnder;
    }
    public GameObject GetSGObjUnder()
    {
        return this.snowGlobesObjUnder;
    }
    public GameObject GetQuitButton()
    {
        return this.quitButton;
    }
    public GameObject GetSlotParent()
    {
        return this.slotParent;
    }
    public SnowglobeObj GetSlotOriginal()
    {
        return this.slotOriginal;
    }
    public List<Snowglobe> GetSnowglobes()
    {
        return this.snowglobes;
    }

    public void SetToyItems(List<Item> items)
    {
        this.toysItems.Clear();

        foreach (var item in items)
            this.toysItems.Add(item);
    }
    public List<Item> GetToyItems()
    {
        return this.toysItems;
    }

    public void SetTapeItems(List<Item> items)
    {
        this.tapeItems.Clear();

        foreach (var item in items)
            this.tapeItems.Add(item);
    }
    public List<Item> GetTapeItems()
    {
        return this.tapeItems;
    }
    public void SetTPItems(Kettle kettle, Cookies cookies, Teabags teabags, TeaSet teaset)
    {
        this.tpItems.Clear();

        this.tpItems.Add(kettle);
        this.tpItems.Add(cookies);
        this.tpItems.Add(teabags);
        this.tpItems.Add(teaset);
    }
    public List<Item> GetTPItems()
    {
        return this.tpItems;
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
