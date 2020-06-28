using UnityEngine;

public class BoundryHorizontal : MonoBehaviour
{
  private GameController game;
  private BoxCollider2D m_Collider;
  
  void Start() {
    game = GameController.Instance;
    m_Collider = GetComponent<BoxCollider2D>();
    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 200f, 0f);
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

    if (otherCollider.tag == "Terrain") {
      GameObject terrain = otherCollider.gameObject;
      terrain.transform.position += Vector3.right * game.mapWidth;
    }

    if (otherCollider.tag == "BackgroundHorizontal") {
      GameObject bg = otherCollider.gameObject;
      bg.transform.position += Vector3.right * game.backgroundWidth;
    }
  }
}
