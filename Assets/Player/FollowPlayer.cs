using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
  public Transform player;
  [SerializeField]
  float playerFollowOffeset;
  [SerializeField]
  float offesetDistance;
  void Start() {

  }


  void Update() {
    if (player)
      transform.position = new Vector3(player.transform.position.x + playerFollowOffeset, 0, offesetDistance);
  }

}
