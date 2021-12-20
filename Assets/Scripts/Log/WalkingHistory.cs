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
        Log = LogObj.GetComponent<Log>();
        // ログの記録を開始
        StartCoroutine(nameof(AddWalkingHistory));
    }

    void Update()
    {
        // コントローラのAボタンが押されたら、ログを保存する
        if (OVRInput.Get(OVRInput.Button.One))
        {
            // 適当にsampleuser_日付.csvの形式に
            var fileName = "sampleuser_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            Save(fileName, walkings);
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
            // 2秒待つ
            yield return cachedWait;
        }
    }

    private void Save(string fileName, List<Walking> history)
    {
        // ログの出力をする
        Log.Output(fileName, WalkingListToCSV(history));
    }

    private List<string> WalkingListToCSV(List<Walking> history) {
        List<string> str = new List<string>() {"time,position.x,position.y,position.z,forward.x,forward.y,forward.z"};
        for (int i = 0; i < history.Count; i++)
        {
            str.Add(string.Join(",", new List<string>(){
                history[i].time.ToString(), 
                history[i].position.x.ToString(),
                history[i].position.y.ToString(),
                history[i].position.z.ToString(),
                history[i].forward.x.ToString(),
                history[i].forward.y.ToString(),
                history[i].forward.z.ToString()
            }));
        }
        return str;
    }
}
