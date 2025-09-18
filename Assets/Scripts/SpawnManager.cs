using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   [Header("Prefabs")]
   [SerializeField] private GameObject platformPrefab;
   [SerializeField] private GameObject platformGapPrefab;
   [SerializeField] private GameObject platformUpperPrefab;
   /*[SerializeField] private GameObject obstaclePrefab;
   [SerializeField] private GameObject enemyPrefab;*/

   [Header("References")]
   /*[SerializeField] private Transform player;
   [SerializeField] private Transform startGround;*/
   [SerializeField] private float spawnDistance = 10f;
   [SerializeField] private float platformLength = 5f;
   [SerializeField] private float upperPlatformHeight = 3f;

   private Transform player;
   private float lastSpawnX;

   private void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player").transform;
      lastSpawnX = player.position.x - platformLength;

      for (int i = 0; i < 3; i++)
      {
         /*SpawnPlatformSegment();*/
         SpawnPlatform(platformPrefab);
      }
   }

   // Update is called once per frame
   void Update()
   {
      if (player.position.x > lastSpawnX - (spawnDistance * 2))
      {
         float roll = Random.value;

         if (roll < 0.3f)
            // 30% gap bawah
            /*SpawnPlatformSegment();*/
            SpawnPlatform(platformGapPrefab);
         else if (roll < 0.4f)
            // 10% platform atas
            SpawnPlatform(platformUpperPrefab, true);
         else if (roll < 0.5f)
         {
            // 10% platform atas + bawah (normal)
            Vector3 basePos = new Vector3(lastSpawnX + platformLength, -2, 0);

            Instantiate(platformPrefab, basePos, Quaternion.identity);
            Instantiate(platformUpperPrefab, basePos + Vector3.up * upperPlatformHeight, Quaternion.identity);

            lastSpawnX += platformLength;
         }
         else
            // 50% platform bawah
            SpawnPlatform(platformPrefab);
      }
   }

   private void SpawnPlatform(GameObject prefab, bool isUpper = false)
   {
      if (prefab == null) return;

      Vector3 spawnPos = new Vector3(lastSpawnX + platformLength, -2, 0);
      if (isUpper) { spawnPos.y += upperPlatformHeight; }

      Instantiate(prefab, spawnPos, Quaternion.identity);
      lastSpawnX += platformLength;
   }

   /*void SpawnPlatformSegment()
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
   }*/

   /*void SpawnObstacle(Vector3 platformPos)
   {
      Vector3 spawnPos = new Vector3(platformPos.x, platformPos.y + 1f, 0f);
      Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
   }

   void SpawnEnemy(Vector3 platformPos)
   {
      Vector3 spawnPos = new Vector3(platformPos.x, platformPos.y + 1f, 0f);
      Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
   }*/
}
