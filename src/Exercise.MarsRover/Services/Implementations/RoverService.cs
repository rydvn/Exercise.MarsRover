using Exercise.MarsRover.Models;
using Exercise.MarsRover.Models.Enums;
using Exercise.MarsRover.Models.Responses;
using System;

namespace Exercise.MarsRover.Services.Implementations
{
    public class RoverService : IRoverService
    {
        public BaseResponse<RoverModel> Run(PlateauModel plateau, RoverModel rover)
        {
            var result = new BaseResponse<RoverModel>();

            for (int i = 0; i < rover.Commands.Length; i++)
            {
                if (Commands.L.ToString() == rover.Commands[i].ToString())
                {
                    TurnLeft(rover);
                }
                else if (Commands.R.ToString() == rover.Commands[i].ToString())
                {
                    TurnRight(rover);
                }
                else if (Commands.M.ToString() == rover.Commands[i].ToString())
                {
                    if (!Move(plateau, rover))
                    {
                        var message = "Error: this step is out of boundary!!!";
                        result.Errors.Add(message);
                        Console.WriteLine(message);
                        return result;
                    }
                }
                else
                {
                    var message = $"The parameter '{rover.Commands[i]}' you entered not one of 'L', 'R', 'M'";
                    result.Errors.Add(message);
                    Console.WriteLine(message);
                    return result;
                }
            }

            result.Data = rover;
            return result;
        }

        public bool Move(PlateauModel plateau, RoverModel rover)
        {
            if (!CanItMove(plateau, rover))
            {
                return false;
            }

            switch (rover.Direction)
            {
                case DirectionEnum.N:
                    rover.CurrentYCoordinate += 1;
                    break;
                case DirectionEnum.E:
                    rover.CurrentXCoordinate += 1;
                    break;
                case DirectionEnum.S:
                    rover.CurrentYCoordinate -= 1;
                    break;
                case DirectionEnum.W:
                    rover.CurrentXCoordinate -= 1;
                    break;
            }

            return true;
        }

        private void TurnLeft(RoverModel rover)
        {
            rover.Direction = ((int)rover.Direction - 1) < (int)DirectionEnum.N ? DirectionEnum.W : (DirectionEnum)((int)rover.Direction - 1);
        }

        private void TurnRight(RoverModel rover)
        {
            rover.Direction = ((int)rover.Direction + 1) > (int)DirectionEnum.W ? DirectionEnum.N : (DirectionEnum)((int)rover.Direction + 1);
        }

        public bool CanItMove(PlateauModel plateau, RoverModel rover)
        {
            return plateau.MinWidth <= rover.CurrentXCoordinate && rover.CurrentXCoordinate <= plateau.MaxWidth && plateau.MinHeight <= rover.CurrentYCoordinate && rover.CurrentYCoordinate <= plateau.MaxHeight;
        }
    }
}
