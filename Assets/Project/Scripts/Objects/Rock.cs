using System;
using System.Collections.Generic;
using Project.Scripts.Player;
using UnityEngine;

namespace Project.Scripts.Objects
{
    public class Rock : MonoBehaviour
    {
        [SerializeField] private List<Debuff> _debuffs;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _defaultMaterialColor;
        [SerializeField] private Color _transparentMaterialColor;
        [SerializeField] private float _changeColorDuration;

        public bool PlayerInTrigger { get; private set; }
        public GameObject Player { get; private set; }
        private bool _needChangeMaterialColor;
        private float _changeMaterialColorTimeElapsed;
        private Color _startChangeMaterialColor;
        
        private void Update()
        {
            if (!_needChangeMaterialColor) return;
            
            _changeMaterialColorTimeElapsed += Time.deltaTime;
            var targetColor = PlayerInTrigger ? _transparentMaterialColor : _defaultMaterialColor;

            _renderer.material.color = Color.Lerp(_startChangeMaterialColor, new Color(_startChangeMaterialColor.r, _startChangeMaterialColor.g, _startChangeMaterialColor.b , targetColor.a), _changeMaterialColorTimeElapsed / _changeColorDuration);
                
            if (_changeMaterialColorTimeElapsed >= _changeColorDuration)
            {
                _changeMaterialColorTimeElapsed = 0f;
                _needChangeMaterialColor = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<Player.Player>()) return;

            PlayerInTrigger = true;
            Player = other.gameObject;
            ChangeColor();
            foreach (var debuff in _debuffs)
            {
                other.gameObject.GetComponent<PlayerDebuffManager>()?.AddDebuff(debuff);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.GetComponent<Player.Player>()) return;
            
            PlayerInTrigger = false;
            Player = null;
            ChangeColor();
            foreach (var debuff in _debuffs)
            {
                other.gameObject.GetComponent<PlayerDebuffManager>()?.RemoveDebuff(debuff);
            }
        }

        private void ChangeColor()
        {
            _needChangeMaterialColor = true;
            _changeMaterialColorTimeElapsed = 0;
            _startChangeMaterialColor = _renderer.material.color;
        }
    }
}
