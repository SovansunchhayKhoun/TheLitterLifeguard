using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CaughtObjectData
{
    public string ObjectName;
    public Vector3 Position;
    public Vector3 Scale;
}

public static class CaughtObjectStorage
{
    public static List<CaughtObjectData> CaughtObjectDataList = new List<CaughtObjectData>();

    public static void SaveCaughtObjects(List<GameObject> caughtObjects)
    {
        CaughtObjectDataList.Clear();
        foreach (var obj in caughtObjects)
        {
            CaughtObjectDataList.Add(new CaughtObjectData
            {
                ObjectName = obj.name,
                Position = obj.transform.position,
                Scale = obj.transform.localScale
            });
        }
    }
}
