using System.Collections;
using System.Collections.Generic;
public class Singleton<T>
    where T : Singleton<T>, new()
{
    private static T instance;
    public void SetInstance(T obj)
    {
        instance = obj;
    }
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                throw new System.NullReferenceException();
            }
            return instance;
        }
    }
}