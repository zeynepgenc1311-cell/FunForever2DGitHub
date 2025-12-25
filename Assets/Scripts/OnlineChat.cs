using Photon.Pun;
using TMPro;
using UnityEngine;

public class OnlineChat : MonoBehaviourPun
{
    public TMP_InputField inputField;

    public void SendMessage()
    {
        if (string.IsNullOrEmpty(inputField.text)) return;

        photonView.RPC(
            "RPC_ShowMessage",
            RpcTarget.All,
            PhotonNetwork.NickName,
            inputField.text
        );

        inputField.text = "";
    }

    [PunRPC]
    void RPC_ShowMessage(string senderName, string message)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            PhotonView pv = player.GetComponent<PhotonView>();

            if (pv != null && pv.Owner.NickName == senderName)
            {
                player.GetComponent<PlayerChatBubble>()
                      .ShowMessage(message);
                break;
            }
        }
    }
}
