using System.Collections;
using UnityEngine;

[System.Serializable]
public class BoardData
{
    [System.Serializable]
    public struct rowData
    {
        public GameObject[] row;
    }
    public rowData[] rows = new rowData[10];
}
