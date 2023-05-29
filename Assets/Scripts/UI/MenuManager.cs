using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // [SerializeField] private PopUp _newLevelPopup;
    // [SerializeField] private PopUp _settingsMenu;
    [Header("Menus")]
    [SerializeField] private WinMenu _winMenu;
    [SerializeField] private LoseMenu _loseMenu;

    [Header("Warnings")] [SerializeField] 
    private PopUp _notEnoughRobbers;

    [Header("Pulsating buttons")]
    [SerializeField] private RectTransform _robButton;

    [Header("Events Sources")] 
    [SerializeField] private Robbery _robbery;
    [SerializeField] private RobStarter _robStarter;
    
    private WaitForSeconds _waitForAnimationDuration = new WaitForSeconds(MenuAnimator.AnimationDuration);

    private void OnEnable()
    {
        _robbery.BankRobbed += OnBankRobbed;
        _robbery.BankNotRobbed += OnBankNotRobbed;
        _robStarter.NotEnoughRobbers += OnNotEnoughRobbers;
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= OnBankRobbed;
        _robbery.BankNotRobbed -= OnBankNotRobbed;
        _robStarter.NotEnoughRobbers -= OnNotEnoughRobbers;
    }

    private void Start()
    {
        MenuAnimator.PulsateButton(_robButton);
    }

    public void Show(PopUp menu)
    {
        menu.gameObject.SetActive(true);
        MenuAnimator.FadeIn(menu.Dimed);
        MenuAnimator.DragMenuDown(menu.Popup);
    }

    public void Close(PopUp menu)
    {
        MenuAnimator.DragMenuUp(menu.Popup);
        MenuAnimator.FadeOut(menu.Dimed);
        StartCoroutine(DisableIn(_waitForAnimationDuration, menu));
    }
    
    public void ShowEndgameMenu(WinMenu winMenu)
    {
        winMenu.gameObject.SetActive(true);
        MenuAnimator.FadeIn(winMenu.Dimed);
        MenuAnimator.MoveWinTitle(winMenu.WinPanel, winMenu.WinTitle);
        MenuAnimator.ZoomInElement(winMenu.NewItem);

        if (winMenu.IsMobileScreen)
        {
            MenuAnimator.ZoomInElement(winMenu.NextButtonMobile);
            MenuAnimator.ZoomInAndPulsateButton(winMenu.ADButtonMobile);
        }
        else
        {
            MenuAnimator.ZoomInElement(winMenu.NextButtonDesktop);
            MenuAnimator.ZoomInAndPulsateButton(winMenu.ADButtonDesktop);
        }
    }

    public void ShowEndgameMenu(LoseMenu loseMenu)
    {
        loseMenu.gameObject.SetActive(true);
        MenuAnimator.FadeIn(loseMenu.Dimed);
        MenuAnimator.MoveWinTitle(loseMenu.LossPanel, loseMenu.LossTitle);

        if (loseMenu.IsMobileScreen)
        {
            MenuAnimator.ZoomInElement(loseMenu.NextButtonMobile);
            MenuAnimator.ZoomInAndPulsateButton(loseMenu.ADButtonMobile);
        }
        else
        {
            MenuAnimator.ZoomInElement(loseMenu.NextButtonDesktop);
            MenuAnimator.ZoomInAndPulsateButton(loseMenu.ADButtonDesktop);
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

    private IEnumerator DisableIn(WaitForSeconds waitForSeconds, PopUp menu)
    {
        yield return waitForSeconds;
        menu.gameObject.SetActive(false);
    }
}