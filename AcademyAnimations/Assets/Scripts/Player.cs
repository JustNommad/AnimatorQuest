using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 _defaultPos;
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _highestJump = 2.0f;
    [SerializeField] private LayerMask _raycastMask;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private AudioClip _damage;
    [SerializeField] private AudioClip _step;
    [SerializeField] private AudioClip _swing;

    private SpriteRenderer _sprite;
    private Animator _animator;

    private bool isAttack;
    private bool isJump;
    void Start()
    {
        transform.position = _defaultPos;
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if (!isAttack)
            CalculateMovement();
        PlayAnimation();
    }

    private void CalculateMovement()
    {
        var hor = Input.GetAxis("Horizontal");
        var dir = new Vector3(hor, 0, 0);
        transform.Translate(dir * _speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * _highestJump,ForceMode2D.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.F) && !isJump)
            isAttack = true;
    }

    private void PlayAnimation()
    {
        if (!isAttack)
        {
            if(!isJump)
                _animator.SetFloat("Move", Mathf.Abs(Input.GetAxis("Horizontal")));
            else
                _animator.SetBool("Jump", isJump);
            
            if (Input.GetKey(KeyCode.D))
                _sprite.flipX = true;
            else if (Input.GetKey(KeyCode.A))
                _sprite.flipX = false;
            return;
        }
        _animator.Play("PlayerAttack");
    }

    private void Attack()
    {
        AudioSource.PlayClipAtPoint(_swing, transform.position);
        var side = _sprite.flipX ? Vector2.right : Vector2.left;
        var hit = Physics2D.Raycast(transform.position, side, 0.5f, _raycastMask);
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<Enemy>()?.Damage(side);
            AudioSource.PlayClipAtPoint(_damage, transform.position);
        }
    }

    private void EndOfAttacking()
    {
        isAttack = false;
        _animator.Play("PlayerIdle");
    }
    private void EndOfJump()
    {
        isJump = false;
        _animator.SetBool("Jump", isJump);
    }

    private void Step()
    {
        _particle.Play();
        AudioSource.PlayClipAtPoint(_step, transform.position);
    }
}