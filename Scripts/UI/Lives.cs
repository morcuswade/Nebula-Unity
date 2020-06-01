using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lives : MonoBehaviour {
  private TextMeshProUGUI livesUi;
  private Player player;

  void Start() {
    player = Player.Instance;
    livesUi = GetComponent<TextMeshProUGUI>();
  }

  void Update() {
      livesUi.text = $"lives: {player.lives}";
  }
}
