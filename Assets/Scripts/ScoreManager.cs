using TMPro;
using UnityEngine;

/// <summary>
/// Manages player score and combo system.
/// </summary>
public class ScoreManager : MonoBehaviour
{
   public static ScoreManager instance;

   [Header("UI References")]
   [SerializeField] private TextMeshProUGUI scoreText;
   [SerializeField] private TextMeshProUGUI comboText;

   private int score = 0;
   private int combo = 0;
   private float comboTimer = 0f;
   [SerializeField] private float comboDuration = 2f; // waktu max antar ketikan biar combo lanjut

   private void Awake()
   {
      if (instance == null) instance = this;
   }

   // Update is called once per frame
   void Update()
   {
      // reset combo kalau kelamaan
      if (combo > 0)
      {
         comboTimer -= Time.deltaTime;
         if (comboTimer < 0)
         {
            combo = 0;
            UpdateUI();
         }
      }
   }

   public void AddScore(int baseScore)
   {
      combo++;
      comboTimer = comboDuration;

      int finalScore = baseScore * Mathf.Max(1, combo);
      score += finalScore;

      UpdateUI();
   }

   private void UpdateUI()
   {
      if (scoreText != null) scoreText.text = $"Score: {score}";
      if (comboText != null) comboText.text = combo > 0 ? $"Combo x{combo}" : "";
   }
}
