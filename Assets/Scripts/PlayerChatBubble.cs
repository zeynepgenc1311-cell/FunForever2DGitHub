using Photon.Pun;
using TMPro;
using UnityEngine;
using System.Collections;

public class PlayerChatBubble : MonoBehaviourPun
{
    public TextMeshProUGUI chatText;

    [PunRPC]
    public void RPC_ShowMessage(string msg)
    {
        chatText.text = msg;
        StopAllCoroutines();
        StartCoroutine(ClearAfterSeconds());
    }

    IEnumerator ClearAfterSeconds()
    {
        yield return new WaitForSeconds(3f);
        chatText.text = "";
    }
}
