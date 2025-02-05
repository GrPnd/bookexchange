namespace DAL;

public static class FileHelper
{
    public static readonly string BasePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                             + Path.DirectorySeparatorChar
                                             + "RiderProjects" + Path.DirectorySeparatorChar
                                             + "bookexchange" + Path.DirectorySeparatorChar;
}