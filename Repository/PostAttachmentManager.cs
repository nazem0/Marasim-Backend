﻿using System;
using Models;

namespace Repository
{
    public class PostAttachmentManager : MainManager<PostAttachment>
    {
        public PostAttachmentManager(EntitiesContext _dBContext) : base(_dBContext) { }
    }
}
