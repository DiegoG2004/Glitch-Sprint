using System;
using JetBrains.Annotations;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class DayliesManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static DayliesManager m_Instance { get; private set; }
    public float[] RequirementsDailies;
    public TypesofMission[] TypeofDaily;
    public bool[] CompleatedDailies;
    DateTime currentDate;
    DateTime oldDate;
    bool MoreThanADay = false;
    public enum TypesofMission
    {
        NONE = -1,
        GRAB_COINS = 0,
        GET_POINTS = 1,
        ACHIEVE_COMBO = 2,
        TIME_SURVIVED = 3
    }

private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
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
    void Start()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications for this class",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        if (PlayerPrefs.GetInt("DailyAlreadySet") == 0)
        {
            //Set it to 1 so it never works again
            PlayerPrefs.SetInt("DailyAlreadySet", 1);
            PlayerPrefs.Save();
            //We call notification function for the daily
            CreateDailyNotification("Claim your Price","Play some Games");
        }
        //Store the current time when it starts
        currentDate = System.DateTime.Now;

        //Grab the old time from the player prefs as a long
        long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString"));

        //Convert the old time from binary to a DataTime variable
        DateTime oldDate = DateTime.FromBinary(temp);
        print("oldDate: " + oldDate);

        //Use the Subtract method and store the result as a timespan variable
        TimeSpan difference = currentDate.Subtract(oldDate);
        print("Difference: " + difference);
        if(difference.Days >= 1)
        {
            CreatingDaylies();
            MoreThanADay = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateDailyNotification(string title, string text)
    {
        var notification = new AndroidNotification();
        notification.Title = title; //??????????????
        notification.Text = text;
        notification.FireTime = System.DateTime.Now.AddDays(1);
        notification.RepeatInterval = new TimeSpan(1, 0, 0, 0);
        notification.SmallIcon = "icon_small";
        notification.SmallIcon = "icon_large";

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

    public void CreatingDaylies()
    {
        for(int i = 0; i < 3; i++)
        {
            TypeofDaily[i] = (TypesofMission)Random.Range(0, 5);
            RequirementsDailies[i] = Random.Range(1, 5);
            CompleatedDailies[i] = false;
            switch(TypeofDaily[i])
            {
                case TypesofMission.GRAB_COINS:
                    RequirementsDailies[i] = RequirementsDailies[i] * 5;
                    break;
                case TypesofMission.GET_POINTS:
                    RequirementsDailies[i] = RequirementsDailies[i] * 1000;
                    break;
                case TypesofMission.ACHIEVE_COMBO:
                    RequirementsDailies[i] = RequirementsDailies[i] * 5;
                    break;
                case TypesofMission.TIME_SURVIVED:
                    RequirementsDailies[i] = RequirementsDailies[i] * 10;
                    break;
            }
        }
    }

    public void OnGameStart()
    {
        //Create Dailies Tracker
    }

    public string WriteDailiesText(int i)
    {
        switch (TypeofDaily[i])
        {
            case TypesofMission.GRAB_COINS:
                return "Get " + RequirementsDailies[i] + " Coins";
            case TypesofMission.GET_POINTS:
                return "Get " + RequirementsDailies[i] + " Points";
            case TypesofMission.ACHIEVE_COMBO:
                return "Achive " + RequirementsDailies[i] + "x Combo";
            case TypesofMission.TIME_SURVIVED:
                return "Survive " + RequirementsDailies[i] + " Seconds";
            default:
                return null;
        }
    }


    void OnApplicationQuit()
    {
        if (MoreThanADay)
        {
            PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
        }

    }
}
