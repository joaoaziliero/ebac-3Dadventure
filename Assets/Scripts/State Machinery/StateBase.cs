using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    public bool stateUpdateEnabled = false;

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();

    private void Update()
    {
        if (stateUpdateEnabled)
        {
            OnStateUpdate();
        }
    }
}
