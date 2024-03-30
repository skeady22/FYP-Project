using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    // script to allow the camera to follow the player in the X axis without following them in the Y axis when they go up/down
    public class CameraController : MonoBehaviour
    {
        [SerializeField] GameObject player;

        // the original coords of the camera
        Vector3 offset = new Vector3(5,0,-5);

        // use LateUpdate over update so the player is moved before the camera
        void LateUpdate()
        {
            // moves the camera along with the player while keeping the original coords
            transform.position = new Vector3 (player.transform.position.x + offset.x, offset.y, offset.z);
        }
    }
}