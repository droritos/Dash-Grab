using UnityEngine;
using TMPro;
using Game.Data;
using Game.Client.Enemy;

namespace Game.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;

        private int _score = 0;

        private const string _TEXT= "Score: ";

        private void Start()
        {
            UpdateText(0);
            EventObserver.OnPickUpCollected += AddScore;
            EventObserver.OnEnemyHit += SubstractScore;
        }

        private void AddScore(PickUpBlock valuePickUpToAdd)
        {
            _score += valuePickUpToAdd.WorthValue;
            UpdateText(_score);
        }

        private void SubstractScore(Enemy valueEnemy)
        {
            _score -= valueEnemy.WorthValue;
            UpdateText(_score);
        }


        private void UpdateText(int newValue)
        {
            scoreText.SetText(_TEXT + newValue.ToString());
        }
    }
}
