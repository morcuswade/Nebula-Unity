using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore {
  namespace Page {
    public class PageController : MonoBehaviour {

      static public PageController instance;

      public bool debug;
      public Page[] pages;

      private PageType m_CurrentPage = PageType.MainMenu;
      private Hashtable m_Pages;

      public PageType currentPage {
        get { return m_CurrentPage; }
        private set { m_CurrentPage = value; }
      }

#region Unity Functions
    private void Awake() {
      if (!instance) {
        instance = this;
        m_Pages = new Hashtable();

        RegisterAllPages();
        // TurnPageOn(currentPage);
        DontDestroyOnLoad(gameObject);
      } else {
        Destroy(gameObject);
      }
    }
#endregion



#region Public Functions
      public void TurnPageOn(PageType _type) {
        if (_type == PageType.None) return;
        if (!PageExists(_type)) {
          LogWarning($"You are trying to turn a page [{_type}] on that doesn't exist.");
          return;
        }

        Page _page = GetPage(_type); 
        _page.gameObject.SetActive(true);
        _page.Animate(true);
        currentPage = _type;
      }

      public void TurnToPage(PageType newPage=PageType.None, bool _waitForExit=false) {
        if (newPage == PageType.None) {
          LogWarning($"newPage could not be loaded");
          return;
        }

        Page _offPage = GetPage(m_CurrentPage);
        if (_offPage.gameObject.activeSelf) {
          _offPage.Animate(_waitForExit);
        }

        if (newPage != PageType.None) {
          Page _onPage = GetPage(newPage);
          if (_waitForExit) {
            StopCoroutine("WaitForPageExit");
            StartCoroutine(WaitForPageExit(_onPage, _offPage));
          } else {
            TurnPageOn(newPage);
          }
        }
      }

      public void TurnPageOff(PageType _off, PageType _on=PageType.None, bool _waitForExit=false) {
        if (_off == PageType.None) return;
        if (!PageExists(_off)) {
          LogWarning($"You are trying to turn a page [{_off}] off that has not been registered");
          return;
        }

        Page _offPage = GetPage(_off);
        if (_offPage.gameObject.activeSelf) {
          _offPage.Animate(false);
        }

        if (_on != PageType.None) {
          Page _onPage = GetPage(_on);
          if (_waitForExit) {
            StopCoroutine("WaitForPageExit");
            StartCoroutine(WaitForPageExit(_onPage, _offPage));
          } else {
            TurnPageOn(_on);
          }
        }
      }

      public bool PageIsOn(PageType _type) {
        if (!PageExists(_type)){
          LogWarning($"You are trying to detect if a page [{_type}] is on, but it has not been registered.");
          return false;
        }

        return GetPage(_type).isOn;
      }
#endregion

#region Private functions
      private IEnumerator WaitForPageExit(Page _on, Page _off) {
        while (_off.targetState != Page.FLAG_NONE) {
          yield return null;
        }

        TurnPageOn(_on.type);
      }
      private bool PageExists(PageType _type) {
        return m_Pages.ContainsKey(_type);
      }
      private void RegisterAllPages() {
        foreach(Page _page in pages) {
          RegisterPage(_page);
        }
      }
      private void RegisterPage(Page _page) {
        if(PageExists(_page.type)) {
          LogWarning($"You are trying to register a page [{_page.type}] that has already been registered: {_page.gameObject.name}");
          return;
        }

        m_Pages.Add(_page.type, _page);
        Log($"Registered new page [{_page.type}]");
      }
      private Page GetPage(PageType _type) {
        if (!PageExists(_type)) {
          LogWarning($"You are trying to get a page [{_type}] that has not been registered");
          return null;
        }
        
        return (Page)m_Pages[_type];
      }
      private void Log(string _msg) {
        if (!debug) return;
        Debug.Log($"[PageController]: {_msg}");
      }
      private void LogWarning(string _msg) {
        if (!debug) return;
        Debug.LogWarning($"[PageController]: {_msg}");
      }
#endregion
    }
  }
}
