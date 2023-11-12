using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxTape : Interaction
{
    [SerializeField] MusicBox musicTask;
    enum Tapes { TapeOne, TapeTwo, TapeThree, TapeSpirits};
    [SerializeField] Tapes cassetteTape;
    public override void onClick()
    {
        if (musicTask == null)
        {
            musicTask = FindFirstObjectByType<MusicBox>();
        }
        musicTask.ClickedTape(this);
    }
    public Item GetTapeItem()
    {
        switch (cassetteTape)
        {
            case Tapes.TapeOne:
                return FindFirstObjectByType<Cassette1>();
            case Tapes.TapeTwo:
                return FindFirstObjectByType<Cassette2>();
            case Tapes.TapeThree:
                return FindFirstObjectByType<Cassette3>();
            default:
                return FindFirstObjectByType<CassetteSpirits>();
        }
    }
}
