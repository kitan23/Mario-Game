using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public float bounceHeight = 0.5f; 
    public float bounceSpeed = 4f; 
    public float coinMoveSpeed = 8f; 
    public float coinMoveHeight = 3f; 
    public float coinFallDistance = 2f; 
    public Sprite emptyBlockSprite; 
    private Vector2 originalPosition; 
    private bool canBounce = true; 
    public AudioSource popSound; 
    // Start is called before the first frame update
    void Start()
    {
        popSound = GetComponent<AudioSource>(); 
        originalPosition = transform.localPosition; 
        
    }

    public void questionBlockBounce ()
    {
        if (canBounce)
        {
            canBounce = false; 
            StartCoroutine(Bounce()); 
        }
    }

    //Update is called once per frame
    void Update()
    {
        
    }

    void changeSprite ()
    {
        GetComponent<Animator>().enabled = false; 
        GetComponent<SpriteRenderer>().sprite = emptyBlockSprite; 
    }

    void presentCoin ()
    {
        popSound.Play(); 
        GameObject spinningCoin = (GameObject)Instantiate (Resources.Load("Prefabs/Spinning_Coin", typeof(GameObject)));
        spinningCoin.transform.SetParent (this.transform.parent); 
        spinningCoin.transform.localPosition = new Vector2 (originalPosition.x, originalPosition.y + 1); 
        StartCoroutine(moveCoin(spinningCoin)); 
    }
    IEnumerator Bounce ()
    {
        changeSprite();
        presentCoin(); 
        while (true)
        { 
            transform.localPosition = new Vector2 (transform.localPosition.x, transform.localPosition.y + bounceSpeed * Time.deltaTime);
            if (transform.localPosition.y >= originalPosition.y + bounceHeight)
            {
                break;  
            }
            yield return null; 
        }
        while (true)
        {
            transform.localPosition = new Vector2 (transform.localPosition.x, transform.localPosition.y - bounceSpeed * Time.deltaTime); 
            if (transform.localPosition.y <= originalPosition.y)
            {
                transform.localPosition = originalPosition; 
                break;
            }
            yield return null; 
        }

    }

    IEnumerator moveCoin (GameObject coin)
    {
        while (true)
        {
            coin.transform.localPosition = new Vector2 (coin.transform.localPosition.x, coin.transform.localPosition.y + coinMoveHeight * Time.deltaTime); 
            if (coin.transform.localPosition.y >= originalPosition.y + coinMoveHeight + 1)
            {
                break; 
            }
            yield return null; 
        }
        while (true)
        {
            coin.transform.localPosition = new Vector2 (coin.transform.localPosition.x, coin.transform.localPosition.y - coinMoveSpeed * Time.deltaTime); 
            if (coin.transform.localPosition.y <= originalPosition.y + coinFallDistance + 1)
            {
                Destroy(coin.gameObject); 
                break; 
            }

        }

    }

}
