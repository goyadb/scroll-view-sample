using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(RectTransform))]
public class ScrollViewController : MonoBehaviour
{
    [SerializeField] private float cellHeight;
    
    private ScrollRect _scrollRect;
    private RectTransform _rectTransform;
    
    private List<Item> _items;                                              // Cell에 표시할 Item 정보
    private LinkedList<Cell> _visibleCells = new LinkedList<Cell>();        // 화면에 표시되고 있는 Cell 정보
    
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
    /// 현재 보여질 Cell 인덱스를 반환하는 메서드
    /// </summary>
    /// <returns>startIndex: 가장 위에 표시될 Cell 인덱스, endIndex: 가장 아래에 표시될 Cell Index</returns>
    private (int startIndex, int endIndex) GetVisibleIndexRange()
    {
        var visibleRect = new Rect(
            _scrollRect.content.anchoredPosition.x,
            _scrollRect.content.anchoredPosition.y,
            _rectTransform.rect.width,
            _rectTransform.rect.height);

        // 스크롤 위치에 따른 시작 인덱스 계산
        var startIndex = Mathf.FloorToInt(visibleRect.y / cellHeight);

        // 화면에 보이게 될 Cell 개수 계산
        int visibleCount = Mathf.CeilToInt(visibleRect.height / cellHeight);

        // 버퍼 추가
        startIndex = Mathf.Max(0, startIndex - 1);      // startIndex가 0보다 크면 startIndex - 1, 아니면 0
        visibleCount += 2;

        return (startIndex, startIndex + visibleCount - 1);
    }

    /// <summary>
    /// _items에 있는 값을 Scroll View에 표시하는 함수
    /// _items에 새로운 값이 추가되거나 기존 값이 삭제되면 호출됨
    /// </summary>
    private void ReloadData()
    {
        // Content의 높이를 _items의 데이터의 수만큼 계산해서 높이를 지정
        var contentSizeDelta = _scrollRect.content.sizeDelta;
        contentSizeDelta.y = _items.Count * cellHeight;
        _scrollRect.content.sizeDelta = contentSizeDelta;

        // 화면에 보이는 영역에 Cell 추가
        var (startIndex, endIndex) = GetVisibleIndexRange();
        var maxEndIndex = Mathf.Min(endIndex, _items.Count - 1);
        for (int i = startIndex; i < maxEndIndex; i++)
        {
            // 셀 만들기
            var cellObject = ObjectPool.Instance.GetObject();
            var cell = cellObject.GetComponent<Cell>();
            cell.SetItem(_items[i]);
            cell.transform.localPosition = new Vector3(0, -i * cellHeight, 0);

            _visibleCells.AddLast(cell);
        }
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
