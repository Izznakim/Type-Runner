using UnityEngine;

/// <summary>
/// Spawns floating text prefabs at world positions.
/// </summary>
public class FloatingTextSpawner : MonoBehaviour
{
   public static FloatingTextSpawner instance;

   [SerializeField] private GameObject floatingTextPrefab;

   private void Awake()
   {
      if (instance == null) { instance = this; }
   }

   public void SpawnText(string message, Vector3 position, Color color)
   {
      if (floatingTextPrefab == null) return;

      GameObject go = Instantiate(floatingTextPrefab, position, Quaternion.identity);
      FloatingText ft = go.GetComponent<FloatingText>();
      if (ft != null)
      {
         ft.Setup(message, color);
      }
   }
}
