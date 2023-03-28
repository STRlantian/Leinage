using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager MainInstance;

    [Header("��Ϸ����")]
    public bool IsPlaying;
    public bool AutoPlay;
    public float CurrentTime;
    public DreamOfStars.GamePlay.Chart CurrentChart; // !!!! ע�� �����õ����ҵ� Chart�������ٺϲ�һ��

    [Header("����")]
    public Camera MainCamera;
    //public Transform PaneContainer; ��ʱ�����ˣ���̬������

    [Header("����")]
    public AudioSource GameAudioPlayer;
    public GameSong Song;

    [Header("��Ϸ����")]
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
        
        // ��������ӳ�
        float camRatio=Mathf.Min(1,(3f*Screen.width)/(2f*Screen.height));
        MainCamera.fieldOfView = Mathf.Atan2(Mathf.Tan(30 * Mathf.Deg2Rad), camRatio) * 2 * Mathf.Rad2Deg;

        StartCoroutine(InitChart());
    }

    public IEnumerator InitChart()
    {
        yield return null;

        _testCode();

        // ���� PaneGroup ��Ϣ
        Dictionary<string, PaneGroupController> groupControllers = new Dictionary<string, PaneGroupController>();
        foreach (PaneGroup group in CurrentChart.PaneGroups)
        {
            PaneGroupController gc = new GameObject(group.Name).AddComponent<PaneGroupController>();
            gc.SetGroup(group);
            groupControllers.Add(group.Name, gc);
        }

        // ѭ������ PaneGroup��ȷ�� PaneGroup ֮��ĸ��ӹ�ϵ
        foreach (KeyValuePair<string, PaneGroupController> pair in groupControllers)
        {
            string parent = pair.Value.CurrentGroup.Group;
            if (!string.IsNullOrEmpty(parent))
            {
                pair.Value.transform.SetParent(groupControllers[parent].transform);
                pair.Value.ParentGroup = groupControllers[parent];
            }
        }

        // ѭ������ PaneList��ȷ�� Pane �� PaneGroup �ĸ��ӹ�ϵ
        foreach (Pane p in CurrentChart.PaneList)
        {
            PaneController pp = new GameObject(p.Name).AddComponent<PaneController>();
            if (!string.IsNullOrEmpty(p.Group))
            {
                pp.transform.SetParent(groupControllers[p.Group].transform);
                pp.ParentGroup = groupControllers[p.Group];
            }
            pp.InitPane(p);
        }

        IsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlaying)
        {
            CurrentTime += Time.deltaTime;
            if(CurrentTime > 0 && CurrentTime<GameAudioPlayer.clip.length) { 
                if(!GameAudioPlayer.isPlaying) GameAudioPlayer.Play(); // ��������
                // TODO: ����׼������ʱ�����
            }

            // TODO: ����������

            // ����˶�����
            CurrentChart.Camera.Update(CurrentTime);
            MainCamera.transform.position = CurrentChart.Camera.CameraPivot;
            MainCamera.transform.eulerAngles = CurrentChart.Camera.CameraRotation;
            MainCamera.transform.Translate(Vector3.back * CurrentChart.Camera.PivotDistance);

        }
    }

    private void _testCode()

    {
        // ������������ TODO: δ��ɾ��

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
            ID = "CameraPivot_Z", // ��bug?
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

        #region cubeTestCode
        float cubeSize = 200f;
        // TODO: ����ĳ���������bug����û���ü�ϸ��
        float[,] position3D = new float[,] { { -cubeSize / 2, 0, 0 }, { cubeSize / 2, 0, 0 }, { 0, cubeSize / 2, 0 }, { 0, -cubeSize / 2, 0 }, { 0, 0, -cubeSize / 2 }, { 0, 0, cubeSize / 2 } }; // ��ʼλ������
        float[,] rotation3D = new float[,] { { 0, -90, 0 }, { 0, 90, 0 }, { -90, 0, 0 }, { 90, 0, 0 }, { 180, 0, 0 }, { 0, 0, 0 } }; // ��ʼ��ת����
        string[] paneName = new string[] { "left", "right", "top", "bottom", "front", "back" }; // ��������

        List<Pane> paneList = new List<Pane>();
        JudgeLine testJudgeLine = new JudgeLine() {
            Vertices = {0,1}
        };

        // Ӳ�˽���
        for (int i = 0; i < 6; i++)
        {
            paneList.Add(new Pane()
            {
                Position = new Vector3(position3D[i, 0], position3D[i, 1], position3D[i, 2]),
                Rotation = new Vector3(rotation3D[i, 0], rotation3D[i, 1], rotation3D[i, 2]),
                Width = cubeSize,
                Height = cubeSize,
                Name = paneName[i],
                Group="CubeGroup"
            });

            // Ҫչʾ�Ķ�Ч(x
            paneList[i].StoryBoard.Add(new TimeNode()
            {
                ID = "PanePos_Z",
                To = 0,
                Offset = 5,
                Duration = 5f,
                EaseFunc = EaseFunction.Cubic,
                EaseMode = EaseMode.InOut
            });
            paneList[i].StoryBoard.Add(new TimeNode()
            {
                ID = "PaneRot_Z",
                To = 0,
                Offset = 5,
                Duration = 5f
            });
            paneList[i].StoryBoard.Add(new TimeNode()
            {
                ID = "PaneRot_X",
                To = 0,
                Offset = 5,
                Duration = 5f
            });
            paneList[i].StoryBoard.Add(new TimeNode()
            {
                ID = "PaneRot_Y",
                To = 0,
                Offset = 5,
                Duration = 5f
            });

            if (i == 4)
            {
                paneList[i].StoryBoard.Add(new TimeNode()
                {
                    ID = "PanePos_X",
                    To = -400,
                    Offset = 10f,
                    Duration = 5f
                });
                paneList[i].StoryBoard.Add(new TimeNode()
                {
                    ID = "PanePos_Y",
                    To = -400,
                    Offset = 10f,
                    Duration = 5f
                });
                paneList[i].Lines.Add(testJudgeLine);
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
            To = 360,
            Offset = 10,
            Duration = 4f
        });
        groups[0].StoryBoard.Add(new TimeNode()
        {
            ID = "GroupPos_X",
            To = 720,
            Offset = 10,
            Duration = 4f
        });

        groups[1].StoryBoard.Add(
            new TimeNode()
            {
                ID = "GroupRot_Y",
                To = 720,
                Offset = 10,
                Duration = 4f
            });
        //cubeController.CurrentGroup.StoryBoard.Add(new TimeNode()
        //{
        //    ID = "GroupPos_Y",
        //    To = 360,
        //    Offset = 10,
        //    Duration = 4f
        //});




        CurrentChart = new DreamOfStars.GamePlay.Chart()
        {
            Camera = controller,
            PaneGroups = groups,
            PaneList = paneList
        };
    }

}
