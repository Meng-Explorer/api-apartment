namespace APARTMENT_API.DTOs.Response
{
    public class FloorResDto
    {
        public int Id { get; set; }
        public int FloorNo { get; set; }
        public int BuildingId { get; set; }
        public BuildingResDto? Building { get; set; }

    }
}
