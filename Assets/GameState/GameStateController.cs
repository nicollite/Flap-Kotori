using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour {
  GameObject canvas;
  public GameObject player;
  [SerializeField]
  public Vector2 playerStartPosition = new Vector2(-7, 0.5f);
  public event System.Action OnRestartGame;
  bool checkInput;
  void Start() {
    canvas = FindObjectOfType<Canvas>(true).gameObject;
    print($"canvas.activeSelf: {canvas.activeSelf}");

    if (SceneManager.GetActiveScene().name == "Game")
      RestartGame();
  }

  void Update() {
    if (checkInput) {
      if (Input.GetKeyDown(KeyCode.Space)) {
        RestartGame();
        checkInput = false;
      }
    }
  }

  public void RestartGame() {
    print("RestartGame");
    checkInput = false;
    canvas.SetActive(false);
    Difficulty.ResetDifficulty();
    InstantiatePlayer();
    SetOnPlayerDeath();
    if (OnRestartGame != null) OnRestartGame();
  }

  void InstantiatePlayer() {
    Instantiate(player, playerStartPosition, Quaternion.identity);
  }

  void SetOnPlayerDeath() {
    FindObjectOfType<Player>().OnPlayerDeath += OnPlayerDeath;
  }

  void OnPlayerDeath() {
    canvas.SetActive(true);
    checkInput = true;
  }
}
