using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMenuManager : MonoBehaviour
{
    // Menu
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject options;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject background;


    private void Update()
    {
        // Menú
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            background.SetActive(true);
            buttons.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        buttons.SetActive(false);
        background.SetActive(false);
        canvas.SetActive(true);
    }

    public void OptionsButton()
    {
        canvas.SetActive(false);
        buttons.SetActive(false);
        options.SetActive(true);
    }

    public void ControlsButton()
    {
        background.SetActive(true);
        buttons.SetActive(false);
        controls.SetActive(true);
    }

    public void BackButton()
    {
        background.SetActive(true);
        options.SetActive(false);
        controls.SetActive(false);
        buttons.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
