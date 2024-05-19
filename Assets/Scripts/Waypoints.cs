using UnityEngine;

public class Waypoints : MonoBehaviour
{
    /* This script will only work properly if waypoints objects in the editor are listed in a proper path from top to bottom!
     * If you run into bugs, please ensure the above is true as step 1 in debugging.
    */
    public static Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
