using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using ReactiveUI;

namespace NekoMacro.Utils.TreeDataGrid
{
    public class TreeGridElement : ReactiveObject
    {
        private const string NullItemError = "The item added to the collection cannot be null.";
        
        private TreeGridElement _parent;
        public TreeGridElement Parent
        {
            get => _parent;
            set => this.RaiseAndSetIfChanged(ref _parent, value);
        }

        private TreeGridModel _model;
        public TreeGridModel Model
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }

        private ObservableCollection<TreeGridElement> _childs;
        public ObservableCollection<TreeGridElement> Childs
        {
            get => _childs;
            set => this.RaiseAndSetIfChanged(ref _childs, value);
        }
        
        [JsonIgnore] public  int Level => Parent == null ? 0 : Parent.Level + 1;

        private bool _isExpanded;
        [JsonIgnore]
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                this.RaiseAndSetIfChanged(ref _isExpanded, value);
                OnIsExpandedChanged();
            }
        }

        [JsonIgnore] public bool HasChilds => Childs?.Count > 0;
        

        public TreeGridElement()
        {
            // Initialize the element
            Childs = new ObservableCollection<TreeGridElement>();

            // Attach events
            Childs.CollectionChanged += OnChildrenChanged;
        }

        internal void SetModel(TreeGridModel model, TreeGridElement parent = null)
        {
            // Set the element information
            Model = model;
            Parent = parent;

            // Iterate through all child elements
            foreach (var child in Childs)
            {
                // Set the model for the child
                child.SetModel(model, this);
            }
        }

        private void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            // Process the event
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    // Process added child
                    OnChildAdded(args.NewItems[0]);
                    break;

                case NotifyCollectionChangedAction.Replace:

                    // Process replaced child
                    OnChildReplaced((TreeGridElement)args.OldItems[0], args.NewItems[0], args.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:

                    // Process removed child
                    OnChildRemoved((TreeGridElement)args.OldItems[0]);
                    break;

                case NotifyCollectionChangedAction.Reset:

                    // Process cleared children
                    OnChildrenCleared(args.OldItems);
                    break;
            }
            this.RaisePropertyChanged("HasChilds");
        }

        private void OnChildAdded(object item)
        {
            // Verify the new child
            TreeGridElement child = VerifyItem(item);

            // Set the model for the child
            child.SetModel(Model, this);

            // Notify the model that a child was added to the item
            Model?.OnChildAdded(child);
        }

        private void OnChildReplaced(TreeGridElement oldChild, object item, int index)
        {
            // Verify the new child
            TreeGridElement child = VerifyItem(item);

            // Clear the model for the old child
            oldChild.SetModel(null);

            // Notify the model that a child was replaced
            Model?.OnChildReplaced(oldChild, child, index);
        }

        private void OnChildRemoved(TreeGridElement child)
        {
            // Clear the model for the child
            child.SetModel(null);

            // Notify the model that a child was removed from the item
            Model?.OnChildRemoved(child);
        }

        private void OnChildrenCleared(IList children)
        {
            // Iterate through all of the children
            foreach (TreeGridElement child in children)
            {
                // Clear the model for the child
                child.SetModel(null);
            }

            // Notify the model that all of the children were removed from the item
            Model?.OnChildrenRemoved(this, children);
        }

        internal static TreeGridElement VerifyItem(object item)
        {
            // Is the item valid?
            if (item == null)
            {
                // The item is not valid
                throw new ArgumentNullException(nameof(item), NullItemError);
            }

            // Return the element
            return (TreeGridElement)item;
        }

        private void OnIsExpandedChanged()
        {
            if (_isExpanded)
            {
                Model?.Expand(this);
            }
            else
            {
                Model?.Collapse(this);
            }
        }
    }
}
