using UnityEngine;

public class ButtonPulsator : MonoBehaviour
{
    [SerializeField] private RectTransform _buttonToPulsate;
    
    private void OnEnable()
    {
        MenuAnimator.PulsateButton(_buttonToPulsate);
    }
}
