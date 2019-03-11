using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{

    public static void DrawArrowsToChildren(Transform parentBone) {

        foreach (Transform childBone in parentBone) {

            Vector3 arrowDir = childBone.position - parentBone.position;
            DrawGizmoArrow(parentBone.position, childBone.position, Color.white);

            DrawArrowsToChildren(childBone);
        }
    }

    public static void DrawLinesBetweenPoints(Vector3[] points, Color color) {

        Gizmos.color = color;
        for (int i = 0; i < points.Length - 1; i++) {

            Gizmos.DrawLine(points[i], points[i+1]);
        }
    }
    

    public static void DrawGizmoArrow(Vector3 start, Vector3 end, Color color) {
        Gizmos.color = color;
        Gizmos.DrawLine(start, end);
        Vector3 dir = end - start;

        float arrowHeadLength = .25f;
        start = end - dir.normalized * arrowHeadLength + Vector3.up * arrowHeadLength;
        Gizmos.DrawLine(start, end);

        start = end - dir.normalized * arrowHeadLength - Vector3.up * arrowHeadLength;
        Gizmos.DrawLine(start, end);
    }

    public static int GetDeepChildCount(Transform parent) {
        int childCount = parent.childCount;
        foreach (Transform child in parent) {
            childCount += GetDeepChildCount(child);
        }
        return childCount;
    }
    public static Vector3 TSpline(Vector3 a, Vector3 dirA, Vector3 b, Vector3 dirB, float t) {

        Vector3 ad = a + dirA;
        Vector3 bd = b + dirB;

        return Bezier(a, ad, bd, b, t);
    }
    public static Vector3 TSpline(List<PosDirPair> pointsInfo, float t) {

        int whereToStart = (int) ((pointsInfo.Count-1) * t);

        

        if (t >= 1) {
            return pointsInfo[pointsInfo.Count - 1].pos;
        } else if (t <= 0) {
            return pointsInfo[0].pos;
        }

        Vector3 a       = pointsInfo[whereToStart].pos;
        Vector3 b       = pointsInfo[whereToStart+1].pos;
        Vector3 dirA    = b - a;
        Vector3 dirB;
        if (whereToStart+1 == pointsInfo.Count - 1) {

            dirB = pointsInfo[whereToStart+1].dir.normalized;
        } else {
            dirB = pointsInfo[whereToStart+2].pos - b;
        }

        dirA    = pointsInfo[whereToStart].dir * 1;
        dirB    = pointsInfo[whereToStart+1].dir * 1;

        Vector3 ad = a + dirA;
        Vector3 bd = b - dirB;
        float subT = (t * (pointsInfo.Count-1)); //(t*(pointsInfo.Count-1));// - (int)(t*pointsInfo.Count-1);
        subT -= (float) System.Math.Truncate(subT);
        //Debug.Log("Start: " + whereToStart + ", subT: " + subT);
        return Bezier(a, ad, bd, b, subT);
    }
    public static Vector3 Bezier(List<PosDirPair> pointsInfo, float t) {

        int whereToStart = (int) ((pointsInfo.Count-1) * t);

        if (t >= 1) {
            return pointsInfo[pointsInfo.Count - 1].pos;
        } else if (t <= 0) {
            return pointsInfo[0].pos;
        }

        if (pointsInfo.Count == 3) {

            Vector3 a       = pointsInfo[0].pos;
            Vector3 b       = pointsInfo[1].pos;
            Vector3 c       = pointsInfo[2].pos;
            return Bezier(a, b, c, t);
        }



        return Vector3.zero;
    }
    public static Vector3 Bezier(Vector3 A, Vector3 B, float t) 
    {
        return Vector3.Lerp(A, B, t);  
    }
    
    public static Vector3 Bezier(Vector3 A, Vector3 B, Vector3 C, float t) 
    {
        Vector3 S = Bezier(A,B,t);
        Vector3 E = Bezier(B,C,t);
        return  Bezier(S,E,t);
    }
    
    public static Vector3 Bezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t) 
    {
        Vector3 S = Bezier(A,B,C,t);
        Vector3 E = Bezier(B,C,D,t);
        return  Bezier(S,E,t);
    }
}
public class PosDirPair {
    public Vector3 pos;
    public Vector3 dir;
    public PosDirPair(Vector3 p, Vector3 d) {
        pos = p;
        dir = d;
    }
    public PosDirPair(PosDirPair pair) {
        pos = pair.pos;
        dir = pair.dir;
    }
}