using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   [Header("Prefabs")]
   [SerializeField] private GameObject platformPrefab;
   [SerializeField] private GameObject platformGapPrefab;
   [SerializeField] private GameObject platformUpperPrefab;
   [SerializeField] private GameObject platformUpperGapPrefab;

   [Header("References")]
   [SerializeField] private float spawnDistance = 10f;
   [SerializeField] private float platformLength = 5f;
   [SerializeField] private float upperPlatformHeight = 3f;

   private Transform player;
   private float lastSpawnX;

   private enum PlatformType { Normal, Gap, Upper, UpperGap, ComboNormalUpper, ComboNormalGapUpper }
   private PlatformType lastType = PlatformType.Normal;

   private void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player").transform;
      lastSpawnX = player.position.x - platformLength;

      for (int i = 0; i < 3; i++)
      {
         SpawnPlatform(platformPrefab);
         lastType = PlatformType.Normal;
      }
   }

   // Update is called once per frame
   void Update()
   {
      if (player.position.x > lastSpawnX - (spawnDistance * 2))
      {
         // Random roll untuk variasi platform
         float roll = Random.value;

         // ----- aturan khusus -----
         // Jika terakhir spawn gap bawah, jangan langsung gap atas
         if (lastType == PlatformType.Gap && roll < 0.50f)
         {
            // Paksa normal dulu
            SpawnPlatform(platformPrefab);
            lastType = PlatformType.Normal;
            return;
         }

         if (roll < 0.25f)
         {
            // 25% gap bawah
            SpawnPlatform(platformGapPrefab);
            lastType = PlatformType.Gap;
         }
         else if (roll < 0.35f)
         {
            // 10% platform atas
            SpawnPlatform(platformUpperPrefab, true);
            lastType = PlatformType.Upper;
         }
         else if (roll < 0.45f)
         {
            // 10% platform atas + bawah (normal)
            Vector3 basePos = new Vector3(lastSpawnX + platformLength, -2, 0);

            Instantiate(platformPrefab, basePos, Quaternion.identity);
            Instantiate(platformUpperPrefab, basePos + Vector3.up * upperPlatformHeight, Quaternion.identity);

            lastSpawnX += platformLength;
            lastType = PlatformType.ComboNormalUpper;
         }
         else if (roll < 0.50f)
         {
            // 5% gap atas
            SpawnPlatform(platformUpperGapPrefab, true);
            lastType = PlatformType.UpperGap;
         }
         else if (roll < 0.60f)
         {
            // 10% gap atas + bawah (normal)
            Vector3 basePos = new Vector3(lastSpawnX + platformLength, -2, 0);

            Instantiate(platformPrefab, basePos, Quaternion.identity);
            Instantiate(platformUpperGapPrefab, basePos + Vector3.up * upperPlatformHeight, Quaternion.identity);

            lastSpawnX += platformLength;
            lastType = PlatformType.ComboNormalGapUpper;
         }
         else
         {
            // 40% platform bawah
            SpawnPlatform(platformPrefab);
            lastType = PlatformType.Normal;
         }
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
}
