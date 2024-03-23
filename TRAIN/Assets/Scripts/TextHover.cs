using UnityEngine;

public class TextHover : MonoBehaviour
{
    private Renderer _renderer;
    private Color _baseColor;
    private Color _hoverColor;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _baseColor = Color.white;
        _hoverColor = Color.yellow;
        _renderer.material.color = _baseColor;
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = _hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _baseColor;
    }
}
