using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialPanel[] _tutorialPanels;
    [SerializeField] private PerksPanel _perksPanel;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _robButton;
    [SerializeField] private Image _dimed1;
    [SerializeField] private Image _dimed2;
    [SerializeField] private TimeChanger _timeChanger;
    [SerializeField] private float _secondsToWait;
    [SerializeField] private Robbery _robbery;

    private int _currentIndex;
    
    private void OnEnable()
    {
        _currentIndex = 0;
        _dimed1.gameObject.SetActive(true);
        _robButton.interactable = false;
        _tutorialPanels[_currentIndex].gameObject.SetActive(true);

        _robbery.BankRobbed += OnBankRobbed;
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= OnBankRobbed;
    }

    public void PressBuyButton()
    {
        ShowNextTutorialPanel();
        _buyButton.interactable = false;
        _robButton.interactable = true;
        _robButton.GetComponent<FlashingButton>().enabled = true;
    }

    public void PressRobButton()
    {
        _dimed1.gameObject.SetActive(false);
        StartCoroutine(ShowPerkTutorialWithDelay());
    }

    public void PressPerkButton()
    {
        _timeChanger.DisableSlowmo();
        _tutorialPanels[_currentIndex].gameObject.SetActive(false);
        _dimed2.gameObject.SetActive(false);
    }

    private void OnBankRobbed()
    {
        _tutorialPanels[_currentIndex].gameObject.SetActive(false);
        _dimed2.gameObject.SetActive(false);
    }

    private void ShowPerkTutorial()
    {
        _timeChanger.EnableSlowmo();
        ShowNextTutorialPanel();
        _dimed2.gameObject.SetActive(true);
        _perksPanel.gameObject.SetActive(true);
    }

    private void ShowNextTutorialPanel()
    {
        _tutorialPanels[_currentIndex].gameObject.SetActive(false);
        _currentIndex++;
        _tutorialPanels[_currentIndex].gameObject.SetActive(true);
    }

    private IEnumerator ShowPerkTutorialWithDelay()
    {
        yield return new WaitForSeconds(_secondsToWait);
        ShowPerkTutorial();
    }
}
