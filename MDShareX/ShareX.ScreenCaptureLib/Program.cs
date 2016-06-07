using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace ShareX.ScreenCaptureLib
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BaseRegionForm form;
            TaskSettings taskSettings = TaskSettings.GetDefaultTaskSettings();

            RectangleRegionMode mode = RectangleRegionMode.Annotation;
            form = new RectangleRegionForm(mode);

            var cap = new Cap();
            cap.DoCapture(() =>
            {
                Image img = null;

                try
                {
                    form.Config = taskSettings.CaptureSettingsReference.SurfaceOptions;
                    form.Prepare();
                    form.ShowDialog();

                    img = form.GetResultImage();

                    if (img != null)
                    {
                        if (form.Result == RegionResult.Region && taskSettings.UploadSettings.RegionCaptureUseWindowPattern)
                        {
                            WindowInfo windowInfo = form.GetWindowInfo();

                            if (windowInfo != null)
                            {
                                img.Tag = new ImageTag
                                {
                                    WindowTitle = windowInfo.Text,
                                    ProcessName = windowInfo.ProcessName
                                };
                            }
                        }

                        lastRegionCaptureType = LastRegionCaptureType.Default;
                    }
                }
                finally
                {
                    if (form != null)
                    {
                        form.Dispose();
                    }
                }

                return img;
            }, captureType,taskSettings,autoHideForm);


            Application.Run(new ());
        }

     


        /*
        CaptureScreenshot(CaptureType.Rectangle); 
        CaptureRegion(captureType, null, null);
        RectangleRegionMode mode = taskSettings.CaptureSettings.SurfaceOptions.AnnotationEnabled ? RectangleRegionMode.Annotation : RectangleRegionMode.Default;
        form = new RectangleRegionForm(mode);
         
         */









    }
}
