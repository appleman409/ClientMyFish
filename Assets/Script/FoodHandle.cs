using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodHandle : MonoBehaviour
{
    [Header("Food Handle")]
    [SerializeField]
    private float timefood;
    private Image frontFoodBar;
    [SerializeField] private bool hungry;
    [SerializeField] private bool eating;
    void Start()
    {
        Fish fish = transform.parent.GetComponent<Fish>();
        timefood = fish.Food();
        if (timefood == 0)
        {
            hungry = true;
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fish fish = transform.parent.GetComponent<Fish>();
        if (timefood == 0)
        {
            hungry = true;
            gameObject.SetActive(true);
        }
        else hungry = false;

        if (eating)
        {
            fish.Food(timefood);
            gameObject.SetActive(true);
        }
        
    }

    public void eatting(float food)
    {
        timefood += food;
        eating = true;
        frontFoodBar.fillAmount = (float)timefood / 100;
        if (timefood >= 100)
        {
            Invoke("DelayedFunction", 2f);
        }
        Invoke("DelayedFunction", 20f);
    }
    
    void DelayedFunction()
    {
        eating = false;
        gameObject.SetActive(false);
    }
    
    IEnumerator Waiting()
    {

        // Chờ 10 giây
        yield return new WaitForSeconds(1000000);
        
    }
}
