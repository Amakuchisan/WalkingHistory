using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Log : MonoBehaviour
{
    private static string filePath = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        CreateDirectory();
    }

    // ログ保存用のディレクトリを作成
    private void CreateDirectory()
    {
        filePath = Application.persistentDataPath + "/Log/";

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
    }
    
    // ログファイルの保存
    public static void Output(string fileName, List<string> logs)
    {
        var FullPath = Path.Combine(filePath, fileName);
        File.WriteAllLines(FullPath, logs);
    }
}
