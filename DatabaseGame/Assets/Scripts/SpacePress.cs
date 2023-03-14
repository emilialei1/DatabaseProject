using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpacePress : MonoBehaviour
{
    public Animator spaceAnim;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceAnim.SetBool("isKeyDown", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            spaceAnim.SetBool("isKeyDown", false);
        }
    }
}
