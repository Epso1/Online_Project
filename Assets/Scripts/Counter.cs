using Unity.Netcode;
using UnityEngine;
using TMPro;

public class Counter : NetworkBehaviour
{
    int count = 0;
    [SerializeField] TMP_Text counterText;

    [ServerRpc(RequireOwnership = false)]
    private void IncreaseCounterServerRpc(ServerRpcParams rpcParams = default)
    {
        count++;
        counterText.text = count.ToString();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DecreaseCounterServerRpc(ServerRpcParams rpcParams = default)
    {
        count--;
        counterText.text = count.ToString();
    }

    public void IncreaseCounter()
    {
        if (IsServer)
        {
            count++;
            counterText.text = count.ToString();
        }
        else if (IsClient)
        {
            IncreaseCounterServerRpc();
        }
    }

    public void DecreaseCounter()
    {
        if (IsServer)
        {
            count--;
            counterText.text = count.ToString();
        }
        else if (IsClient)
        {
            DecreaseCounterServerRpc();
        }
    }
}
