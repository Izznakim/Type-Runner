using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
   [SerializeField] private float offsetBehindPlayer = -15f;

   private Transform player;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      player = GameObject.FindGameObjectWithTag("Player").transform;
   }

   // Update is called once per frame
   void Update()
   {
      if (player == null) return;

      if (transform.position.x < player.position.x + offsetBehindPlayer)
      {
         Destroy(gameObject);
      }
   }
}
