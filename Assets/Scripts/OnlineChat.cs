using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class OnlineChat : MonoBehaviourPun
{
    public Text chatBubbleText;

    [PunRPC]
    public void RPC_ShowMessage(string msg)
    {
        Debug.Log("💬 RPC GELDİ: " + msg);
        chatBubbleText.text = msg;
    }
}
