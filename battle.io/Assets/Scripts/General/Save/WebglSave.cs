public class WebglSave : ISave
{   
    public void Save(string saveData)
    { 
    }

    public bool Load(out string savedData)
    {
        savedData = null;
        return false;
    }
}