using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTestPagedList.Models
{
    public class PostService
    {
        public static IEnumerable<Post> posts = new List<Post>() {
            new Post{Id=1,Title="Title 1",Body="Body 1"},
            new Post{Id=2,Title="Title 2",Body="Body 2"},
            new Post{Id=3,Title="Title 3",Body="Body 3"},
            new Post{Id=4,Title="Title 4",Body="Body 4"},
            new Post{Id=5,Title="Title 5",Body="Body 5"},
            new Post{Id=6,Title="Title 6",Body="Body 6"},
            new Post{Id=7,Title="Title 7",Body="Body 7"},
            new Post{Id=8,Title="Title 8",Body="Body 8"},
            new Post{Id=9,Title="Title 9",Body="Body 9"},
            new Post{Id=10,Title="Title 10",Body="Body 10"},
            new Post{Id=11,Title="Title 11",Body="Body 11"},
            new Post{Id=12,Title="Title 12",Body="Body 12"},
            new Post{Id=13,Title="Title 13",Body="Body 13"},
            new Post{Id=14,Title="Title 14",Body="Body 14"},
            new Post{Id=15,Title="Title 15",Body="Body 15"},
            new Post{Id=16,Title="Title 16",Body="Body 16"},
            new Post{Id=17,Title="Title 17",Body="Body 17"},
            new Post{Id=18,Title="Title 18",Body="Body 18"},
            new Post{Id=19,Title="Title 19",Body="Body 19"},
            new Post{Id=20,Title="Title 20",Body="Body 20"},
            new Post{Id=21,Title="Title 21",Body="Body 21"},
            new Post{Id=22,Title="Title 22",Body="Body 22"},
            };

        public static IEnumerable<Post> GetAll()
        {
            return posts
            .OrderBy(row => row.Id);
        }
        public static IEnumerable<Post> GetAll(int page, int recordsPerPage, out int totalCount)
        {
            totalCount = posts.Count();
            return posts
            .OrderBy(row => row.Id)
            .Skip(page * recordsPerPage).Take(recordsPerPage); // in real projects change like this .skip(()=>resultforSkip).Take(()=>recordsPerPage )
        }
    }
}