﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityCore {
  namespace Page {
    public class Page : MonoBehaviour {
      public static readonly string FLAG_ON = "On";
      public static readonly string FLAG_OFF = "Off";
      public static readonly string FLAG_NONE = "None";

      public PageType type;
      public bool debug;
      public bool useAnimation;
      public string targetState {get; private set;}

      private Animator m_Animator;
      private bool m_IsOn;
      public bool isOn {
        get {
          return m_IsOn;
        }

        private set {
          m_IsOn = value;
        }
      }

      #region Unity Functions
        private void OnEnable() {
          CheckAnimatorIntegrity();
        }
      #endregion

      #region Public functions
        public void Animate(bool _on) {
          if (useAnimation) {
            m_Animator.SetBool("on", _on);

            StopCoroutine("AwaitAnimation");
            StartCoroutine("AwaitAnimation", _on);
          } else {
            if (!_on) {
              isOn = false;
              gameObject.SetActive(false);
            } else {
              isOn = true;
            }
          }
        }
      #endregion

      #region Private functions
        private IEnumerator AwaitAnimation(bool _on) {
          targetState = _on ? FLAG_ON : FLAG_OFF;

          //wait for animator to reach the target state
          while (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName(targetState)) { 
            yield return null;
          }

          //wait for the animator to finish
          while (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) {
            yield return null;
          }

          targetState = FLAG_NONE;

          Log("Page ["+type+"] finished transitioning to "+(_on ? "on" : "off"));

          if (!_on) {
            isOn = false;
            gameObject.SetActive(false);
          } else {
            isOn = true;
          }
        }

        private void CheckAnimatorIntegrity() {
          if (useAnimation) {
            m_Animator = GetComponent<Animator>();
            if (!m_Animator) {
              LogWarning($"You are animating a page [{type}], but no Animator component exists on the object");
            }
          }
        }

        private void Log(string _msg) {
          if (!debug) return;
          Debug.Log($"[Page]: {_msg}");
        }

        private void LogWarning(string _msg) {
          if (!debug) return;
          Debug.LogWarning($"[Page]: {_msg}");
        }
      #endregion
    }
  }
}
