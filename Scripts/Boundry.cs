using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundry : MonoBehaviour
{
  private GameController gameController;
  
  void Start() {
    gameController = GameController.Instance;
  }

  void OnTriggerExit2D(Collider2D otherCollider){
    if (otherCollider.tag == "Bullet") {
        Bullet bullet = otherCollider.gameObject.GetComponent<Bullet>();
        BulletPool.Instance.ReturnToPool(bullet);
    }
    if (otherCollider.tag == "Enemy") {
        Enemy enemy = otherCollider.gameObject.GetComponent<Enemy>();
        EnemyPool.Instance.ReturnToPool(enemy);
    }
    if (otherCollider.tag == "Player") {
        Player.Instance.lives--;
    }
    if (otherCollider.tag == "Terrain") {
      GameObject terrain = otherCollider.gameObject;
      Terrain.SetPosition(terrain, terrain.transform.position + Vector3.right * (gameController.GetMapWidth() + otherCollider.bounds.size.x));
    }
  }
}
