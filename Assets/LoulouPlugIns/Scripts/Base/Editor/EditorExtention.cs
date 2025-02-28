using System.Collections;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System.Drawing.Printing;

public static class EditorExtention 
{
    //To clean and to put somewhere you can access for every project 
    public static object GetValueFromObject<T>(T obj, string propertyPath) where T : class
    {

        Type type = obj.GetType();
        FieldInfo field = null;

        BindingFlags flags = BindingFlags.Default | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.GetField | BindingFlags.Default | BindingFlags.SetField | BindingFlags.DeclaredOnly |BindingFlags.Instance;

        string[] parts = propertyPath.Split('.');
        object value = obj;

        for (int i = 0; i < parts.Length; i++)
        {
            var part = parts[i];
            field = type.GetField(part,flags);
            var x = field.GetValue(value);

            if(x is IList) //Not really clean
            {
                var tempart = parts[i+2];
                var index = int.Parse(tempart[5].ToString());
                value = (x as IList)[index];
                type = value.GetType(); 
                i += 2;
            }
            else
            {
                value = x;
                type = value.GetType();
            }
        }
        return value;

    }
}
