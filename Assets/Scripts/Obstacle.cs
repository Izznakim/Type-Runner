using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
   [SerializeField] TextMeshPro letterText;

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

   public char GetLetter()
   {
      return obstacleLetter;
   }
}
