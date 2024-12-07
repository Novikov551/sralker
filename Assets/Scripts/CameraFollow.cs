using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // префаб игрока
    private Vector3 playerVector;
    public int Speed;

    void Update()
    {
        if (player != null)
        {
            playerVector = player.position;
            playerVector.z = playerVector.z - 10;


            transform.position = new Vector3(playerVector.x, playerVector.y, playerVector.z);
        }
    }
}
