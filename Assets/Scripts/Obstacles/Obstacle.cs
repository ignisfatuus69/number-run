using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Obstacle : MonoBehaviour
{
    public System.Action<Obstacle> OnPlayerCollision;
    //let obstacle spawner assign this
    public int additive=0;
    public int numberValue=0;
    [SerializeField] Text numberValueText;
    [SerializeField] GameObject correctPFX;

    public void SetNumberValue(int number)
    {
        this.numberValue = number;
        numberValueText.text = numberValue.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("tesst");
        Instantiate(correctPFX,transform.position,transform.rotation);
        //this.gameObject.SetActive(false);
        other.GetComponentInChildren<EquationChecker>().CheckEquation(this.additive, this.numberValue,this.additive);
        OnPlayerCollision?.Invoke(this);
        Debug.Log("Checking answers");
    }
}
