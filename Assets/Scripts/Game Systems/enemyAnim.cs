using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyAnim : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _colldierDistance;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _range;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameObject _player;
    private float cooldownTmer = Mathf.Infinity;
    [SerializeField] private float dmg = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTmer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTmer >= _attackCooldown)
            {
                cooldownTmer = 0;
                _animator.SetTrigger("Attack");
                StartCoroutine(DamagePlayer());
            }
        }

        if (_rb.velocity.magnitude > 0) 
        {
            _animator.SetBool("isWalking", true);
        }
        
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center + transform.right *_range * transform.localScale.x * _colldierDistance,
           new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z), 0, Vector2.left, 0, _playerLayer);
        
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_boxCollider.bounds.center +transform.right*_range * transform.localScale.x * _colldierDistance, new Vector3(_boxCollider.bounds.size.x * _range, _boxCollider.bounds.size.y, _boxCollider.bounds.size.z));
    }
    IEnumerator DamagePlayer()
    {
        yield return new WaitForSeconds(.5f);
        if (PlayerInSight())
        {
            HealthSystem hp = _player.GetComponent<HealthSystem>();
            hp.TakeDMG(dmg);
        }
    }
}

