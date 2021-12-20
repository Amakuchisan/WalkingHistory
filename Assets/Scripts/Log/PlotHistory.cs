using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotHistory : MonoBehaviour
{
    [SerializeField]
    private GameObject Cube;
    [SerializeField]
    private GameObject Line;
    [SerializeField]
    private float seconds = 2.0f;

    // 2秒に1回生成する
    public IEnumerator Plot(List<Walking> walkings)
    {
        WaitForSeconds cachedWait = new WaitForSeconds(seconds);
        GameObject CubeObj;
        for (int i = 0; i < walkings.Count; i++)
        {
            CubeObj = Instantiate(Cube, walkings[i].position, Quaternion.Euler(walkings[i].forward));
            CubeObj.transform.parent = transform;
            // seconds 秒待つ
            yield return cachedWait;
        }
        RenderLine(walkings);
    }

    private void RenderLine(List<Walking> walkings)
    {
        GameObject beam = Instantiate(Line, walkings[0].position, Quaternion.Euler(walkings[0].forward));
        LineRenderer line = beam.GetComponent<LineRenderer>();
        // 頂点数
        line.positionCount = walkings.Count;
        // 線を引く
        for (int i = 0; i < walkings.Count; i++)
        {
            line.SetPosition(i, walkings[i].position);
        }
    }
}
