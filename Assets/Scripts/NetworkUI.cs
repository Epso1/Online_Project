using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button disconnectButton;
    void Awake()
    {
        serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        clientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
        hostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        disconnectButton.onClick.AddListener(() => NetworkManager.Singleton.Shutdown());
    }    
}
