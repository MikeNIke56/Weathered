using System.Collections;
using UnityEngine;

public class ArvinLogic : Interaction
{
    CelebAutoGraphs autoGraphs;
    public int stage = 0;
    public bool isSaved = false;
    bool JustGaveRose = false;
    bool HeardRoseTalk = false;
    bool IsTalking = false;

    [SerializeField] ArvinAutograph autograph;

    private void Start()
    {
        autoGraphs = FindAnyObjectByType<CelebAutoGraphs>();
    }
    public override void onClick()
    {
        if (!IsTalking)
        {
            IsTalking = true;
            if (ItemController.itemInHand is BloodyRose && !autoGraphs.requirementsMet[1])
            {
                JustGaveRose = true;
                autoGraphs.requirementsMet[1] = true;
            }
            StartCoroutine(TalkToArvin(stage));
        }
    }
    IEnumerator TalkToArvin(int stage)
    {
        GameManager.PC.moveBlockers["Cutscene"] = true;
        UIController.UIControl.OpenDialog();
        DialogManager.Instance.OpenDialog();
        if (JustGaveRose && !HeardRoseTalk)
        {
            JustGaveRose = false;
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Oh? A rose from a fan?");
            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "How could I resist~");
        }
        else
        {
            switch (stage)
            {
                case 0:
                    autoGraphs.OnInProgress();
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "A star! No other way to describe him!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "The amazing! Spectacular!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Arvin Nova!!!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Who?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Destroy me, why don't you.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Is it you?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Not anymore!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I-");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Yes, it is me. And between you and me, a living being that doesn't know who I am is rare.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "But you don't look like that.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "As perceptive as you are... I'm retired~");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Naturally, I'd look a little different from how I was in my prime.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Are you sure?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Yes! And a legacy befit me!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "A room dedicated to my success, with fans that came to gape in awe!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Okay...");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "And I don't do autographs anymore.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Y-");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "They are worth millions now! The only 'fans' of mine asking for one must prove themselves to me.");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I don't-");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "OH, sure! Of course you want one!");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "How about... Hmmm.... Make sure the mirrors around the store are clean, yes?");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "That comment about not recognizing me has gotten me a little worried about my appearance...");
                    yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "...");
                    this.stage = 1;
                    break;
                case 1:
                    if (autoGraphs.requirementsMet[0] == false)
                    {
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I can tell that mirrors still need to be cleaned.");
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "I won't settle for just one! They are untrustworthy, you know~");
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "If I am truly to see my visage, I will need plenty of variety.");
                    }
                    else
                    {
                        if (!autoGraphs.requirementsMet[1])
                        {
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Right!");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "You got me worried for nothing... I look great!");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I'm not sure...");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Well if you are truly conflicted then I suppose my one flaw may be my lacking charm...");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "A rose!");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "A perfect flower to suit my allure~");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I thought roses were bad flowers?");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "That is the charm of them! Their thorns only protect their beauty.");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "You talk a lot. Hehe-");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "... Here's your autograph.");
                        }
                        else
                        {
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Right!");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "You got me worried for nothing... I look great!");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "And an autograph for good measure. So you could truly affirm my likeness.");
                        }
                        HeardRoseTalk = true;
                        this.stage = 2;
                        autograph.GiveAutographObject();
                    }
                    break;
                case 2:
                    if (autoGraphs.requirementsMet[1] == false)
                    {
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Only a perfect rose will sate me now~");
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "You're silly.");
                    }
                    else
                    {
                        if (!autoGraphs.requirementsMet[2])
                        {
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "A wonderful rose to perfectly frame my stardom.");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Hmmmm....");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "No! You must'nt.");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Hmmmmmmmmm....");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "It cannot be me! I am perfect right now! Truly!");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I don't-");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Wait! It must be the statue, then!");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Only by seeing my image in full rendering will you fully understand!");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Hehe, okay!");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Oh, and take this for the trouble- No, the privilege!");
                        }
                        else
                        {
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "A wonderful rose to perfectly frame my stardom.");
                            yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Oh, and take this for the trouble- No, the privilege!");
                        }
                        this.stage = 3;
                        autograph.GiveAutographObject();
                    }
                    break;
                case 3:
                    if (autoGraphs.requirementsMet[2] == false)
                    {
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Ahead, there is nothing. I cannot see ahead, because I have... No head.");
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "...?");
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "A head. My statue needs a head. Wax, preferably.");
                    }
                    else
                    {
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "So?");
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "I guess you are a star!");
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "All along, I have been!");
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Hehe-");
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Celebrity, "Here, a great reward for you efforts.");
                        this.stage = 4;
                        autograph.GiveAutographObject();
                        yield return DialogManager.Instance.ShowDialog(DialogManager.DialogTriggers.Mazarine, "Thank you!");
                    }
                    break;
                case 4:
                    Debug.Log("Complete Arving Logic");
                    break;
                default:
                    Debug.Log("fail");
                    break;
            }
        }

        DialogManager.Instance.CloseDialog();
        UIController.UIControl.CloseDialog();
        GameManager.PC.moveBlockers["Cutscene"] = false;
        IsTalking = false;
    }
}
