using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class WordManager : MonoBehaviour
{
   [Header("Typing Window")]
   [SerializeField] private float screenPadding = 1.0f;
   [SerializeField] private float behindTolerance = 0.25f;

   private Transform player;

   private void Start()
   {
      GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
      if (playerObj != null) player = playerObj.transform;

      if (Keyboard.current != null)
         Keyboard.current.onTextInput += OnTextInput;
   }

   private void OnDestroy()
   {
      if(Keyboard.current != null)
         Keyboard.current.onTextInput -= OnTextInput;
   }

   // ----------- TEXT INPUT -----------------
   private void OnTextInput(char c)
   {
      // Semua huruf, angka, simbol, termasuk Shift+angka (@, !, ?)
      char inputChar = char.ToUpper(c);
      CheckInput(inputChar);
   }

   // ----------------- CAMERA WINDOW FILTER ----------------
   bool IsInCameraWindow(Vector3 worldPos)
   {
      Camera cam = Camera.main;
      if (cam == null) return true;

      // batas kamera (orthographic)
      float halfH = cam.orthographicSize;
      float halfW = halfH * cam.aspect;

      float left = cam.transform.position.x - halfW - screenPadding;
      float right = cam.transform.position.x + halfW + screenPadding;
      float bottom = cam.transform.position.y - halfH - screenPadding;
      float top = cam.transform.position.y + halfH + screenPadding;

      // harus di dalam kamera (+padding)
      if (worldPos.x < left || worldPos.x > right || worldPos.y < bottom || worldPos.y > top)
      {
         return false;
      }

      if (worldPos.x < player.position.x - behindTolerance) return false;

      return true;
   }


   // ------------------ FIND CLOSEST -----------------
   public Obstacle FindClosestObstacle()
   {
      Obstacle[] obstacles = FindObjectsByType<Obstacle>(FindObjectsSortMode.None);
      if (player == null) return null;

      Vector3 playerPos = player.position;

      Obstacle closest = null;
      float minDistance = Mathf.Infinity;

      foreach (Obstacle obstacle in obstacles)
      {
         if (obstacle == null) continue;

         Vector3 pos = obstacle.transform.position;
         if (!IsInCameraWindow(pos)) continue;

         float dist = Vector3.Distance(playerPos, pos);
         if (dist < minDistance)
         {
            minDistance = dist;
            closest = obstacle;
         }
      }
      
      return closest;
   }

   public Enemy FindClosestEnemy()
   {
      Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
      if (player == null) return null;

      Vector3 playerPos = player.position;

      Enemy closest = null;
      float minDistance = Mathf.Infinity;

      foreach (Enemy enemy in enemies)
      {
         if (enemy == null) continue;

         Vector3 pos = enemy.transform.position;
         if (!IsInCameraWindow(pos)) continue;

         float dist = Vector3.Distance(playerPos, pos);
         if (dist < minDistance)
         {
            minDistance = dist;
            closest = enemy;
         }
      }

      return closest;
   }

   // ---------- CHECK INPUT ------------------
   void CheckInput(char inputChar)
   {
      // 1. Obstacle
      Obstacle currentObstacle = FindClosestObstacle();
      if (currentObstacle != null && inputChar == currentObstacle.GetLetter())
      {
         currentObstacle.OnCorrectType();
         return;
      }
      // 2. Enemy
      Enemy currentEnemy = FindClosestEnemy();
      if (currentEnemy != null)
      {
         string word = currentEnemy.GetWord();
         if (!string.IsNullOrEmpty(word) && inputChar == char.ToUpper(word[0]))
         {
            currentEnemy.RemoveLetter();
         }
      }
   }
}
