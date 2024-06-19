using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Monster
{
    public class MonsterHpBar : MonoBehaviour
    {
        private Monster _monster;

        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private Transform heartContainer;
        [SerializeField] private List<Image> heartImage;
        
        // Start is called before the first frame update
        void Start()
        {
            _monster = GetComponent<Monster>();
            InitialHeart(_monster.MaxHealth);
        }

        public void UpdateMonsterHealth()
        {
            var heartFill = _monster.Health;
            heartImage[heartFill].color = Color.clear;
        }

        private void InitialHeart(int value)
        {
            for (int i = 0; i < value; i++)
            {
                var health = Instantiate(heartPrefab, heartContainer);
                heartImage.Add(health.GetComponent<Image>());
            }
        }
    }
}
