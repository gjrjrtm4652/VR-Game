using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;
using System;
public class ButtonController : MonoBehaviour
{
    static readonly Dictionary<string, InputFeatureUsage<bool>>
        avaibleFeature =
        new Dictionary<string, InputFeatureUsage<bool>>
        {
            { "primaryButton", CommonUsages.primaryButton },
            { "primaryTouch", CommonUsages.primaryTouch },
            { "secondaryButton", CommonUsages.secondaryButton },
            { "secondaryTouch", CommonUsages.secondaryTouch },
            { "gripButton", CommonUsages.gripButton },
            { "triggerButton", CommonUsages.triggerButton },
            { "menuButton", CommonUsages.menuButton },          
            { "primary2DAxisClick", CommonUsages.primary2DAxisClick },
            { "primary2DAxisTouch", CommonUsages.primary2DAxisTouch },
        };

    public enum FeatureOption
    {
        primaryButton, primaryTouch, secondaryButton,
        secondaryTouch, gripButton, triggerButton, menuButton,
        primary2DAxisClick, primary2DAxisTouch
    }
    public InputDeviceRole deviceRole;
    public FeatureOption feature;
    List<InputDevice> devices;
    InputFeatureUsage<bool> selectedFeature;
    bool inputValue;

    public UnityEvent OnPress;
    public UnityEvent OnRelease;
    public bool isPressed;
    private void Awake()
    {
        devices = new List<InputDevice>();
        string featureLabel = Enum.GetName(typeof(FeatureOption), feature);
        avaibleFeature.TryGetValue(featureLabel, out selectedFeature);
    }
    // Update is called once per frame
    void Update()
    {
        InputDevices.GetDevicesWithRole(deviceRole, devices);
        for(int i = 0; i < devices.Count; i++)
        {
            if(devices[i].TryGetFeatureValue
                (selectedFeature, out inputValue) && inputValue
                )
            {
                if (!isPressed)
                {
                    isPressed = true;
                    OnPress.Invoke();
                }
                else if (isPressed)
                {
                    isPressed = false;
                    OnRelease.Invoke();
                }
            }
        }
    }
}
