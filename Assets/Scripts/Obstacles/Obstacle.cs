using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Obstacle : MonoBehaviour
{
    //let obstacle spawner assign this
    public int numberValue;
    [SerializeField] Text numberValueText;

    public void SetNumberValue(int number)
    {
        this.numberValue = number;
        numberValueText.text = numberValue.ToString();
    }

    
}
