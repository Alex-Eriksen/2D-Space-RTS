using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITimeManager : MonoBehaviour
{
    public static bool IsPaused = false;
    public static string CurrentTime = "2022-05-29";
    public static float TimeSpeed = 1.0f;

    private string m_displayTimeString = "2022-05-29";

    public TextMeshProUGUI PauseButton, SlowDownButton, SpeedUpButton, TimeDisplay;

    public void TogglePaused()
    {
        IsPaused = !IsPaused;
        PauseButton.text = IsPaused ? ">" : "I I"; // Add some icons instead of text.
    }

    public void IncreaseTimeSpeed()
    {
        if (TimeSpeed < 0.5f)
        {
            TimeSpeed += 0.25f;
        }
        else if (TimeSpeed < 1f)
        {
            TimeSpeed += 0.5f;
        }
        else if (TimeSpeed >= 1f)
        {
            TimeSpeed += 1f;
        }
    }

    public void DecreaseTimeSpeed()
    {
        if(TimeSpeed <= 0.5f)
        {
            TimeSpeed -= 0.25f;
        }
        else if(TimeSpeed <= 1f)
        {
            TimeSpeed -= 0.5f;
        }
        else if(TimeSpeed >= 2)
        {
            TimeSpeed -= 1f;
        }
    }

    private void Update()
    {
        TimeSpeed = Mathf.Clamp(TimeSpeed, 0.25f, 4);
        m_displayTimeString = CurrentTime + $" ({TimeSpeed}x)";
        TimeDisplay.text = m_displayTimeString;
    }
}
