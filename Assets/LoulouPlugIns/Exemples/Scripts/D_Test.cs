using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Test : MonoBehaviour
{
    //Use the MyDictionary Class as you would have used the Classic C# Dictionary class
    public MyDictionary<string, int> myIntStringDictionary;
    public MyDictionary<string, MaClass> myIntStringDictionry;
    // Start is called before the first frame update
    void Start()
    {
        //You can Use Add to add default element to the dictionary or use parameters to add custom elements
        myIntStringDictionary.Add();
        myIntStringDictionary.Add("Cool", 32);

        //You can use [] to find the value coresponding to the key in the brackets
        Debug.Log(myIntStringDictionary["Cool"]);
        
        myIntStringDictionary.GetValue("Cool"); //This works too

        myIntStringDictionary.CheckValue(2);  //returns true if the value exists in the dictionary
        myIntStringDictionary.CheckKey("Cool"); //Returns true if the key exists in the dictionary


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[System.Serializable]
public class MaClass
{
    public int i;
    public int j;
    public int k;
}