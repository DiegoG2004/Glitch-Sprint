using UnityEngine.Android;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{
    private void Awake()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        //Once The Channel is set, start (or not) the daily remainder
        if(PlayerPrefs.GetInt("DailyAlreadySet") == 0)
        {
            //Set it to 1 so it never works again
            PlayerPrefs.SetInt("DailyAlreadySet", 1);
            PlayerPrefs.Save();
            //We call notification function for the daily
            DailyReminder();
        }

        CreateSimpleNotification("30 s Notification", "The first", 30);
        CreateSimpleNotification("2 min Notification", "The second", 120);
        CreateSimpleNotification("20 min Notification", "The second", 1200);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateSimpleNotification(string title, string text, int SecondsToWait)
    {
        var notification = new AndroidNotification();
        notification.Title = title; //??????????????
        notification.Text = text;
        notification.FireTime = System.DateTime.Now.AddSeconds(SecondsToWait);
        notification.SmallIcon = "icon_small";
        notification.SmallIcon = "icon_large";

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

    void DailyReminder()
    {
        var notification = new AndroidNotification();
        notification.Title = "Enteh da game, m8"; //??????????????
        notification.Text = "Daily Money";
        notification.FireTime = System.DateTime.Now.AddDays(1);
        notification.RepeatInterval = new TimeSpan(1, 0, 0, 0); 
        notification.SmallIcon = "icon_small";
        notification.SmallIcon = "icon_large";

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}
