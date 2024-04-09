﻿namespace TreeCraftingVoyager.Server.Models.Dto.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        //[Url]
        public string ImageURL { get; set; }

        public string SEOKeywords { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }

        public long? ParentId { get; set; }
    }
}
