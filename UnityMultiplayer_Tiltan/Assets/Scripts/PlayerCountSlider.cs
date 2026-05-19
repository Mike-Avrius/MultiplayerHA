using System;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCountSlider : MonoBehaviour
{
    [SerializeField] private SessionManager _sessionManager;
    [SerializeField] private Slider slider;
    
    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    public int GetNumber()
    {
        return (int)slider.value;
    }

    private void OnSliderChanged(float v)
    {
        _sessionManager.SetMaxRoomPLayer(v);
    }
}
