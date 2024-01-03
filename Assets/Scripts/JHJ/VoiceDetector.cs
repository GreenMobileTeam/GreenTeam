using Photon.Pun;
using UnityEngine;
using Photon.Voice.PUN;
using TMPro;

public class VoiceDetector : MonoBehaviourPunCallbacks
{
    public GameObject micImage;
    //public Image speakerImage;
    public PhotonVoiceView photonVoiceView;
    public TextMeshProUGUI name_;

    private void Awake()
    {
        //micImage = GetComponentInParent<VoiceDebugUI>().transform.Find("Image_Mic").GetComponent<Image>(); 
        this.micImage.SetActive(false);
        //this.speakerImage.enabled = false;
    }

    private void Start()
    {
        name_.text = PhotonNetwork.LocalPlayer.NickName;
    }

    // Update is called once per frame 
    void Update()
    {
        if (this.photonVoiceView.IsRecording)
            this.micImage.SetActive(true);
        else
            this.micImage.SetActive(false);
        //this.speakerImage.enabled = this.photonVoiceView.IsSpeaking;
    }
}
