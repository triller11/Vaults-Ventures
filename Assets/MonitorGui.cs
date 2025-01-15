using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorGUI : MonoBehaviour
{
    public Canvas monitorCanvas;

    private void Start()
    {
        monitorCanvas.gameObject.SetActive(false); // Hide GUI initially
    }

    public void ShowGUI()
    {
        monitorCanvas.gameObject.SetActive(true);
    }

    public void HideGUI()
    {
        monitorCanvas.gameObject.SetActive(false);
    }
}

