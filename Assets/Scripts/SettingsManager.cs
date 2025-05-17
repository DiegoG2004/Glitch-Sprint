using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager m_Instance { get; private set; }

    public GameSettings m_CurrentGameSettings;
    private string m_SettingsFileName = "Settings.moai";

    public AudioMixer m_AudioMixer;

    //Singleton thingie
    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        m_CurrentGameSettings = SaveLoadManager.Load<GameSettings>(m_SettingsFileName);

        if (m_CurrentGameSettings == null)
        {
            //There are no settings loaded or they broke
            m_CurrentGameSettings = new GameSettings();
            SaveSettings();
        }
        SetCurrentVolume();
        //Set the localization
        LocalizationSettings.SelectedLocale =
            LocalizationSettings.AvailableLocales.GetLocale(m_CurrentGameSettings.m_LocaleCode);
    }

    public void SaveSettings()
    {
        SaveLoadManager.Save(m_SettingsFileName, m_CurrentGameSettings);
    }
    public void UpdateCurrentVolume(float newMasterVolume, float newMusicVolume, float newSFXVolume)
    {
        //Set the settings volume values
        m_CurrentGameSettings.m_MasterVolume = newMasterVolume;
        m_CurrentGameSettings.m_MusicVolume = newMusicVolume;
        m_CurrentGameSettings.m_SFXVolume = newSFXVolume;
        SetCurrentVolume();
        
    }
    public void SetCurrentVolume()
    {
        //Set the audio mixer's volumes, from the 0-1 of the settings to the 0-20 of the mixer
        m_AudioMixer.SetFloat("MasterVolume", m_CurrentGameSettings.m_MasterVolume * 20f);
        m_AudioMixer.SetFloat("MusicVolume", m_CurrentGameSettings.m_MusicVolume * 20f);
        m_AudioMixer.SetFloat("SFXVolume", m_CurrentGameSettings.m_SFXVolume * 20f);
    }
}

[System.Serializable]
public class GameSettings
{
    //Volume
    public float m_MasterVolume = 0.75f;
    public float m_SFXVolume = 0.70f;
    public float m_MusicVolume = 0.65f;
    //Language
    public string m_LocaleCode = "en";
}