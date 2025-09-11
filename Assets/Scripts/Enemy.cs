

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
      string[] wordList = { "word", "rabbit", "stone", "table", "chair", "123", "456", "789", "`0-=", "[]\\", ";'", ",./", "~!@#$", "%^&*()", "_+{}|", ":\"<>?"};
      enemyWord = wordList[Random.Range(0, wordList.Length)];
      wordText.text = enemyWord;
   }

   public string GetWord()
   {
      return enemyWord;
   }

   public void RemoveLetter()
   {
      if (enemyWord.Length > 0)
      {
         enemyWord = enemyWord[1..];
         wordText.text = enemyWord;
      }

      if (enemyWord.Length == 0)
      {
         Die();
      }
   }

   private void Die()
   {
      Destroy(gameObject);
   }
}
