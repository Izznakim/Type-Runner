using TMPro;
using UnityEngine;

/// <summary>
/// Represents a single-letter obstacle in the game.
/// </summary>
public class Obstacle : MonoBehaviour
{
   [SerializeField] private TextMeshPro letterText;

   private float botBound = -7;
   private char obstacleLetter;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      GenerateLetter();
   }

   // Update is called once per frame
   void Update()
   {
      if (transform.position.y < botBound)
      {
         Destroy(gameObject);
      }
   }

   private void GenerateLetter()
   {
      obstacleLetter = (char)('A' + Random.Range(0, 26));
      letterText.text = obstacleLetter.ToString();
   }

   public void OnCorrectType()
   {
      if (letterText != null)
      {
         letterText.color = Color.green;
      }

      //Tambah score
      ScoreManager.instance.AddScore(10);

      // Floating text feedback
      FloatingTextSpawner.instance.SpawnText("+10", transform.position, Color.yellow);

      Destroy(gameObject, 0.1f);
   }

   public char GetLetter()
   {
      return obstacleLetter;
   }
}
