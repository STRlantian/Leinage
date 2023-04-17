using STRlantian.Gameplay.Note;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    //public Transform PaneContainer; 暂时不用了，动态生成了

    [Header("音乐")]
    public AudioSource GameAudioPlayer;
    public GameSong Song;

    [Header("游戏设置")]
    public float AudioOffset;
    public float VisualOffset;
    public float SyncThreshold = .05f;
    [Space]
    public float PerfectWindow = 64;
    public float GoodWindow = 128;
    public float BadWindow = 256;


    private float previousTime;

    public void Awake()
    {
        MainInstance = this;
    }

    private void OnDestroy()
    {
        MainInstance=MainInstance == this ? null : MainInstance;
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
        long time = System.DateTime.Now.Ticks;
        long t;

        _testCode();

        // 读入 PaneGroup 信息
        Dictionary<string, PaneGroupController> groupControllers = new Dictionary<string, PaneGroupController>();
        foreach (PaneGroup group in CurrentChart.PaneGroups)
        {
            PaneGroupController gc = new GameObject(group.Name).AddComponent<PaneGroupController>();
            gc.SetGroup(group);
            groupControllers.Add(group.Name, gc);

            t = System.DateTime.Now.Ticks;
            if (t - time > 33e4)
            {
                time = t;
                yield return null;
            }
        }

        // 循环遍历 PaneGroup，确认 PaneGroup 之间的父子关系
        foreach (KeyValuePair<string, PaneGroupController> pair in groupControllers)
        {
            string parent = pair.Value.CurrentGroup.Group;
            if (!string.IsNullOrEmpty(parent))
            {
                pair.Value.transform.SetParent(groupControllers[parent].transform);
                pair.Value.ParentGroup = groupControllers[parent];
            }

            t = System.DateTime.Now.Ticks;
            if (t - time > 33e4)
            {
                time = t;
                yield return null;
            }
        }

        // 循环遍历 PaneList，确认 Pane 和 PaneGroup 的父子关系
        foreach (Pane p in CurrentChart.PaneList)
        {
            PaneController pc = new GameObject(p.Name).AddComponent<PaneController>();
            if (!string.IsNullOrEmpty(p.Group))
            {
                pc.transform.SetParent(groupControllers[p.Group].transform);
                pc.parentGroup = groupControllers[p.Group];
            }
            pc.InitPane(p);
            // 循环遍历 Pane 的判定线，并生成 Note
            foreach(JudgeLine l in p.Lines)
            {
                JudgeLineController jc = new GameObject(l.name).AddComponent<JudgeLineController>();
                jc.transform.parent = pc.transform;
                jc.InitJudgeLine(l);
                if (jc.isReady)
                {
                    t = System.DateTime.Now.Ticks;
                    if (t - time > 33e4)
                    {
                        time = t;
                        yield return null;
                    }
                }
                else
                {
                    yield return new WaitUntil(() => jc.isReady);
                }
            }
        }


        IsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlaying)
        {
            CurrentTime += Time.deltaTime;
            if(CurrentTime > 0 && CurrentTime < GameAudioPlayer.clip.length) { 
                if(!GameAudioPlayer.isPlaying) GameAudioPlayer.Play(); // 播放音乐
                float preciseTime = GameAudioPlayer.timeSamples / (float)Song.Clip.frequency;
                if (Mathf.Abs(GameAudioPlayer.time - CurrentTime) > SyncThreshold) GameAudioPlayer.time = CurrentTime;
                else if (previousTime != preciseTime)
                {
                    CurrentTime = preciseTime;
                    previousTime = preciseTime;
                }
                // TODO: 更精准的音乐时间控制
            }

            // TODO: 进度条控制

            // 相机运动控制
            CurrentChart.Camera.Update(CurrentTime + AudioOffset + VisualOffset);
            MainCamera.transform.position = CurrentChart.Camera.CameraPivot;
            MainCamera.transform.eulerAngles = CurrentChart.Camera.CameraRotation;
            MainCamera.transform.Translate(Vector3.back * CurrentChart.Camera.PivotDistance);


            // 响应触摸事件
            Dictionary<Touch,NoteController> touchEvents= new Dictionary<Touch,NoteController>();
            if(!AutoPlay)
            {
                foreach(Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began) touchEvents.Add(touch, null);
                    else if (touch.phase== TouchPhase.Moved)
                    {
                        if (Vector2.SqrMagnitude(touch.deltaPosition) < 100) continue; // 太小的滑动不判断（防止误判
                        float dir = Mathf.Atan2(touch.deltaPosition.x,touch.deltaPosition.y) * Mathf.Rad2Deg;

                        // TODO: 后续的滑动判定
                    }
                    else if (touch.phase==TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        // TODO: 尾判？
                    }
                }
            }

        }
    }

    private void _testCode()

    {
        // 创建测试用例 TODO: 未来删除

        CameraController controller = new CameraController()
        {
            CameraPivot = new Vector3(0,0,-10),
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
            To = -8f,
            EaseFunc = EaseFunction.Cubic,
            EaseMode = EaseMode.In
        };

        //controller.StoryBoard.Add(test1);
        //controller.StoryBoard.Add(test2);
        controller.StoryBoard.Add(test3);

        //controller.SetCameraCenter(10f, 6f, 1000f, EaseFunction.Quartic);

        #region cubeTestCode
        float cubeSize = 5f;
        // TODO: 这里的常量可能有bug，还没来得及细看
        float[,] position3D = new float[,] { { -cubeSize / 2, 0, 0 }, { cubeSize / 2, 0, 0 }, { 0, cubeSize / 2, 0 }, { 0, -cubeSize / 2, 0 }, { 0, 0, -cubeSize / 2 }, { 0, 0, cubeSize / 2 } }; // 初始位置数组
        float[,] rotation3D = new float[,] { { 0, -90, 0 }, { 0, 90, 0 }, { -90, 0, 0 }, { 90, 0, 0 }, { 180, 0, 0 }, { 0, 0, 0 } }; // 初始旋转方向
        string[] paneName = new string[] { "left", "right", "top", "bottom", "front", "back" }; // 各面名称

        List<Pane> paneList = new List<Pane>();

        // 硬核建面
        for (int i = 0; i < 6; i++)
        {
            paneList.Add(new Pane()
            {
                Position = new Vector3(position3D[i, 0], position3D[i, 1], position3D[i, 2]),
                Rotation = new Vector3(rotation3D[i, 0], rotation3D[i, 1], rotation3D[i, 2]),
                Width = cubeSize,
                Height = cubeSize,
                Name = paneName[i],
                Group = "CubeGroup"
            });

            // 要展示的动效(x
            //paneList[i].StoryBoard.Add(new TimeNode()
            //{
            //    ID = "PanePos_Z",
            //    To = 0,
            //    Offset = 5,
            //    Duration = 5f,
            //    EaseFunc = EaseFunction.Cubic,
            //    EaseMode = EaseMode.InOut
            //});
            //paneList[i].StoryBoard.Add(new TimeNode()
            //{
            //    ID = "PaneRot_Z",
            //    To = 0,
            //    Offset = 5,
            //    Duration = 5f
            //});
            //paneList[i].StoryBoard.Add(new TimeNode()
            //{
            //    ID = "PaneRot_X",
            //    To = 0,
            //    Offset = 5,
            //    Duration = 5f
            //});
            //paneList[i].StoryBoard.Add(new TimeNode()
            //{
            //    ID = "PaneRot_Y",
            //    To = 0,
            //    Offset = 5,
            //    Duration = 5f
            //});
            if (i > 1)
            {
                paneList[i].StoryBoard.Add(new TimeNode()
                {
                    ID = "PaneWidth",
                    To = 20,
                    Offset = 4,
                    Duration = 8
                });
            }

            if (i == 4)
            {
                JudgeLine line= new JudgeLine()
                {
                    vertices = { 0, 1 }
                };
                Note note = new Note()
                {
                    noteType = NoteType.TAP,
                    position = 0,
                    length = 1
                };
                //for(int j=0;j<5; j++)
                //{
                //    Note noteClone = note.DeepClone();
                //    noteClone.offset = j;
                //    if (j == 1) noteClone.noteType = NoteType.FLICK;
                //    if (j == 2) noteClone.noteType = NoteType.HOLD;
                //    line.notes.Add(noteClone);
                //}
                paneList[i].Lines.Add(line);
                paneList[i].Lines.Add(new JudgeLine()
                {
                    vertices = {1, 2}
                });
            }
            if(i == 2)
            {
                paneList[i].Lines.Add(new JudgeLine()
                {
                    vertices = { 0,1,2,3,0 }
                });
            }
        }


        #endregion

        paneList.Add(new Pane()
        {
            Position = new Vector3(500,500,0),
            Rotation = new Vector3(0,0,0),
            Width = cubeSize*2,
            Height = cubeSize*2,
            Name = "Single Pane",
            Group = "CubeCubeGroup"
        });

        List<PaneGroup> groups= new List<PaneGroup>() { 
            new PaneGroup()
            {
                Name = "CubeGroup",
                Position = Vector3.zero,
                Rotation = Vector3.zero,
                Group="CubeCubeGroup"
            },
            new PaneGroup()
            {
                Name="CubeCubeGroup",
                Position = Vector3.zero,
                Rotation = Vector3.zero
            }
        };


        groups[0].StoryBoard.Add(new TimeNode()
        {
            ID = "GroupRot_X",
            To = -90,
            Offset = 5,
            Duration = 4f,
            EaseFunc=EaseFunction.Quartic
        });
        groups[0].StoryBoard.Add(new TimeNode()
        {
            ID = "GroupRot_Y",
            To = -20,
            Offset = 5,
            Duration = 4f
        });
        groups[0].StoryBoard.Add(new TimeNode()
        {
            ID = "GroupRot_Z",
            To = -20,
            Offset = 5,
            Duration = 4f
        });
        //groups[0].StoryBoard.Add(new TimeNode()
        //{
        //    ID = "GroupPos_X",
        //    To = 720,
        //    Offset = 10,
        //    Duration = 4f
        //});

        //groups[1].StoryBoard.Add(
        //    new TimeNode()
        //    {
        //        ID = "GroupRot_Y",
        //        To = 720,
        //        Offset = 10,
        //        Duration = 4f
        //    });


        CurrentChart = new DreamOfStars.GamePlay.Chart()
        {
            Camera = controller,
            PaneGroups = groups,
            PaneList = paneList
        };
    }

}
