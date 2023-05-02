using System;
using Data;
using Mirror;
using Service;
using Service.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerNickname : NetworkBehaviour
    {
        [SerializeField] private TMP_Text _nicknameText;
        [SerializeField] private RectTransform _nicknameEditPanel;
        [SerializeField] private TMP_InputField _newNickNameField;
        [SerializeField] private Button _edit;
        [SerializeField] private Button _exitEditPanel;
        [SerializeField] private Button _apply;

        private PlayerData _playerData;
        
        private void Awake()
        {
            _playerData = Services
                .Container
                .Resolve<IDataProvider>()
                .GetPlayerData();
        }

        private void OnEnable()
        {
            _apply.onClick.AddListener(OnNicknameChangeApply);
            _exitEditPanel.onClick.AddListener(OnExitChangePanel);
            _edit.onClick.AddListener(OnEditNickname);
        }

        private void OnEditNickname() => _nicknameEditPanel.gameObject.SetActive(true);

        private void OnExitChangePanel() => _nicknameEditPanel.gameObject.SetActive(false);
        
        private void OnNicknameChangeApply()
        {
            _nicknameText.text = _newNickNameField.text;
            CommandChangeNickname(_newNickNameField.text);
        }

        [Command]
        private void CommandChangeNickname(string nickname) => _playerData.Nickname = nickname;
    }
}