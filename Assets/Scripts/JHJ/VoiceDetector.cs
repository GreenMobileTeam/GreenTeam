using Photon.Pun;
using UnityEngine;
using Photon.Voice.PUN;
using TMPro;
using System.Collections;

public class VoiceDetector : MonoBehaviourPunCallbacks
{
    public GameObject micImage;
    public GameObject talkImg;
    public GameObject chatBox_;
    public TextMeshProUGUI chat;
    //public Image speakerImage;
    public PhotonVoiceView photonVoiceView;
    public TextMeshProUGUI name_;

    bool flag = false;

    private void Awake()
    {
        //micImage = GetComponentInParent<VoiceDebugUI>().transform.Find("Image_Mic").GetComponent<Image>(); 
        this.micImage.SetActive(false);
        //this.speakerImage.enabled = false;
    }

    private void Start()
    {
        name_.text = PhotonNetwork.LocalPlayer.NickName;
        talkImg.SetActive(false);
        chatBox_.SetActive(false);
    }

    // Update is called once per frame 
    void Update()
    {
        if (this.photonVoiceView.IsRecording)
            this.micImage.SetActive(true);
        else
            this.micImage.SetActive(false);
        //this.speakerImage.enabled = this.photonVoiceView.IsSpeaking;

        if(PlayerPrefs.GetInt("IsChatting") == 1)
        {
            talkImg.SetActive(true);
        }
        else
        {
            talkImg.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StopAllCoroutines();
            StartCoroutine(startChat());
        }
    }

    IEnumerator startChat()
    {
        chatBox_.SetActive(true);
        chat.text = PlayerPrefs.GetString("Chat");
        yield return new WaitForSecondsRealtime(3f);
        chatBox_.SetActive(false);
        chat.text = "";
    }
}
