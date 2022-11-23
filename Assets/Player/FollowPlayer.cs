using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
  public Transform player;
  float PLAYER_CAMERA_OFFSET = 7;
  float CAMERA_DISTANCE = -10;
  void Start() {

  }


  void Update() {
    // transform.position = player.transform.position + new Vector3(0, 1, -10);
    transform.position = new Vector3(player.transform.position.x + PLAYER_CAMERA_OFFSET, 0, CAMERA_DISTANCE);
  }

}
