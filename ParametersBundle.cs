using Godot;
using System;

public partial class ParametersBundle : Node
{
    public ParametersBundle(object[] parameters) {
        this.parameters = parameters;
    }
    private object[] parameters;
    public object[] GetParameters()
    {
        return parameters;
    }
}
