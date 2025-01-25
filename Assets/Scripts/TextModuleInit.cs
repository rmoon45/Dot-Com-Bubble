using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class TextModuleInit
    {
        private string[] texts =
        {
            "Jan 1. 1998 - hello! :) my name is des, and this right here is my very own corner of the internet dedicated to the things that i love. i mostly use it as a digital journal, of both my personal life and interests."
            
        };

        [SerializeField] private TextMeshPro t;
        
        void OnEnable()
        {
            t.text = texts[0];
        }
    }
}