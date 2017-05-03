using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Common
{
    public enum HighchartTypeEnum
    {
        混合型 = 0,
        饼图型 = 1,
        柱状图 = 2,
        多柱状图 = 3,
        多流线图 = 4,
        多横柱图 = 5,
        层叠图 = 6,
        区域图 = 7,
        温度计型 = 8,


    };

    /// <summary>
    /// 基础HighChart图形类
    /// </summary>
    public class Chart
    {
        public Chart()
        {
            this.renderTo = string.Empty;
            this.type = string.Empty;
            this.borderWidth = 1;
            this.borderColor = "#DDDDDD";
            this.animation = new Animation();
            this.ignoreHiddenSeries = false;
            this.style = null;
            this.className = null;
        }
        /// <summary>
        /// 展示的dom元素区域，一般为ID
        /// </summary>
        public string renderTo { get; set; }

        /// <summary>
        /// 设置或获取图表类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 设置或获取图表外部边框，默认为0，不显示边框
        /// </summary>
        public int borderWidth { get; set; }

        /// <summary>
        /// 设置或获取外部边框颜色
        /// </summary>
        public string borderColor { get; set; }

        /// <summary>
        /// 设置或获取图表背景色-默认颜色白底
        /// </summary>
        public string backgroundColor { get; set; }

        /// <summary>
        /// 动画效果，若是想要关闭动画效果请将duration属性设置或获取为0
        /// </summary>
        public Animation animation { get; set; }

        /// <summary>
        /// 设置或获取图表显示的render所用到的div上的css样式
        /// </summary>
        public string className { get; set; }

        /// <summary>
        /// 设置或获取样式
        /// </summary>

        public string style { get; set; }

        /// <summary>
        /// 设置或获取图表高度
        /// </summary>
        public int? height { get; set; }

        /// <summary>
        /// 设置或获取宽度
        /// </summary>
        public int? width { get; set; }

        /// <summary>
        /// 设置隐藏Series指标轴是否动态变化
        /// </summary>
        public bool ignoreHiddenSeries { get; set; }

        /// <summary>
        /// 设置X-Y坐标轴是否翻转
        /// </summary>
        public bool? inverted { get; set; }

        /// <summary>
        /// 设置图表与div边框底部距离
        /// </summary>
        public int? marginBottom { get; set; }

        /// <summary>
        /// 设置图表与div边框左边距离
        /// </summary>
        public int? marginLeft { get; set; }

        /// <summary>
        /// 设置图表与div边框右边距离
        /// </summary>
        public int? marginRight { get; set; }

        /// <summary>
        /// 设置图表与div边框顶部距离
        /// </summary>
        public int? marginTop { get; set; }

        /// <summary>
        /// 是否自适应宽度高度
        /// </summary>
        public bool reflow { get; set; }
    }

    public class Title
    {
        public Title()
        {
            this.text = string.Empty;
        }
        /// <summary>
        /// 设置图表主标题
        /// </summary>
        public string text { get; set; }
    }

    /// <summary>
    /// 图表副标题
    /// </summary>
    public class SubTitle
    {
        public SubTitle()
        {
            this.text = string.Empty;
        }

        public string text { get; set; }
    }

    /// <summary>
    /// 图形X轴
    /// </summary>
    public class XAxis
    {
        public XAxis()
        {
            this.categories = new List<string>();
            this.plotLines = new List<PlotLines>();
            this.opposite = false;
            this.reversed = false;
            this.title = new Title();
        }
        public Title title { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public List<string> categories { get; set; }

        /// <summary>
        /// 趋势线（X轴，可以设置多条）
        /// </summary>
        public List<PlotLines> plotLines { get; set; }

        /// <summary>
        /// 是否bar图形模式下的左右对称
        /// </summary>
        public bool opposite { get; set; }

        /// <summary>
        /// 获取或者设置是否允许翻转
        /// </summary>
        public bool reversed { get; set; }

        /// <summary>
        /// X轴中心线
        /// </summary>
        public int? linkedTo { get; set; }
    }

    /// <summary>
    /// Y轴
    /// </summary>
    public class YAxis
    {
        public YAxis()
        {
            this.title = new Title();
            this.plotLines = new List<PlotLines>();
            this.min = null;
            this.max = null;
        }

        public int? min { get; set; }

        public int? max { get; set; }

        public Title title { get; set; }

        public List<PlotLines> plotLines { get; set; }
    }

    /// <summary>
    /// 数据列
    /// </summary>
    public class Series
    {
        public Series()
        {
            this.name = string.Empty;
            this.allowPointSelect = true;
            //this.size = 180;
            this.color = null;
            this.showInLegend = true;
            //this.center = new int[] { 700, 150 };
        }

        public string type { get; set; }

        public string name { get; set; }

        public bool allowPointSelect { get; set; }

        public List<object> data { get; set; }

        public int[] center { get; set; }

        public int? size { get; set; }

        public string color { get; set; }

        public bool? showInLegend { get; set; }

        public int? pointInterval { get; set; }
    }

    /// <summary>
    /// 动画类
    /// </summary>
    public class Animation
    {
        public Animation()
        {
            this.duration = 600;
        }

        /// <summary>
        /// 设置动画持续时间
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// 设置动画效果类似jquery效果中的easeOutBounce
        /// </summary>
        public string easing { get; set; }
    }

    /// <summary>
    /// Tooltip信息属性
    /// </summary>
    public class ToolTip
    {
        private string _pointFormat = string.Empty;

        public ToolTip()
        {
            this.backgroundColor = "#FFFFFF";
            //this.borderRadius = 5;
            //this.borderWidth = 2;
            this.crosshairs = new List<bool>(2);
            this.crosshairs.Add(false);
            this.crosshairs.Add(false);
            this.enabled = true;
            this.shared = false;
            this.useHTML = false;
            this.headerFormat = "<small>{point.key}<small><br/>";
            this.pointFormat = string.Empty;
            this.footerFormat = string.Empty;
        }

        /// <summary>
        /// 设置或获取Tooltip提示背景默认淡黄色
        /// </summary>
        public string backgroundColor { get; set; }

        /// <summary>
        /// 设置或获取Tooltip边框颜色
        /// </summary>
        public string borderColor { get; set; }

        /// <summary>
        /// 设置或获取Tooltip边框圆角默认为5，为0时为矩形
        /// </summary>
        public int borderRadius { get; set; }

        /// <summary>
        /// 设置或获取Tooltip边框宽度默认为2
        /// </summary>
        public int borderWidth { get; set; }

        /// <summary>
        /// 设置或获取需不需要开启跟踪x,y跟踪条-第一个为x，第二个为y,注意只能输入2个参数
        /// </summary>
        public List<bool> crosshairs { get; set; }

        /// <summary>
        /// 设置或获取是否启用tooltip

        /// </summary>
        public bool enabled { get; set; }

        /// <summary>
        /// 设置或获取多Series情况下是否共享自己的tooltip消息框
        /// </summary>
        public bool shared { get; set; }

        /// <summary>
        /// 设置是否使用HTML模式来展示Tooltip框，默认使用SVG格式
        /// </summary>
        public bool useHTML { get; set; }

        /// <summary>
        /// 设置支持以下几个HTML标签b, strong, i, e, b, span, br 标签头的值：{point.key}
        /// </summary>
        public string headerFormat { get; set; }

        /// <summary>
        /// 设置数据点的格式，颜色：{series.color}，名字：{series.name}，值：{point.y}
        /// </summary>
        public string pointFormat
        {
            get
            {
                if (string.IsNullOrEmpty(this._pointFormat) && this.shared)
                    return "<span style=\"color:{series.color}\">{series.name}</span>: <b>{point.y}</b><br/>";
                else if (string.IsNullOrEmpty(this._pointFormat) && !this.shared)
                    return "<span style=\"color:{series.color}\">{series.name}</span>: <b>{point.y}</b>";
                else
                    return _pointFormat;
            }
            set
            {
                _pointFormat = value;
            }
        }

        /// <summary>
        /// 设置数据底部格式，headerFormat，pointFormat，footerFormat三个加起来可以拼接成一个完成html
        /// </summary>
        public string footerFormat { get; set; }

        /// <summary>
        /// 精确到小数点后的位数
        /// </summary>
        public int percentageDecimals { get; set; }
    }

    /// <summary>
    /// 版权信息属性
    /// </summary>
    public class Credits
    {
        public Credits()
        {
            this.enabled = false;
            this.href = string.Empty;
            this.text = string.Empty;
            this.position = string.Empty;
        }

        /// <summary>
        /// 设置是否开启版权信息
        /// </summary>
        public bool enabled { get; set; }

        /// <summary>
        /// 设置版权信息文本链接
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// 设置或获取版权信息文本
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 设置版权信息文本
        /// </summary>
        public string position { get; set; }
    }

    /// <summary>
    /// 显示Legend标签
    /// </summary>
    public class Legend
    {
        public Legend()
        {
            this.align = "center";
            this.verticalAlign = "bottom";
            this.backgroundColor = string.Empty;
            this.borderColor = "#909090";
            //this.borderRadius = 5;
            this.enabled = true;
            this.floating = false;
            this.layout = new LayoutTypeEnum();
            this.navigation = new Navigation();
        }

        public string align { get; set; }

        public string verticalAlign { get; set; }

        public string backgroundColor { get; set; }

        public string borderColor { get; set; }

        public int borderRadius { get; set; }

        public bool enabled { get; set; }

        public bool floating { get; set; }

        public LayoutTypeEnum layout { get; set; }

        public Navigation navigation { get; set; }
    }

    /// <summary>
    /// Legend标签是否需要导航条
    /// </summary>
    public class Navigation
    {
        public Navigation()
        {
            this.animation = false;
        }

        public bool animation { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PlotLines
    {
        public PlotLines()
        {
            this.color = "#FFEE99";
            this.dashStyle = "Solid";
            this.width = 2;
            this.value = 0;
        }

        public string color { get; set; }

        public string dashStyle { get; set; }

        public double value { get; set; }

        public int width { get; set; }
    }

    /// <summary>
    /// 具体各个图形操作属性
    /// </summary>
    public class PlotOptions
    {
        public PlotOptions()
        {
            this.enableDataLabels = false;
            this.enableMouseTracking = true;
            this.stacking = null;
            this.showInLegend = false;
            this.cursor = "pointer";
        }

        public bool enableDataLabels { get; set; }

        public bool enableMouseTracking { get; set; }

        public string stacking { get; set; }

        public bool showInLegend { get; set; }

        public string cursor { get; set; }
    }

    public class Exporting
    {
        public Exporting()
        {
            this.exporting = true;
        }

        public bool exporting { get; set; }
    }

    /// <summary>
    /// 图形类型枚举
    /// </summary>
    public enum ChartTypeEnum
    {
        bar = 0,
        line,
        spline,
        column,
        pie,
        scattar,
        gauge,
        area,
        arearange,
        areasplinerange,
        areaspline
    }

    /// <summary>
    /// 布局显示枚举
    /// </summary>
    public enum LayoutTypeEnum
    {
        horizontal = 0,
        vertical
    }

    public enum StackingTypeEnum
    {
        normal = 0,
        percent
    }
    public class HighChartOptions
    {
        public HighChartOptions()
        {
            this.chart = new Chart();
            this.title = new Title();
            this.subtitle = new SubTitle();
            this.xAxis = new List<XAxis>();
            this.yAxis = new YAxis();
            this.series = new List<Series>();
            this.tooltip = new ToolTip();
            this.plotOptions = new PlotOptions();
            this.credits = new Credits();
            this.exporting = new Exporting();
        }

        public Chart chart { get; set; }

        public Title title { get; set; }

        public SubTitle subtitle { get; set; }

        public List<XAxis> xAxis { get; set; }

        public YAxis yAxis { get; set; }

        public List<Series> series { get; set; }

        public ToolTip tooltip { get; set; }

        public PlotOptions plotOptions { get; set; }

        public Credits credits { get; set; }

        public Exporting exporting { get; set; }
    }

    public class PieSeriesData
    {
        public string name { get; set; }
        public bool sliced { get; set; }
        public int y { get; set; }
        public bool selected { get; set; }
    }
}
