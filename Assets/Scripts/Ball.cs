using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    [SerializeField] float destructionTime = 2f;
    void Start()
    {
        Invoke("DestroyBallServerRpc", destructionTime);
    }

    [ServerRpc(RequireOwnership = false)]
    void DestroyBallServerRpc()
    {
        NetworkObject.Despawn(true);
    }   
}
