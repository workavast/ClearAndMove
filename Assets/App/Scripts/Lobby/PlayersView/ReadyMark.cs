using UnityEngine;

namespace App.Lobby.PlayersView
{
    public class ReadyMark : MonoBehaviour
    {
        [SerializeField] private GameObject readyMark;
        [SerializeField] private GameObject unReadyMark;

        public void SetState(bool isReady)
        {
            if (isReady)
            {
                readyMark.SetActive(true);
                unReadyMark.SetActive(false);
            }
            else
            {
                readyMark.SetActive(false);
                unReadyMark.SetActive(true);
            }
        }
    }
}