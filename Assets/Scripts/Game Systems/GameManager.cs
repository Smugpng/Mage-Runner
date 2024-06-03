using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement _pm;
    [SerializeField] private Rigidbody2D _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _pm.enabled = true;
        _player.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDeath()
    {
        _pm.enabled = false;
        _player.isKinematic = true;
        StartCoroutine(Death());

        
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
