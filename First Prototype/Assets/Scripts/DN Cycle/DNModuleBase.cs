using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DNModuleBase : MonoBehaviour
{
    protected DayNightCycle dayNightControl;

    private void OnEnable()
    {
        dayNightControl = this.GetComponent<DayNightCycle>();
        dayNightControl.AddModule(this);
    }

    private void OnDisable()
    {
        if (dayNightControl != null)
        {
            dayNightControl.RemoveModule(this);
        }
    }

    public abstract void UpdateModule(float intensity);
}
