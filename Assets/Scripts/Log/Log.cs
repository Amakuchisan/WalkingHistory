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

    // ���O�ۑ��p�̃f�B���N�g�����쐬
    private void CreateDirectory()
    {
        filePath = Application.persistentDataPath + "/Log/";

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
    }
    
    // ���O�t�@�C���̕ۑ�
    public static void Output(string fileName, List<string> logs)
    {
        var FullPath = Path.Combine(filePath, fileName);
        if (!File.Exists(FullPath))
        {
            File.Create(FullPath).Close();
        }

        File.WriteAllLines(FullPath, logs);
    }
}
