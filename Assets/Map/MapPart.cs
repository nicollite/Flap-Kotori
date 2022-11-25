using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapPart {
  public List<GameObject> swordPairs;
  public Dictionary<string, Vector3Int> partDestroy;
  System.Action<Object> Destroy;
  public MapPart(List<GameObject> swordPairObj, Dictionary<string, Vector3Int> pd) {
    swordPairs = swordPairObj;
    partDestroy = pd;
  }
}
