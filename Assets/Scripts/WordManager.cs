using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class WordManager : MonoBehaviour
{
   // Update is called once per frame
   void Update()
   {
      var keyboard = Keyboard.current;
      if (keyboard == null) return;

      foreach (KeyControl key in keyboard.allKeys)
      {
         if (key == null) continue;

         if (key.wasPressedThisFrame)
         {
            string keyName = key.displayName;
            if (!string.IsNullOrEmpty(keyName) && keyName.Length == 1)
            {
               char inputChar = char.ToUpper(keyName[0]);
               CheckInput(inputChar);
            }
         }
      }
   }

   public Obstacle FindClosestObstacle()
   {
      Obstacle[] obstacles = FindObjectsByType<Obstacle>(FindObjectsSortMode.None);

      GameObject player = GameObject.FindGameObjectWithTag("Player");
      Obstacle closest = null;

      Vector3 playerPos = player.transform.position;
      float minDistance = Mathf.Infinity;

      foreach (Obstacle obstacle in obstacles)
      {
         float dist = Vector3.Distance(playerPos, obstacle.transform.position);
         if (dist < minDistance)
         {
            minDistance = dist;
            closest = obstacle;
         }
      }

      return closest;
   }

   void CheckInput(char inputChar)
   {
      Obstacle currentObstacle = FindClosestObstacle();

      if (currentObstacle != null && inputChar == currentObstacle.GetLetter())
      {
         Destroy(currentObstacle.gameObject);
      }
   }
}
