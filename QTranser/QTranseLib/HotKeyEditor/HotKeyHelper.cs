using QTranser;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HotKeyEditor
{
    public class HotKeyHelper
    {
        private static HashSet<Key> _ignoredKey = new HashSet<Key>() { Key.LeftAlt, Key.RightAlt, Key.LeftCtrl,
            Key.RightCtrl, Key.LeftShift, Key.RightShift, Key.RWin, Key.LWin};

        public static readonly DependencyProperty IsHotKeyEditorProperty =
            DependencyProperty.RegisterAttached(
                "IsHotKeyEditor",
                typeof(Boolean),
                typeof(TextBox),
                new UIPropertyMetadata(false, OnIsHotKeyEditorChanged));

        public static void SetIsHotKeyEditor(UIElement element, Boolean value = true)
        {
            element.SetValue(IsHotKeyEditorProperty, value);
        }

        public static Boolean GetIsHotKeyEditor(UIElement element)
        {
            return (Boolean)element.GetValue(IsHotKeyEditorProperty);
        }

        static void OnIsHotKeyEditorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;

            if ((bool)e.NewValue)
            {
                textBox.PreviewKeyDown += textBox_PreviewKeyDown;
            }
            else
            {
                textBox.PreviewKeyDown -= textBox_PreviewKeyDown;
            }
        }

        private static void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!_ignoredKey.Contains(e.Key) && (e.Key != Key.System || (e.Key == Key.System && !_ignoredKey.Contains(e.SystemKey))))
            {
                var key = (e.Key == Key.System && !_ignoredKey.Contains(e.SystemKey)) ? e.SystemKey : e.Key;
                var hotKey = new HotKey()
                {
                    Ctrl = ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control),
                    Alt = ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt),
                    Shift = ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift),
                    Key = key
                };
                if(hotKey.Ctrl || hotKey.Alt || hotKey.Shift == true)
                {
                    if(textBox.Name == "HotKeyQ")
                    {
                        if (e.Key == Key.C ) return;
                        QTranse.HotKeyManage.Unregister(HotKey.hotKeyQ, HotKey.hotKeyModQ);
                        QTranse.HotKeyManage.Register(e.Key, Keyboard.Modifiers);
                        HotKey.hotKeyQ = e.Key;
                        HotKey.hotKeyModQ = Keyboard.Modifiers;
                        QTranse.Mvvm.HotKeyQ = string.Format($"{hotKey}");
                    }
                    if (textBox.Name == "HotKeyW")
                    {
                        if (e.Key == Key.C) return;
                        QTranse.HotKeyManage.Unregister(HotKey.hotKeyW, HotKey.hotKeyModW);
                        QTranse.HotKeyManage.Register(e.Key, Keyboard.Modifiers);
                        HotKey.hotKeyW = e.Key;
                        HotKey.hotKeyModW = Keyboard.Modifiers;
                        QTranse.Mvvm.HotKeyW = string.Format($"{hotKey}");
                    }
                    if (textBox.Name == "HotKeyB")
                    {
                        if (e.Key == Key.C ) return;
                        QTranse.HotKeyManage.Unregister(HotKey.hotKeyB, HotKey.hotKeyModB);
                        QTranse.HotKeyManage.Register(e.Key, Keyboard.Modifiers);
                        HotKey.hotKeyB = e.Key;
                        HotKey.hotKeyModB = Keyboard.Modifiers;
                        QTranse.Mvvm.HotKeyB= string.Format($"{hotKey}");
                    }
                    if (textBox.Name == "HotKeyG")
                    {
                        if (e.Key == Key.C) return;
                        QTranse.HotKeyManage.Unregister(HotKey.hotKeyG, HotKey.hotKeyModG);
                        QTranse.HotKeyManage.Register(e.Key, Keyboard.Modifiers);
                        HotKey.hotKeyG = e.Key;
                        HotKey.hotKeyModG = Keyboard.Modifiers;
                        QTranse.Mvvm.HotKeyG = string.Format($"{hotKey}");
                    }

                }
                    
            }
            e.Handled = true;
        }
    }
}



