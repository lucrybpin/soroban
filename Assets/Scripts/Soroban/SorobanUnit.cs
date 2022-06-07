using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.Soroban {


    [RequireComponent( typeof( AudioSource ) )]
    public class SorobanUnit : MonoBehaviour {

        [SerializeField] bool isOn = false;
        [SerializeField] int value;
        [SerializeField] float deltaPosition;
        [SerializeField] SorobanUnit above;
        [SerializeField] SorobanUnit bellow;
        [SerializeField] Color offColor;
        [SerializeField] Color onColor;
        [SerializeField] iTween.EaseType easeType;
        [SerializeField] AudioClip soundFx;
        RectTransform rectTransform;
        Vector2 offPosition;
        Vector2 onPosition;
        AudioSource audioSource;

        void Start () {

            isOn = false;
            rectTransform = GetComponent<RectTransform>();
            offPosition = rectTransform.anchoredPosition;
            onPosition = new Vector3( offPosition.x, offPosition.y + deltaPosition );
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = soundFx;

        }

        void Update () {
            if (Input.GetKeyDown(KeyCode.J)) {
                Debug.Log( GetComponent<RectTransform>().anchoredPosition );
                rectTransform.anchoredPosition = onPosition;
            }

            if (Input.GetKeyDown( KeyCode.C )) {
                Debug.Log( GetComponent<RectTransform>().anchoredPosition );
                rectTransform.anchoredPosition = offPosition;
            }
        }

        public void Switch() {

            SorobanRuler soroban = FindObjectOfType<SorobanRuler>();

            if (!isOn) {
                
                if (above != null) {
                    if (!above.isOn)
                        above.Switch();
                }

                Move( onPosition, easeType );
                soroban.value += value;
                ChangeColor( onColor );
                isOn = true;

            } else {

                Move( offPosition, easeType );
                soroban.value -= value;
                ChangeColor( offColor );
                isOn = false;

                if (bellow != null) {
                    if (bellow.isOn)
                        bellow.Switch();
                }

            }
            FindObjectOfType<SorobanOutputText>().UpdateText();

        }

        private void Move (Vector2 finalPosition, iTween.EaseType easeType) {

            iTween.ValueTo(
                gameObject,
                iTween.Hash(
                    "from", rectTransform.anchoredPosition,
                    "to", finalPosition,
                    "time", 0.21f,
                    "easeType", easeType,
                    "onupdate", "MoveExecute",
                    "oncomplete", "PlaySound" 
                )
            );

        }

        private void MoveExecute (Vector2 position) {
            rectTransform.anchoredPosition = position;
        }

        private void OnComplete() {
            PlaySound();
        }

        private void PlaySound() {
            audioSource.PlayDelayed( UnityEngine.Random.Range( 0, 0.1f ) );
            audioSource.pitch = UnityEngine.Random.Range( 0.95f, 1.05f );
        }

        private void ChangeColor (Color newColor) {
            GetComponent<Image>().color = newColor;
        }
    }

}