using System;
using System.Linq;
using DIYHIIT.Models;
using DIYHIIT.Models.Exercise;
using Microsoft.Data.SqlClient;

namespace DIYHIIT.API.Models
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext dbContext)
        {
            if (!dbContext.Exercises.Any())
            {
                //dbContext.AddRange
                //(
                //    new Exercise() { Name = "PressUps", BodyFocus = "Chest", Type = WorkoutType.Calisthenics, ImageURL = "https://img.webmd.com/dtmcms/live/webmd/consumer_assets/site_images/article_thumbnails/other/exercise_fitness_alt4_other/650x350_exercise_fitness_alt4_other.jpg" },
                //    new Exercise() { Name = "DownwardDog", BodyFocus = "Back, shoulders", Type = WorkoutType.Yoga, ImageURL = "https://upload.wikimedia.org/wikipedia/commons/5/57/Downward-Facing-Dog.JPG" },
                //    new Exercise() { Name = "RopeWobblies", BodyFocus = "Upper body, Core", Type = WorkoutType.HIIT, ImageURL = "https://www.t-nation.com/system/publishing/articles/10006613/original/Get-Ripped-with-Battle-Ropes.jpg?1534796054" },
                //    new Exercise() { Name = "V-Sit", BodyFocus = "Core", Type = WorkoutType.Pilates, ImageURL = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/yoga-vs-pilates-1545221124.jpg?resize=768:*" }
                //);
            }

            dbContext.SaveChanges();
        }
    }
}
