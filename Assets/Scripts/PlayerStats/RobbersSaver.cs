using UnityEngine;

public class RobbersSaver : MonoBehaviour
{   
    [Header("Events sources")]
    [SerializeField] private Robbery _robbery;
    [SerializeField] private RobStarter _robStarter;
    
    [Header("Debug")] 
    [SerializeField] private int[] _initialRobbers;
    [SerializeField] private int[] _aliveRobbers;
    [SerializeField] private int[] _robbersToSave;

    public int[] RobbersToSave => _robbersToSave;

    private void OnEnable()
    {
        _robStarter.Started += OnStarted;
        _robbery.BankRobbed += OnBankRobbed;
        _robbery.BankNotRobbed += OnBankNotRobbed;
    }
    
    private void OnDisable()
    {
        _robStarter.Started -= OnStarted;
        _robbery.BankRobbed -= OnBankRobbed;
        _robbery.BankNotRobbed -= OnBankNotRobbed;
    }

    private void OnStarted()
    {
        _initialRobbers = _robbery.SendRobbersListTo(this);
    }
    
    private void OnBankRobbed()
    {
        SaveAliveRobbers();
    }

    private void OnBankNotRobbed()
    {
        SaveInitialRobbers();
    }

    private void SaveAliveRobbers()
    {
        _aliveRobbers = _robbery.CountAliveRobbers();
        _robbersToSave = _aliveRobbers;
    }

    private void SaveInitialRobbers()
    {
        _robbersToSave = _initialRobbers;
    }
}
