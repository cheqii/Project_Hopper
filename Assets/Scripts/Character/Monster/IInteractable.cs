using System;
using UnityEngine;

namespace Character.Monster
{
    // [System.Serializable]
    public interface IInteractable
    {
        public void InteractToObject(int damage = default);
    }
}