using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using DynamicData;

namespace NekoMacro.Utils
{
    //public class ObservableCollectionWithSelectedItem1<T> : ObservableCollection<T> where T : class, ISelected
    //{
    //    private bool _multiSelect;

    //    public delegate bool SelectionChangingHandler(ObservableCollectionWithSelectedItem<T> sender, IList<T>? addedItems, IList<T>? removedItems);
    //    public event SelectionChangingHandler? SelectionChanging;

    //    public delegate void SelectedChangedHandler(ObservableCollectionWithSelectedItem<T> sender, IList<T>? addedItems, IList<T>? removedItems);
    //    public event SelectedChangedHandler? SelectionChanged;

    //    public T? SelectedItem
    //    {
    //        get => _selectedItems.LastOrDefault();
    //        set
    //        {
    //            var oldItems = _selectedItems.ToList();
    //            var res = SelectionChanging?.Invoke(this, value == null ? new List<T>() : new List<T>() { value }, oldItems) ?? true;
    //            if (res)
    //            {
    //                ClearSelection();
    //                if (value != null)
    //                {
    //                    SelectedItems.Add(value);
    //                    value.IsSelected = true;
    //                }

    //                SelectionChanged?.Invoke(this, value == null ? new List<T>() : new List<T>() { value }, oldItems.ToList());
    //            }
    //            OnPropertyChanged();
    //        }
    //    }

    //    private ObservableCollection<T> _selectedItems = new();

    //    public ObservableCollection<T> SelectedItems
    //    {
    //        get => _selectedItems;
    //        set
    //        {
    //            _selectedItems = value;

    //            OnPropertyChanged();
    //        }
    //    }

    //    public bool IsSelectedLast => Count > 0 && SelectedItem != null && IndexOf(SelectedItem) == Count - 1;
    //    public bool IsSelectedFirst => Count > 0 && SelectedItem != null && IndexOf(SelectedItem) == 0;

    //    public ObservableCollectionWithSelectedItem(bool multiSelection = false) : base()
    //    {
    //        _multiSelect = multiSelection;
    //        SetSelectedToFirst();
    //    }

    //    public ObservableCollectionWithSelectedItem(IEnumerable<T> list, bool multiSelection = false) : base(list)
    //    {
    //        _multiSelect = multiSelection;
    //        SetSelectedToFirst();
    //    }

    //    public void SetSelectedToFirst()
    //    {
    //        SelectedItem = this.FirstOrDefault();
    //    }

    //    public void SetSelectedToLast()
    //    {
    //        SelectedItem = this.LastOrDefault();
    //    }

    //    public bool SetSelectedTo(T item)
    //    {
    //        var obj = this.FirstOrDefault(x => x.Equals(item));
    //        if (obj == null) return false;
    //        SelectedItem = obj;
    //        return true;
    //    }

    //    public bool SetSelectedToId(int id)
    //    {
    //        var prop = typeof(T).GetProperty("Id");
    //        if (prop == null) return false;
    //        foreach (var x in this)
    //        {
    //            if (!int.TryParse(prop.GetValue(x)?.ToString(), out var res) || res != id) continue;

    //            SelectedItem = x;
    //            return true;
    //        }

    //        return false;
    //    }

    //    public bool SetSelectedToPosition(int pos)
    //    {
    //        if (pos < 0 || pos > Count - 1) return false;

    //        SelectedItem = this[pos];
    //        return true;
    //    }

    //    public T? GetPrev()
    //    {
    //        if (SelectedItem == null) return default;
    //        var ind = IndexOf(SelectedItem);
    //        return ind == 0 ? default : this[ind - 1];
    //    }

    //    public T? GetNext()
    //    {
    //        if (SelectedItem == null) return default;
    //        var ind = IndexOf(SelectedItem);
    //        return ind == Count - 1 ? default : this[ind - 1];
    //    }

    //    public new void Clear()
    //    {
    //        ClearSelection();
    //        base.Clear();
    //    }

    //    public void SetRange(IEnumerable<T> list)
    //    {
    //        Clear();
    //        this.AddRange(list);
    //        SetSelectedToFirst();
    //    }

    //    public void AddSelected(T? item)
    //    {
    //        if (item != null)
    //        {
    //            var oldItems = _multiSelect ? new List<T>() : _selectedItems.ToList();
    //            var res = SelectionChanging?.Invoke(this, new List<T>() { item }, oldItems) ?? true;
    //            if (res)
    //            {
    //                if (!_multiSelect)
    //                    ClearSelection();
    //                SelectedItems.Add(item);
    //                item.IsSelected = true;
    //                SelectionChanged?.Invoke(this, new List<T>() { item }, oldItems);
    //            }
    //        }
    //        OnPropertyChanged("SelectedItem");
    //    }

    //    //public void AddSelected(IEnumerable<T?>? items)
    //    //{
    //    //    if (items == null) return;
    //    //    foreach (var x in items)
    //    //    {
    //    //        if(x == null) continue;
    //    //        SelectedItems.Add(x);
    //    //        x.IsSelected = true;
    //    //    }
    //    //}

    //    public void RemoveSelected(T? item)
    //    {
    //        if (item != null)
    //        {
    //            var res = SelectionChanging?.Invoke(this, new List<T>(), new List<T>() { item }) ?? true;
    //            if (res)
    //            {
    //                SelectedItems.Remove(item);
    //                item.IsSelected = false;
    //                SelectionChanged?.Invoke(this, new List<T>(), new List<T>() { item });
    //            }
    //        }
    //        OnPropertyChanged("SelectedItem");
    //    }

    //    //public void RemoveSelected(IEnumerable<T?>? items)
    //    //{
    //    //    if (items == null) return;
    //    //    foreach (var x in items)
    //    //    {
    //    //        if (x == null) continue;
    //    //        SelectedItems.Remove(x);
    //    //        x.IsSelected = false;
    //    //    }
    //    //}

    //    public void ClearSelection()
    //    {
    //        foreach (var x in SelectedItems.ToList())
    //        {
    //            x.IsSelected = false;
    //            SelectedItems.Remove(x);
    //        }
    //        OnPropertyChanged("SelectedItem");
    //        OnPropertyChanged("SelectedItems");
    //    }


    //    protected override event PropertyChangedEventHandler? PropertyChanged;

    //    public void OnPropertyChanged([CallerMemberName] string prop = "")
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    //    }
    //}
}
