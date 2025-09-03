using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   [Header("Prefabs")]
   [SerializeField] private GameObject platformPrefab;
   [SerializeField] private GameObject obstaclePrefab;
   [SerializeField] private GameObject enemyPrefab;

   [Header("References")]
   [SerializeField] private Transform player;
   [SerializeField] private Transform startGround;
   [SerializeField] private float spawnOffset = 20f;
   [SerializeField] private float platformLength = 10f;

   private float lastSpawnX;

   private void Start()
   {
      lastSpawnX = startGround.position.x + 5;

      for (int i = 0; i < 3; i++)
      {
         SpawnPlatformSegment();
      }
   }

   // Update is called once per frame
   void Update()
   {
      if (player.position.x > lastSpawnX - (2 * platformLength))
      {
         SpawnPlatformSegment();
      }
   }

   void SpawnPlatformSegment()
   {
      Vector3 spawnPos = new Vector3(lastSpawnX + platformLength, -2f, 0f);
      Instantiate(platformPrefab, spawnPos, Quaternion.identity);

      int rand = Random.Range(0, 10);
      if (rand < 4)
      {
         SpawnObstacle(spawnPos);
      }
      else if (rand < 6)
      {
         SpawnEnemy(spawnPos);
      }

      lastSpawnX += platformLength;
   }

   void SpawnObstacle(Vector3 platformPos)
   {
      Vector3 spawnPos = new Vector3(platformPos.x, platformPos.y + 1f, 0f);
      Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
   }

   void SpawnEnemy(Vector3 platformPos)
   {
      Vector3 spawnPos = new Vector3(platformPos.x, platformPos.y + 1f, 0f);
      Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
   }
}
