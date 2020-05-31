using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
  private GameController gameController;
  private float tileWidth;
  private Vector3 startPosition;

  void Start() {
    startPosition = transform.position;
    tileWidth = transform.localScale.x;
    gameController = GameController.Instance;
  }

  void Update()
  {
    float newPosition = Mathf.Repeat(gameController.backgroundSpeed * Time.time, tileWidth);
    transform.position = startPosition + Vector3.left * newPosition;
  }
}
