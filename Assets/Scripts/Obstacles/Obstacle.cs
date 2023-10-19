using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Obstacle : MonoBehaviour
{
    public System.Action<Obstacle> OnPlayerInteraction;
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

    private void OnTriggerEnter(Collider player)
    {
        Debug.Log("Interacted with player");
        Instantiate(correctPFX,transform.position,transform.rotation);
        //this.gameObject.SetActive(false);
        PlayerInteraction(player.GetComponentInChildren<EquationChecker>());
    }

    public void PlayerInteraction(EquationChecker player)
    {
        player.CheckEquation(this.additive, this.numberValue, this.additive);
        OnPlayerInteraction?.Invoke(this);
    }
}
