using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed => GameManager.Instance.PlayerConfig.MoveSpeed;
    
    
    [SerializeField] private bool isDragging;
    [SerializeField] private float defaultX;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    private BoxCollider2D collider;
    private Vector2 refPos;
    // Start is called before the first frame update
    void Start()
    {
        isDragging = false;
        EventManager.Instance.OnWin += onWin;
        collider = transform.GetComponent<BoxCollider2D>();
    }

    private void onWin()
    {
        collider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                var fingerPos = Camera.main.ScreenToWorldPoint(touch.position);
                var _distance = Vector2.Distance(fingerPos, transform.position);
                if (_distance<=1f)
                {
                    isDragging = true;
                }

            }   

            if (touch.phase == TouchPhase.Moved)
            {
                if (isDragging)
                {
                    Vector2 _currentPos = Camera.main.ScreenToWorldPoint(new Vector2(touch.position.x,touch.position.y));
                    _currentPos.x = defaultX;
                    _currentPos.y = Mathf.Clamp(_currentPos.y, minY, maxY);
                    // var _myPos = Camera.main.ScreenToWorldPoint(touch.deltaPosition);
                    // transform.Translate(_direction * moveSpeed * Time.deltaTime);
                    transform.position = Vector2.SmoothDamp(transform.position, _currentPos, ref refPos,
                        moveSpeed * Time.deltaTime);
                }
            }

            if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }
}
