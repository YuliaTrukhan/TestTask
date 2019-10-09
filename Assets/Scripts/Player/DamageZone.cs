using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DamageZone
{
    public Vector3[] points;
    public int[] indices;

    public List<T> GetComponents<T>(Transform unit, LayerMask mask, float radius)
    {
        GetBox(out Vector3 center, out Vector3 halfExtents);
        center = unit.rotation * center;

        var colliders = Physics.OverlapSphere(unit.position, radius, mask);
        var result = new List<T>();

        for (int i = 0; i < colliders.Length; i++)
        {
            var comp = colliders[i].GetComponent<T>();
            if (comp == null)
                continue;
            result.Add(comp);
        }

        return result;
    }

    private bool OverlapPoint(Vector3 point)
    {
        for (int i = 0; i < indices.Length; i += 3)
        {
            if (PointInTriangle(point, points[indices[i + 0]], points[indices[i + 1]], points[indices[i + 2]]))
                return true;
        }

        return false;
    }

    private void GetBox(out Vector3 center, out Vector3 halfExtents)
    {
        Vector3 left = Vector3.zero, right = Vector3.zero, top = Vector3.zero, bot = Vector3.zero;

        for (int i = 0; i < points.Length; i++)
        {
            if (left.x > points[i].x)
                left = points[i];
            if (right.x < points[i].x)
                right = points[i];
            if (top.z < points[i].z)
                top = points[i];
            if (bot.z > points[i].z)
                bot = points[i];
        }

        center = new Vector3(left.x + right.x, 0.0f, top.z + bot.z) / 2f;
        halfExtents = new Vector3(right.x - left.x, 1.0f, top.z - bot.z) / 2f;
    }

    private bool PointInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
    {
        return SameSide(p, a, b, c) && SameSide(p, b, a, c) && SameSide(p, c, a, b);
    }

    private bool SameSide(Vector3 p1, Vector3 p2, Vector3 a, Vector3 b)
    {
        var cp1 = Vector3.Cross(b - a, p1 - a);
        var cp2 = Vector3.Cross(b - a, p2 - a);
        return Vector3.Dot(cp1, cp2) >= 0f;
    }
}
