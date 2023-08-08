using AutoMapper;
using HotelManagement_backend.DomainModels;
using DataModels=HotelManagement_backend.DataModels;

namespace HotelManagement_backend.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<DataModels.Room, Room>().ReverseMap();
            CreateMap<UpdateRoomRequest,DataModels.Room>().ReverseMap();
            CreateMap<AddRoomRequest,DataModels.Room>().ReverseMap();
           
            CreateMap<DataModels.Guest,Guest>().ReverseMap();
            CreateMap<UpdateGuestRequest,DataModels.Guest>().ReverseMap();
            CreateMap<AddGuestRequest,DataModels.Guest>().ReverseMap();

            CreateMap<DataModels.Staff, Staff>().ReverseMap();
            CreateMap<UpdateStaffRequest,DataModels.Staff>().ReverseMap();
            CreateMap<AddStaffRequest, DataModels.Staff>().ReverseMap();

            CreateMap<DataModels.Department, Department>().ReverseMap();
            CreateMap<UpdateDepartmentRequest, DataModels.Department>().ReverseMap();
            CreateMap<AddDepartmentRequest, DataModels.Department>().ReverseMap();

            CreateMap<DataModels.Reservation, Reservation>().ReverseMap();
            CreateMap<UpdateReservationRequest, DataModels.Reservation>().ReverseMap();
            CreateMap<AddReservationRequest, DataModels.Reservation>().ReverseMap();

            CreateMap<DataModels.Payment, Payment>().ReverseMap();
            CreateMap<AddPaymentRequest, DataModels.Payment>().ReverseMap();

            CreateMap<DataModels.Bill, Bill>().ReverseMap();
            CreateMap<AddBillRequest, DataModels.Bill>().ReverseMap();

            // CreateMap<DataModels.Inventory, Inventory>().ReverseMap();
            // CreateMap<UpdateInventoryRequest, DataModels.Inventory>().ReverseMap();
            // CreateMap<AddInventoryRequest, DataModels.Inventory>().ReverseMap();

        }
    }
}
