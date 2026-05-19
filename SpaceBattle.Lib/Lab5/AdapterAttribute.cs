using System;

namespace SpaceBattle.Lib.Lab5;

[AttributeUsage(AttributeTargets.Property)]
public class AdapterAttribute : Attribute
{
    public Type InterfaceType { get; }
    public string PropertyName { get; }
    public string StrategyName { get; }

    public AdapterAttribute(Type interfaceType, string propertyName, string strategyName)
    {
        InterfaceType = interfaceType;
        PropertyName = propertyName;
        StrategyName = strategyName;
    }
}