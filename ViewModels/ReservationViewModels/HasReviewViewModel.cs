﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ReservationViewModels
{
    public class HasReviewViewModel
    {
        public required string UserId { get; set; }
        public int ServiceId { get; set; }

    }
}