using Unity.Netcode;
using UnityEngine;
using TMPro;

public class Counter : NetworkBehaviour
{
    int count = 0;
    [SerializeField] TextMeshProUGUI counterText;

    [ServerRpc(RequireOwnership = false)]
    private void IncreaseCounterServerRpc()
    {
        count++;
        counterText.text = count.ToString();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DecreaseCounterServerRpc()
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
