using Exercise.MarsRover.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise.MarsRover.Models
{
    public class RoverModel
    {
        public RoverModel(int xcoordinate, int ycoordinate, DirectionEnum direction, string commands)
        {
            CurrentXCoordinate = xcoordinate;
            CurrentYCoordinate = ycoordinate;
            Direction = direction;
            Commands = commands;
        }

        public int CurrentXCoordinate { get; set; }

        public int CurrentYCoordinate { get; set; }

        public DirectionEnum Direction { get; set; }

        public string Commands { get; set; }
    }
}
