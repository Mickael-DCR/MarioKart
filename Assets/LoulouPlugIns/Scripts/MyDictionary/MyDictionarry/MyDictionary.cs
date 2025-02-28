using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Script by Louis Viktor Celeyron

[System.Serializable]
public struct DictionaryEntry<Tk,Tv> 
{
    public Tk key;
    public Tv value;

    public DictionaryEntry(Tk key, Tv value)
    {
        this.key = key;
        this.value = value;
    }

    public override string ToString()
    {
        return "Key : " + key.ToString() + " Value : " + value.ToString();
    }
}


public abstract class BaseDictionary
{
    /// <summary>
    /// Current size of the Dictionary
    /// </summary>
    public virtual int Size => 0;
    /// <summary>
    /// Is the dictionary not empty or null 
    /// </summary>
    public virtual bool IsValid => false;
    /// <summary>
    /// Element Name displayed on the drawer
    /// </summary>
    public virtual string ElementName(int i) => "Element";
    /// <summary>
    /// Return true if the same key is showed twice
    /// </summary>
    public virtual bool HasTheSameKeyTwice => false;
    
    /// <summary>
    /// Add Default entry
    /// </summary>
    public virtual void Add() { }
    /// <summary>
    /// Remove entry at index
    /// </summary>
    /// <param name="i"></param>
    public virtual void Remove(int i) { }

    /// <summary>
    /// Initialize the list
    /// </summary>
    public virtual void InititializeIfNull() { }

    /// <summary>
    /// Calls a debug warning if 2 keys have the same values for example
    /// </summary>
    public virtual void CheckIfEverythingIsOkay()
    {

    }
}

[System.Serializable]
public class MyDictionary<Tk, Tv> : BaseDictionary
{
    [SerializeField]
    private List<DictionaryEntry<Tk, Tv>> _dictionaryEntries;

    public Tv this[Tk key]
    {
        get
        {
            return GetValue(key);
        }
        set
        {
            SetValue(key, value);   
        }
    }

    //Base vars
    public override bool IsValid
    {
        get
        {
            return _dictionaryEntries != null && _dictionaryEntries.Count > 0;
        }
    }
    public override int Size => IsValid ? _dictionaryEntries.Count : 0;
    public override bool HasTheSameKeyTwice
    {
        get
        {
            if (IsValid)
            {
                foreach (var dictionaryEntry in _dictionaryEntries)
                {
                    if ( dictionaryEntry.key != null)
                    {
                        if (_dictionaryEntries.FindAll((DictionaryEntry<Tk,Tv> entry) => entry.key!=null&&entry.key.Equals(dictionaryEntry.key)).Count>1)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }

    public MyDictionary()
    {
        InititializeIfNull();
    }

    //See in base
    public override string ElementName(int i)
    {
        var _isOk = IsValid;

        return _isOk ? _dictionaryEntries[i].key.ToString() : base.ElementName(i);
    }
    
    //See in base
    public override void CheckIfEverythingIsOkay()
    {
        
    }

    //See in Base
    public override void InititializeIfNull()
    {
        _dictionaryEntries = _dictionaryEntries ==  null? new List<DictionaryEntry<Tk,Tv>>() : _dictionaryEntries;
    }

    //See in base
    public override void Add()
    {
        _dictionaryEntries.Add(new DictionaryEntry<Tk,Tv>(default,default));
    }
    /// <summary>
    /// Add a new entry to the dictionary
    /// </summary>
    /// <param name="key">key of the entry</param>
    /// <param name="value">value of the entry</param>
    public void Add(Tk key, Tv value)
    {

        //negate the methods if the key exist 
        //Maybe just place a Warning or something
        if (CheckKey(key))
        {
            Debug.LogError("You can't add an entry if a key already exist in an entry of the dictionary");
        }

        //Add a new entry for the dictionary
        _dictionaryEntries.Add(new DictionaryEntry<Tk, Tv>(key,value));
    }
    
    //See in base
    public override void Remove(int i)
    {
        _dictionaryEntries.RemoveAt(i);
    }
    /// <summary>
    /// Remove an entry of the dictionary
    /// </summary>
    /// <param name="key"> key of the entry to remove</param>
    public void Remove(Tk key)
    {
        var _index = GetIndex(key);
        if (_index < 0)
        {
            Remove(_index);
        }
    }

    /// <summary>
    /// Perform a foreach on the list and launch the action in consequence 
    /// </summary>
    /// <param name="action"></param>
    public void ForEach(System.Action<Tk,Tv> action)
    {
        foreach (var entrie in _dictionaryEntries)
        {
            action.Invoke(entrie.key, entrie.value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">key to check</param>
    /// <returns>Does the key exist in the dictionary</returns>
    public bool CheckKey(Tk key)
    {
        if(!IsValid)
        {
            return false;
        }


        foreach (var _entry in _dictionaryEntries)
        {
            
            if (_entry.key != null && _entry.key.Equals(key))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">value to check</param>
    /// <returns>Does the value exist in the dictionary</returns>
    public bool CheckValue(Tv value)
    {
        if(!IsValid)
        {
            return false;
        }
        foreach (var _entry in _dictionaryEntries)
        {
            if (_entry.value.Equals(value))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">value to check</param>
    /// <param name="howMany"> how many value are in the dictionary</param>
    /// <returns>Does the value exist in the dictionary</returns>
    public bool CheckValue(Tv value, out int howMany)
    {
        howMany = 0;

        //Raise how many if value exist
        foreach (var _entry in _dictionaryEntries)
        {
            if (_entry.value.Equals(value))
            {
                howMany++;
            }
        }

        return howMany > 0;
    }

    /// <summary>
    /// Return the current index of the entered  key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int GetIndex(Tk key)
    {
        for (int i = 0; i < Size; i++)
        {
            if (key.Equals(_dictionaryEntries[i].key))
            {
                return i;
            }
        }
        return -1;
    }
    
    /// <summary>
    /// Return the matching value of a key
    /// </summary>
    /// <returns>value in the dictionary at the entry "key", return default if key do not exist</returns>
    public Tv GetValue(Tk key)
    {
        var _index = GetIndex(key);

        if (_index >= 0)
        {
            return _dictionaryEntries[_index].value;
        }

        return default;
    }
    /// <summary>
    /// Return the matching value of a key
    /// </summary>
    /// <param name="isValid"> out true if a value is returned</param>
    /// <returns>value in the dictionary at the entry "key", return default if key do not exist</returns>
    public Tv GetValue(Tk key, out bool isValid)
    {
        var _index = GetIndex(key);

        if (_index >= 0)
        {
            isValid = true;
            return _dictionaryEntries[_index].value;
        }

        isValid = false;
        return default;
    }

    public void SetValue(Tk key, Tv value)
    {
        var _index = GetIndex(key);
        if( _index >= 0)
        {
            _dictionaryEntries[_index] = new DictionaryEntry<Tk, Tv>(key,value);
        }
    }

    public List<Tk> GetKeys()
    {
        var list = new List<Tk>();
        foreach (var entry in _dictionaryEntries)
        {
            list.Add(entry.key);
        }
        return list;
    }
    public List<Tv> GetValues()
    {
        var list = new List<Tv>();
        foreach (var entry in _dictionaryEntries)
        {
            list.Add(entry.value );
        }
        return list;
    }
}
