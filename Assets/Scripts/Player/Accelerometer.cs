using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Accelerometer : MonoBehaviour
{
    [SerializeField] private GameObject _rightGameObject = default;
    [SerializeField] private GameObject _leftGameObject = default;
    [SerializeField] private TextMeshProUGUI time;
    private float dx = default;
    private bool right = default;
    private float moveSpeed = 1.0f;
    private float _timer = 5f;
    


    private void Awake()
    {
        SelectDirection();
    }

    private void Update()
    {
        dx = Input.acceleration.x;
        Debug.Log(dx);
        SelectDirection();
    }
    
    void SelectDirection()
    {
        if (dx >= 0.1f)
        {
            right = true;
        }
        else if(dx <= -0.1f)
        {
            right = false;
        }
        SetDirection();
        TimeToSelect();
    }

    void SetDirection()
    {
        if (right)
        {
            _rightGameObject.GetComponent<Renderer>().material.color = Color.green;
           // StartCoroutine(LockDirection());
        }
        else
        {
            _rightGameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        if (right == false)
        {
            _leftGameObject.GetComponent<Renderer>().material.color = Color.green;
          //  StartCoroutine(LockDirection());
        }
        else
        {
            _leftGameObject.GetComponent<Renderer>().material.color = Color.red;

        }
    }

    void TimeToSelect()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = 0;
            time.text = "0";
            LockDirection();
        }
        else
        {
            time.text = time.ToString();
        }
    }

   

   void LockDirection()
    {
        float elapsedTime = 0;
        Vector3 targetPosition = new Vector3(0, 2.98f, -4.48f);
       
        if (right)
        {
                    while (elapsedTime < 1)
                    {
                        _rightGameObject.transform.position = Vector3.Lerp(_rightGameObject.transform.position,
                            targetPosition, elapsedTime);
                        elapsedTime += Time.deltaTime * moveSpeed;
                        
                        _leftGameObject.SetActive(false);
                        

                    }

        }
        if(!right)
        {

                    while (elapsedTime < 1)
                    {
                        _leftGameObject.transform.position = Vector3.Lerp(_leftGameObject.transform.position,
                            targetPosition, elapsedTime);
                        elapsedTime += Time.deltaTime * moveSpeed;
                        _rightGameObject.SetActive(false);
                        
                    }
                

        }

       
    }
}
