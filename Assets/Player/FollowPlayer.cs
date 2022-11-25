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
    if (player)
      transform.position = new Vector3(player.transform.position.x + PLAYER_CAMERA_OFFSET, 0, CAMERA_DISTANCE);
  }

}
