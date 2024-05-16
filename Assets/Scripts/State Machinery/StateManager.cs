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

    [SerializeField] private GameObject _target;
    [SerializeField] private StateGroupings _stateGrouping;

    public Dictionary<StateNames, Type> StateByName { get; private set; }
    public StateNames CurrentState { get; private set; }

    private void Awake()
    {
        var statesByGrouping = Assembly
            .GetExecutingAssembly()
            .GetTypesWithAttribute<StateFilterAttribute>()
            .Where(type => type.GetCustomAttribute<StateFilterAttribute>().Grouping == _stateGrouping);
        
        StateByName = new Dictionary<StateNames, Type>();
        CurrentState = StateNames.None;

        foreach (var type in statesByGrouping)
        {
            if (Enum.TryParse(type.Name, out StateNames stateName))
            {
                StateByName.Add(stateName, type);
                _target.AddComponent(type);
            }
        }
    }

    public void ChooseState(StateNames newestState)
    {
        if (newestState != CurrentState)
        {
            if (StateByName.TryGetValue(CurrentState, out Type type))
            {
                SetStateUpdateOnTarget(type, false);
                FromComponentInvokeMethodOnTarget(type, EXIT_STATE_PROMPT);
            }

            if (StateByName.TryGetValue(newestState, out Type newestType))
            {
                CurrentState = newestState;
                FromComponentInvokeMethodOnTarget(newestType, ENTER_STATE_PROMPT);
                SetStateUpdateOnTarget(newestType, true);
            }
        }
    }

    private void FromComponentInvokeMethodOnTarget(Type component, string methodName)
    {
        component.GetMethod(methodName).Invoke(_target.GetComponent(component), null);
    }

    private void SetStateUpdateOnTarget(Type component, bool update)
    {
        component
            .GetField(UPDATE_STATE_PROMPT)
            .SetValue(_target.GetComponent(component), update);
    }
}
