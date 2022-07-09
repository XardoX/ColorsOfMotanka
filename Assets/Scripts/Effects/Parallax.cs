using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
   [SerializeField] private Vector2 _parallaxMultiplier = default;
    private GameObject _cam;
    private Vector3 _lastCamPosition;
    private float _textureUnitSizeX;
    
    private void Start()
    {
        _cam = Camera.main.gameObject;
        _lastCamPosition = _cam.transform.position;
        Sprite _sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D _texture = _sprite.texture;
        _textureUnitSizeX = _texture.width / _sprite.pixelsPerUnit;
    }
    private void Update() 
    {
        Vector3 _deltaMovement = _cam.transform.position - _lastCamPosition;
        transform.position += new Vector3(_deltaMovement.x * _parallaxMultiplier.x, _deltaMovement.y * _parallaxMultiplier.y, transform.position.z );
        _lastCamPosition = _cam.transform.position;

        if(Mathf.Abs(_cam.transform.position.x - transform.position.x) >= _textureUnitSizeX)
        {
            float _offsetPosX = (_cam.transform.position.x - transform.position.x) % _textureUnitSizeX;
            transform.position = new Vector3(_cam.transform.position.x + _offsetPosX, transform.position.y);
        }
    }
}
