using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState {
  public Player _player;
  public StateMachine _stateMachine;

  protected BaseState(Player player, StateMachine stateMachine) {
    _player = player;
    _stateMachine = stateMachine;
  }

  public virtual void OnEnter() {

  }

  public virtual void HandlePhysics() {

  }

  public virtual void HandleCollisions(Collision2D collizion) {

  }

  public virtual void HandleInput() {

  }

  public virtual void HandleUpdate() {

  }

  public virtual void OnExit() {

  }
}
