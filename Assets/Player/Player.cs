using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField]
  float jumpSpeed = 10;
  [SerializeField]
  float jumpModifier = 1;
  Rigidbody2D rb;
  Animator an;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    an = GetComponent<Animator>();
  }

  void Update() {
    Flap();
    Debug.Log(rb.velocity);
  }

  void Flap() {
    bool isJumping = Input.GetButtonDown("Jump");
    if (isJumping) {
      rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
      if (!an.GetBool("Flapping"))
        an.Play("Flap-wings", -1, 0f);
      an.SetBool("Flapping", true);
    } else if (rb.velocity.y < -1) {
      an.SetBool("Flapping", false);
    }
  }
}