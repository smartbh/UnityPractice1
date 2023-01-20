using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 플레이어를 원하는 방향으로 이동 V
// 2. 플레이어가 원을 그리면서 이동 (싸이클로이드, 꽃패턴 전부 가능)
// 3. 미사일을 발사(순차적으로)
// 4. 미사일을 한번에 발사 (특정 범위 )

public enum Pattern { One, Two };

public class PlayerController : MonoBehaviour
{
    public GameObject bulletObject;
    public Transform bulletContainer;

    public Pattern shotPattern;
    public float moveSpeed = 2f;
    public float circleScale = 5f;
    public int angleInterval = 10;
    public int startAngle = 30;
    public int endAngle = 330;

    private int iteration = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (shotPattern == Pattern.One)
            StartCoroutine(MakeBullet());
        else if (shotPattern == Pattern.Two)
            StartCoroutine(MakeBullet2());

    }

    // Update is called once per frame
    void Update()
    {
        //PlayerMove(30); //Deg를 말하는것
        //PlayerCircle();
    }

    void PlayerMove(float _angle)
    {
        if(Input.GetKey(KeyCode.Space)) //space키를 누르고 있으면 true
        {
            Vector2 direction = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad));
            //math.cos, sin은 rad,즉 라디안으로 들어감 Rad = 0~2pi, Deg = 0~360(각).
            //Deg값을 Rad 값으로 변경해줘야함
            transform.Translate(moveSpeed * direction * Time.deltaTime);//이동을 시켜줌
            //time.deltaTime = 프레임간 시간 다른걸 보정해주기 위해 곱해줌.
        }
    }
    void PlayerCircle()
    {
        //itertaion 0->360 각도가 계속 1씩 증가
        //Deg 값이니 Rad값으로 변환해줘야함, Deg2Rad 쓰는 이유
        // 원의 크기를 결정
        Vector2 direction = new Vector2(Mathf.Cos(iteration * Mathf.Deg2Rad), Mathf.Sin(iteration * Mathf.Deg2Rad));
        transform.Translate(direction * (circleScale * Time.deltaTime));
        iteration++;
        if (iteration > 360) iteration -= 360;

    }

    private IEnumerator MakeBullet()
    {
        int fireAngle = 0; //초기 값
        while(true)
        {
            GameObject tempObject = Instantiate(bulletObject, bulletContainer, true);
            //bulletContainer 안에 bulletObject를 생성하겠다.
            Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), 
                                        Mathf.Sin(fireAngle * Mathf.Deg2Rad));
            tempObject.transform.right = direction;
            //총알 오브젝트 오른쪽을 방향으로 설정
            
            tempObject.transform.position = transform.position;
            //총알오브젝트의 위치는 플레이어의 위치로 설젖ㅇ

            yield return new WaitForSeconds(0.1f);
            //0.1초간 기다리고

            fireAngle += angleInterval;
            //발사한 각도를 설정한 값에 따라서 증가시켜준다.

            if (fireAngle > 360) fireAngle -= 360;
            //0~360 하기위함
        }
    }
    private IEnumerator MakeBullet2()
    {
        while(true)
        {
            //한번에 미사일을 만들어줌 여러개

            for(int fireAngle = startAngle; fireAngle < endAngle; fireAngle += angleInterval)
            {
                GameObject tempObject = Instantiate(bulletObject, bulletContainer, true);
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), 
                                                Mathf.Sin(fireAngle * Mathf.Deg2Rad));

                tempObject.transform.right = direction; //총알의 방향
                tempObject.transform.position = transform.position; //위치는 플레이어의 위치

            }
            //생성 끝

            yield return new WaitForSeconds(4f);

            //4초간 대기
        }
    }
}
