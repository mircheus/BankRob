using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // [SerializeField] private PopUp _newLevelPopup;
    // [SerializeField] private PopUp _settingsMenu;
    [Header("Pulsating buttons")]
    [SerializeField] private RectTransform _robButton;
    
    private WaitForSeconds _waitForAnimationDuration = new WaitForSeconds(MenuAnimator.AnimationDuration);

    private void Start()
    {
        MenuAnimator.PulsateButton(_robButton);
    }

    public void ShowWithAnimation(PopUp menu)
    {
        menu.gameObject.SetActive(true);
        MenuAnimator.FadeIn(menu.Dimed);
        MenuAnimator.DragMenuDown(menu.Popup);
    }

    public void CloseWithAnimation(PopUp menu)
    {
        MenuAnimator.DragMenuUp(menu.Popup);
        MenuAnimator.FadeOut(menu.Dimed);
        StartCoroutine(DisableIn(_waitForAnimationDuration, menu));
    }

    public void ShowWinMenu(WinMenu winMenu)
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
    
    private IEnumerator DisableIn(WaitForSeconds waitForSeconds, PopUp menu)
    {
        yield return waitForSeconds;
        menu.gameObject.SetActive(false);
    }
}
