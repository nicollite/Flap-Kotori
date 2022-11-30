using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField]
  float speed = 5;

  [SerializeField]
  float jumpSpeed = 10;
  [SerializeField]
  float jumpModifier = 1;
  [SerializeField]
  float maxHeight;

  Rigidbody2D rb;
  Animator an;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    an = GetComponent<Animator>();
  }

  void Update() {
    Move();
    Flap();
  }

  private void Move() {
    rb.velocity = new Vector2(speed, rb.velocity.y);
  }

  void Flap() {
    if (transform.position.y > maxHeight) return;
    bool isJumping = Input.GetButtonDown("Jump");
    if (isJumping) {
      rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * jumpModifier);
      if (!an.GetBool("Flapping"))
        an.Play("Flap-wings", -1, 0f);
      an.SetBool("Flapping", true);
    } else if (rb.velocity.y < -1) {
      an.SetBool("Flapping", false);
    }
  }

  void OnTriggerEnter2D(Collider2D triggerCollider) {
    if (triggerCollider.tag == "sword") {
      Destroy(gameObject);
    }
  }
}
