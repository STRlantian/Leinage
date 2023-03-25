using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager MainInstance;

    [Header("游戏控制")]
    public bool IsPlaying;
    public bool AutoPlay;
    public float CurrentTime;
    public DreamOfStars.GamePlay.Chart CurrentChart; // !!!! 注意 这里用的是我的 Chart，后续再合并一下

    [Header("物体")]
    public Camera MainCamera;
    public Transform PaneContainer;

    [Header("音乐")]
    public AudioSource GameAudioPlayer;
    public GameSong Song;

    [Header("游戏设置")]
    public float AudioOffset;
    public float VisualOffset;

    public void Awake()
    {
        MainInstance = this;
    }

    private void OnDestroy()
    {
        MainInstance=MainInstance==this?null:MainInstance;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!MainCamera) MainCamera= Camera.main;
        
        // 调节相机视场
        float camRatio=Mathf.Min(1,(3f*Screen.width)/(2f*Screen.height));
        MainCamera.fieldOfView = Mathf.Atan2(Mathf.Tan(30 * Mathf.Deg2Rad), camRatio) * 2 * Mathf.Rad2Deg;

        StartCoroutine(InitChart());
    }

    public IEnumerator InitChart()
    {
        yield return null;

        _testCode();

        IsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlaying)
        {
            CurrentTime += Time.deltaTime;
            if(CurrentTime > 0 && CurrentTime<GameAudioPlayer.clip.length) { 
                if(!GameAudioPlayer.isPlaying) GameAudioPlayer.Play(); // 播放音乐
                // TODO: 更精准的音乐时间控制
            }

            // TODO: 进度条控制

            // 相机运动控制
            CurrentChart.Camera.Update(CurrentTime);
            MainCamera.transform.position = CurrentChart.Camera.CameraPivot;
            MainCamera.transform.eulerAngles = CurrentChart.Camera.CameraRotation;
            MainCamera.transform.Translate(Vector3.back * CurrentChart.Camera.PivotDistance);

        }
    }

    private void _testCode()

    {
        // 创建测试用例 TODO: 未来删除

        CameraController controller = new CameraController()
        {
            CameraPivot = new Vector3(635, 300, -900),
            CameraRotation = new Vector3(0, 0, 0),
            PivotDistance = 0
        };
        TimeNode test1 = new TimeNode()
        {
            ID = "CameraRotation_X",
            Duration = 6f,
            Offset = 0,
            To = 40,
            EaseFunc = EaseFunction.Sine,
            EaseMode=EaseMode.In
        };
        TimeNode test2 = new TimeNode()
        {
            ID = "CameraRotation_Z",
            Offset = 3f,
            To = 60,
            Duration = 6f,
            EaseFunc = EaseFunction.Cubic,
            EaseMode = EaseMode.In
        };
        TimeNode test3 = new TimeNode()
        {
            ID = "CameraPivot_Z", // 有bug?
            Duration = 5f,
            Offset = 5f,
            To = -5f,
            EaseFunc = EaseFunction.Cubic,
            EaseMode = EaseMode.In
        };

        //controller.StoryBoard.Add(test1);
        //controller.StoryBoard.Add(test2);
        //controller.StoryBoard.Add(test3);


        //controller.SetCameraCenter(10f, 6f, 1000f, EaseFunction.Quartic);

        CurrentChart = new DreamOfStars.GamePlay.Chart()
        {
            Camera = controller,
        };
    }

}
