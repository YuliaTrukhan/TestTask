using System.Collections.Generic;
using UnityEngine;

public static class SpawnerController
{
    private static List<Vector3> generetadPoints = new List<Vector3>();
    private  const float boundsX = 40f;
    private  const float boundsY = 40f;

    public static Vector3 GetRandonPoint(Vector2 spawnBounds)
    {
        Vector3? result = null;

        if (generetadPoints.Count == 0)
        {
            result = new Vector3(Random.Range(-boundsX, boundsX), 0f, Random.Range(-boundsY, boundsY));
            generetadPoints.Add(result.Value);
        }
        else
        {
            while (result == null)
            {
                var point = new Vector3(Random.Range(-boundsX, boundsX), 0f, Random.Range(-boundsY, boundsY));
                if (IsPointNearAnother(point))
                {
                    continue;
                }

                result = point;

            }
        }
        return result.Value;
    }

    private static bool IsPointNearAnother(Vector3 point)
    {
        for (int i = 0; i < generetadPoints.Count; i++)
        {
            if ((point - generetadPoints[i]).sqrMagnitude <= 1f)
            {
                return true;
            }
        }

        return false;
    }

    public static void ClearSpawner()
    {
        generetadPoints.Clear();
    }
}
