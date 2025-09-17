using TMPro;
using UnityEngine;

/// <summary>
/// Displays floating feedback text (e.g., +10, Combo!).
/// </summary>
public class FloatingText : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI text;
   [SerializeField] private float moveSpeed = 1f;
   [SerializeField] private float lifetime = 1f;

   public void Setup(string message, Color color)
   {
      if (text != null)
      {
         text.text = message;
         text.color = color;
      }

      Destroy(gameObject, lifetime);
   }

   // Update is called once per frame
   void Update()
   {
      transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
   }
}
