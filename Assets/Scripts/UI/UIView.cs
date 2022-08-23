using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform m_tabContainerTransform;
    [SerializeField] private Transform m_tabContentContainerTransform;

    private int m_viewId;

    public int ViewID { get { return m_viewId; } }

    public List<UIViewTab> allUITabViews = new List<UIViewTab>();
    public UIViewTab activeTab;

    public GameObject tabPrefab, tabContentPrefab;

    public void AddTab( string tabName, GameObject tabContent = null )
    {
        var tabObj = Instantiate(tabPrefab, m_tabContainerTransform);

        if(tabContent == null)
        {
            tabContent = Instantiate(tabContentPrefab, m_tabContentContainerTransform);
        }

        var tab = tabObj.GetComponent<UIViewTab>();
        tab.myUIView = this;
        tab.tabName = tabName;
        tab.content = tabContent;
        tabContent.transform.SetParent(m_tabContentContainerTransform);
        tabContent.transform.localScale = Vector3.one;
        var tabContentRectTransform = tabContent.GetComponent<RectTransform>();
        tabContentRectTransform.anchoredPosition = Vector2.zero;
        tabContentRectTransform.sizeDelta = Vector2.zero;
        tab.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => CloseTab(tab));
        activeTab = tab;
        allUITabViews.Add(tab);
        SwitchTo(tab);
    }

    public void CloseTab( UIViewTab tab )
    {
        allUITabViews.Remove(tab);

        Destroy( tab.gameObject );

        if(allUITabViews.Count == 0)
        {
            ViewManager.Instance.Close(this);
        }
        else
        {
            SwitchTo(allUITabViews[allUITabViews.Count - 1]);
        }
    }

    public void SwitchTo( UIViewTab tab )
    {
        foreach(UIViewTab ltab in allUITabViews)
        {
            if (ltab.Equals( tab ))
            {
                continue;
            }

            ltab.content.SetActive(false);
        }

        activeTab = tab;
        activeTab.content.SetActive(true);
    }

    public void MinimizeView()
    {
        
    }

    public void MaximizeView()
    {
        
    }

    public void CloseView()
    {
        ViewManager.Instance.Close(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
