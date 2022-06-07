using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Puzzle.Soroban {

    public class SorobanOutputText : MonoBehaviour {
        TMP_Text text;
        SorobanRuler sorobanRuler;

        private void Start () {

            sorobanRuler = GameObject.FindObjectOfType<SorobanRuler>();
            text = GetComponent<TMP_Text>();
            UpdateText();

        }

        public void UpdateText () {

            text.text = sorobanRuler.value.ToString( "D7" );

        }
    } 

}
