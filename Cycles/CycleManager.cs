using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cycle {
   Day,
   Night
};

public class CycleManager : MonoBehaviour {




 Transform sun;
    
    public float hoursPerSecond;
    public float currentHour = 5;
    Cycle currentCycle = Cycle.Day;

    public int cycleCount = 0;
    public int defaultTimePassage;

    public VoidEvent onNightTime;
    public IntEvent onNewDay;
    public StringEvent onTimeUpdate;
    public FloatEvent onTimeUpdateFloat;

    public VoidEvent onSixOClock;
    public VoidEvent onMidnight;
    bool isPaused;
    bool hasPassed6;


    void Start () {

        sun = transform.GetComponentInChildren<Light>().transform;

	}

    public void Pause()
    {
        isPaused = true;
    }

    public void Restart()
    {
        currentHour = 5;
        isPaused = false;
    }
    
    void OnNightTime()
    {
        currentCycle = Cycle.Night;
        //        Debug.Log("night night dont let the mythical creatures bite");
        onNightTime.Invoke();
//        enemyManager.EnemiesExitStage();
    }

    void OnDayTime()
    {
        currentCycle = Cycle.Day;
        onNewDay.Invoke(cycleCount);
        //      Debug.Log("wakey wakey look out for snakey");
        //         enemyManager.EnemiesEnterStage(cycleCount);
        cycleCount++;
    }

    void ResetCheckers()
    {

    }

    // Update is called once per frame
    void Update () {
        if (isPaused)
            return;
        currentHour += hoursPerSecond * Time.deltaTime;
        if(currentHour > 18 && !hasPassed6)
        {
            hasPassed6 = true;
            onSixOClock.Invoke();
        }
        if (currentHour >= 24)
        {
            currentHour = 0;
            onMidnight.Invoke();
            hasPassed6 = false;
        }
        currentHour %= 24;



        var timeString = currentHour.ToString();

        onTimeUpdate.Invoke(timeString);
        onTimeUpdateFloat.Invoke(currentHour);


        var sunRotation = ((currentHour - 6) / 24) * 360;

        if ((currentHour > 18 || currentHour < 6)  && currentCycle == Cycle.Day)
            OnNightTime();
        else if (currentHour > 6 && currentHour < 18 && currentCycle == Cycle.Night)
            OnDayTime();

        var rotation =new Vector3(sunRotation,sun.rotation.y,sun.rotation.z);
        sun.rotation = Quaternion.Euler(rotation);
    }
}
