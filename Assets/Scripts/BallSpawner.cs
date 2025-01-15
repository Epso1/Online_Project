using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BallSpawner : NetworkBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform ballOrigin;
    [SerializeField] float initialForce = 300f;

    public void SpawnBall()
    {
        if (IsServer)
        {
            SpawnBallOnServer();
        }  
        else if (IsClient)
        {
            SpawnBallByClientServerRpc();
        }
    }

    void SpawnBallOnServer()
    {
        GameObject ball = Instantiate(ballPrefab, ballOrigin.position, Quaternion.identity);
        ball.GetComponent<NetworkObject>().Spawn();
        ball.GetComponent<Rigidbody>().AddForce(Vector3.up * initialForce, ForceMode.Impulse);
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnBallByClientServerRpc()
    {
        SpawnBallOnServer();
    }


}
