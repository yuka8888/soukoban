using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    //���ł���܂ł̎���
    private float lifeTime;
    //���ł���܂ł̎c�莞��
    private float leftLifeTime;
    //�ړ���
    private Vector3 velocity;
    //����scale
    private Vector3 defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 0.3f;
        leftLifeTime = lifeTime;
        defaultScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.localScale = defaultScale;
        //�����_���Ō��܂�ړ��ʂ̍ő�l
        float maxVelocity = 5;
        //�e�����փ����_���Ŕ�΂�
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
        //�c�莞�Ԃɂ�菙�X��Scale������������
        transform.localScale = Vector3.Lerp(
            new Vector3(0, 0, 0),
            defaultScale,
            leftLifeTime / lifeTime);
        //�c�莞�Ԃ�0�ȉ��ɂȂ����玩�g�̃Q�[���I�u�W�F�N�g������
        if (leftLifeTime <= 0) { Destroy(gameObject); }

    }
}
