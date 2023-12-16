using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Transform = UnityEngine.Transform;

public class Movement : MonoBehaviour
{
    public float speed = 0.3f;
    public Vector2 targetPoint;
    public float distance = 3f;
    public bool havefood = false;
    private GameObject foodObject;
    void Start()
    {
        targetPoint = RandomPoint();
        Rotation();
    }
    void Update()
    {
        Fish fish = GetComponent<Fish>();
        if (!fish.info.activeSelf)
        {
            if (fish.Food() < 28800 && fish.Food() >= 0 && !havefood)
            {
                findfood();
            }
            Move();
            if (havefood)
            {
                targetPoint = foodObject.transform.position;
                speed = 0.5f;
                return;
            }
            else speed = 0.3f;
            
            
            foreach (Transform child in transform.parent.transform)
            { 
                if (child != transform)
                {
                    if (selectcheck(targetPoint, child.position))
                    {
                        if (Vector3.Distance(child.position, transform.position) <= distance)
                        {
                            Debug.Log(Vector3.Distance(child.position, transform.position));
                            targetPoint = RandomPoint();
                            Rotation();
                        
                        }
                    }
                }
            }
        }
        else
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 5f);
        }
    }

    public void findfood()
    {
        foreach (Transform child in transform.parent.transform)
        {
            // Kiểm tra xem đối tượng con có khác với đối tượng hiện tại không
            if (child != transform)
            {
                Food foodScript = child.GetComponent<Food>();
                if (foodScript != null)
                {
                    if (!foodScript.getTarget())
                    {
                        havefood = true;
                        targetPoint = child.position;
                        foodObject = child.gameObject;
                        foodScript.settarget();
                        break;
                    }
                    
                }
            }
        }
    }

    public float getfood()
    {
        Food foodScript = foodObject.GetComponent<Food>();
        if (foodScript != null)
        {
            Debug.Log(foodScript.getfood());
            return foodScript.getfood();
        }
        return 0;
    }
    
    public bool selectcheck(Vector3 firstpos, Vector3 secondpos)
    {
        var bounds = new Bounds(transform.position, Vector3.zero);
        bounds.Encapsulate(firstpos);
        return bounds.Contains(secondpos);
    }
    
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, (float)90.00);
    }

    void Move()
    {
        if ((Vector2)transform.position != targetPoint)
        {
            Vector2 temptransform = new Vector2(transform.position.x, transform.position.y);
            Vector2 tempVector2 = Vector2.MoveTowards(temptransform, targetPoint, speed * Time.deltaTime);
            
            transform.position = new Vector3(tempVector2.x, tempVector2.y, transform.position.z);
        }
        else
        {
            if (havefood)
            {
                havefood = false;
                eatfood();
                Destroy(foodObject);
                
            }
            targetPoint = RandomPoint();
            Rotation();
        }
    }

    void eatfood()
    {
        FoodHandle foodHandle = GetComponentInParent<FoodHandle>();

        if (foodHandle != null)
        {
            foodHandle.eatting(getfood());
        }
    }
    
    void Rotation()
    {
        if ((transform.position.x < targetPoint.x && transform.position.y < targetPoint.y ) || (transform.position.x > targetPoint.x && transform.position.y > targetPoint.y ))
        {
            if (transform.position.x < targetPoint.x && !facingRight())
            {
                Vector3 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                transform.localScale = currentScale;
            }else if (transform.position.x > targetPoint.x && facingRight())
            {
                Vector3 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                transform.localScale = currentScale;
            }
            Quaternion target = Quaternion.Euler(0, 0, -10);
            transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 5f);
        }
        else if ((transform.position.x < targetPoint.x && transform.position.y > targetPoint.y ) || (transform.position.x > targetPoint.x && transform.position.y < targetPoint.y ))
        {
            if (transform.position.x < targetPoint.x && !facingRight())
            {
                Vector3 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                transform.localScale = currentScale;
            }else if (transform.position.x > targetPoint.x && facingRight())
            {
                Vector3 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                transform.localScale = currentScale;
            }
        }

    }

    bool facingRight()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        if (currentScale.x < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    Vector2 RandomPoint()
    {
        float randomX = Random.Range(-8.6f, 7.6f);
        float randomY = Random.Range(-2.5f, 1f) ;
        return new Vector2(randomX, randomY);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Water")
        {
            targetPoint = RandomPoint();
        }
    }
}
