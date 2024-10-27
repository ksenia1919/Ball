using UnityEngine;

public class TrackingBall : MonoBehaviour
{
    public GameObject _ball;
    public float respawnDistance = 40f;
    private EdgeCollider2D _edgeCollider;
    private Vector2 _lastContactPoint;
    private Vector2 _initialPosition;
    private bool hasTouchedTrack = false;

    private float _initialGravityScale;
    private bool _initialGravityFlipped;

    void Start()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();
        _initialPosition = _ball.transform.position; // Сохраняем начальную позицию
        _lastContactPoint = _ball.transform.position; 

        _initialGravityScale = BallController.instance.gravityScale;
        _initialGravityFlipped = BallController.instance.isUpsideDown;
    }

    void Update()
    {
        if (_ball == null) return; 

        if (Physics2D.IsTouching(_edgeCollider, _ball.GetComponent<Collider2D>()))
        {
            hasTouchedTrack = true; 
            _lastContactPoint = _ball.transform.position;
        }
        
        if (hasTouchedTrack && !Physics2D.IsTouching(_edgeCollider, _ball.GetComponent<Collider2D>()))
        {
            float distance = Vector2.Distance(_ball.transform.position, _lastContactPoint);

            if (distance > respawnDistance)
            {
                RespawnBall();
            }
        }
        else
        {
            _lastContactPoint = _ball.transform.position;
        }
    }

    public void RespawnBall()
    {
        hasTouchedTrack = false;

        _ball.transform.position = _initialPosition;
        _ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _ball.GetComponent<Rigidbody2D>().gravityScale = _initialGravityScale;
        if (_initialGravityFlipped)
        {
            _ball.GetComponent<Rigidbody2D>().gravityScale *= -1;
        }
    }
}

