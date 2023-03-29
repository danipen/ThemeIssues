using System;
using System.Collections.Generic;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace ThemeIssues
{
    public class ButtonGroup : StackPanel
    {
        public enum SelectionModeType
        {
            // at least one button is always selected
            AlwaysSelected = 0,
            // each segment can be mutually exclusive selected/unselected
            OnOff = 1,
        }

        internal event EventHandler SelectedSegmentChanged;

        public SelectionModeType SelectionMode { get; set; }

        public int SelectedSegment
        {
            get
            {
                return mButtons.IndexOf(mSelectedButton);
            }
            set
            {
                ToggleButton selectedButton = null;

                if (value >= 0 && value < mButtons.Count)
                    selectedButton = mButtons[value];

                SetSelectedButton(selectedButton);
            }
        }

        public ButtonGroup()
        {
            Orientation = Orientation.Horizontal;
        }

        public void Dispose()
        {
            foreach (ToggleButton toggleButton in mButtons)
            {
                    toggleButton.Click += ToggleButton_Click;
            }
        }

        public void AddSegments(params string[] buttons)
        {
            AddObjectSegments(buttons);
        }

        public void AddSegments(params Panel[] panels)
        {
            AddObjectSegments(panels);
        }

        public ToggleButton GetButton(string text)
        {
            foreach (ToggleButton button in mButtons)
            {
                if (button.Content == text)
                    return button;
            }

            return null;
        }

        void AddObjectSegments(params object[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                ToggleButton toggleButton = new ToggleButton();
                toggleButton.Content = buttons[i];
                toggleButton.HorizontalContentAlignment = HorizontalAlignment.Center;
                toggleButton.Classes.Add(
                    GetToggleButtonClass(i, buttons.Length));

                toggleButton.Click += ToggleButton_Click;

                mButtons.Add(toggleButton);
                Children.Add(toggleButton);
            }
        }

        public void SetTooltip(string tooltip, int segmentIndex)
        {
            if (segmentIndex < 0 || segmentIndex > Children.Count - 1)
                return;

            ToolTip.SetTip((Control)Children[segmentIndex], tooltip);
        }

        public void SetButtonsMinHeight(double minHeight)
        {
            for (int i = 0; i < mButtons.Count; i++)
                mButtons[i].MinHeight = minHeight;
        }

        public void SetFontSize(double fontSize)
        {
            for (int i = 0; i < mButtons.Count; i++)
                mButtons[i].FontSize = fontSize;
        }

        public void SetButtonsPadding(Thickness padding)
        {
            for (int i = 0; i < mButtons.Count; i++)
                mButtons[i].Padding = padding;
        }

        public void AddClassToAllSegments(string style)
        {
            foreach (ToggleButton segment in mButtons)
            {
                segment.Classes.Add(style);
            }
        }

        public void AddClassToSegment(string style, int segmentIndex)
        {
            mButtons[segmentIndex].Classes.Add(style);
        }

        public void SetForeground(IBrush brush)
        {
            foreach (ToggleButton toggleButton in mButtons)
            {
                toggleButton.Foreground = brush;
            }
        }

        public void SetEnabled(bool isEnabled, int segmentIndex)
        {
            if (segmentIndex < 0 || segmentIndex > Children.Count - 1)
                return;

            mButtons[segmentIndex].IsEnabled = isEnabled;
        }

        void ToggleButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (SelectionMode == SelectionModeType.OnOff)
            {
                if (mSelectedButton == sender && !mSelectedButton.IsChecked.Value)
                {
                    mSelectedButton = null;
                    OnSelectedSegmentChanged(mSelectedButton);
                    return;
                }
            }

            SetSelectedButton((ToggleButton)sender);
        }

        void SetSelectedButton(ToggleButton selectedButton)
        {
            mSelectedButton = selectedButton;

            foreach (ToggleButton button in mButtons)
                button.IsChecked = button == selectedButton;

            OnSelectedSegmentChanged(mSelectedButton);
        }

        void OnSelectedSegmentChanged(ToggleButton selectedButton)
        {
            int selectedButtonIndex = mButtons.IndexOf(selectedButton);

            SelectedSegmentChanged?.Invoke(this, EventArgs.Empty);
        }

        static string GetToggleButtonClass(int index, int total)
        {
            if (total == 1)
                return "buttonGroupOnly";

            if (index == 0)
                return "buttonGroupFirst";

            if (index == total - 1)
                return "buttonGroupLast";

            return "buttonGroupMiddle";
        }

        ToggleButton mSelectedButton;

        List<ToggleButton> mButtons = new List<ToggleButton>();
    }
}