using TMPro;
using UnityEngine;

public class PeopleUpdate : MonoBehaviour
{
    public TextMeshProUGUI pt;
    int btnN;

    private void Start()
    {
        string n = this.gameObject.name;
        btnN = int.Parse(n.Substring(n.Length - 1, 1));
    }

    void Update()
    {
        pt.text = PlayerPrefs.GetInt("Room" + btnN) + " / 10";
    }
}
