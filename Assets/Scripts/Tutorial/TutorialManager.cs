using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private const int RobbersAmountForCombo = 2;
    
    [SerializeField] private TutorialPanel[] _tutorialPanels;

    [Header("Common")] 
    [SerializeField] private PlayerData _playerData;
    
    [Header("Tutorial Level 1")]
    [SerializeField] private PerksPanel _perksPanel;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _robButton;
    [SerializeField] private Image _dimed1;
    [SerializeField] private Image _dimed2;
    [SerializeField] private TimeChanger _timeChanger;
    [SerializeField] private float _secondsToWait;
    [SerializeField] private Robbery _robbery;
    
    private int _currentIndex;

    [Header("Tutorial Level 2")]
    [SerializeField] private GameObject _preparePanel;
    [SerializeField] private Image _dimed3;

    private int _robbersCounter = 0;
    

    private void OnEnable()
    {
        _currentIndex = 0;
        _dimed1.gameObject.SetActive(true);
        _robButton.interactable = false;
        _tutorialPanels[_currentIndex].gameObject.SetActive(true);
        _robbery.BankRobbed += OnBankRobbed;
        _playerData.ResetPlayerData();
    }

    private void OnDisable()
    {
        _robbery.BankRobbed -= OnBankRobbed;
    }
    
    // common methods
    
    private void ShowNextTutorialPanel()
    {
        _tutorialPanels[_currentIndex].gameObject.SetActive(false);
        _currentIndex++;
        _tutorialPanels[_currentIndex].gameObject.SetActive(true);
    }
    
    // tutorial Level 1

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
    
    // Tutorial Level 2

    public void BuyRobber()
    {
        _robbersCounter++;

        if (_robbersCounter == RobbersAmountForCombo)
        {
            ShowNextTutorialPanel();
            _dimed1.gameObject.SetActive(false);
            _preparePanel.SetActive(false);
        }
    }

    public void OnCloseNewRobber()
    {
        ShowNextTutorialPanel();
        _dimed3.gameObject.SetActive(true);
    }

    private IEnumerator ShowPerkTutorialWithDelay()
    {
        yield return new WaitForSeconds(_secondsToWait);
        ShowPerkTutorial();
    }
}
