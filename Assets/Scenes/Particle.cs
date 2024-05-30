using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    //消滅するまでの時間
    private float lifeTime;
    //消滅するまでの残り時間
    private float leftLifeTime;
    //移動量
    private Vector3 velocity;
    //初期scale
    private Vector3 defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 0.3f;
        leftLifeTime = lifeTime;
        defaultScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.localScale = defaultScale;
        //ランダムで決まる移動量の最大値
        float maxVelocity = 5;
        //各方向へランダムで飛ばす
        velocity = new Vector3(
            Random.Range(-maxVelocity, maxVelocity),
            Random.Range(-maxVelocity, maxVelocity),
            0
            );
    }

    // Update is called once per frame
    void Update()
    {
        leftLifeTime -= Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        //残り時間により徐々にScaleを小さくする
        transform.localScale = Vector3.Lerp(
            new Vector3(0, 0, 0),
            defaultScale,
            leftLifeTime / lifeTime);
        //残り時間が0以下になったら自身のゲームオブジェクトを消滅
        if (leftLifeTime <= 0) { Destroy(gameObject); }

    }
}
