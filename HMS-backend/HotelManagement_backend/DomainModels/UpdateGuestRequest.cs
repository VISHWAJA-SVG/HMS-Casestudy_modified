﻿namespace HotelManagement_backend.DomainModels
{
    public class UpdateGuestRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
