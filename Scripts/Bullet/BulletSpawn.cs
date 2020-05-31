using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour {
  private float spawnRate = 0.1f;
  private float timer;
  
  void Update() {
    timer += Time.deltaTime;
    if(timer >= spawnRate) {
      timer = 0;
      Spawn();
    }
  }

  private void Spawn() {
    var bullet = BulletPool.Instance.Get();
    bullet.transform.position = transform.position;
    bullet.transform.rotation = transform.rotation;
    bullet.gameObject.SetActive(true);
  }

}
