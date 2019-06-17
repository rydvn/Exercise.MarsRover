using Exercise.MarsRover.Models;
using Exercise.MarsRover.Models.Responses;

namespace Exercise.MarsRover.Services
{
    public interface IRoverService
    {
        BaseResponse<RoverModel> Run(PlateauModel plateau, RoverModel rover);
    }
}
