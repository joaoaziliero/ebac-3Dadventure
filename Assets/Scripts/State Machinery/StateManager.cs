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

    #region FRONTEND
    [SerializeField] private GameObject _target;
    [SerializeField] private StateGroupings _stateGrouping;
    #endregion

    #region BACKEND
    private Dictionary<StateNames, Type> stateByName;
    private StateNames _currentState;
    #endregion

    #region UNITY_METHODS
    private void Awake()
    {
        var statesByGrouping = Assembly
            .GetExecutingAssembly()
            .GetTypesWithAttribute<StateFilterAttribute>()
            .Where(type => type.GetCustomAttribute<StateFilterAttribute>().Grouping == _stateGrouping);
        
        stateByName = new Dictionary<StateNames, Type>();

        foreach (var type in statesByGrouping)
        {
            Enum.TryParse(type.Name, out StateNames stateName);
            stateByName.Add(stateName, type);
            _target.AddComponent(type);
        }
    }
    #endregion

    #region OTHER_METHODS
    public void ChooseState(StateNames newestState)
    {
        if (stateByName.TryGetValue(_currentState, out Type type))
        {
            FromComponentInvokeMethodOnTarget(type, EXIT_STATE_PROMPT);
            SetStateUpdateOnTarget(type, false);
        }

        stateByName.TryGetValue(newestState, out Type newestType);
        FromComponentInvokeMethodOnTarget(newestType, ENTER_STATE_PROMPT);
        SetStateUpdateOnTarget(newestType, true);

        _currentState = newestState;
    }

    private void FromComponentInvokeMethodOnTarget(Type component, string methodName)
    {
        component.GetMethod(methodName).Invoke(_target.GetComponent(component), null);
    }

    private void SetStateUpdateOnTarget(Type component, bool update)
    {
        component
            .GetField(UPDATE_STATE_PROMPT, BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(_target.GetComponent(component), update);
    }
    #endregion
}
