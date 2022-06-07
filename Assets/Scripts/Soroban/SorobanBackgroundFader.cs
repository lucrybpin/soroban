using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SorobanBackgroundFader : MonoBehaviour
{
    [SerializeField] Image background0;
    [SerializeField] Image background1;
    [SerializeField] float transitionTime;
    int state = 0;
    float transitionTimer;

    [SerializeField] List<Sprite> imageList = new List<Sprite>();
    int currentIndex = 0;

    private void Start () {
        state = 0;
        transitionTimer = 0f;
        transitionTimer = ( transitionTimer == 0f ) ? 30f : transitionTimer;
    }

    private void Update () {

        transitionTimer += Time.deltaTime;
        if (transitionTimer >= transitionTime) {

            if (state == 0) {
                background1.sprite = imageList [ currentIndex ];
                StartCoroutine( FadeTo( background1, 1 , 5f ));
                state = 1;
            } else {
                background0.sprite = imageList [ currentIndex ];
                StartCoroutine( FadeTo( background1, 0, 5f ) );
                state = 0;
            }
            
            transitionTimer = 0;
            currentIndex = ( currentIndex + 1 ) % imageList.Count;
        }

    }


    private IEnumerator FadeTo (Image image, float finalAlpha, float totalTime) {

        float initialAlpha = image.color.a;
        for (float i = 0; i < 1f; i += Time.deltaTime / totalTime) {
            image.color = new Color( image.color.r, image.color.g, image.color.b, Mathf.Lerp( initialAlpha, finalAlpha, i ) );
            yield return null;
        }
        yield return null;

    }


}
