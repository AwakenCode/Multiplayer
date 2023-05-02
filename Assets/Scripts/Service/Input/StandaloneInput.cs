using System;
using Common;
using UnityEngine;
using UInput = UnityEngine.Input;

namespace Service.Input
{
    public class StandaloneInput : IInputService, IUpdateable
    {
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";

        private readonly Updater _updater;

        public event Action OnLeftMouseButtonClicked;
        public Vector2 Rotation { get; private set; }
        public Vector2 Movement { get; private set; }

        public void Update(float deltaTime)
        {
            Rotation = new Vector2(UInput.GetAxis(MouseX), UInput.GetAxis(MouseY));
            Movement = new Vector2(UInput.GetAxis(Horizontal), UInput.GetAxis(Vertical));

            if (UInput.GetMouseButtonDown(0)) 
                OnLeftMouseButtonClicked?.Invoke();
        }
    }
}