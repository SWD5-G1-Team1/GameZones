using Game.DAL.Entities;

namespace DAL.ViewModels
{
    public class HomePageVM
    {
        public List<Category> Categories { get; set; } = new();
        public List<Banner> banners { get; set; } = new();

    }
}

