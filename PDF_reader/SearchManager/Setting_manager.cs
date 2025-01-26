using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_reader.SearchManager
{
    internal class Setting_manager
    {
        private float _zoomLevel = 1.0f;
        private const float ZoomStep = 0.1f;
        private const float MaxZoom = 3.0f;
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
