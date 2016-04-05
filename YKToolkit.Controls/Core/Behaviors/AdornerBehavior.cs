namespace YKToolkit.Controls.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    /// 装飾に関するビヘイビアを表します。
    /// </summary>
    public class AdornerBehavior
    {
        #region AdornerTemplate 添付プロパティ
        /// <summary>
        /// AdornerTemplate 添付プロパティを表します。
        /// </summary>
        public static readonly DependencyProperty AdornerTemplateProperty = DependencyProperty.RegisterAttached("AdornerTemplate", typeof(DataTemplate), typeof(AdornerBehavior), new UIPropertyMetadata(null, OnAdornerTemplateProperty));

        /// <summary>
        /// AdornerTemplate 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static DataTemplate GetAdornerTemplate(DependencyObject target)
        {
            return (DataTemplate)target.GetValue(AdornerTemplateProperty);
        }

        /// <summary>
        /// AdornerTemplate 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetAdornerTemplate(DependencyObject target, DataTemplate value)
        {
            target.SetValue(AdornerTemplateProperty, value);
        }
        #endregion AdornerTemplate 添付プロパティ

        #region AdornerTemplateSelector 添付プロパティ
        /// <summary>
        /// AdornerTemplateSelector 添付プロパティを表します。
        /// </summary>
        public static readonly DependencyProperty AdornerTemplateSelectorProperty = DependencyProperty.RegisterAttached("AdornerTemplateSelector", typeof(DataTemplateSelector), typeof(AdornerBehavior), new UIPropertyMetadata(null, OnAdornerTemplateSelectorProperty));

        /// <summary>
        /// AdornerTemplateSelector 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static DataTemplateSelector GetAdornerTemplateSelector(DependencyObject target)
        {
            return (DataTemplateSelector)target.GetValue(AdornerTemplateSelectorProperty);
        }

        /// <summary>
        /// AdornerTemplateSelector 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetAdornerTemplateSelector(DependencyObject target, DataTemplateSelector value)
        {
            target.SetValue(AdornerTemplateSelectorProperty, value);
        }
        #endregion AdornerTemplateSelector 添付プロパティ

        #region IsEnabled 添付プロパティ
        /// <summary>
        /// IsEnabled 添付プロパティを表します。
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(AdornerBehavior), new UIPropertyMetadata(false, OnIsEnabledPropertyChanged));

        /// <summary>
        /// IsEnabled 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static bool GetIsEnabled(DependencyObject target)
        {
            return (bool)target.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// IsEnabled 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetIsEnabled(DependencyObject target, bool value)
        {
            target.SetValue(IsEnabledProperty, value);
        }
        #endregion IsEnabled 添付プロパティ

        #region DataContextElement 添付プロパティ
        /// <summary>
        /// DataContextElement 添付プロパティを表します。
        /// </summary>
        public static readonly DependencyProperty DataContextElementProperty = DependencyProperty.RegisterAttached("DataContextElement", typeof(FrameworkElement), typeof(AdornerBehavior), new UIPropertyMetadata(null, OnDataContextElementPropertyChanged));

        /// <summary>
        /// DataContextElement 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static FrameworkElement GetDataContextElement(DependencyObject target)
        {
            return (FrameworkElement)target.GetValue(DataContextElementProperty);
        }

        /// <summary>
        /// DataContextElement 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetDataContextElement(DependencyObject target, FrameworkElement value)
        {
            target.SetValue(DataContextElementProperty, value);
        }
        #endregion DataContextElement 添付プロパティ

        #region 添付プロパティ変更イベントハンドラ

        /// <summary>
        /// AdornerTemplate 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnAdornerTemplateProperty(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChange(sender);
        }

        /// <summary>
        /// AdornerTemplateSelector 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnAdornerTemplateSelectorProperty(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChange(sender);
        }

        /// <summary>
        /// IsEnabled 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsEnabledPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChange(sender);
        }

        /// <summary>
        /// DataContextElement 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnDataContextElementPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SetAdornerDataContext(sender);
        }

        #endregion 添付プロパティ変更イベントハンドラ

        #region ヘルパ

        /// <summary>
        /// テンプレート変更時のヘルパ
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        private static void OnChange(DependencyObject target)
        {
            var element = target as FrameworkElement;
            var template = GetDataTemplate(element);
            var isEnabled = GetIsEnabled(element);

            var adorner = element.GetValue(AdornerCore.AdornerProperty) as AdornerCore;
            if (isEnabled)
            {
                if (adorner == null)
                {
                    if ((element != null) && (template != null))
                    {
                        var frameworkElement = template.LoadContent() as FrameworkElement;
                        adorner = new AdornerCore(element, frameworkElement);
                        element.SetValue(AdornerCore.AdornerProperty, adorner);
                        SetAdornerDataContext(target);
                    }
                }
                else
                {
                    adorner.Attach();
                }
            }
            else
            {
                if (adorner != null)
                {
                    adorner.Detach();
                }
            }
        }

        /// <summary>
        /// 装飾に対する DataContext を設定します。
        /// </summary>
        /// <param name="target">添付ビヘイビアの対象となっている DependencyObject を指定します。</param>
        private static void SetAdornerDataContext(DependencyObject target)
        {
            var adorner = (target as DependencyObject).GetValue(AdornerCore.AdornerProperty) as AdornerCore;
            if (adorner != null)
            {
                var dataContextElement = GetDataContextElement(target);
                if (dataContextElement != null)
                {
                    adorner.DataContext = dataContextElement.DataContext;
                }
                else
                {
                    adorner.DataContext = (target as FrameworkElement).DataContext;
                }
            }
        }

        /// <summary>
        /// AdornerTemplateSelector を優先的に DataTemplate を取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>装飾に対する DataTemplate を返します。</returns>
        private static DataTemplate GetDataTemplate(DependencyObject target)
        {
            var selector = GetAdornerTemplateSelector(target);
            var templateFromSelector = selector != null ? selector.SelectTemplate((target as FrameworkElement).DataContext, target) : null;
            return templateFromSelector != null ? templateFromSelector : GetAdornerTemplate(target);
        }

        #endregion ヘルパ

        /// <summary>
        /// 装飾のためのクラスを表します。
        /// </summary>
        internal class AdornerCore : Adorner
        {
            /// <summary>
            /// 新しいインスタンスを生成します。
            /// </summary>
            /// <param name="adornedElement">装飾対象を指定します。</param>
            /// <param name="adorner">装飾を指定します。</param>
            public AdornerCore(UIElement adornedElement, FrameworkElement adorner)
                : base(adornedElement)
            {
                this._layer = AdornerLayer.GetAdornerLayer(adornedElement);
                this._adorner = adorner;
                AddVisualChild(this._adorner);
                Attach();
            }

            /// <summary>
            /// Adorner 依存関係プロパティの定義を表します。
            /// </summary>
            public static readonly DependencyProperty AdornerProperty = DependencyProperty.Register("Adorner", typeof(AdornerCore), typeof(AdornerCore), new PropertyMetadata(null));

            /// <summary>
            /// 装飾を表示します。
            /// </summary>
            public void Attach()
            {
                if (this._layer != null)
                {
                    this._layer.Add(this);
                }
            }

            /// <summary>
            /// 装飾を非表示にします。
            /// </summary>
            public void Detach()
            {
                if (this._layer != null)
                {
                    this._layer.Remove(this);
                }
            }

            /// <summary>
            /// 装飾対象の描画サイズを取得します。
            /// </summary>
            private Size ActualAdoredElementSize
            {
                get { return new Size((this.AdornedElement as FrameworkElement).ActualWidth, (this.AdornedElement as FrameworkElement).ActualHeight); }
            }

            /// <summary>
            /// コントロールのサイズ計測処理のオーバーライド
            /// </summary>
            /// <param name="constraint">サイズに対する制約</param>
            /// <returns>計測結果</returns>
            protected override Size MeasureOverride(Size constraint)
            {
                return this.ActualAdoredElementSize;
            }

            /// <summary>
            /// 子要素の配置処理のオーバーライド
            /// </summary>
            /// <param name="finalSize">親から与えられる領域のサイズ</param>
            /// <returns>配置に要したサイズ</returns>
            protected override Size ArrangeOverride(Size finalSize)
            {
                var size = this.ActualAdoredElementSize;
                if (this._adorner != null)
                {
                    this._adorner.Arrange(new Rect(size));
                }
                return size;
            }

            /// <summary>
            /// ビジュアルツリーにおける子要素の数を取得します。
            /// </summary>
            protected override int VisualChildrenCount
            {
                get
                {
                    return 1;
                }
            }

            /// <summary>
            /// ビジュアルツリーにおける子要素を取得します。
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            protected override Visual GetVisualChild(int index)
            {
                return this._adorner;
            }

            /// <summary>
            /// 装飾層を保持します。
            /// </summary>
            private AdornerLayer _layer;

            /// <summary>
            /// 装飾を保持します。
            /// </summary>
            private FrameworkElement _adorner;
        }
    }
}
