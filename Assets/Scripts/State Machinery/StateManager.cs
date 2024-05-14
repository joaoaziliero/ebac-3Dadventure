using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Utils.StateMachines.Conventions;

public class StateManager : MonoBehaviour
{
    #region CONSTANTS
    private const string ENTER_STATE_PROMPT = "OnStateEnter";
    private const string UPDATE_STATE_PROMPT = "stateUpdateEnabled";
    private const string EXIT_STATE_PROMPT = "OnStateExit";
    #endregion

    public GameObject target;
    public StateGroupings stateGrouping;

    private Dictionary<StateNames, Type> _stateByName;
    private StateNames _currentState;

    private void Awake()
    {
        var statesByGrouping = Assembly
            .GetExecutingAssembly()
            .GetTypesWithAttribute<StateFilterAttribute>()
            .Where(type => type.GetCustomAttribute<StateFilterAttribute>().Grouping == stateGrouping);
        
        _stateByName = new Dictionary<StateNames, Type>();

        foreach (var type in statesByGrouping)
        {
            if (Enum.TryParse(type.Name, out StateNames stateName))
            {
                _stateByName.Add(stateName, type);
                target.AddComponent(type);
            }
        }
    }

    public void ChooseState(StateNames newestState)
    {
        if (_stateByName.TryGetValue(_currentState, out Type type))
        {
            FromComponentInvokeMethodOnTarget(type, EXIT_STATE_PROMPT);
            SetStateUpdateOnTarget(type, false);
        }

        if (_stateByName.TryGetValue(newestState, out Type newestType))
        {
            FromComponentInvokeMethodOnTarget(newestType, ENTER_STATE_PROMPT);
            SetStateUpdateOnTarget(newestType, true);
        }

        _currentState = newestState;
    }

    private void FromComponentInvokeMethodOnTarget(Type component, string methodName)
    {
        component.GetMethod(methodName).Invoke(target.GetComponent(component), null);
    }

    private void SetStateUpdateOnTarget(Type component, bool update)
    {
        component
            .GetField(UPDATE_STATE_PROMPT)
            .SetValue(target.GetComponent(component), update);
    }
}
