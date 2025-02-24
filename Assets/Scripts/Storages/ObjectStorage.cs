using UnityEngine;

public class ObjectStorage : Storage
{
    [SerializeField] string Name;
    [SerializeField] public int MaxCapacity;

    private void Update()
    {
        if (Smthng > MaxCapacity)
        {
            LostSmt(Smthng - MaxCapacity);
            Smthng = MaxCapacity;
        }
    }

    public void LostEverything()
    {
        ValueSpended?.Invoke(Smthng);
        Smthng = 0;
    }

    public void NewMaxCup(int ToAdd)
    {
        MaxCapacity += ToAdd;
    }
}
