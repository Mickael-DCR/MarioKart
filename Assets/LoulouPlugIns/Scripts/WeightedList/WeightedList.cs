using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;




/// <summary>
/// I Exist only to abstract stuffs
/// </summary>
public abstract class SerializedWeightedListParent
{
    protected virtual void AddNewElement()
    {

    }
    protected virtual void RemoveElementAt(int i)
    {

    }

    public virtual void InitializeIfNull(){}
}
[System.Serializable]
public struct WeightedElement<T>
{
    public T Element;
    public float Weight;

    public WeightedElement(T element, float weight = 1)
    {
        Element = element;
        Weight = weight;
    }
}


[System.Serializable]
public class WeightedList<T> : SerializedWeightedListParent
{

    public List<WeightedElement<T>> _weightedElementsList;

    public WeightedList()
    {
        _weightedElementsList = new List<WeightedElement<T>>();
    }

    public int Count => _weightedElementsList.Count;

    public float TotalWeight => _weightedElementsList.Sum((_weightedElement)=> MathF.Max(_weightedElement.Weight,0));

    public void Add(T element, float weight)
    {
        _weightedElementsList.Add(new WeightedElement<T>(element, weight));
    }

    

    public T this[int index]
    {
        get
        {
            return _weightedElementsList[index].Element;
        }
        set
        {
            //To see what is the less consuming
            //base[index] = new WeightedElement<T>(value, base[index].Weight);
            var _temp = _weightedElementsList[index];
            _temp.Element = value;
            _weightedElementsList[index] = _temp;
        }

    }

    public float GetWeightAtIndex(int index)
    {
        return _weightedElementsList[index].Weight;
    }

    public void SetWeightAtIndex(int index, float weight)
    {

        //To see what is the less consuming
        //base[index] = new WeightedElement<T>(value, base[index].Weight);
        var _temp = _weightedElementsList[index];
        _temp.Weight = weight;
        _weightedElementsList[index] = _temp;
    }

    public void SetWeightOfObject(T element, float weight)
    {
        var _tempIndex = _weightedElementsList.FindIndex((_weightedElement) => _weightedElement.Element.Equals(element));
        if( _tempIndex < 0 ) 
        { 
            Debug.LogError("Please make sure that your list have the element : " + element.ToString());
            return;
        }
        SetWeightAtIndex(_tempIndex,weight);
    }

    public T GetRandomElement()
    {
        #region Bug Prenventing
        if (_weightedElementsList.Count <= 0)
        {
            Debug.LogError("The list has a count of 0");
            return default;
        }
        //if(_weightedElementsList.FindIndex((_weightedElement) => _weightedElement.Weight <=0)>=0)
        //{
        //    Debug.LogError("An element of the weighted list has a non-positive weight, first element of the list returned");
        //    return this[0];
        //}
        #endregion

        var _totalWeight = this.TotalWeight;
        var _indexByWheight = UnityEngine.Random.Range(0, _totalWeight);
        var _currentWeight = .0f;

        foreach (var item in _weightedElementsList)
        {
            if (item.Weight <= 0)
            {
                continue;
            }
            _currentWeight += item.Weight;
            if(_indexByWheight<=_currentWeight)
            {
                return item.Element;
            }
        }

        return default;
    }

    protected override void AddNewElement()
    {
        Add(default, 1);
    }

    protected override void RemoveElementAt(int i)
    {
        _weightedElementsList.RemoveAt(i);
    }


    public override void InitializeIfNull()
    {
        if(_weightedElementsList == null)
        {
            _weightedElementsList = new List<WeightedElement<T>>();
        }
    }

}
