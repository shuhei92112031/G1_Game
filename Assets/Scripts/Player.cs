using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    bool isMoving;
    [SerializeField] LayerMask LimitLayer; //壁判定のレイヤーの変数

    void Start()
    {
        
    }

    
    void Update()
    {
        if(isMoving == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if(y != 0)
            {
                x = 0;
            }
            
            StartCoroutine(Move(new Vector2(x,y)));
        }
        
    }


    //動くプログラム
    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        Vector3 targetPos = transform.position  + direction;
        if(isWalkable(targetPos) == false)
        {
            isMoving = false;
            yield break;
        }
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5f*Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    } 


    //歩けるかどうかを調べるプログラム
    bool isWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos,0.5f,LimitLayer) == false;
    }
}
