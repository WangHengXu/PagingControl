using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PagerControl
{
    [TemplatePart(Name = "PART_BdMain", Type = typeof(Border))]
    [TemplatePart(Name = "PART_First", Type = typeof(Hyperlink))]
    [TemplatePart(Name = "PART_Prev", Type = typeof(Hyperlink))]
    [TemplatePart(Name = "PART_Next", Type = typeof(Hyperlink))]
    [TemplatePart(Name = "PART_Last", Type = typeof(Hyperlink))]
    [TemplatePart(Name = "PART_PageState", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_PageNum", Type = typeof(ComboBox))]
    [TemplatePart(Name = "PART_Goto", Type = typeof(Hyperlink))]
    [TemplatePart(Name = "PART_ShowRows", Type = typeof(ComboBox))]
    [TemplatePart(Name = "PART_PageCurrentPageNum", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_PageTotalNum", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_TotalRow", Type = typeof(TextBlock))]
    public class PagerControl:Control
    {

        static PagerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PagerControl), new FrameworkPropertyMetadata(typeof(PagerControl)));
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get { return (int)GetValue(TotalPagesProperty); }
            set { SetValue(TotalPagesProperty, value); }
        }

        public static readonly DependencyProperty TotalPagesProperty =
            DependencyProperty.Register("TotalPages", typeof(int), typeof(PagerControl),
                new FrameworkPropertyMetadata(default(Int32),new PropertyChangedCallback(OnTotalPagesChanged)));

        private static void OnTotalPagesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
            PagerControl pagerControl = (PagerControl)d;
            if (pagerControl.PageNums == null)
            {
                pagerControl.PageNums = new List<int>();
            }
            if (pagerControl.TotalPages >= 1)
            {
                pagerControl.CurrentPageNum = 1;
            
                int i = 1;
                while (i <= pagerControl.TotalPages)
                {
                    pagerControl.PageNums.Add(i);
                    i++;
                }
            }
            else
            {
                pagerControl.CurrentPageNum = 0;
                pagerControl.PageNums.Clear();
                pagerControl.cmbPageNums.Text = string.Empty;
                pagerControl.TotalRow = 0;
            }
        }



        public int TotalRow
        {
            get { return (int)GetValue(TatolRowProperty); }
            set { SetValue(TatolRowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TatolRow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TatolRowProperty =
            DependencyProperty.Register("TatolRow", typeof(int), typeof(PagerControl) );



        /// <summary>
        /// 当前显示页数
        /// </summary>
        public int CurrentPageNum
        {
            get { return (int)GetValue(CurrentPageNumProperty); }
            set { SetValue(CurrentPageNumProperty, value); }
        }
        public static readonly DependencyProperty CurrentPageNumProperty =
            DependencyProperty.Register("CurrentPageNum", typeof(int), typeof(PagerControl), 
                new FrameworkPropertyMetadata(default(int),new PropertyChangedCallback(OnCurrentPageNumChanged)));

        private static void OnCurrentPageNumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PagerControl pagerControl = (PagerControl)d;
            pagerControl.OnCurrentPageNumChanged((int)e.OldValue, (int)e.NewValue);
        }


        /// <summary>
        /// 当前显示行数
        /// </summary>
        public int CurrentShowRows
        {
            get { return (int)GetValue(CurrentShowRowsProperty); }
            set { SetValue(CurrentShowRowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentShowRows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentShowRowsProperty =
            DependencyProperty.Register("CurrentShowRows", typeof(int), typeof(PagerControl), 
                new FrameworkPropertyMetadata(default(int),new PropertyChangedCallback(OnCurrentShowRowsChanged)));

        private static void OnCurrentShowRowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PagerControl pagerControl = (PagerControl)d;
            pagerControl.OnCurrentShowRowsChanged((int)e.OldValue, (int)e.NewValue);
        }

        public List<int> PageNums
        {
            get { return (List<int>)GetValue(PageNumsProperty); }
            set { SetValue(PageNumsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageNums.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageNumsProperty =
            DependencyProperty.Register("PageNums", typeof(List<int>), typeof(PagerControl));


        public static readonly RoutedEvent CurrentPageNumChangedEvent =
            EventManager.RegisterRoutedEvent("CurrentPageNumChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<int>), typeof(PagerControl));
        /// <summary>
        /// 当前页面数改变事件
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> CurrentPageNumChanged
        {
            add { AddHandler(CurrentPageNumChangedEvent, value); }
            remove { RemoveHandler(CurrentPageNumChangedEvent, value); }
        }
        private void OnCurrentPageNumChanged(int oldValue, int newValue)
        {
            RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(oldValue, newValue);
            args.RoutedEvent = PagerControl.CurrentPageNumChangedEvent;
            RaiseEvent(args);
        }

        public static readonly RoutedEvent CurrentShowRowsChangedEvent =
           EventManager.RegisterRoutedEvent("CurrentShowRowsChanged", RoutingStrategy.Bubble,
               typeof(RoutedPropertyChangedEventHandler<int>), typeof(PagerControl));
        /// <summary>
        /// 当前显示行数改变事件
        /// </summary>
        public event RoutedPropertyChangedEventHandler<int> CurrentShowRowsChanged
        {
            add { AddHandler(CurrentShowRowsChangedEvent, value); }
            remove { RemoveHandler(CurrentShowRowsChangedEvent, value); }
        }
        private void OnCurrentShowRowsChanged(int oldValue, int newValue)
        {
            RoutedPropertyChangedEventArgs<int> args = new RoutedPropertyChangedEventArgs<int>(oldValue, newValue);
            args.RoutedEvent = PagerControl.CurrentShowRowsChangedEvent;
            RaiseEvent(args);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            InitControl();
        }
        Hyperlink hyperlinkFirst;
        Hyperlink hyperlinkPrev;
        Hyperlink hyperlinkNext;
        Hyperlink hyperlinkLast;
        Hyperlink hyperlinkGoto;
        ComboBox cmbPageNums;
        ComboBox cmbShowRows;
        private void InitControl()
        {
            hyperlinkFirst = GetTemplateChild("PART_First") as Hyperlink;
            hyperlinkFirst.Click += HyperlinkFirst_Click;

            hyperlinkPrev = GetTemplateChild("PART_Prev") as Hyperlink;
            hyperlinkPrev.Click += HyperlinkPrev_Click;

            hyperlinkNext = GetTemplateChild("PART_Next") as Hyperlink;
            hyperlinkNext.Click += HyperlinkNext_Click;

            hyperlinkLast = GetTemplateChild("PART_Last") as Hyperlink;
            hyperlinkLast.Click += HyperlinkLast_Click;

            hyperlinkGoto = GetTemplateChild("PART_Goto") as Hyperlink;
            hyperlinkGoto.Click += HyperlinkGoto_Click;

            cmbPageNums = GetTemplateChild("PART_PageNum") as ComboBox;

            cmbShowRows = GetTemplateChild("PART_ShowRows") as ComboBox;
            this.CurrentShowRows = 50;

        }

        private void HyperlinkGoto_Click(object sender, RoutedEventArgs e)
        {
            int num;
            if(int.TryParse(cmbPageNums.Text,out num))
            {
                if(num<=TotalPages&&num>=1)
                {
                    this.CurrentPageNum = num;
                }
            }
        }

        private void HyperlinkLast_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageNum =this.TotalPages;
        }

        private void HyperlinkNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentPageNum < this.TotalPages)
                this.CurrentPageNum++;
        }

        private void HyperlinkPrev_Click(object sender, RoutedEventArgs e)
        {
            if (this.CurrentPageNum > 1)
                this.CurrentPageNum--;
        }
        private void HyperlinkFirst_Click(object sender, RoutedEventArgs e)
        {
            if (this.TotalPages >= 1)
                this.CurrentPageNum = 1;
        }
    }
}
