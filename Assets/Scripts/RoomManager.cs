using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.JoinOrCreateRoom(
            "TestRoom",
            new RoomOptions { MaxPlayers = 10 },
            TypedLobby.Default
        );
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(">>> ODAYA GIRILDI <<<");

        PhotonNetwork.Instantiate(
            "Player",
            Vector3.zero,
            Quaternion.identity
        );
    }
}
