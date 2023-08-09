using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private WinMenu _winMenu;
    [SerializeField] private LoseMenu _loseMenu;
    [SerializeField] private PopUp _leaderboard;

    [Header("PerkPanel")] 
    [SerializeField] private PerksPanel _perksPanel;
    
    [Header("UI elements to hide")] 
    [SerializeField] private RectTransform[] _elementsToDragUp;
    [SerializeField] private RectTransform[] _elementsToDragDown;
    [SerializeField] private RectTransform _moneyIndicator;
    [SerializeField] private RectTransform _preparingMenu;

    [Header("Warnings")] 
    [SerializeField] private PopUp _notEnoughRobbers;
    [SerializeField] private PopUp _notEnoughMoney;
    [SerializeField] private PopUp _allSlotsBusy;
    [SerializeField] private PopUp _notAuthorized; 
        
    [Header("Pulsating buttons")]
    [SerializeField] private RectTransform _robButton;

    [Header("Events Sources")] 
    [SerializeField] private Robbery _robbery;
    [SerializeField] private RobStarter _robStarter;
    [SerializeField] private Shop _shop;
    // [SerializeField] private AspectRatioChecker _aspectRatioChecker;

    [Header("PlayerData")] 
    [SerializeField] private PlayerData _playerData;

    private WaitForSecondsRealtime _waitForAnimationDuration = new WaitForSecondsRealtime(MenuAnimator.AnimationDuration);
    private Tween _adButtonTween;

    private void OnEnable()
    {
        _robbery.BankRobbed += OnBankRobbed;
        _robbery.BankNotRobbed += OnBankNotRobbed;
        _robStarter.NotEnoughRobbers += OnNotEnoughRobbers;
        _shop.NotEnoughMoney += OnNotEnoughMoney;
        _robStarter.Started += OnStarted;
        _shop.AllSlotsBusy += OnAllSlotsBusy;
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= OnBankRobbed;
        _robbery.BankNotRobbed -= OnBankNotRobbed;
        _robStarter.NotEnoughRobbers -= OnNotEnoughRobbers;
        _shop.NotEnoughMoney -= OnNotEnoughMoney;
        _robStarter.Started -= OnStarted;
        _shop.AllSlotsBusy -= OnAllSlotsBusy;
    }

    private void Start()
    {
        if (_robButton != null)
        {
            MenuAnimator.PulsateButton(_robButton);
        }
    }

    public void Show(PopUp menu)
    {
        StopTime();
        menu.gameObject.SetActive(true);
        MenuAnimator.FadeIn(menu.Dimed);
        MenuAnimator.DragMenuDown(menu.Popup);
    }

    public void Close(PopUp menu)
    {
        ContinueTime();
        MenuAnimator.DragMenuUp(menu.Popup);
        MenuAnimator.FadeOut(menu.Dimed);
        StartCoroutine(DisableIn(_waitForAnimationDuration, menu));
    }

    public void ShowEndgameMenu(WinMenu winMenu)
    {
        HideGameElements();
        winMenu.gameObject.SetActive(true);
        MenuAnimator.FadeIn(winMenu.Dimed);
        MenuAnimator.MoveWinTitle(winMenu.WinPanel, winMenu.WinTitle);
        MenuAnimator.ZoomInElement(winMenu.NewItem);
        
        MenuAnimator.ZoomInElement(winMenu.NextButtonMobile);
        MenuAnimator.ZoomInAndPulsateButton(winMenu.ADButtonMobile);
        MenuAnimator.ZoomInElement(winMenu.NextButtonDesktop);
        MenuAnimator.ZoomInAndPulsateButton(winMenu.ADButtonDesktop);
    }

    public void ShowEndgameMenu(LoseMenu loseMenu)
    {
        HideGameElements();
        loseMenu.gameObject.SetActive(true);
        MenuAnimator.FadeIn(loseMenu.Dimed);
        MenuAnimator.MoveWinTitle(loseMenu.LossPanel, loseMenu.LossTitle);
        MenuAnimator.ZoomInElement(loseMenu.NextButtonDesktop);
        MenuAnimator.ZoomInElement(loseMenu.NextButtonMobile);
    }

    public void TryOpenLeaderboard()
    {
        if (_playerData.IsAuthorized)
        {
            Close(_notAuthorized);
            Show(_leaderboard);
        }
        else
        {
            Show(_notAuthorized);
        }
    }

    private void OnBankRobbed()
    {
        ShowEndgameMenu(_winMenu);
    }

    private void OnBankNotRobbed()
    {
        ShowEndgameMenu(_loseMenu);
    }

    private void OnNotEnoughRobbers()
    {
        Show(_notEnoughRobbers);
    }

    private void HideGameElements()
    {
        foreach (var element in _elementsToDragUp)
        {
            MenuAnimator.MoveElementUp(element);
        }

        foreach (var element in _elementsToDragDown)
        {
            MenuAnimator.MoveElementDown(element);
        }
    }

    private void OnStarted()
    {
        _preparingMenu.gameObject.SetActive(false);
        _perksPanel.gameObject.SetActive(true);
        MenuAnimator.DragMenuUpSlowly(_moneyIndicator);
    }
    
    private void OnNotEnoughMoney()
    {
        Show(_notEnoughMoney);
    }
    
    private void OnAllSlotsBusy()
    {
        Show(_allSlotsBusy);
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }

    private void ContinueTime()
    {
        Time.timeScale = 1f;
    }

    private IEnumerator DisableIn(WaitForSecondsRealtime waitForSecondsRealtime, PopUp menu)
    {
        yield return waitForSecondsRealtime;
        menu.gameObject.SetActive(false);
    }
}
