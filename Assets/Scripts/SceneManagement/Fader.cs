using System;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private Coroutine currentlyActiveFade = null;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /*IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            print("Faded out");
            yield return FadeIn(1f); 
            print("Faded in");
        }*/

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }

        public Coroutine Fade(float target, float time)
        {
            if (currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }
            currentlyActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentlyActiveFade;//Waits for coroutine to finish
        }
        
        private IEnumerator FadeRoutine(float target, float time)
        {
            while (! Mathf.Approximately(canvasGroup.alpha, target)) // alpha is not 1
            {
                //alpha will move towards target with speed(delta) 
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null; // In coroutine means "wait for one frame"
            }
        }
        
    }
}