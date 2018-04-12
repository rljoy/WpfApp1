using SciChart.Charting.Visuals.Axes;
using SciChart.Charting3D;
using SciChart.Charting3D.Axis;
using SciChart.Charting3D.Model;
using SciChart.Charting3D.Modifiers;
using SciChart.Charting3D.PointMarkers;
using SciChart.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SciChart.Charting3D.Interop;
using System.IO;
using CsvHelper;
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class DataRecord
    {
        public string TIMESTAMP { get; set; }
        public string TIMER { get; set; }
        public string JOINTTYPE { get; set; }
        public string xPOSITION { get; set; }
        public string yPOSITION { get; set; }
        public string zPOSITION { get; set; }

    }



    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
           ReadInCSV(); 

            
           

            // Create the SciChart3DSurface
            // Requires
            //   using SciChart.Charting3D
            //   using SciChart.Charting3D.Axis
            //   using SciChart.Charting3D.Modifiers
            var sciChart3DSurface = new SciChart3DSurface()
            {
                IsAxisCubeVisible = true,
                IsFpsCounterVisible = true,
                IsXyzGizmoVisible = true
            };
            // Create the X,Y,Z Axis
            sciChart3DSurface.XAxis = new NumericAxis3D();
            sciChart3DSurface.YAxis = new NumericAxis3D();
            sciChart3DSurface.ZAxis = new NumericAxis3D();
            // Specify Interactivity Modifiers
            sciChart3DSurface.ChartModifier = new ModifierGroup3D(
                     new OrbitModifier3D(),
                     new ZoomExtentsModifier3D());

            

        }

        




        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            

            const int Count = 15;
            var uniformDataSeries = new UniformGridDataSeries3D<double>(Count, Count)
            {
                StepX = 1,
                StepZ = 1,
                SeriesName = "Impulse Series 3D",
            };
            for (var x = 0; x < Count; x++)
            {
                for (var z = 0; z < Count; z++)
                {
                    var y = Math.Sin(x * 0.25) / ((z + 1) * 2);
                    uniformDataSeries[z, x] = y;
                }
            }
            ImpulseSeries3D.DataSeries = uniformDataSeries;
            
              
        }

        public static List<DataRecord> ReadInCSV()
        {
            List<DataRecord> result = new List<DataRecord>();

           

            using (TextReader fileReader = File.OpenText(@"C:\Users\ronan\Documents\SkeletonBasics-WPF - Test\bin\Debug\pro.csv"))
            {
                var csv = new CsvReader(fileReader);
                
                //csv.Read(); 
                while (csv.Read())
                {

                   result.Add(csv.GetRecord<DataRecord>());
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                Debug.WriteLine(result[i]);
            }


            return result;
        }


    }
}
