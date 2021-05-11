using System.Collections.Generic;

public static class DictionaryExcetions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <param name="name"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static T Get<T>(this Dictionary<string, object> instance, string name, object defaultValue)
    {
        if(instance.ContainsKey(name))
        {
            return (T)instance[name];
        }
        else
        {
            return (T)defaultValue;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public static void Set<T>(this Dictionary<string, object> instance, string name, object value)
    {
        instance.Add(name, (T)value);
    }
}
