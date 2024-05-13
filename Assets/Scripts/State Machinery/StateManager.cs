using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Utils.StateMachines.Conventions;

public class StateManager : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private StateGroupings _stateGrouping;

    private Dictionary<StateNames, Type> stateByName;
    private StateNames _currentState;

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

    public void ChangeStateTo(StateNames newestState)
    {
        if (stateByName.TryGetValue(_currentState, out Type type))
        {
            FromComponentInvokeMethodOnTarget(type, "OnStateExit");
        }

        FromComponentInvokeMethodOnTarget(stateByName[newestState], "OnStateEnter");

        _currentState = newestState;
    }

    private void FromComponentInvokeMethodOnTarget(Type component, string methodName)
    {
        component.GetMethod(methodName).Invoke(_target.GetComponent(component), null);
    }
}
