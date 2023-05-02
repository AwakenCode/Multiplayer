using Service;
using Service.Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerLabelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;

        public void Awake()
        {
            _name.text = Services
                .Container
                .Resolve<IDataProvider>()
                .GetPlayerData().Nickname;
        }
    }
}