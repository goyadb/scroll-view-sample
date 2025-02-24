using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(RectTransform))]
public class ScrollViewController : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    
    private ScrollRect _scrollRect;
    private RectTransform _rectTransform;
    
    private List<Item> _items;
    
    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        LoadData();
    }

    /// <summary>
    /// _items에 있는 값을 Scroll View에 표시하는 함수
    /// </summary>
    private void ReloadData()
    {
        
    }

    private void LoadData()
    {
        _items = new List<Item>
        {
            new Item {imageFileName = "image1", title = "Title 1", subtitle = "Subtitle 1"},
            new Item {imageFileName = "image2", title = "Title 2", subtitle = "Subtitle 2"},
            new Item {imageFileName = "image3", title = "Title 3", subtitle = "Subtitle 3"},
            new Item {imageFileName = "image4", title = "Title 4", subtitle = "Subtitle 4"},
            new Item {imageFileName = "image5", title = "Title 5", subtitle = "Subtitle 5"},
            new Item {imageFileName = "image6", title = "Title 6", subtitle = "Subtitle 6"},
            new Item {imageFileName = "image7", title = "Title 7", subtitle = "Subtitle 7"},
            new Item {imageFileName = "image8", title = "Title 8", subtitle = "Subtitle 8"},
            new Item {imageFileName = "image9", title = "Title 9", subtitle = "Subtitle 9"},
            new Item {imageFileName = "image10", title = "Title 10", subtitle = "Subtitle 10"}
        };
        ReloadData();
    }

    #region Scroll Rect Events

    public void OnValueChanged(Vector2 value)
    {
        var x = _scrollRect.content.anchoredPosition.x;
        var y = _scrollRect.content.anchoredPosition.y;
        var w = _rectTransform.rect.width;
        var h = _rectTransform.rect.height;
        
        Debug.Log($"x: {x}, y: {y}, w: {w}, h: {h}");
    }

    #endregion
}
