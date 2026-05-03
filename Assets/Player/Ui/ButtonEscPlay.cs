using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEscPlay : MonoBehaviour
{
    public GameObject pausePanel;
    // Start is called before the first frame update
    public void OnUIButtonPressed()
    {
        pausePanel.SetActive(false);
    }
}
