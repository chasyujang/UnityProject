using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    public int walkCount;
    private int currentWalkCount;


    private Vector3 vector;

    private bool canMove = true;

    private Animator animator;

    private BoxCollider2D boxCollider;
    public LayerMask layerMask; // 통과가 불가능한 layermask 설정하기 위함

    public string currentMapName; // transferMap 스크립트에 있는 transferMapName 저장!!!

    static public MovingObject instance;
    //static : 정적 변수, 해당 스크립트가 적용된 모든 객체들은 값을 공유함!

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null) // instance 가 null일때만 캐릭터 삭제되지 않게 하기 위함
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // 씬을 이동하더라도 캐릭터를 삭제하지 않도록 함
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
        else // 처음 외에는 instance에 값이 존재하므로 Destroy된다.
        {
            Destroy(this.gameObject);
        }

    }
    IEnumerator MoveCoroutine() // Coroutine을 사용하는 이유는 키를 한번 눌렀을 때 한번만 실행하기 위함(프레임 조절)
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) // 누르고 있는 동안은 반복!! - 빠르게 움직일 경우 자연스럽지 못한 것을 방지하기 위해 while문 사용
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0) vector.y = 0; // 대각선 이동 방지!! -> 추후에는 대각선으로 이동 가능하게!!
            else if (vector.y != 0) vector.x = 0;

            animator.SetFloat("DirX", vector.x); // vector.x가 -1일 경우 DirX가 -1로 전달
            animator.SetFloat("DirY", vector.y); // vector.x가 -1일 경우 DirX가 -1로 전달


            RaycastHit2D hit;
            // A지점, B지점으로 레이저를 하여 벽이 있는지 확인

            Vector2 start = transform.position; // A지점 : 현재 위치
            Vector2 end = start + new Vector2(vector.x*speed*walkCount,vector.y*speed*walkCount); // B지점 : 이동하고자 하는 위치
             
            boxCollider.enabled = false; // 자신의 BoxCollider와 부딪히지 않도록 false로 설정해둠
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true; // 다시 true로 전환

            if (hit.transform != null) // A지점에서 B지점으로 layermask(벽)이 있다면 break함으로 이동하지 않음
            {
                break;
            }

            animator.SetBool("Walking", true); // Walking 에 true로 전달

            while (currentWalkCount < walkCount)
            {
                transform.position += vector * (speed + applyRunSpeed); // 아래 개선! => x,y 한번에
                /*if (vector.x != 0)
                {
                    transform.Translate(vector.x * speed, 0, 0);
                    //transform.position += vector * (speed + applyRunSpeed);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * speed, 0);
                    //transform.position += vector * (speed + applyRunSpeed);
                }*/
                if (applyRunFlag) // 뛰고 있을 때는 속도가 2배이므로 카운트를 2배로 올려 이동거리 조정
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f); // 0.01초 기다리기!
            }
            currentWalkCount = 0;
        }

        animator.SetBool("Walking", false); // Walking 에 true로 전달
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) // 한번만 이동하기 위함
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
