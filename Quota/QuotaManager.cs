using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class QuotaManager : MonoBehaviour {

    public IntEvent onCollectedUpdate;
    public IntEvent onRequiredUpdate;

    float totalCollected;

    int damHealth = 0;
    int collected = 0;

    public void ChangeAmountCollected(int amount)
    {
        collected = amount;
      //  Debug.Log("collected update");
        InvokeOnCollectedUpdate();
    }

    public void ChangeDamHealth(int amount)
    {
        damHealth = amount;
    //    Debug.Log("dam health update");
        InvokeOnCollectedUpdate();
    }

    void InvokeOnCollectedUpdate()
    {
//        Debug.Log("collected: "+ collected);
  //      Debug.Log("dam health: " + damHealth);
        onCollectedUpdate.Invoke(damHealth + collected);

    }

    public void ChangeAmountRequired(int cycle)
    {
        switch (cycle)
        {
            case 0:
                onRequiredUpdate.Invoke(10);
                break;
            case 1:
                onRequiredUpdate.Invoke(15);
                break;
            case 2:
                onRequiredUpdate.Invoke(0);
                break;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
