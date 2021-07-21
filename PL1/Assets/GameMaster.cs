using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Transform[] background; //
    public float smoothing = 1f;

    private float[] parallaxScales; //
    private Transform cam;
    private Vector3 prevCamPos;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        prevCamPos = cam.position;

        parallaxScales = new float[background.Length];

        for (int i = 0; i < background.Length; i++)
        {
            parallaxScales[i] = background[i].position.z * -1;
        }
    }

    void Update()
    {
        for (int i = 0; i < background.Length; i++)
        {
            float parallax = (prevCamPos.x - cam.position.x) * parallaxScales[i];

            float backGroundTargetPosX = background[i].position.x + parallax;

            Vector3 backGroundTargetPos = new Vector3(backGroundTargetPosX, background[i].position.y, background[i].position.z);

            background[i].position = Vector3.Lerp(background[i].position, backGroundTargetPos, smoothing * Time.deltaTime);
        }

        prevCamPos = cam.position;
    }
}
