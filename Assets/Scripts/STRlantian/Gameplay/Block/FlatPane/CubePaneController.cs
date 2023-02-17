using UnityEngine;

namespace STRlantian.Gameplay.Block.FlatPane
{
    public class CubePaneController : PaneController
    {


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }
        protected override void ChangePanePosition()
        {
            void SwitchSide(float angel, int pane)
            {
                if ((transform.rotation.x < 0
                    && transform.rotation.x > -180)
                    || (transform.rotation.x > 180
                    && transform.rotation.x < 360))
                {
                    paneList[pane].position = new Vector3(pane == 2 ? 1 : 0, pane == 1 ? 1 : 0, pane == 0 ? 1 : 0);
                }
                if ((transform.rotation.x < -180
                    && transform.position.x > -360)
                    || (transform.rotation.x > 0
                    && transform.rotation.x < 180))
                {
                    paneList[pane].position = new Vector3(pane == 2 ? -1 : 0, pane == 1 ? -1 : 0, pane == 0 ? -1 : 0);
                }
            }

            SwitchSide(transform.rotation.x, 1);
            SwitchSide(transform.rotation.y, 2);
            SwitchSide(transform.rotation.z, 0);
        }
    }
}