using ShareX.HelpersLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace ShareX.ScreenCaptureLib
{
    class Cap
    {

        public delegate Image ScreenCaptureDelegate();

        public void DoCapture(ScreenCaptureDelegate capture, CaptureType captureType, TaskSettings taskSettings = null, bool autoHideForm = true)
        {
            if (taskSettings == null) taskSettings = TaskSettings.GetDefaultTaskSettings();

            if (taskSettings.CaptureSettings.IsDelayScreenshot && taskSettings.CaptureSettings.DelayScreenshot > 0)
            {
                TaskEx.Run(() =>
                {
                    int sleep = (int)(taskSettings.CaptureSettings.DelayScreenshot * 1000);
                    Thread.Sleep(sleep);
                },
                () =>
                {
                    DoCaptureWork(capture, captureType, taskSettings, autoHideForm);
                });
            }
            else
            {
                DoCaptureWork(capture, captureType, taskSettings, autoHideForm);
            }
        }


        public void DoCaptureWork(ScreenCaptureDelegate capture, CaptureType captureType, TaskSettings taskSettings, bool autoHideForm = true)
        {
            //if (autoHideForm)
            //{
            //    Hide();
            //    Thread.Sleep(250);
            //}

            Image img = null;

            try
            {
                Screenshot.CaptureCursor = taskSettings.CaptureSettings.ShowCursor;
                Screenshot.CaptureShadow = taskSettings.CaptureSettings.CaptureShadow;
                Screenshot.ShadowOffset = taskSettings.CaptureSettings.CaptureShadowOffset;
                Screenshot.CaptureClientArea = taskSettings.CaptureSettings.CaptureClientArea;
                Screenshot.AutoHideTaskbar = taskSettings.CaptureSettings.CaptureAutoHideTaskbar;

                img = capture();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException(ex);
            }
            finally
            {
                if (autoHideForm)
                {
                    //this.ForceActivate();
                }

                AfterCapture(img, captureType, taskSettings);
            }
        }

        public void AfterCapture(Image img, CaptureType captureType, TaskSettings taskSettings)
        {
            if (img != null)
            {
            
            }
        }

    }
}
