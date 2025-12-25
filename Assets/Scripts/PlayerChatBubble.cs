using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerChatBubble : MonoBehaviourPun
{
    public TMP_Text chatText;

    public void ShowMessage(string message)
    {
        chatText.text = message;
        CancelInvoke();
        Invoke(nameof(ClearMessage), 3f);
    }

    void ClearMessage()
    {
        chatText.text = "";
    }
}
