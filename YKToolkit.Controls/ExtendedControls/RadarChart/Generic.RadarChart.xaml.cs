namespace YKToolkit.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// レーダーチャートを表示するためのコントロールです。
    /// </summary>
    //[TemplatePart(Name = PART_DropDownPopup, Type = typeof(Popup))]
    public class RadarChart : ContentControl
    {
        #region TemplatePart
        //private const string PART_DropDownPopup = "PART_DropDownPopup";

        //private Popup _dropDownPopup;
        //private Popup DropDownPopup
        //{
        //    get { return _dropDownPopup; }
        //    set { _dropDownPopup = value; }
        //}

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //this.DropDownPopup = this.Template.FindName(PART_DropDownPopup, this) as Popup;
        }
        #endregion TemplatePart

        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static RadarChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadarChart), new FrameworkPropertyMetadata(typeof(RadarChart)));
        }
        #endregion コンストラクタ
    }
}
