using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float runSpeed = 5f;
   [SerializeField] private float jumpForce = 7f;
   [SerializeField] private Rigidbody2D rb;

   private bool isGrounded = true;
   private bool isSliding = false;

   void Update()
   {
      rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);

      if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
      {
         rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
         isGrounded = false;
      }

      if (Keyboard.current.leftCtrlKey.wasPressedThisFrame && !isSliding)
      {
         StartCoroutine(SlideRoutine());
      }
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.CompareTag("Platform"))
      {
         isGrounded = true;
      }
   }

   private IEnumerator SlideRoutine()
   {
      isSliding = true;
      Debug.Log("Slide!");

      yield return new WaitForSeconds(0.5f);

      isSliding = false;
   }
}
