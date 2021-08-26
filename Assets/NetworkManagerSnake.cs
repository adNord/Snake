using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


// Custom NetworkManager that simply assigns the correct racket positions when
// spawning players. The built in RoundRobin spawn method wouldn't work after
// someone reconnects (both players would be on the same side).
[AddComponentMenu("")]
public class NetworkManagerSnake : NetworkManager
{
    public Transform leftSnake;
    public Transform rightSnake;
    GameObject food;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? leftSnake : rightSnake;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        // spawn food if two players
        if (numPlayers == 2)
        {
            food = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Food"));
            NetworkServer.Spawn(food);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy food
        if (food != null)
            NetworkServer.Destroy(food);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}
