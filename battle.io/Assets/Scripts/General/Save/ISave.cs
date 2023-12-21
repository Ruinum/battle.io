public interface ISave
{
    void Save(string saveData);
    bool Load(out string savedData);
}
