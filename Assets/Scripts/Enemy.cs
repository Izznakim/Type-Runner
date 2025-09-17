

using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] TextMeshPro wordText;
   private string enemyWord;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      GenerateWord();
   }

   // Update is called once per frame
   void Update()
   {

   }

   private void GenerateWord()
   {
      string[] wordList = { "word", "rabbit", "stone", "table", "chair", "123", "`0-=", "~!@#$" };
      enemyWord = wordList[Random.Range(0, wordList.Length)];
      wordText.text = enemyWord;
   }

   public string GetWord()
   {
      return enemyWord;
   }

   public void RemoveLetter()
   {
      if (!string.IsNullOrEmpty(enemyWord))
      {
         enemyWord = enemyWord[1..];
         if (wordText != null) wordText.text = enemyWord;

         // Score tiap huruf
         ScoreManager.instance.AddScore(20);

         // Floating text feedback
         FloatingTextSpawner.instance.SpawnText("+20", transform.position, Color.yellow);

         if (enemyWord.Length == 0)
         {
            // Floating text feedback
            FloatingTextSpawner.instance.SpawnText("Enemy Down!", transform.position, Color.red);
            Die();
         }
      }

   }

   private void Die()
   {
      Destroy(gameObject);
   }
}
