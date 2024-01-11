using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Mycolor"))
            PlayerPrefs.SetString("Mycolor", "FFFFFF");
    }

    public void RedSelect()
    {
        //gm.myColor = "FF0000";
        PlayerPrefs.SetString("Mycolor", "FF2F43");
        GameManager.instance.myColor = "FF2F43";
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
        PlayerPrefs.SetString("Mycolor", "85E8FF");
        GameManager.instance.myColor = "85E8FF";
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
