using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public Vector3 spawnValues;
  public GameObject[] terrains;
  public Vector2 terrainVelocity;
  public float backgroundSpeed;

  private float mapWidth;

  void Start() {
    SpawnWaves();
  }

  static public GameController Instance { get; private set; }

  void Awake() {
    Instance = this;
  }

  void OnEnable(){
    terrains = GameObject.FindGameObjectsWithTag("Terrain");
    Bounds mapBounds = new Bounds(new Vector3(30f, 0f, 0f), new Vector3(40.0f, 20.0f, 0.0f));
    for (int i = 0; i < terrains.Length; i++) {
        Collider2D terrainCollider = terrains[i].GetComponent<Collider2D>();
        mapBounds.Encapsulate(terrainCollider.bounds);
    }
    mapWidth = mapBounds.size.x;
  }

  void SpawnWaves () {
    Vector3 spawnPosition = new Vector3(60f, Random.Range(-18, 18), 0f);
    Quaternion spawnRotation = Quaternion.identity;
    var enemy = EnemyPool.Instance.Get();
    enemy.transform.position = spawnPosition;
    enemy.transform.rotation = spawnRotation;
    enemy.gameObject.SetActive(true);
  }

  public float GetMapWidth () {
    return this.mapWidth;
  }
}
