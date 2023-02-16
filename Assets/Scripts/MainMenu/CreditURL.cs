using UnityEngine;

namespace MainMenu
{
    public class CreditURL : MonoBehaviour
    {
        [SerializeField] private string _url;
        
        public void Open() => Application.OpenURL(_url);
    }
}