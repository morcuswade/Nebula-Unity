using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
  [SerializeField]
  public float spawnRate = 1f;
  private float timer;
  
  void Update() {
    timer += Time.deltaTime;
    if(timer >= spawnRate) {
      timer = 0;
      Spawn();
    }
  }

  private void Spawn() {
    var enemy = EnemyPool.Instance.Get();
    enemy.transform.position = transform.position;
    enemy.transform.rotation = transform.rotation;
    enemy.gameObject.SetActive(true);
  }
}
