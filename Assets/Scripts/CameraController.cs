using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // this variable is assigned to the player object in the scene
    [SerializeField] private Transform player;

    private void Update()
    {
        // make camera follow the character/player
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
