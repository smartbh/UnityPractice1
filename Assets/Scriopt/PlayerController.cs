using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. �÷��̾ ���ϴ� �������� �̵� V
// 2. �÷��̾ ���� �׸��鼭 �̵� (����Ŭ���̵�, ������ ���� ����)
// 3. �̻����� �߻�(����������)
// 4. �̻����� �ѹ��� �߻� (Ư�� ���� )

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
        //PlayerMove(30); //Deg�� ���ϴ°�
        //PlayerCircle();
    }

    void PlayerMove(float _angle)
    {
        if(Input.GetKey(KeyCode.Space)) //spaceŰ�� ������ ������ true
        {
            Vector2 direction = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad));
            //math.cos, sin�� rad,�� �������� �� Rad = 0~2pi, Deg = 0~360(��).
            //Deg���� Rad ������ �����������
            transform.Translate(moveSpeed * direction * Time.deltaTime);//�̵��� ������
            //time.deltaTime = �����Ӱ� �ð� �ٸ��� �������ֱ� ���� ������.
        }
    }
    void PlayerCircle()
    {
        //itertaion 0->360 ������ ��� 1�� ����
        //Deg ���̴� Rad������ ��ȯ�������, Deg2Rad ���� ����
        // ���� ũ�⸦ ����
        Vector2 direction = new Vector2(Mathf.Cos(iteration * Mathf.Deg2Rad), Mathf.Sin(iteration * Mathf.Deg2Rad));
        transform.Translate(direction * (circleScale * Time.deltaTime));
        iteration++;
        if (iteration > 360) iteration -= 360;

    }

    private IEnumerator MakeBullet()
    {
        int fireAngle = 0; //�ʱ� ��
        while(true)
        {
            GameObject tempObject = Instantiate(bulletObject, bulletContainer, true);
            //bulletContainer �ȿ� bulletObject�� �����ϰڴ�.
            Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), 
                                        Mathf.Sin(fireAngle * Mathf.Deg2Rad));
            tempObject.transform.right = direction;
            //�Ѿ� ������Ʈ �������� �������� ����
            
            tempObject.transform.position = transform.position;
            //�Ѿ˿�����Ʈ�� ��ġ�� �÷��̾��� ��ġ�� ������

            yield return new WaitForSeconds(0.1f);
            //0.1�ʰ� ��ٸ���

            fireAngle += angleInterval;
            //�߻��� ������ ������ ���� ���� ���������ش�.

            if (fireAngle > 360) fireAngle -= 360;
            //0~360 �ϱ�����
        }
    }
    private IEnumerator MakeBullet2()
    {
        while(true)
        {
            //�ѹ��� �̻����� ������� ������

            for(int fireAngle = startAngle; fireAngle < endAngle; fireAngle += angleInterval)
            {
                GameObject tempObject = Instantiate(bulletObject, bulletContainer, true);
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), 
                                                Mathf.Sin(fireAngle * Mathf.Deg2Rad));

                tempObject.transform.right = direction; //�Ѿ��� ����
                tempObject.transform.position = transform.position; //��ġ�� �÷��̾��� ��ġ

            }
            //���� ��

            yield return new WaitForSeconds(4f);

            //4�ʰ� ���
        }
    }
}
