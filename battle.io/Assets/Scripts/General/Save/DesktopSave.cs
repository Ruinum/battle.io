using System.IO;
using UnityEngine;

public class DesktopSave : ISave
{
    private const string FILE_NAME = "Save.data";

    public void Save(string saveData)
    {
        string destination = Application.dataPath + FILE_NAME;

        FileStream file;

        if (File.Exists(destination)) file = File.Open(destination, FileMode.Truncate);
        else file = File.Create(destination);

        StreamWriter stream = new StreamWriter(file);

        stream.WriteLine(saveData);
        stream.Close();
    }

    public bool Load(out string savedData)
    {
        var destination = Application.dataPath + FILE_NAME;
        savedData = "";

        FileStream file;
        if (File.Exists(destination)) file = File.OpenRead(destination);
        else return false;

        StreamReader stream = new StreamReader(file);
        savedData = stream.ReadToEnd();
        stream.Close();

        return true;       
    }
}
