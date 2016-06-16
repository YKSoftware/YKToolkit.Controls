namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// 登録したイベントが発行されたときにアクションをおこなうためのビヘイビアを表します。
    /// </summary>
    public class EventTriggerCommandBehavior
    {
        #region Command 添付プロパティ
        /// <summary>
        /// Command 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(EventTriggerCommandBehavior), new PropertyMetadata(null));

        /// <summary>
        /// Command 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static ICommand GetCommand(DependencyObject target)
        {
            return (ICommand)target.GetValue(CommandProperty);
        }

        /// <summary>
        /// Command 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(CommandProperty, value);
        }
        #endregion Command 添付プロパティ

        #region RoutedEvent 添付プロパティ
        /// <summary>
        /// RoutedEvent 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty RoutedEventProperty = DependencyProperty.RegisterAttached("RoutedEvent", typeof(RoutedEvent), typeof(EventTriggerCommandBehavior), new PropertyMetadata(null, OnRoutedEventPropertyChanged));

        /// <summary>
        /// RoutedEvent 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static RoutedEvent GetRoutedEvent(DependencyObject target)
        {
            return (RoutedEvent)target.GetValue(RoutedEventProperty);
        }

        /// <summary>
        /// RoutedEvent 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetRoutedEvent(DependencyObject target, RoutedEvent value)
        {
            target.SetValue(RoutedEventProperty, value);
        }

        /// <summary>
        /// RoutedEvent 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnRoutedEventPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as UIElement;
            if (element == null)
                return;

            var routedEvent = GetRoutedEvent(element);
            if (routedEvent == null)
                return;

            for (var type = element.GetType(); type != null; type = type.BaseType)
            {
                // ルーティングイベントリスト取得
                var eventList = EventManager.GetRoutedEventsForOwner(type);
                if (eventList != null)
                {
                    var isEnd = false;
                    foreach (var item in eventList)
                    {
                        // ルーティングイベント名が一致したらイベントハンドラを登録する
                        if (routedEvent.Name == item.Name)
                        {
                            element.AddHandler(routedEvent, new RoutedEventHandler((obj, args) =>
                                {
                                    var control = obj as UIElement;
                                    if (control == null)
                                        return;
                                    var command = GetCommand(control);
                                    if (command == null)
                                        return;
                                    if (command.CanExecute(args))
                                        command.Execute(args);
                                }));
                            isEnd = true;
                            break;
                        }
                    }
                    if (isEnd)
                        break;
                }
            }
        }
        #endregion RoutedEvent 添付プロパティ
    }
}
