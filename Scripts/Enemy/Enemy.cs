using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  public enum EnemyState { Alive, Hit, Dying, Dead }
  public EnemyState enemyState;
  public Vector2 enemyVelocity = new Vector2(-10f, 0f);
  private float health;
  private Rigidbody2D rigidbody;
  
  void OnEnable() {
    health = 100f;
    rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.velocity = enemyVelocity;
    enemyState = EnemyState.Alive;
  }

  void Update() {
    rigidbody.velocity = enemyVelocity;
    if (health <= 0f) EnemyPool.Instance.ReturnToPool(this);
  }

  void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.tag == "Terrain") return;
    health -= 100;
  }
}