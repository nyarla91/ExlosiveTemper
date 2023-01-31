using System;
using System.Collections.Generic;
using Extentions;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace UIUtility
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private List<MenuWindow> _windows;
        [SerializeField] private bool _independent = true;
        [SerializeField] private bool _openedAtStart;
        [SerializeField] private bool _pausesTheGame;
        [SerializeField] private MenuWindow _firstMenuWindow;

        public bool IsOpened { get; private set; }
        public MenuWindow CurrentWindow { get; private set; }
        
        public MenuWindow FirstMenuWindow => _firstMenuWindow;
        
        [Inject] protected Pause Pause { get; private set; }
        [Inject] private UINavigationSystem NavigationSystem { get; set; }

        public event Action OnOpen;
        public event Action OnClose;

        public UnityEvent UnityOnOpen;
        public UnityEvent UnityOnClose;

        public void SwitchToWindow(MenuWindow menuWindow)
        {
            if ( ! _windows.Contains(menuWindow) || ! IsOpened)
                return;
            
            foreach (MenuWindow searchedWindow in _windows)
            {
                if (searchedWindow == menuWindow)
                {
                    searchedWindow.Open();
                    CurrentWindow = searchedWindow;
                    continue;
                }
                searchedWindow.Close();
            }
        }
        
        public void Open()
        {
            IsOpened = true;
            if (_independent)
                NavigationSystem.AddMenuOpen(this);
            if (_pausesTheGame)
                Pause.AddPauseSource(this);
            if (_firstMenuWindow)
                SwitchToWindow(_firstMenuWindow);
            OnOpen?.Invoke();
            UnityOnOpen?.Invoke();
        }

        public void Close()
        {
            IsOpened = false;
            if (_independent)
                NavigationSystem.OnMenuClosed(this);
            if (_pausesTheGame)
                Pause.RemovePauseSource(this);
            foreach (MenuWindow searchedWindow in _windows)
            {
                searchedWindow.Close();
            }
            OnClose?.Invoke();
            UnityOnClose?.Invoke();
        }

        protected virtual void Awake()
        {
            foreach (MenuWindow window in _windows)
            {
                window.Menu = this;
            }
        }

        protected virtual void Start()
        {
            Close();
            if (_openedAtStart)
                Open();
        }
    }
}