using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Load : MonoBehaviour
{
    [SerializeField]
    private GameObject Plots;
    private List<Walking> walkings;

    void Start()
    {
        walkings = new List<Walking>();
        var filePath = Application.persistentDataPath + "/Log/data.csv";
        Read(filePath);
    }
    public void Read(string filePath)
    {
        if (File.Exists(filePath))
        {
            string[] data = File.ReadAllLines(filePath);
            for (int i = 1; i < data.Length; i++)
            {
                string[] d = data[i].Split(',');
                walkings.Add(new Walking(
                    DateTime.Parse(d[0]),
                    new Vector3(Convert.ToSingle(d[1]), Convert.ToSingle(d[2]), Convert.ToSingle(d[3])),
                    new Vector3(Convert.ToSingle(d[4]), Convert.ToSingle(d[5]), Convert.ToSingle(d[6]))
                ));
            }
            StartCoroutine(Plots.GetComponent<PlotHistory>().Plot(walkings));
        }
    }
}