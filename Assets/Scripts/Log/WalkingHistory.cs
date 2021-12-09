using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking
{
    public DateTime time;
    public Vector3 position;
    public Vector3 forward;
    public Walking(DateTime time, Vector3 position, Vector3 forward)
    {
        this.time = time;
        this.position = position;
        this.forward = forward;
    }
}

public class WalkingHistory : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject LogObj;
    private Log Log;
    private List<Walking> walkings = new List<Walking>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(AddWalkingHistory));
        Log = LogObj.GetComponent<Log>();
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            var fileName = "sampleuser_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            Save(fileName, // TODO: filename
            walkings);
        }
    }

    private IEnumerator AddWalkingHistory()
    {
        WaitForSeconds cachedWait = new WaitForSeconds(2f);
        while (true)
        {
            walkings.Add(new Walking(
                DateTime.Now,
                player.transform.position,
                player.transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor").transform.forward
            ));
            // 2•b‘Ò‚Â
            yield return cachedWait;
        }
    }

    private void Save(string fileName, List<Walking> history)
    {
        Log.Output(fileName, WalkingHistoryToCSV(history));
    }

    private List<string> WalkingHistoryToCSV(List<Walking> history) {
        List<string> str = new List<string>() {"time,position.x,position.y,position.z,forward.x,forward.y,forward.z"};
        for (int i = 0; i < history.Count; i++)
        {
            str.Add(string.Join(",", new List<string>() { history[i].time.ToString(), history[i].position.ToString(), history[i].forward.ToString() }));
        }
        return str;
    }
}
