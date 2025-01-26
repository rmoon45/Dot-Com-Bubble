using System.Collections;
using UnityEngine;

public class CoroutineEndAnimation : MonoBehaviour
{
    // rotate the Transform.rotate in +z direction

    [SerializeField] float timeAnimation;

    void OnEnable() {

        StartCoroutine(FunnyRotateAnimation());
    }

    // interface type from System.Collections; used to support iteration
    IEnumerator FunnyRotateAnimation() {
        
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 100));
        yield return null;

        // for (float i = 0; i < timeAnimation; i += Time.deltaTime) {
        //     // rotate utnil z is 0
        //     // gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(-360, 0, i / timeAnimation));
        //     // gameObject.transform.Rotate(new Vector3(0, 0, Mathf.Sin(Time.time * 50)));

        //     // changing
        //     float scaleNum = Mathf.Lerp(0, 0.64645f, i / timeAnimation);
        //     gameObject.transform.localScale = new Vector3(scaleNum, scaleNum, scaleNum);

        //     yield return null;
        // }
    }

    void OnDisable() {
        StopCoroutine(FunnyRotateAnimation());
    }


}
