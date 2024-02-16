using System.Collections;

namespace SpaceRobotics;

public class RingArray<T>(int size)
{
    public T[] array = new T[size];

    public T this[int index]
    {
        get => array[GetIndexNormalized(index)];
        set => array[GetIndexNormalized(index)] = value;
    }

    public int Length => array.Length;

    public IEnumerator GetEnumerator() => array.GetEnumerator();

    private int GetIndexNormalized(int index)
        => index < 0 ? Length - 1 - index : index % (Length - 1);
}
