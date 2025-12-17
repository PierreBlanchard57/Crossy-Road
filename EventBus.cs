using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class EventBus : Node
{
    private static Dictionary<string,Callable> callbacks=new Dictionary<string, Callable>();
    private EventBus() { }
    public static void RegisterCallBack(string signal,Callable callback)
    {
        callbacks.Add(signal, callback);
    }
    public static void Reset()
    {
        callbacks.Clear();
    }
    public static void Emit(string signal,params Variant[] args)
    {
        if (args != null) callbacks[signal].Call(args);
        else callbacks[signal].Call();
    }
}
