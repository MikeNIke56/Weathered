using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHUD : MonoBehaviour
{
    [SerializeField] Image itemImg;

    public void SetImage(Sprite img)
    {
        itemImg.sprite = img;
    }

    public void ClearImage()
    {
        itemImg = null;
    }
}
