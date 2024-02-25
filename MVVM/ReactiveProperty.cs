using System;

[Serializable]
public class ReactiveProperty<T>
{

    public event Action<T> OnChange;

    private T _value;
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            OnChange?.Invoke(_value);
        }
    }

    public void Invoke()
    {
        OnChange?.Invoke(_value);
    }
}

[Serializable]
public class ReactiveProperty<T,Y>
{

    public event Action<T,Y> OnChange;

    private T _tValue;
    private Y _yValue;

    public T FirstValue
    {
        get => _tValue;
        set
        {
            _tValue = value;
            OnChange?.Invoke(_tValue,_yValue);
        }
    }
    public Y SecondValue
    {
        get => _yValue;
        set
        {
            _yValue = value;
            OnChange?.Invoke(_tValue, _yValue);
        }
    }

    public void Invoke()
    {
        OnChange?.Invoke(_tValue,_yValue);
    }
}
