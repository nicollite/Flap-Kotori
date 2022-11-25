using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour {
  [SerializeField]
  public float PART_SIZE = 10;
  [SerializeField]
  public float SWORD_GAP_SIZE = 2f;
  [SerializeField]
  public float DISTANCE_BETWEEN_SWORDS = 2f;
  public float SWORD_SIZE = 2;

  public Camera cam;
  public Tilemap ground;
  public TileBase groundTile;
  public GameObject sword;
  Vector3Int nextNewPartPosition;
  Dictionary<string, Vector3Int> nextPartDestroy = new Dictionary<string, Vector3Int>();
  List<GameObject> oldSwordPairs = new List<GameObject>();

  float camHalfHeight;
  float camHalfWidth;

  public Vector2 ySwordPairSpawnMinMax = new Vector2(0, 0);

  MapPart oldPart;

  void Start() {
    camHalfHeight = cam.orthographicSize;
    camHalfWidth = 2f * cam.orthographicSize * cam.aspect / 2;
    SetNewPartsPosition();
    nextPartDestroy["start"] = new Vector3Int(ground.cellBounds.xMax, ground.cellBounds.y, 0);
    oldPart = new MapPart(null, nextPartDestroy);

  }

  void Update() {
    if (cam.transform.position.x + camHalfWidth > nextNewPartPosition.x) {
      GenerateNewPart();
    }
    if (cam.transform.position.x - camHalfWidth > oldPart.partDestroy["start"].x + 1) {
      DestroyOldPArt();
    }


  }

  void GenerateNewPart() {
    int startX = ground.cellBounds.xMax;
    GenerateGround(startX);

    List<GameObject> swordPairs = oldSwordPairs;
    oldSwordPairs = GenerateSwords(startX);

    oldPart = new MapPart(swordPairs, nextPartDestroy);
  }

  void GenerateGround(int startX) {
    for (int i = 0; i < PART_SIZE; i++) {
      ground.SetTile(new Vector3Int(startX + i, ground.cellBounds.y, ground.cellBounds.z), groundTile);
    }

    SetNewPartsPosition();
  }

  List<GameObject> GenerateSwords(int startX) {
    int numberOfSwordPairs = (int)(PART_SIZE / (DISTANCE_BETWEEN_SWORDS + SWORD_SIZE));
    List<GameObject> swordPairs = new List<GameObject>();
    for (int i = 0; i < numberOfSwordPairs; i++) {
      float x = startX + (i * (DISTANCE_BETWEEN_SWORDS + SWORD_SIZE));
      swordPairs.Add(GenerateSwordPair(x, Random.Range(ySwordPairSpawnMinMax.x, ySwordPairSpawnMinMax.y)));
    }
    return swordPairs;
  }

  GameObject GenerateSwordPair(float x, float y) {
    GameObject upSword = Instantiate(sword, new Vector2(x, -((SWORD_GAP_SIZE / 2))), Quaternion.identity);
    GameObject downSword = Instantiate(sword, new Vector2(x, (SWORD_GAP_SIZE / 2)), Quaternion.identity);
    downSword.transform.localScale = new Vector2(downSword.transform.localScale.x, -downSword.transform.localScale.y);

    GameObject swordPair = new GameObject("sword pair");
    swordPair.transform.position = new Vector2(x, 0);
    upSword.transform.SetParent(swordPair.transform);
    downSword.transform.SetParent(swordPair.transform);
    swordPair.transform.position = new Vector2(swordPair.transform.position.x, y);

    return swordPair;
  }

  void SetNewPartsPosition() {
    nextPartDestroy["start"] = new Vector3Int(nextNewPartPosition.x, nextNewPartPosition.y, nextNewPartPosition.z);
    nextPartDestroy["end"] = new Vector3Int(ground.cellBounds.xMin, ground.cellBounds.y, ground.cellBounds.z);
    nextNewPartPosition = new Vector3Int(ground.cellBounds.xMax, ground.cellBounds.y, ground.cellBounds.z);
  }

  void DestroyOldPArt() {
    foreach (GameObject s in oldPart.swordPairs) {
      Destroy(s.gameObject);
    }

    for (int i = oldPart.partDestroy["start"].x; i >= oldPart.partDestroy["end"].x; i--) {
      ground.SetTile(new Vector3Int(i, -5, 0), null);
    }


    SetNewPartsPosition();
  }
}
