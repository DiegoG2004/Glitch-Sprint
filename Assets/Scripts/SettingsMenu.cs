using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{    
    private int m_CurrentLanguageIndex;
    [Header("Language")]
    public Button m_NextLanguageButton;
    public TextMeshProUGUI m_LanguageText;
    public Button m_PreviousLanguageButton;
    [Header("Sliders")]
    public Slider m_MasterVolumeSlider;
    public Slider m_SFXVolumeSlider;
    public Slider m_MusicVolumeSlider;
    [Header("Buttons")]
    public Button m_ApplyButton;
    public Button m_CancelButton;

    private async void Awake()
    {
        m_MasterVolumeSlider.value = SettingsManager.m_Instance.m_CurrentGameSettings.m_MasterVolume;
        m_SFXVolumeSlider.value = SettingsManager.m_Instance.m_CurrentGameSettings.m_SFXVolume;
        m_MusicVolumeSlider.value = SettingsManager.m_Instance.m_CurrentGameSettings.m_MusicVolume;

        await LocalizationSettings.InitializationOperation.Task;

        m_CurrentLanguageIndex = 0;
        foreach (var item in LocalizationSettings.AvailableLocales.Locales)
        {
            if (item.Identifier.Code == SettingsManager.m_Instance.m_CurrentGameSettings.m_LocaleCode)
            {
                m_LanguageText.text = item.LocaleName;
                break;
            }
            m_CurrentLanguageIndex++;
        }
        //Add the functions to the buttons
        m_MasterVolumeSlider.onValueChanged.AddListener(delegate { VolumeSliderUpdated(); });
        m_SFXVolumeSlider.onValueChanged.AddListener(delegate { VolumeSliderUpdated(); });
        m_MusicVolumeSlider.onValueChanged.AddListener(delegate { VolumeSliderUpdated(); });
        
        m_ApplyButton.interactable = false;
        m_ApplyButton.onClick.AddListener(ApplyButtonPressed);

        m_NextLanguageButton.onClick.AddListener(() => ChangeLanguage(1));
        m_PreviousLanguageButton.onClick.AddListener(() => ChangeLanguage(-1));
    }

    private void VolumeSliderUpdated()
    {
        SettingsManager.m_Instance.UpdateCurrentVolume(m_MasterVolumeSlider.value, m_MusicVolumeSlider.value, m_SFXVolumeSlider.value);

        m_ApplyButton.interactable = true;

    }
    private void ApplyButtonPressed()
    {
        m_ApplyButton.interactable = false;
        SettingsManager.m_Instance.SaveSettings();
    }

    private void ChangeLanguage(int value)
    {
        m_CurrentLanguageIndex += value;
        if (m_CurrentLanguageIndex < 0)
        {
            //If out of range from the left, put the index to the max
            m_CurrentLanguageIndex = LocalizationSettings.AvailableLocales.Locales.Count - 1;
        }
        else if(m_CurrentLanguageIndex >= LocalizationSettings.AvailableLocales.Locales.Count)
        {
            //If out of range from the right, put the index to 0
            m_CurrentLanguageIndex = 0;
        }
        //Set the language to the correct one from the list
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[m_CurrentLanguageIndex];
        m_LanguageText.text = LocalizationSettings.AvailableLocales.Locales[m_CurrentLanguageIndex].LocaleName;
        //Place the change in the manager
        SettingsManager.m_Instance.m_CurrentGameSettings.m_LocaleCode =
            LocalizationSettings.AvailableLocales.Locales[m_CurrentLanguageIndex].Identifier.Code;
        
        m_ApplyButton.interactable = true;
    }

    private void OnDestroy()
    {
        m_MasterVolumeSlider.onValueChanged.RemoveAllListeners();
        m_MusicVolumeSlider.onValueChanged.RemoveAllListeners();
        m_SFXVolumeSlider.onValueChanged.RemoveAllListeners();
        
        m_ApplyButton.onClick.RemoveAllListeners();

        m_NextLanguageButton.onClick.RemoveAllListeners();
        m_PreviousLanguageButton.onClick.RemoveAllListeners();
    }
}
