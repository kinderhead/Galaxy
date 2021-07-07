using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Components
{
    public static Component baseComponent = new BaseComponent();

    public static List<Component> components;

    public static void Load()
    {
        components = new List<Component>();

        foreach (FieldInfo i in typeof(Components).GetFields())
        {
            if (i.FieldType == typeof(Component))
            {
                components.Add(i.GetValue(null) as Component);
            }
        }
    }
}
