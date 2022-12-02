using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
  [SerializeField]
  Transform player;
  [SerializeField]
  float playerFollowOffeset;
  [SerializeField]
  float offesetDistance;
  [SerializeField]
  Vector3 defaultPosition;
  void Start() {
    FindObjectOfType<GameStateController>().OnRestartGame += ResetFollow;
  }

  void Update() {
    if (player)
      transform.position = new Vector3(player.transform.position.x + playerFollowOffeset, 0, offesetDistance);
  }

  void ResetFollow() {
    transform.position = defaultPosition;
    player = FindObjectOfType<Player>().transform;
  }
}
