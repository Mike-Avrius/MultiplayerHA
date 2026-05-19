using System;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCountSlider : MonoBehaviour
{
    [SerializeField] private SessionManager _sessionManager;
    [SerializeField] private Slider slider;
    
    public int GetNumber()
    {
        return (int)slider.value;
    }

    
}
