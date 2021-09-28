using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    public GameObject target;// 카메라가 따라갈 대상
    public float moveSpeed;
    private Vector3 targetPosition;

    static public CameraManager instance;

    public BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;
    // BoxCollider 영역의 최소 최대 값을 지님

    private float halfWidth;
    private float halfHeight;
    // 카메라의 반너비, 반높이 => 화면이 밖으로 나가지 않도록 세팅을 위함

    private Camera theCamera;

    private void Awake() // Awake는 Start보다 먼저 실행하도록 함
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // 씬을 이동하더라도 캐릭터를 삭제하지 않도록 함 ( instance = null일때만, 즉 처음에만)
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start() // 캐릭터가 삭제하지 않도록 설정 후 카메라 영역 설정
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;//카메라의 Size의 반 높이
        halfWidth = halfHeight * Screen.width / Screen.height; // 반높이 공식 = 반높이 * 해상도 넓이/해상도 높이
                
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position,targetPosition,Time.deltaTime*moveSpeed) ;
            // Vector3.Lerp(A,B,Time.delta) : A값에서 B값으로 이동 ( Update마다 부드럽게 이동을 시키기 위해 Time.deltaTime이용
            // Time.DeltaTime는 다음 업데이트까지 걸린 시간(지난 프레임이 완료되는 데까지 걸린 시간)
            // 1초에 moveSpeed만큼 이동시키기 위함

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            // Clamp 함수는 최소/최대값을 설정하여 float값이 범위 이외의 값을 넘지 않도록 함!

            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
