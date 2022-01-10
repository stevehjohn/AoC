namespace AoC.Solutions.Common;

public class FixedLengthQueue<T>
{
    private readonly int _length;

    private readonly T[] _data;

    private int _position;

    public FixedLengthQueue(int length)
    {
        _length = length;

        _data = new T[length];
    }

    public void Add(T item)
    {
        _data[_position] = item;

        _position++;

        if (_position >= _length)
        {
            _position = 0;
        }
    }

    public T First()
    {
        return _position == 0
               ? _data[_length - 1]
               : _data[_position - 1];
    }
}