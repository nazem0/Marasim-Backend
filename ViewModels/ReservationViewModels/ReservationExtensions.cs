﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ReservationViewModels
{
    public static class ReservationExtensions
    {
        public static Reservation ToReservation(this AddReservationViewModel Reservation) {
            return new Reservation
            {
                UserId = Reservation.UserId,
                ServiceId = Reservation.ServiceId,
            };
        } 
    }
}
