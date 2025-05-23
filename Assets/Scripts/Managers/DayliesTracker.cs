using UnityEngine;

public class DayliesTracker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum TypesofMission
    {
        NONE = -1,
        GRAB_COINS = 0,
        GET_POINTS = 1,
        ACHIEVE_COMBO = 2,
        TIME_SURVIVED = 3
    }
    public float RequirementsDailies;
    public bool DailyComplete;
    public DayliesManager m_Base;
    public TypesofMission[] TypeofDaily;
    public PlayerMovement m_Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChooseTracker(TypesofMission Tracking)
    {

    }

    public void OnDeath()
    {
        switch (TypeofDaily)
        {
            case TypesofMission.GRAB_COINS:

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
}
