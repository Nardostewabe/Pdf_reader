using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_reader.Settings_manager
{
    internal class SettingsManager
    {
        private static float _zoomLevel = 1.0f;
        private const float ZoomStep = 1.0f;
        private const float MaxZoom = 5.0f;
        private const float MinZoom = 0.5f;

        public float ZoomIn()
        {
            if (_zoomLevel < MaxZoom)
                _zoomLevel += ZoomStep;
            return _zoomLevel;
        }

        public float ZoomOut()
        {
            if (_zoomLevel > MinZoom)
                _zoomLevel -= ZoomStep;
            return _zoomLevel;
        }

        public float ResetZoom()
        {
            _zoomLevel = 1.0f;
            return _zoomLevel;
        }
    }
}
