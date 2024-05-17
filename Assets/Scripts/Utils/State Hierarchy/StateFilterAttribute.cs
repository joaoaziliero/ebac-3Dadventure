using System;
using Utils.StateMachines.Conventions;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class StateFilterAttribute : Attribute
{
    public StateGroupings Grouping { get; }

    public StateFilterAttribute(StateGroupings grouping)
    {
        this.Grouping = grouping;
    }
}
