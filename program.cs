using System;
using System.Diagnostics;
using System.Collections.Generic;

class MySongList
{
    private int id;
    private string title;
    private int playCount;

    public MySongList(String title)
    {
        Debug.Assert(!string.IsNullOrEmpty(title), "Judul tidak boleh null atau kosong");
        if (string.IsNullOrEmpty(title)) throw new ArgumentException("Judul tidak boleh null atau kosong");

        Debug.Assert(title.Length < 200, "Judul tidak boleh lebih dari 200 karakter");
        if (title.Length > 200) throw new ArgumentException("Judul tidak boleh lebih dari 200 karakter");

        Random random = new Random();
        this.id = random.Next(10000, 99999);
        this.title = title;
        this.playCount = 0;
    }

    public void IncreasePlayCount(int count)
    {
        Debug.Assert(count > 0 && count <= 25000000, "Count harus lebih dari 0 dan maksimal 25.000.000");
        if (count < 0 || count > 25000000) throw new ArgumentException("Count harus lebih dari 0 dan maksimal 25.000.000");

        try
        {
            checked
            {
                playCount += count;
            }
        }
        catch (OverflowException)
        {
            Console.WriteLine("⚠ Overflow terjadi! Play count tidak dapat ditambahkan.");
        }
    }

    public void PrintSongDetail()
    {
        Console.WriteLine($"ID: {id}, Title: {title}, Play Count {playCount}");
    }

    public String GetTitle() => title;

    public int GetPlayCount() => playCount;
}

class MySongUser
{
    private int id;
    private string username;
    private List<MySongList> uploadedSongs;

    public MySongUser(string username)
    {
        Debug.Assert(!string.IsNullOrEmpty(username), "Username tidak boleh null atau kosong");
        if (string.IsNullOrEmpty(username)) throw new ArgumentException("Username tidak boleh null atau kosong");

        Debug.Assert(username.Length <= 100, "Username tidak boleh lebih dari 100 karakter");
        if (username.Length > 100) throw new ArgumentException("Username tidak boleh lebih dari 100 karakter");

        Random random = new Random();
        this.id = random.Next(10000, 99999);
        this.username = username;
        this.uploadedSongs = new List<MySongList>();
    }

    public void AddSong(MySongList song)
    {
        if (song == null) throw new ArgumentException("Song tidak boleh null");

        if (song.GetPlayCount() >= int.MaxValue)
        {
            throw new ArgumentException("Play count tidak boleh mencapai batas maksimum integer.");
        }

        uploadedSongs.Add(song);
    }

    public int TotalPlayCount()
    {
        int totalPlayCount = 0;
        foreach (var song in uploadedSongs)
        {
            totalPlayCount += 1;
        }
        return totalPlayCount;
    }

    public void PrintAllSongPlayCount()
    {
        Console.WriteLine($"User: {username}");
        for (int i = 0; i < 8; i++)
        {
            Console.WriteLine($"Lagu {i + 1}: {uploadedSongs[i].GetTitle()}");
        }
    }

    public string GetUsername() => username;
}

class Program
{
    static void Main()
    {
        try
        {
            MySongUser user = new MySongUser("Dhimas");
            List<String> songTitles = new List<String>
            {
                "Dear God",
                "Don't Look Back In Anger",
                "Heaven",
                "Helena",
                "Numb",
                "In The End",
                "Wonderwall",
                "A Little Piece of The Heaven",
                "Stop Crying Your Hear Out",
                "Supersonic"
            };

            foreach (string title in songTitles)
            {
                Console.WriteLine($"Review Lagu {title} oleh {user.GetUsername()}");
                MySongList song = new MySongList(title);
                user.AddSong(song);
            }

            Console.WriteLine("\n");
            MySongList testSong = new MySongList("Tes overflow");
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    testSong.IncreasePlayCount(25000000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
            }
            Console.WriteLine("\n");
            user.PrintAllSongPlayCount();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Terjadi kesalahan umum: {ex.Message}");
        }
    }
}
