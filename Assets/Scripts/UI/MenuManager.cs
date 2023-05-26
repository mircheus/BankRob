using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private PopUp _newLevelPopup;
    [SerializeField] private PopUp _settingsMenu;

    private WaitForSeconds _waitForAnimationDuration = new WaitForSeconds(MenuAnimator.AnimationDuration);
    // private MenuAnimator _menuAnimator = new MenuAnimator();

    public void ShowNewLevelPopup()
    {
        _newLevelPopup.gameObject.SetActive(true);
        MenuAnimator.FadeIn(_newLevelPopup.Dimed);
        MenuAnimator.DragMenuDown(_newLevelPopup.Popup);
    }

    public void ShowSettingsMenu()
    {
        // _settingsMenu.
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

    private IEnumerator DisableIn(WaitForSeconds waitForSeconds, PopUp menu)
    {
        yield return waitForSeconds;
        menu.gameObject.SetActive(false);
    } 
}
