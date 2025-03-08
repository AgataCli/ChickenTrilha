using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerContactHandler : MonoBehaviour
{
    public Image itemImage;
    public PlayerAudioController audioController;

    public string nextLevelName = "Level 1";
    bool canWinLevel = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("PERDEUU");
            SceneManager.LoadScene("GameOver");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Debug.Log("PEGOU O OVIN");
            canWinLevel = true;
            audioController.PlayGetItem();
            Destroy(collision.gameObject);
            itemImage.color = Color.white;
        }

        if (collision.gameObject.CompareTag("FinalPoint"))
        {
            if (canWinLevel)
            {
                Debug.Log("GANHOUU! :)");
                SceneManager.LoadScene(nextLevelName);
            }
            else
            {
                Debug.Log("nop, ainda nao");
            }
        }

    }
}
