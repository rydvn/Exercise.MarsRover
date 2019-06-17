using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise.MarsRover.Models
{
    public class PlateauModel
    {
        public PlateauModel(int maxWidth, int maxHeight)
        {
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
        }

        public int MinWidth => 0;

        public int MinHeight => 0;

        public int MaxWidth { get; set; }

        public int MaxHeight { get; set; }
    }
}
