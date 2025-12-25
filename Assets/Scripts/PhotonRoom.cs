using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("✅ Master'a bağlandı");

        PhotonNetwork.JoinOrCreateRoom(
            "TestRoom",
            new RoomOptions { MaxPlayers = 10 },
            TypedLobby.Default
        );
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("🔥 ODAYA GİRİLDİ");
        PhotonNetwork.Instantiate(
            "Player",
            Vector3.zero,
            Quaternion.identity
        );
    }
}
