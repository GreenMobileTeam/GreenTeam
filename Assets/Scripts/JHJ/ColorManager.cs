using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetString("Myscolor", "FFFFFF");
    }

    public void RedSelect()
    {
        //gm.myColor = "FF0000";
        PlayerPrefs.SetString("Mycolor", "FF0000");
    }

    public void YellowSelect()
    {
        //gm.myColor = "FFFF00";
        PlayerPrefs.SetString("Mycolor", "FFFF00");
    }

    public void LimeSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "00FF00");
    }

    public void AquaSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "00FFFF");
    }

    public void BlueSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "0000FF");
    }

    public void MagentaSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "FF00FF");
    }

    public void BlackSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "000000");
    }

    public void GraySelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "808080");
    }

    public void WhiteSelect()
    {
        //gm.myColor = "00FF00";
        PlayerPrefs.SetString("Mycolor", "FFFFFF");
    }
}
