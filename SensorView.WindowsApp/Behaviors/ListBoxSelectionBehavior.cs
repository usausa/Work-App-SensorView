﻿namespace SensorView.WindowsApp.Behaviors
{
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    /// <summary>
    ///
    /// </summary>
    public class ListBoxSelectionBehavior : Behavior<ListBox>
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(ListBoxSelectionBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        ///
        /// </summary>
        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        ///
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject == null)
            {
                return;
            }

            AssociatedObject.SelectionMode = SelectionMode.Multiple;
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }

        /// <summary>
        ///
        /// </summary>
        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.SelectionChanged -= OnSelectionChanged;
            }

            base.OnDetaching();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                if (e.AddedItems.Count > 0)
                {
                    var target = e.AddedItems[0];
                    foreach (var item in new ArrayList(listBox.SelectedItems))
                    {
                        if (item != target)
                        {
                            listBox.SelectedItems.Remove(item);
                        }
                    }
                }

                SelectedItem = listBox.SelectedItems.Count > 0 ? listBox.SelectedItems[0] : null;
            }
        }
    }
}
