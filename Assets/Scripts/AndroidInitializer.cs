using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AndroidInitializer : MonoBehaviour
{
    public UnityEvent AndroidInitialized;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        //Load the saved file (fake for now)
        yield return new WaitForSeconds(6f);

        //Stuff is loaded (it isn't), invoke the event
        AndroidInitialized?.Invoke();
    }   
}
