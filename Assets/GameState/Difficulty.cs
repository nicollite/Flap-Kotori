using UnityEngine;
using System.Collections;

public static class Difficulty {

  static float secondsToMaxDifficulty = 60;
  public static float timeSinceStart = Time.time;


  public static float GetDifficultyPercent() {
    float time = Time.time - timeSinceStart;
    // Debug.Log($"time: {time}");
    // Debug.Log($"percentage: {Mathf.Clamp01(time / secondsToMaxDifficulty) * 100}");
    return Mathf.Clamp01(time / secondsToMaxDifficulty);
  }
  public static void ResetDifficulty() {
    Difficulty.timeSinceStart = Time.time;
  }

}