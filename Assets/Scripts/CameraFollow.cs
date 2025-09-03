using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   [SerializeField] private Transform player;
   [SerializeField] private float offsetX = 3f;

   void LateUpdate()
   {
      Vector3 newPos = transform.position;
      newPos.x = player.position.x + offsetX;
      transform.position = newPos;
   }
}
