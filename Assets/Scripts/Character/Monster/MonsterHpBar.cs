using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Monster
{
    public class MonsterHpBar : MonoBehaviour
    {
        [SerializeField] private Monster _monster;

        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private Transform heartContainer;
        [SerializeField] private List<Image> heartImage;

        // Start is called before the first frame update
        void Start()
        {
            InitialHeart(_monster.MaxHealth);
        }

        public void UpdateMonsterHealth(bool initial)
        {
            var heartFill = _monster.Health;
            if(!initial)
                heartImage[heartFill].color = Color.clear;
            else
            {
                foreach (var heart in heartImage)
                {
                    heart.color = Color.white;
                }
            }
        }

        private void InitialHeart(int value)
        {
            if (heartImage.Count > 0) return;
            for (int i = 0; i < value; i++)
            {
                var health = Instantiate(heartPrefab, heartContainer);
                heartImage.Add(health.GetComponent<Image>());
            }
        }
        
    }
}
