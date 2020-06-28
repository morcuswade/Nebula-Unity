using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
  private BaseState _currentState;

  public void SetState(BaseState newState) {
    _currentState?.OnExit();
    _currentState = newState;
    _currentState.OnEnter();
  }
  private void FixedUpdate() {
    _currentState.HandlePhysics();
  }

  private void OnCollisionEnter2D(Collision2D collizion) {
    _currentState.HandleCollisions(collizion);
  }

  private void Update() {
    _currentState.HandleInput();
    _currentState.HandleUpdate();
  }
}
