using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    // Resolución y gráficos
    [SerializeField] TMP_Dropdown dropResolucion, dropCalidad;
    Resolution[] screenRes;
    int resPorDefecto;

    List<string> listRes = new List<string>();

    // Canvas
    Animator anim;
    [SerializeField] GameObject title;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject options;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject languages;
    [SerializeField] AudioMixer audioMix;
    [SerializeField] AudioMixer audioEffects;
    [SerializeField] Slider brightness;
    [SerializeField] Image panelBrightness;

    float brillo;

    float transicion = 1f;

    private void Start()
    {
        anim = GetComponent<Animator>();

        StartDropResolution();

        brightness.onValueChanged.AddListener(ChangeBrightness);

        brightness.value = PlayerPrefs.GetFloat("brillo", 0f);

        brillo = brightness.value;

        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, 0);       
    }

    public void StartButton()
    {
        options.SetActive(false);
        controls.SetActive(false);
        title.SetActive(false);
        buttons.SetActive(false);
        languages.SetActive(true);
    }

    public void Español()
    {
        Auxiliar.aux.english = false;
        SceneManager.LoadScene(1);
    }

    public void English()
    {
        Auxiliar.aux.english = true;
        SceneManager.LoadScene(1);
    }

    public void OptionsButton()
    {
        options.SetActive(true);
        title.SetActive(false);
        buttons.SetActive(false);
        anim.SetTrigger("options");
    }

    public void ControlsButton()
    {
        controls.SetActive(true);
        title.SetActive(false);
        buttons.SetActive(false);
        anim.SetTrigger("controls");
    }

    public void BackButton()
    {
        options.SetActive(false);
        controls.SetActive(false);
        languages.SetActive(false);
        title.SetActive(true);
        buttons.SetActive(true);        
    }

    public void TutorialButton()
    {
        StartButton();
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void Volumen(float volumen)
    {
        audioMix.SetFloat("Volumen", volumen);
    }
    public void VolumenEffects(float volumen)
    {
        audioEffects.SetFloat("Volumen", volumen);
    }

    public void FullScreen(bool screenMode)
    {
        Screen.fullScreen = screenMode;
    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(screenRes[index].width, screenRes[index].height, Screen.fullScreen);

        PlayerPrefs.SetInt("numeroResolution", dropResolucion.value);
    }

    public void StartDropResolution()
    {
        screenRes = Screen.resolutions;
        dropResolucion.ClearOptions();
        for (int i = 0; i < screenRes.Length; i++)
        {
            listRes.Add(screenRes[i].width + " x " + screenRes[i].height);
            
            if (screenRes[i].width == Screen.width && screenRes[i].height == Screen.height)
            {
                resPorDefecto = i;
            }
        }       
        dropResolucion.AddOptions(listRes);
        dropResolucion.value = resPorDefecto;
        dropResolucion.RefreshShownValue();

        dropResolucion.value = PlayerPrefs.GetInt("numeroResolution", 0);
    }

    public void ChangeBrightness(float value)
    {
        Debug.Log("valor: " + value);
        Debug.Log(panelBrightness.color.a + " antes.");

        float nuevoBrillo = Mathf.Lerp(brillo, value, transicion * Time.deltaTime);

        PlayerPrefs.SetFloat("brillo", value);
        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, nuevoBrillo);

        Debug.Log(panelBrightness.color.a + " despues.");
    }
}
