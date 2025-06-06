namespace basics.Models
{
    public class Repository
    {
        private static readonly List<Course> _courses = new();

    static Repository()
    {
        _courses = new List<Course>()
        {   
            new Course() {Id = 1, Title = "1. kurs", Description = "bla bla bla",Image="b.jpg", Tags = new string[] {"aspnet","web geliştirme"}, isActive = true, isHome = true},
            new Course() {Id = 2, Title = "2. kurs", Description = "zla bla bla",Image="b.jpg",Tags = new string[] {"php","web geliştirme"}, isActive = false, isHome = true},
            new Course() {Id = 3, Title = "3. kurs", Description = "fla bla bla",Image="b.jpg", isActive = true, isHome = false},
            new Course() {Id = 4, Title = "4. kurs", Description = "gla bla bla",Image="b.jpg", isActive = true, isHome = true},
            new Course() {Id = 5, Title = "5. kurs", Description = "gla bla bla",Image="b.jpg", isActive = true, isHome = true},
            
        };
    }
        public static List<Course> Courses
        {   
            get{
                return _courses;
            }

        }

        public static Course? GetById(int? id){

            return _courses.FirstOrDefault(c => c.Id == id);
        }
    }
}