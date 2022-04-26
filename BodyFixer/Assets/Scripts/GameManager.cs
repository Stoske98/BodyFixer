using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        #region GameManager Singleton
        private static GameManager _instance;

        public static GameManager Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion
        [SerializeField]
        private List<UIDrag> joints = new List<UIDrag>();
        [SerializeField]
        private List<UIRotate> rotators = new List<UIRotate>();
        [SerializeField]
        private List<UIGrabber> grabbers = new List<UIGrabber>();

        [SerializeField]
        private Canvas GameCanvas;
        [SerializeField]
        private Canvas EndGameCanvas;

        [SerializeField]
        private Animator anim;
        [SerializeField]
        private TextMeshProUGUI text;

        [HideInInspector]
        public float percent = 0;

        [SerializeField]
        private Bar bar;
        public void CheckPerformance()
        {
            int numberOfJoints = joints.Count;
            int numberOfRotators = rotators.Count;
            int numberOfGrabbers = grabbers.Count;

            float max = numberOfJoints + numberOfRotators + numberOfGrabbers;

            float performance = 0;

            foreach (UIDrag dragScript in joints)
            {
                performance += dragScript.getPerformance();
            }

            foreach (UIRotate rotatorsScript in rotators)
            {
                performance += rotatorsScript.getPerformance();
            }

            foreach (UIGrabber grabber in grabbers)
            {
                performance += grabber.getPerformance();
            }

            percent = performance / (max / 100);
            bar.SetBar((int)percent);
            Debug.Log(percent + "%");
            

        }

        public void Finish()
        {
            GameCanvas.enabled = false;
            Invoke("PlayAnim",2);
        }

        public void PlayAnim()
        {
            EndGameCanvas.enabled = true;
            if (percent > 75)
            {
                anim.Play("Star3");
                text.fontSize = 180;
                text.text = "AWESOME !";
            }
            else if(percent >= 25)
            {
                anim.Play("Star2");
                text.fontSize = 180;
                text.text = "GOOD JOB !";
            }
            else
            {
                anim.Play("Star1");
                text.fontSize = 130;
                text.text = "YOU CAN DO BETTER !";
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ResetJoints()
        {
            foreach (UIDrag drag in joints)
            {
                drag.ResetPositionOnJoint();
            }

            foreach (UIRotate rotators in rotators)
            {
                rotators.ResetPositionOnJoint();
            }
        }

    }
}


