using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixMartt
{
    public static class DataStore
    {
        public static List<User> Users = new List<User>()
        {
            new User
            {
                UserID = 1,
                FullName = "Sample User One",
                Username = "user1",
                Password = "1234",
                Email = "user1@gmail.com"
            },
            new User
            {
                UserID = 2,
                FullName = "Sample User Two",
                Username = "user2",
                Password = "1234",
                Email = "user2@gmail.com"
            }
        };

        public static List<Artwork> Artworks = new List<Artwork>();

        public static List<Download> Downloads = new List<Download>();

        public static int NextUserID = 3;
        public static int NextArtworkID = 1;
        public static int NextDownloadID = 1;
    }
}
