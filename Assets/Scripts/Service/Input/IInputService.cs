using System;
using UnityEngine;

namespace Service.Input
{
    public interface IInputService : IService
    {
        event Action OnLeftMouseButtonClicked;
        Vector2 Rotation { get; }
        Vector2 Movement { get; }
    }
}