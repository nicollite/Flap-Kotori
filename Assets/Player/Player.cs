using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField]
  float speed = 5;
  public Vector2 speedMinMax;

  [SerializeField]
  float jumpSpeed = 10;
  [SerializeField]
  float maxHeight;
  [SerializeField]
  bool enableDeath = true;
  Rigidbody2D rb;
  Animator an;

  List<string> deathTags = new List<string>();
  public event System.Action OnPlayerDeath;
  void Start() {
    rb = GetComponent<Rigidbody2D>();
    an = GetComponent<Animator>();
    deathTags.Add("sword");
    deathTags.Add("ground");

  }

  void Update() {
    Move();
    Flap();
  }

  private void Move() {
    speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent());
    // print($"speed: {speed}");
    rb.velocity = new Vector2(speed, rb.velocity.y);
  }

  void Flap() {
    if (transform.position.y > maxHeight) return;
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

  void OnTriggerEnter2D(Collider2D triggerCollider) {
    if (enableDeath && deathTags.Contains(triggerCollider.tag)) {
      if (OnPlayerDeath != null) OnPlayerDeath();
      Destroy(gameObject);
      print($"speed: {speed}");
    }
  }
}
