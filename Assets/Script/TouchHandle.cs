using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchHandle : MonoBehaviour
{
    [SerializeField] private bool food = false;
    [SerializeField] private bool Catch = false;
    [SerializeField] private Button FoodButton; 
    [SerializeField] private Sprite foodImage1;
    [SerializeField] private Sprite foodImage2;
    [SerializeField] private GameObject FoodObject;
    public GameObject Fishbool;
    private Vector2 touchStartPos;
    public float swipeThreshold = 50.0f;

    // Update is called once per frame
    void Update()
    {
        if (food)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // Kiểm tra nếu ngón tay đang di chuyển qua lại
                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPos = touch.position;
                }

                // Kiểm tra nếu người chơi đang di chuyển ngón tay
                if (touch.phase == TouchPhase.Moved)
                {
                    // Tính toán khoảng cách di chuyển
                    float swipeDistX = touch.position.x - touchStartPos.x;

                    // Kiểm tra nếu khoảng cách di chuyển đủ lớn để được coi là một cử chỉ vuốt
                    if (Mathf.Abs(swipeDistX) > swipeThreshold)
                    {
                        // Xác định hướng cử chỉ vuốt (trái hoặc phải)
                        Debug.Log("Spawn FOOD");
                        for (int j = 0; j < 5; j++)
                        {
                            GameObject go = Instantiate(FoodObject, transform.position, Quaternion.identity) as GameObject;
                            go.transform.SetParent(Fishbool.transform);
                            go.transform.localScale = new Vector3(4, 4, 1);
                        }
                    }
                }
            }
        }
    }

    public void foodButton()
    {
        Image buttonImage = FoodButton.GetComponent<Image>();
        if (food)
        {
            buttonImage.sprite = foodImage1;
            food = false;
        }
        else
        {
            buttonImage.sprite = foodImage2;
            food = true;
        }
        
    }
    
}
